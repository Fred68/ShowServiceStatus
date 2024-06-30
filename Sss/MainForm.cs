using System.Windows.Forms;

using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Text;
using System.Windows.Forms.VisualStyles;
using Sss.Properties;

namespace Sss
{

	public delegate void SetNameDelegate(string s);
	public delegate List<string> GetServiceNamesDelegate(string name,bool showNames,bool exact);

	enum StartStop	{Start, Stop};

	public partial class MainForm:Form
	{
		string _serviceName;
		Size wSize;
		bool canExit = true;
		bool runAsAdmin = false;
		
		string _tempServiceName;

		ServiceController? sc;

		public string Status
		{
			get { return sc != null ? sc.Status.ToString() : "Service undefined"; }
		}

		public MainForm(string serviceName)
		{
			InitializeComponent();
			_serviceName = serviceName;
			_tempServiceName = string.Empty;

			//MessageBox.Show(IsRunAsAdmin() ? "Running as admnistrator" : "Error: not running as admnistrator");

			if(IsRunAsAdmin())
			{
				runAsAdmin = true;
				MessageBox.Show("Running as admnistrator");
			}

			AdminRelauncher();

			if(runAsAdmin)
			{
				SetService(_serviceName, false);
			}
		}

		public void SetTempServiceName(string serviceName)
		{
			_tempServiceName = serviceName;
			MessageBox.Show($"<{_tempServiceName}> set !");
		}

		private void Form1_Load(object sender,EventArgs e)
		{
			wSize = this.Size;
			//notifyIcon.Icon = SystemIcons.Application;
			notifyIcon.Icon = Resources.Icona;
			MaximizeBox = false;
			this.ShowInTaskbar = false;
			notifyIcon.ContextMenuStrip = contextMenuStrip1;
			this.WindowState = FormWindowState.Minimized;
			canExit = true;
			this.Icon = Resources.Icona;
			UpdateStat();
		}

		private bool IsRunAsAdmin()
		{
			WindowsIdentity id = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(id);

			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}

		private void AdminRelauncher()
		{
			if(!IsRunAsAdmin())
			{
				ProcessStartInfo proc = new ProcessStartInfo();
				proc.UseShellExecute = true;
				proc.WorkingDirectory = Environment.CurrentDirectory;
				Assembly? asm = Assembly.GetEntryAssembly();
				if(asm != null)
				{
					proc.FileName = asm.Location;
				}
				proc.Verb = "runas";
				proc.Arguments = _serviceName;

				try
				{

					int p = proc.FileName.LastIndexOf(".dll",StringComparison.OrdinalIgnoreCase);
					if(p != -1)
					{
						proc.FileName = proc.FileName.Substring(0,p) + ".exe";
					}
					Process.Start(proc);
					//Application.Current.Shutdown();	//Application.Exit();
					Close();
				}
				catch(Exception ex)
				{
					MessageBox.Show("This program must be run as an administrator! \n\n" + ex.ToString());
				}
			}
		}

		public List<string> GetServiceNames(string name,bool showNames, bool exact)
		{
			StringBuilder sb = new StringBuilder();
			List<string> lsc = new List<string>();
			ServiceController[] scs = ServiceController.GetServices();
			foreach(ServiceController sc in scs)
			{
				sb.Append($"{sc.ServiceName}\t");
				bool add = false;

				if(!exact)
				{
					add = sc.ServiceName.Contains(name,StringComparison.OrdinalIgnoreCase);
				}
				else
				{
					add = sc.ServiceName.Equals(name,StringComparison.OrdinalIgnoreCase);
				}

				if(add)
				{
					lsc.Add(sc.ServiceName);
				}
				

			}
			if(showNames)
			{
				MessageBox.Show(sb.ToString());
			}
			return lsc;
		}

		public bool SetService(string serviceName, bool exact)
		{
			bool ok = false;
			List<string> servicesList = GetServiceNames(serviceName,false,exact);
			if(servicesList.Count == 1)
			{
				sc = new ServiceController(servicesList[0]);
				serviceName = servicesList[0];
				ok = true;
			}
			else
			{
				if(servicesList.Count == 0)
				{
					MessageBox.Show($"No service named '{serviceName}' found.");
				}
				else
				{
					MessageBox.Show($"Found {servicesList.Count} services named '{serviceName}'.");
				}
			}
			return ok;
		}

		void UpdateStat()
		{
			string _status, _serviceName, _text;
			_text = _serviceName = _status = "???";
			if(sc != null)
			{
				ServiceControllerStatus st = sc.Status;
				_serviceName = sc.ServiceName;
				switch(st)
				{
					case ServiceControllerStatus.Running:
						_text = "Stop";
						notifyIcon.Icon = Resources.IconaRun;
						lblStatus.ForeColor = Color.DarkGreen;
						break;
					case ServiceControllerStatus.Stopped:
						notifyIcon.Icon = Resources.Icona;
						lblStatus.ForeColor = Color.Black;
						_text = "Start";
						break;
				}
				_status = st.ToString();
			}

			btStartStop.Text = startStopToolStripMenuItem.Text = _text;
			lblService.Text = _serviceName;
			statToolStripMenuItem.Text = notifyIcon.Text = $"{_serviceName}: {_status}";
			lblStatus.Text = _status;
		}

		void SwapStartStopService()
		{
			if(sc != null)
			{
				switch(sc.Status)
				{
					case ServiceControllerStatus.Running:
						StartStopService(StartStop.Stop);
						break;
					case ServiceControllerStatus.Stopped:
						StartStopService(StartStop.Start);
						break;
					default:
						break;
				}
			}
		}

		void StartStopService(StartStop startOrStop, bool askConfirm = false)
		{
			if(sc != null)
			{
				bool confirmed = true;
				try
				{
					switch(sc.Status)
					{
						case ServiceControllerStatus.Running:
							if(startOrStop == StartStop.Stop)
							{
								if(askConfirm)
								{
									if(MessageBox.Show($"Stop service <{sc.ServiceName}> ?","Stopping service",MessageBoxButtons.YesNo) != DialogResult.Yes)
									{
										confirmed = false;
									}
								}
								if(confirmed)
								{
									sc.Stop();
									sc.WaitForStatus(ServiceControllerStatus.Stopped,new TimeSpan(0,0,30));
								}
							}
							break;
						case ServiceControllerStatus.Stopped:
							if(startOrStop == StartStop.Start)
							{
								if(askConfirm)
								{
									if(MessageBox.Show($"Start service <{sc.ServiceName}> ?","Starting service",MessageBoxButtons.YesNo) != DialogResult.Yes)
									{
										confirmed = false;
									}
								}
								if(confirmed)
								{
									sc.Start();
									sc.WaitForStatus(ServiceControllerStatus.Running,new TimeSpan(0,0,30));
								}
							}
							break;
						default:
							break;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private void Form1_FormClosing(object sender,FormClosingEventArgs e)
		{
			if(!canExit)
			{
				this.WindowState = FormWindowState.Minimized;
				e.Cancel = true;
				canExit = true;
			}
		}

		private void Form1_Resize(object sender,EventArgs e)
		{
			if(this.WindowState == FormWindowState.Minimized)
			{
				Hide();
				notifyIcon.Visible = true;
				canExit = true;
			}
			
			else
			{
				this.Size = wSize;
				canExit = false;
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender,MouseEventArgs e)
		{
			UpdateStat();
			Show();
			this.WindowState = FormWindowState.Normal;
			notifyIcon.Visible = false;
		}

		

		private void statToolStripMenuItem_Click(object sender,EventArgs e)
		{
			MessageBox.Show($"Status: {Status}");
		}

		private void showToolStripMenuItem_Click(object sender,EventArgs e)
		{
			Show();
			this.WindowState = FormWindowState.Normal;	// Dopo Show().
		}

		private void startStopToolStripMenuItem_Click(object sender,EventArgs e)
		{
			SwapStartStopService();
			UpdateStat();
		}

		private void btClose_Click(object sender,EventArgs e)
		{
			Close();
		}

		private void btStartStop_Click(object sender,EventArgs e)
		{
			SwapStartStopService();
			UpdateStat();
		}

		private void exitToolStripMenuItem_Click(object sender,EventArgs e)
		{
			StartStopService(StartStop.Stop,true);
			Close();
		}
		private void btExit_Click(object sender,EventArgs e)
		{
			StartStopService(StartStop.Stop,true);
			canExit = true;
			Close();
		}

		private void btService_Click(object sender,EventArgs e)
		{
			SelectService selForm = new SelectService(_serviceName, SetTempServiceName, GetServiceNames);
			if(selForm.ShowDialog() == DialogResult.OK)
			{
				if(_serviceName != _tempServiceName)
				{
					StartStopService(StartStop.Stop,true);
					_serviceName = _tempServiceName;
					SetService(_serviceName,true);
					UpdateStat();
					
				}
			}
		}
	}
}
