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

namespace Sss
{
	public partial class Form1:Form
	{
		string serviceName;
		Size wSize;

		ServiceController? sc;

		public string Status
		{
			//get { return runningS ? "Running" : "Stopped"; }
			get { return sc.Status.ToString(); }
		}

		public Form1(string srvName)
		{
			InitializeComponent();
			this.serviceName = srvName;

			string msg = IsRunAsAdmin() ? "Running as admnistrator" : "Error: not running as admnistrator";
			MessageBox.Show(msg);

			AdminRelauncher();

			List<string> servicesList = GetServiceNames(serviceName,false);
			if(servicesList.Count == 1)
			{
				sc = new ServiceController(servicesList[0]);
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
				Close();
			}
		}
		private void Form1_Load(object sender,EventArgs e)
		{
			wSize = this.Size;
			notifyIcon.Icon = SystemIcons.Application;
			this.ShowInTaskbar = false;
			notifyIcon.ContextMenuStrip = contextMenuStrip1;
			this.WindowState = FormWindowState.Minimized;

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
				proc.Arguments = serviceName;

				try
				{

					int p = proc.FileName.LastIndexOf(".dll",StringComparison.OrdinalIgnoreCase);
					if(p != -1)
					{
						proc.FileName = proc.FileName.Substring(0,p) + ".exe";
					}
					Process.Start(proc);
					//Application.Current.Shutdown();	//Application.Exit();
					//MessageBox.Show("Closing program...");
					Close();
				}
				catch(Exception ex)
				{
					MessageBox.Show("This program must be run as an administrator! \n\n" + ex.ToString());
				}
			}
		}

		public List<string> GetServiceNames(string name,bool showNames)
		{
			StringBuilder sb = new StringBuilder();
			List<string> lsc = new List<string>();
			ServiceController[] scs = ServiceController.GetServices();
			foreach(ServiceController sc in scs)
			{
				sb.Append($"{sc.ServiceName}\t");
				if(sc.ServiceName.Contains(name,StringComparison.OrdinalIgnoreCase))
				{
					lsc.Add(sc.ServiceName);
				}

			}
			if(showNames) MessageBox.Show(sb.ToString());
			return lsc;
		}

		void UpdateStat()
		{
			ServiceControllerStatus st = sc.Status;
			switch(st)
			{
				case ServiceControllerStatus.Running:
					startStopToolStripMenuItem.Text = "Stop";
					break;
				case ServiceControllerStatus.Stopped:
					startStopToolStripMenuItem.Text = "Start";
					break;
			}
			notifyIcon.Text = st.ToString();
			statToolStripMenuItem.Text = "Status: " + st.ToString();
			lblService.Text = sc.ServiceName;
			lblStatus.Text = st.ToString();
		}

		private void Form1_Resize(object sender,EventArgs e)
		{
			if(this.WindowState == FormWindowState.Minimized)
			{
				Hide();
				notifyIcon.Visible = true;
			}
			else
			{
			this.Size = wSize;
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender,MouseEventArgs e)
		{
			UpdateStat();
			Show();
			this.WindowState = FormWindowState.Normal;
			notifyIcon.Visible = false;
		}

		private void notifyIcon1_Click(object sender,EventArgs e)
		{
			Show();
		}

		private void exitToolStripMenuItem_Click(object sender,EventArgs e)
		{
			Close();
		}

		private void statToolStripMenuItem_Click(object sender,EventArgs e)
		{
			MessageBox.Show($"Status: {Status}");
		}

		private void showToolStripMenuItem_Click(object sender,EventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
		}

		private void startStopToolStripMenuItem_Click(object sender,EventArgs e)
		{

			StartStopService();


			UpdateStat();
		}

		private void Form1_FormClosing(object sender,FormClosingEventArgs e)
		{
			if(this.WindowState != FormWindowState.Minimized)
			{
				this.WindowState = FormWindowState.Minimized;
				e.Cancel = true;
			}
		}

		void StartStopService()
		{
			try
			{
				switch(sc.Status)
				{
					case ServiceControllerStatus.Running:
						sc.Stop();
						sc.WaitForStatus(ServiceControllerStatus.Stopped,new TimeSpan(0,0,30));
						break;
					case ServiceControllerStatus.Stopped:
						sc.Start();
						sc.WaitForStatus(ServiceControllerStatus.Running,new TimeSpan(0,0,30));
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

		private void btClose_Click(object sender,EventArgs e)
		{
			Close();
		}
	}
}
