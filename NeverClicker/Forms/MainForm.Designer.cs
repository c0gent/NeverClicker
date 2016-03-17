namespace NeverClicker.Forms
{
    partial class MainForm
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
            if (disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.buttonPause = new System.Windows.Forms.Button();
			this.buttonReload = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.labelLog = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.buttonOptionsForm = new System.Windows.Forms.Button();
			this.buttonAutoCycle = new System.Windows.Forms.Button();
			this.buttonTestsForm = new System.Windows.Forms.Button();
			this.listBoxTaskQueue = new System.Windows.Forms.ListBox();
			this.labelTaskQueue = new System.Windows.Forms.Label();
			this.buttonErrors = new System.Windows.Forms.Button();
			this.listBoxErrors = new System.Windows.Forms.ListBox();
			this.labelErrors = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxLog
			// 
			this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLog.BackColor = System.Drawing.SystemColors.ControlLight;
			this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxLog.Location = new System.Drawing.Point(12, 222);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.ReadOnly = true;
			this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLog.Size = new System.Drawing.Size(426, 178);
			this.textBoxLog.TabIndex = 1;
			// 
			// buttonPause
			// 
			this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonPause.BackColor = System.Drawing.SystemColors.ControlLight;
			this.buttonPause.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonPause.Enabled = false;
			this.buttonPause.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.buttonPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPause.Location = new System.Drawing.Point(174, 580);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(75, 23);
			this.buttonPause.TabIndex = 6;
			this.buttonPause.Text = "Pause";
			this.buttonPause.UseVisualStyleBackColor = false;
			this.buttonPause.Visible = false;
			this.buttonPause.Click += new System.EventHandler(this.buttonSuspend_Click);
			// 
			// buttonReload
			// 
			this.buttonReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonReload.BackColor = System.Drawing.SystemColors.ControlLight;
			this.buttonReload.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.buttonReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonReload.Location = new System.Drawing.Point(93, 580);
			this.buttonReload.Name = "buttonReload";
			this.buttonReload.Size = new System.Drawing.Size(75, 23);
			this.buttonReload.TabIndex = 7;
			this.buttonReload.Text = "Reload";
			this.buttonReload.UseVisualStyleBackColor = false;
			this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonStop.BackColor = System.Drawing.SystemColors.ControlLight;
			this.buttonStop.Enabled = false;
			this.buttonStop.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStop.Location = new System.Drawing.Point(12, 580);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 8;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = false;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExit.BackColor = System.Drawing.SystemColors.ControlLight;
			this.buttonExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonExit.Location = new System.Drawing.Point(363, 580);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(75, 23);
			this.buttonExit.TabIndex = 11;
			this.buttonExit.Text = "Exit";
			this.buttonExit.UseVisualStyleBackColor = false;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// labelLog
			// 
			this.labelLog.AutoSize = true;
			this.labelLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelLog.Location = new System.Drawing.Point(212, 206);
			this.labelLog.Name = "labelLog";
			this.labelLog.Size = new System.Drawing.Size(25, 13);
			this.labelLog.TabIndex = 14;
			this.labelLog.Text = "Log";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 615);
			this.splitter1.TabIndex = 15;
			this.splitter1.TabStop = false;
			// 
			// buttonOptionsForm
			// 
			this.buttonOptionsForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOptionsForm.BackColor = System.Drawing.SystemColors.ControlLight;
			this.buttonOptionsForm.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.buttonOptionsForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOptionsForm.Location = new System.Drawing.Point(12, 551);
			this.buttonOptionsForm.Name = "buttonOptionsForm";
			this.buttonOptionsForm.Size = new System.Drawing.Size(116, 23);
			this.buttonOptionsForm.TabIndex = 16;
			this.buttonOptionsForm.Text = "Settings...";
			this.buttonOptionsForm.UseVisualStyleBackColor = false;
			this.buttonOptionsForm.Click += new System.EventHandler(this.buttonOptions_Click);
			// 
			// buttonAutoCycle
			// 
			this.buttonAutoCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAutoCycle.BackColor = System.Drawing.SystemColors.ControlLight;
			this.buttonAutoCycle.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.buttonAutoCycle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAutoCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonAutoCycle.ForeColor = System.Drawing.Color.Black;
			this.buttonAutoCycle.Location = new System.Drawing.Point(91, 12);
			this.buttonAutoCycle.Name = "buttonAutoCycle";
			this.buttonAutoCycle.Size = new System.Drawing.Size(277, 37);
			this.buttonAutoCycle.TabIndex = 18;
			this.buttonAutoCycle.Text = "Begin AutoCycle";
			this.buttonAutoCycle.UseVisualStyleBackColor = false;
			this.buttonAutoCycle.Click += new System.EventHandler(this.buttonAutoCycle_Click);
			// 
			// buttonTestsForm
			// 
			this.buttonTestsForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonTestsForm.BackColor = System.Drawing.SystemColors.ControlLight;
			this.buttonTestsForm.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.buttonTestsForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTestsForm.Location = new System.Drawing.Point(134, 551);
			this.buttonTestsForm.Name = "buttonTestsForm";
			this.buttonTestsForm.Size = new System.Drawing.Size(115, 23);
			this.buttonTestsForm.TabIndex = 19;
			this.buttonTestsForm.Text = "Tests...";
			this.buttonTestsForm.UseVisualStyleBackColor = false;
			this.buttonTestsForm.Click += new System.EventHandler(this.buttonTestsForm_Click);
			// 
			// listBoxTaskQueue
			// 
			this.listBoxTaskQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxTaskQueue.BackColor = System.Drawing.SystemColors.ControlLight;
			this.listBoxTaskQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBoxTaskQueue.FormattingEnabled = true;
			this.listBoxTaskQueue.Location = new System.Drawing.Point(12, 82);
			this.listBoxTaskQueue.Name = "listBoxTaskQueue";
			this.listBoxTaskQueue.ScrollAlwaysVisible = true;
			this.listBoxTaskQueue.Size = new System.Drawing.Size(426, 117);
			this.listBoxTaskQueue.TabIndex = 21;
			// 
			// labelTaskQueue
			// 
			this.labelTaskQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTaskQueue.AutoSize = true;
			this.labelTaskQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTaskQueue.Location = new System.Drawing.Point(193, 66);
			this.labelTaskQueue.Name = "labelTaskQueue";
			this.labelTaskQueue.Size = new System.Drawing.Size(66, 13);
			this.labelTaskQueue.TabIndex = 20;
			this.labelTaskQueue.Text = "Task Queue";
			// 
			// buttonErrors
			// 
			this.buttonErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonErrors.Location = new System.Drawing.Point(296, 551);
			this.buttonErrors.Name = "buttonErrors";
			this.buttonErrors.Size = new System.Drawing.Size(142, 23);
			this.buttonErrors.TabIndex = 22;
			this.buttonErrors.Text = "Errors";
			this.buttonErrors.UseVisualStyleBackColor = true;
			this.buttonErrors.Visible = false;
			this.buttonErrors.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBoxErrors
			// 
			this.listBoxErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxErrors.BackColor = System.Drawing.SystemColors.ControlLight;
			this.listBoxErrors.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBoxErrors.ForeColor = System.Drawing.Color.DarkRed;
			this.listBoxErrors.FormattingEnabled = true;
			this.listBoxErrors.HorizontalScrollbar = true;
			this.listBoxErrors.Location = new System.Drawing.Point(12, 424);
			this.listBoxErrors.Name = "listBoxErrors";
			this.listBoxErrors.ScrollAlwaysVisible = true;
			this.listBoxErrors.Size = new System.Drawing.Size(426, 117);
			this.listBoxErrors.TabIndex = 23;
			// 
			// labelErrors
			// 
			this.labelErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelErrors.AutoSize = true;
			this.labelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelErrors.Location = new System.Drawing.Point(206, 408);
			this.labelErrors.Name = "labelErrors";
			this.labelErrors.Size = new System.Drawing.Size(34, 13);
			this.labelErrors.TabIndex = 24;
			this.labelErrors.Text = "Errors";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(450, 615);
			this.Controls.Add(this.labelErrors);
			this.Controls.Add(this.listBoxErrors);
			this.Controls.Add(this.buttonErrors);
			this.Controls.Add(this.listBoxTaskQueue);
			this.Controls.Add(this.labelTaskQueue);
			this.Controls.Add(this.buttonTestsForm);
			this.Controls.Add(this.buttonAutoCycle);
			this.Controls.Add(this.buttonOptionsForm);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.labelLog);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonReload);
			this.Controls.Add(this.buttonPause);
			this.Controls.Add(this.textBoxLog);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(445, 500);
			this.Name = "MainForm";
			this.Text = "NeverClicker";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button buttonOptionsForm;
		private System.Windows.Forms.Button buttonAutoCycle;
		private System.Windows.Forms.Button buttonTestsForm;
		private System.Windows.Forms.ListBox listBoxTaskQueue;
		private System.Windows.Forms.Label labelTaskQueue;
		private System.Windows.Forms.Button buttonErrors;
		private System.Windows.Forms.ListBox listBoxErrors;
		private System.Windows.Forms.Label labelErrors;
	}
}