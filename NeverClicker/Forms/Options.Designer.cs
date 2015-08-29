namespace NeverClicker
{
    partial class Options
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
			this.labelAhkRootPath = new System.Windows.Forms.Label();
			this.textBoxSettingsRootPath = new System.Windows.Forms.TextBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxNwRootPath = new System.Windows.Forms.TextBox();
			this.labelNwRootPath = new System.Windows.Forms.Label();
			this.buttonChooseAhkRootPath = new System.Windows.Forms.Button();
			this.buttonChooseNWGameRootPath = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.buttonPC = new System.Windows.Forms.Button();
			this.buttonLaptop = new System.Windows.Forms.Button();
			this.buttonImagesFolder = new System.Windows.Forms.Button();
			this.textBoxImagesFolder = new System.Windows.Forms.TextBox();
			this.labelImagesFolder = new System.Windows.Forms.Label();
			this.buttonAccountIni = new System.Windows.Forms.Button();
			this.textBoxAccountIni = new System.Windows.Forms.TextBox();
			this.labelAccountIni = new System.Windows.Forms.Label();
			this.buttonGameClientIni = new System.Windows.Forms.Button();
			this.textBoxGameClientIni = new System.Windows.Forms.TextBox();
			this.labelGameClientIni = new System.Windows.Forms.Label();
			this.checkBoxImagesFolder = new System.Windows.Forms.CheckBox();
			this.checkBoxAccountIni = new System.Windows.Forms.CheckBox();
			this.checkBoxGameClientIni = new System.Windows.Forms.CheckBox();
			this.tabControlOptions = new System.Windows.Forms.TabControl();
			this.tabPagePaths = new System.Windows.Forms.TabPage();
			this.buttonLogFilePath = new System.Windows.Forms.Button();
			this.checkBoxLogFilePath = new System.Windows.Forms.CheckBox();
			this.textBoxLogFilePath = new System.Windows.Forms.TextBox();
			this.labelLogFilePath = new System.Windows.Forms.Label();
			this.tabPageClient = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.Account = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControlOptions.SuspendLayout();
			this.tabPagePaths.SuspendLayout();
			this.tabPageClient.SuspendLayout();
			this.Account.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelAhkRootPath
			// 
			this.labelAhkRootPath.AutoSize = true;
			this.labelAhkRootPath.Location = new System.Drawing.Point(6, 40);
			this.labelAhkRootPath.Name = "labelAhkRootPath";
			this.labelAhkRootPath.Size = new System.Drawing.Size(80, 13);
			this.labelAhkRootPath.TabIndex = 0;
			this.labelAhkRootPath.Text = "Settings Folder:";
			// 
			// textBoxSettingsRootPath
			// 
			this.textBoxSettingsRootPath.Location = new System.Drawing.Point(125, 37);
			this.textBoxSettingsRootPath.Name = "textBoxSettingsRootPath";
			this.textBoxSettingsRootPath.Size = new System.Drawing.Size(622, 20);
			this.textBoxSettingsRootPath.TabIndex = 1;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(691, 536);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(772, 536);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// textBoxNwRootPath
			// 
			this.textBoxNwRootPath.Location = new System.Drawing.Point(125, 8);
			this.textBoxNwRootPath.Name = "textBoxNwRootPath";
			this.textBoxNwRootPath.Size = new System.Drawing.Size(622, 20);
			this.textBoxNwRootPath.TabIndex = 5;
			// 
			// labelNwRootPath
			// 
			this.labelNwRootPath.AutoSize = true;
			this.labelNwRootPath.Location = new System.Drawing.Point(6, 11);
			this.labelNwRootPath.Name = "labelNwRootPath";
			this.labelNwRootPath.Size = new System.Drawing.Size(87, 13);
			this.labelNwRootPath.TabIndex = 4;
			this.labelNwRootPath.Text = "Neverwinter.exe:";
			// 
			// buttonChooseAhkRootPath
			// 
			this.buttonChooseAhkRootPath.Location = new System.Drawing.Point(753, 35);
			this.buttonChooseAhkRootPath.Name = "buttonChooseAhkRootPath";
			this.buttonChooseAhkRootPath.Size = new System.Drawing.Size(68, 23);
			this.buttonChooseAhkRootPath.TabIndex = 6;
			this.buttonChooseAhkRootPath.Text = "Browse...";
			this.buttonChooseAhkRootPath.UseVisualStyleBackColor = true;
			this.buttonChooseAhkRootPath.Click += new System.EventHandler(this.buttonChooseAhkRootPath_Click);
			// 
			// buttonChooseNWGameRootPath
			// 
			this.buttonChooseNWGameRootPath.Location = new System.Drawing.Point(753, 6);
			this.buttonChooseNWGameRootPath.Name = "buttonChooseNWGameRootPath";
			this.buttonChooseNWGameRootPath.Size = new System.Drawing.Size(68, 23);
			this.buttonChooseNWGameRootPath.TabIndex = 7;
			this.buttonChooseNWGameRootPath.Text = "Browse...";
			this.buttonChooseNWGameRootPath.UseVisualStyleBackColor = true;
			this.buttonChooseNWGameRootPath.Click += new System.EventHandler(this.buttonChooseNWGameRootPath_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// buttonPC
			// 
			this.buttonPC.Location = new System.Drawing.Point(284, 536);
			this.buttonPC.Name = "buttonPC";
			this.buttonPC.Size = new System.Drawing.Size(131, 23);
			this.buttonPC.TabIndex = 8;
			this.buttonPC.Text = "Load PC Defaults";
			this.buttonPC.UseVisualStyleBackColor = true;
			this.buttonPC.Click += new System.EventHandler(this.buttonPC_Click);
			// 
			// buttonLaptop
			// 
			this.buttonLaptop.Location = new System.Drawing.Point(141, 536);
			this.buttonLaptop.Name = "buttonLaptop";
			this.buttonLaptop.Size = new System.Drawing.Size(137, 23);
			this.buttonLaptop.TabIndex = 9;
			this.buttonLaptop.Text = "Load Laptop Defaults";
			this.buttonLaptop.UseVisualStyleBackColor = true;
			this.buttonLaptop.Click += new System.EventHandler(this.buttonLaptop_Click);
			// 
			// buttonImagesFolder
			// 
			this.buttonImagesFolder.Enabled = false;
			this.buttonImagesFolder.Location = new System.Drawing.Point(753, 64);
			this.buttonImagesFolder.Name = "buttonImagesFolder";
			this.buttonImagesFolder.Size = new System.Drawing.Size(68, 23);
			this.buttonImagesFolder.TabIndex = 12;
			this.buttonImagesFolder.Text = "Browse...";
			this.buttonImagesFolder.UseVisualStyleBackColor = true;
			// 
			// textBoxImagesFolder
			// 
			this.textBoxImagesFolder.Location = new System.Drawing.Point(191, 66);
			this.textBoxImagesFolder.Name = "textBoxImagesFolder";
			this.textBoxImagesFolder.ReadOnly = true;
			this.textBoxImagesFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxImagesFolder.TabIndex = 11;
			// 
			// labelImagesFolder
			// 
			this.labelImagesFolder.AutoSize = true;
			this.labelImagesFolder.Location = new System.Drawing.Point(6, 69);
			this.labelImagesFolder.Name = "labelImagesFolder";
			this.labelImagesFolder.Size = new System.Drawing.Size(76, 13);
			this.labelImagesFolder.TabIndex = 10;
			this.labelImagesFolder.Text = "Images Folder:";
			// 
			// buttonAccountIni
			// 
			this.buttonAccountIni.Enabled = false;
			this.buttonAccountIni.Location = new System.Drawing.Point(753, 93);
			this.buttonAccountIni.Name = "buttonAccountIni";
			this.buttonAccountIni.Size = new System.Drawing.Size(68, 23);
			this.buttonAccountIni.TabIndex = 15;
			this.buttonAccountIni.Text = "Browse...";
			this.buttonAccountIni.UseVisualStyleBackColor = true;
			// 
			// textBoxAccountIni
			// 
			this.textBoxAccountIni.Location = new System.Drawing.Point(191, 95);
			this.textBoxAccountIni.Name = "textBoxAccountIni";
			this.textBoxAccountIni.ReadOnly = true;
			this.textBoxAccountIni.Size = new System.Drawing.Size(556, 20);
			this.textBoxAccountIni.TabIndex = 14;
			// 
			// labelAccountIni
			// 
			this.labelAccountIni.AutoSize = true;
			this.labelAccountIni.Location = new System.Drawing.Point(6, 98);
			this.labelAccountIni.Name = "labelAccountIni";
			this.labelAccountIni.Size = new System.Drawing.Size(95, 13);
			this.labelAccountIni.TabIndex = 13;
			this.labelAccountIni.Text = "Game Account Ini:";
			// 
			// buttonGameClientIni
			// 
			this.buttonGameClientIni.Enabled = false;
			this.buttonGameClientIni.Location = new System.Drawing.Point(753, 122);
			this.buttonGameClientIni.Name = "buttonGameClientIni";
			this.buttonGameClientIni.Size = new System.Drawing.Size(68, 23);
			this.buttonGameClientIni.TabIndex = 18;
			this.buttonGameClientIni.Text = "Browse...";
			this.buttonGameClientIni.UseVisualStyleBackColor = true;
			// 
			// textBoxGameClientIni
			// 
			this.textBoxGameClientIni.Location = new System.Drawing.Point(191, 124);
			this.textBoxGameClientIni.Name = "textBoxGameClientIni";
			this.textBoxGameClientIni.ReadOnly = true;
			this.textBoxGameClientIni.Size = new System.Drawing.Size(556, 20);
			this.textBoxGameClientIni.TabIndex = 17;
			// 
			// labelGameClientIni
			// 
			this.labelGameClientIni.AutoSize = true;
			this.labelGameClientIni.Location = new System.Drawing.Point(6, 127);
			this.labelGameClientIni.Name = "labelGameClientIni";
			this.labelGameClientIni.Size = new System.Drawing.Size(81, 13);
			this.labelGameClientIni.TabIndex = 16;
			this.labelGameClientIni.Text = "Game Client Ini:";
			// 
			// checkBoxImagesFolder
			// 
			this.checkBoxImagesFolder.AutoSize = true;
			this.checkBoxImagesFolder.Checked = true;
			this.checkBoxImagesFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxImagesFolder.Enabled = false;
			this.checkBoxImagesFolder.Location = new System.Drawing.Point(125, 68);
			this.checkBoxImagesFolder.Name = "checkBoxImagesFolder";
			this.checkBoxImagesFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxImagesFolder.TabIndex = 19;
			this.checkBoxImagesFolder.Text = "Default";
			this.checkBoxImagesFolder.UseVisualStyleBackColor = true;
			// 
			// checkBoxAccountIni
			// 
			this.checkBoxAccountIni.AutoSize = true;
			this.checkBoxAccountIni.Checked = true;
			this.checkBoxAccountIni.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxAccountIni.Enabled = false;
			this.checkBoxAccountIni.Location = new System.Drawing.Point(125, 97);
			this.checkBoxAccountIni.Name = "checkBoxAccountIni";
			this.checkBoxAccountIni.Size = new System.Drawing.Size(60, 17);
			this.checkBoxAccountIni.TabIndex = 20;
			this.checkBoxAccountIni.Text = "Default";
			this.checkBoxAccountIni.UseVisualStyleBackColor = true;
			// 
			// checkBoxGameClientIni
			// 
			this.checkBoxGameClientIni.AutoSize = true;
			this.checkBoxGameClientIni.Checked = true;
			this.checkBoxGameClientIni.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxGameClientIni.Enabled = false;
			this.checkBoxGameClientIni.Location = new System.Drawing.Point(125, 126);
			this.checkBoxGameClientIni.Name = "checkBoxGameClientIni";
			this.checkBoxGameClientIni.Size = new System.Drawing.Size(60, 17);
			this.checkBoxGameClientIni.TabIndex = 21;
			this.checkBoxGameClientIni.Text = "Default";
			this.checkBoxGameClientIni.UseVisualStyleBackColor = true;
			// 
			// tabControlOptions
			// 
			this.tabControlOptions.Controls.Add(this.tabPagePaths);
			this.tabControlOptions.Controls.Add(this.tabPageClient);
			this.tabControlOptions.Controls.Add(this.Account);
			this.tabControlOptions.Location = new System.Drawing.Point(12, 12);
			this.tabControlOptions.Name = "tabControlOptions";
			this.tabControlOptions.SelectedIndex = 0;
			this.tabControlOptions.Size = new System.Drawing.Size(835, 485);
			this.tabControlOptions.TabIndex = 22;
			// 
			// tabPagePaths
			// 
			this.tabPagePaths.BackColor = System.Drawing.Color.Transparent;
			this.tabPagePaths.Controls.Add(this.buttonLogFilePath);
			this.tabPagePaths.Controls.Add(this.checkBoxLogFilePath);
			this.tabPagePaths.Controls.Add(this.textBoxLogFilePath);
			this.tabPagePaths.Controls.Add(this.labelLogFilePath);
			this.tabPagePaths.Controls.Add(this.textBoxSettingsRootPath);
			this.tabPagePaths.Controls.Add(this.buttonGameClientIni);
			this.tabPagePaths.Controls.Add(this.checkBoxGameClientIni);
			this.tabPagePaths.Controls.Add(this.buttonAccountIni);
			this.tabPagePaths.Controls.Add(this.labelAhkRootPath);
			this.tabPagePaths.Controls.Add(this.buttonImagesFolder);
			this.tabPagePaths.Controls.Add(this.checkBoxAccountIni);
			this.tabPagePaths.Controls.Add(this.labelNwRootPath);
			this.tabPagePaths.Controls.Add(this.checkBoxImagesFolder);
			this.tabPagePaths.Controls.Add(this.buttonChooseNWGameRootPath);
			this.tabPagePaths.Controls.Add(this.textBoxNwRootPath);
			this.tabPagePaths.Controls.Add(this.buttonChooseAhkRootPath);
			this.tabPagePaths.Controls.Add(this.labelImagesFolder);
			this.tabPagePaths.Controls.Add(this.textBoxGameClientIni);
			this.tabPagePaths.Controls.Add(this.textBoxImagesFolder);
			this.tabPagePaths.Controls.Add(this.labelGameClientIni);
			this.tabPagePaths.Controls.Add(this.labelAccountIni);
			this.tabPagePaths.Controls.Add(this.textBoxAccountIni);
			this.tabPagePaths.Location = new System.Drawing.Point(4, 22);
			this.tabPagePaths.Name = "tabPagePaths";
			this.tabPagePaths.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePaths.Size = new System.Drawing.Size(827, 459);
			this.tabPagePaths.TabIndex = 0;
			this.tabPagePaths.Text = "Paths";
			// 
			// buttonLogFilePath
			// 
			this.buttonLogFilePath.Enabled = false;
			this.buttonLogFilePath.Location = new System.Drawing.Point(753, 151);
			this.buttonLogFilePath.Name = "buttonLogFilePath";
			this.buttonLogFilePath.Size = new System.Drawing.Size(68, 23);
			this.buttonLogFilePath.TabIndex = 24;
			this.buttonLogFilePath.Text = "Browse...";
			this.buttonLogFilePath.UseVisualStyleBackColor = true;
			// 
			// checkBoxLogFilePath
			// 
			this.checkBoxLogFilePath.AutoSize = true;
			this.checkBoxLogFilePath.Checked = true;
			this.checkBoxLogFilePath.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxLogFilePath.Enabled = false;
			this.checkBoxLogFilePath.Location = new System.Drawing.Point(125, 155);
			this.checkBoxLogFilePath.Name = "checkBoxLogFilePath";
			this.checkBoxLogFilePath.Size = new System.Drawing.Size(60, 17);
			this.checkBoxLogFilePath.TabIndex = 25;
			this.checkBoxLogFilePath.Text = "Default";
			this.checkBoxLogFilePath.UseVisualStyleBackColor = true;
			// 
			// textBoxLogFilePath
			// 
			this.textBoxLogFilePath.Location = new System.Drawing.Point(191, 153);
			this.textBoxLogFilePath.Name = "textBoxLogFilePath";
			this.textBoxLogFilePath.ReadOnly = true;
			this.textBoxLogFilePath.Size = new System.Drawing.Size(556, 20);
			this.textBoxLogFilePath.TabIndex = 23;
			// 
			// labelLogFilePath
			// 
			this.labelLogFilePath.AutoSize = true;
			this.labelLogFilePath.Location = new System.Drawing.Point(6, 156);
			this.labelLogFilePath.Name = "labelLogFilePath";
			this.labelLogFilePath.Size = new System.Drawing.Size(47, 13);
			this.labelLogFilePath.TabIndex = 22;
			this.labelLogFilePath.Text = "Log File:";
			// 
			// tabPageClient
			// 
			this.tabPageClient.BackColor = System.Drawing.Color.Transparent;
			this.tabPageClient.Controls.Add(this.label1);
			this.tabPageClient.Location = new System.Drawing.Point(4, 22);
			this.tabPageClient.Name = "tabPageClient";
			this.tabPageClient.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageClient.Size = new System.Drawing.Size(827, 459);
			this.tabPageClient.TabIndex = 1;
			this.tabPageClient.Text = "Client";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(319, 203);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(211, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Not Yet Implemented - Edit Ini File Manually";
			// 
			// Account
			// 
			this.Account.BackColor = System.Drawing.Color.Transparent;
			this.Account.Controls.Add(this.label2);
			this.Account.Location = new System.Drawing.Point(4, 22);
			this.Account.Name = "Account";
			this.Account.Size = new System.Drawing.Size(827, 459);
			this.Account.TabIndex = 2;
			this.Account.Text = "Account";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(315, 203);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(211, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Not Yet Implemented - Edit Ini File Manually";
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(859, 571);
			this.Controls.Add(this.tabControlOptions);
			this.Controls.Add(this.buttonLaptop);
			this.Controls.Add(this.buttonPC);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Name = "Options";
			this.Text = "Options";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Options_Load);
			this.tabControlOptions.ResumeLayout(false);
			this.tabPagePaths.ResumeLayout(false);
			this.tabPagePaths.PerformLayout();
			this.tabPageClient.ResumeLayout(false);
			this.tabPageClient.PerformLayout();
			this.Account.ResumeLayout(false);
			this.Account.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelAhkRootPath;
        private System.Windows.Forms.TextBox textBoxSettingsRootPath;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxNwRootPath;
        private System.Windows.Forms.Label labelNwRootPath;
        private System.Windows.Forms.Button buttonChooseAhkRootPath;
        private System.Windows.Forms.Button buttonChooseNWGameRootPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonPC;
        private System.Windows.Forms.Button buttonLaptop;
		private System.Windows.Forms.Button buttonImagesFolder;
		private System.Windows.Forms.TextBox textBoxImagesFolder;
		private System.Windows.Forms.Label labelImagesFolder;
		private System.Windows.Forms.Button buttonAccountIni;
		private System.Windows.Forms.TextBox textBoxAccountIni;
		private System.Windows.Forms.Label labelAccountIni;
		private System.Windows.Forms.Button buttonGameClientIni;
		private System.Windows.Forms.TextBox textBoxGameClientIni;
		private System.Windows.Forms.Label labelGameClientIni;
		private System.Windows.Forms.CheckBox checkBoxImagesFolder;
		private System.Windows.Forms.CheckBox checkBoxAccountIni;
		private System.Windows.Forms.CheckBox checkBoxGameClientIni;
		private System.Windows.Forms.TabControl tabControlOptions;
		private System.Windows.Forms.TabPage tabPagePaths;
		private System.Windows.Forms.TabPage tabPageClient;
		private System.Windows.Forms.TabPage Account;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonLogFilePath;
		private System.Windows.Forms.CheckBox checkBoxLogFilePath;
		private System.Windows.Forms.TextBox textBoxLogFilePath;
		private System.Windows.Forms.Label labelLogFilePath;
	}
}