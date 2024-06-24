namespace Sss
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			notifyIcon = new NotifyIcon(components);
			contextMenuStrip1 = new ContextMenuStrip(components);
			startStopToolStripMenuItem = new ToolStripMenuItem();
			showToolStripMenuItem = new ToolStripMenuItem();
			statToolStripMenuItem = new ToolStripMenuItem();
			exitToolStripMenuItem = new ToolStripMenuItem();
			label1 = new Label();
			lblService = new Label();
			lblStatus = new Label();
			label4 = new Label();
			btClose = new Button();
			btService = new Button();
			btStartStop = new Button();
			btExit = new Button();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// notifyIcon
			// 
			notifyIcon.Text = "notifyIcon";
			notifyIcon.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
			// 
			// contextMenuStrip1
			// 
			contextMenuStrip1.Items.AddRange(new ToolStripItem[] { startStopToolStripMenuItem,showToolStripMenuItem,statToolStripMenuItem,exitToolStripMenuItem });
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new Size(133,92);
			// 
			// startStopToolStripMenuItem
			// 
			startStopToolStripMenuItem.Name = "startStopToolStripMenuItem";
			startStopToolStripMenuItem.Size = new Size(132,22);
			startStopToolStripMenuItem.Text = "Start / stop";
			startStopToolStripMenuItem.Click += startStopToolStripMenuItem_Click;
			// 
			// showToolStripMenuItem
			// 
			showToolStripMenuItem.Name = "showToolStripMenuItem";
			showToolStripMenuItem.Size = new Size(132,22);
			showToolStripMenuItem.Text = "Show";
			showToolStripMenuItem.Click += showToolStripMenuItem_Click;
			// 
			// statToolStripMenuItem
			// 
			statToolStripMenuItem.Name = "statToolStripMenuItem";
			statToolStripMenuItem.Size = new Size(132,22);
			statToolStripMenuItem.Text = "Stat";
			statToolStripMenuItem.Click += statToolStripMenuItem_Click;
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new Size(132,22);
			exitToolStripMenuItem.Text = "Exit";
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12,18);
			label1.Name = "label1";
			label1.Size = new Size(80,15);
			label1.TabIndex = 1;
			label1.Text = "Service name:";
			// 
			// lblService
			// 
			lblService.AutoSize = true;
			lblService.Location = new Point(107,18);
			lblService.Name = "lblService";
			lblService.Size = new Size(38,15);
			lblService.TabIndex = 2;
			lblService.Text = "label2";
			// 
			// lblStatus
			// 
			lblStatus.AutoSize = true;
			lblStatus.Location = new Point(107,47);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(38,15);
			lblStatus.TabIndex = 4;
			lblStatus.Text = "label3";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(12,47);
			label4.Name = "label4";
			label4.Size = new Size(42,15);
			label4.TabIndex = 3;
			label4.Text = "Status:";
			// 
			// btClose
			// 
			btClose.Location = new Point(228,72);
			btClose.Name = "btClose";
			btClose.Size = new Size(133,23);
			btClose.TabIndex = 5;
			btClose.Text = "Close";
			btClose.UseVisualStyleBackColor = true;
			btClose.Click += btClose_Click;
			// 
			// btService
			// 
			btService.Location = new Point(228,14);
			btService.Name = "btService";
			btService.Size = new Size(133,23);
			btService.TabIndex = 6;
			btService.Text = "Service...";
			btService.UseVisualStyleBackColor = true;
			btService.Click += btService_Click;
			// 
			// btStartStop
			// 
			btStartStop.Location = new Point(228,43);
			btStartStop.Name = "btStartStop";
			btStartStop.Size = new Size(133,23);
			btStartStop.TabIndex = 7;
			btStartStop.Text = "Start/Stop";
			btStartStop.UseVisualStyleBackColor = true;
			btStartStop.Click += btStartStop_Click;
			// 
			// btExit
			// 
			btExit.Location = new Point(12,72);
			btExit.Name = "btExit";
			btExit.Size = new Size(133,23);
			btExit.TabIndex = 8;
			btExit.Text = "Exit";
			btExit.UseVisualStyleBackColor = true;
			btExit.Click += btExit_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F,15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(369,107);
			Controls.Add(btExit);
			Controls.Add(btStartStop);
			Controls.Add(btService);
			Controls.Add(btClose);
			Controls.Add(lblStatus);
			Controls.Add(label4);
			Controls.Add(lblService);
			Controls.Add(label1);
			Name = "Form1";
			Text = "Form1";
			FormClosing += Form1_FormClosing;
			Load += Form1_Load;
			Resize += Form1_Resize;
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private NotifyIcon notifyIcon;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem startStopToolStripMenuItem;
		private ToolStripMenuItem statToolStripMenuItem;
		private ToolStripMenuItem exitToolStripMenuItem;
		private ToolStripMenuItem showToolStripMenuItem;
		private Label label1;
		private Label lblService;
		private Label lblStatus;
		private Label label4;
		private Button btClose;
		private Button btService;
		private Button btStartStop;
		private Button btExit;
	}
}
