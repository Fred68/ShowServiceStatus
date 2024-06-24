using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sss
{
	public partial class SelectService:Form
	{
		string _srvName;
		SetNameDelegate _setServiceName;
		GetServiceNamesDelegate _getServiceNames;

		public SelectService(string serviceName,SetNameDelegate setServiceName,GetServiceNamesDelegate getServiceNames)
		{
			InitializeComponent();
			_srvName = serviceName;
			_setServiceName = setServiceName;
			_getServiceNames = getServiceNames;
		}

		private void SelectService_Load(object sender,EventArgs e)
		{
			tbServiceName.Text = _srvName;
		}

		private void btOK_Click(object sender,EventArgs e)
		{
			GetSelectedService();
			_setServiceName(_srvName);
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void btSet_Click(object sender,EventArgs e)
		{
			GetSelectedService();
			_setServiceName(_srvName);
		}

		private void btCancel_Click(object sender,EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		void FillServiceListBox(string txt)
		{
			List<string> list = _getServiceNames(txt,false);
			lbServiceNames.Items.Clear();
			foreach(string s in list)
			{
				lbServiceNames.Items.Add(s);
			}
		}

		private void btSearch_Click(object sender,EventArgs e)
		{
			FillServiceListBox(tbServiceName.Text);
		}

		void GetSelectedService()
		{
			string? sel = (string?)lbServiceNames.SelectedItem;
			if(sel != null)
			{
				_srvName = tbServiceName.Text = sel;
			}
			//_srvName = tbServiceName.Text;
		}

		private void lbServiceNames_DoubleClick(object sender,EventArgs e)
		{
			GetSelectedService();
		}

		private void tbServiceName_KeyDown(object sender,KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Return)
			{
				FillServiceListBox(tbServiceName.Text);
			}
		}

		private void lbServiceNames_SelectedIndexChanged(object sender,EventArgs e)
		{
			GetSelectedService();
		}
	}
}
