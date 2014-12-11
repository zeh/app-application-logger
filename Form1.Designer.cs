namespace ApplicationLogger {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.button1 = new System.Windows.Forms.Button();
			this.textPathTemplate = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelApplication = new System.Windows.Forms.Label();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.labelPathTemplate = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(398, 185);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.onClickSave);
			// 
			// textPathTemplate
			// 
			this.textPathTemplate.Location = new System.Drawing.Point(12, 159);
			this.textPathTemplate.Name = "textPathTemplate";
			this.textPathTemplate.Size = new System.Drawing.Size(461, 20);
			this.textPathTemplate.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelApplication);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(461, 100);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Application information";
			// 
			// labelApplication
			// 
			this.labelApplication.AutoSize = true;
			this.labelApplication.Location = new System.Drawing.Point(8, 22);
			this.labelApplication.Name = "labelApplication";
			this.labelApplication.Size = new System.Drawing.Size(91, 13);
			this.labelApplication.TabIndex = 0;
			this.labelApplication.Text = "Application name:";
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Application Logger";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.onDoubleClickNotificationIcon);
			// 
			// labelPathTemplate
			// 
			this.labelPathTemplate.AutoSize = true;
			this.labelPathTemplate.Location = new System.Drawing.Point(13, 140);
			this.labelPathTemplate.Name = "labelPathTemplate";
			this.labelPathTemplate.Size = new System.Drawing.Size(283, 13);
			this.labelPathTemplate.TabIndex = 4;
			this.labelPathTemplate.Text = "Path to save file (user [[day]], [[month]], or [[year]] for fields)";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(485, 220);
			this.Controls.Add(this.labelPathTemplate);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textPathTemplate);
			this.Controls.Add(this.button1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Application Logger";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
			this.Load += new System.EventHandler(this.onFormLoad);
			this.Resize += new System.EventHandler(this.onResize);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textPathTemplate;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelApplication;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.Label labelPathTemplate;
	}
}

