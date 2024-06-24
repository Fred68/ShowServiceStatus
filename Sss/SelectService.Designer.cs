namespace Sss
{
	partial class SelectService
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btSearch = new Button();
			groupBox1 = new GroupBox();
			tbServiceName = new TextBox();
			btSet = new Button();
			lbServiceNames = new ListBox();
			btOK = new Button();
			btCancel = new Button();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// btSearch
			// 
			btSearch.Location = new Point(14,51);
			btSearch.Name = "btSearch";
			btSearch.Size = new Size(174,23);
			btSearch.TabIndex = 0;
			btSearch.Text = "Search service...";
			btSearch.UseVisualStyleBackColor = true;
			btSearch.Click += btSearch_Click;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(tbServiceName);
			groupBox1.Controls.Add(btSearch);
			groupBox1.Location = new Point(279,12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(194,87);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			groupBox1.Text = "Service name";
			// 
			// tbServiceName
			// 
			tbServiceName.Location = new Point(14,22);
			tbServiceName.Name = "tbServiceName";
			tbServiceName.Size = new Size(174,23);
			tbServiceName.TabIndex = 1;
			tbServiceName.KeyDown += tbServiceName_KeyDown;
			// 
			// btSet
			// 
			btSet.Location = new Point(293,105);
			btSet.Name = "btSet";
			btSet.Size = new Size(174,23);
			btSet.TabIndex = 3;
			btSet.Text = "Set service";
			btSet.UseVisualStyleBackColor = true;
			btSet.Visible = false;
			btSet.Click += btSet_Click;
			// 
			// lbServiceNames
			// 
			lbServiceNames.FormattingEnabled = true;
			lbServiceNames.ItemHeight = 15;
			lbServiceNames.Location = new Point(12,12);
			lbServiceNames.Name = "lbServiceNames";
			lbServiceNames.Size = new Size(261,289);
			lbServiceNames.TabIndex = 2;
			lbServiceNames.SelectedIndexChanged += lbServiceNames_SelectedIndexChanged;
			lbServiceNames.DoubleClick += lbServiceNames_DoubleClick;
			// 
			// btOK
			// 
			btOK.Location = new Point(293,163);
			btOK.Name = "btOK";
			btOK.Size = new Size(174,23);
			btOK.TabIndex = 4;
			btOK.Text = "OK";
			btOK.UseVisualStyleBackColor = true;
			btOK.Click += btOK_Click;
			// 
			// btCancel
			// 
			btCancel.Location = new Point(293,134);
			btCancel.Name = "btCancel";
			btCancel.Size = new Size(174,23);
			btCancel.TabIndex = 5;
			btCancel.Text = "Cancel";
			btCancel.UseVisualStyleBackColor = true;
			btCancel.Click += btCancel_Click;
			// 
			// SelectService
			// 
			AutoScaleDimensions = new SizeF(7F,15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(485,438);
			Controls.Add(btCancel);
			Controls.Add(btSet);
			Controls.Add(btOK);
			Controls.Add(lbServiceNames);
			Controls.Add(groupBox1);
			Name = "SelectService";
			Text = "SelectService";
			Load += SelectService_Load;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Button btSearch;
		private GroupBox groupBox1;
		private TextBox tbServiceName;
		private ListBox lbServiceNames;
		private Button btSet;
		private Button btOK;
		private Button btCancel;
	}
}