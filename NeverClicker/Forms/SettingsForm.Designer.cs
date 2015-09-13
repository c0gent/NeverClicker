namespace NeverClicker
{
    partial class SettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.tabPageClient = new System.Windows.Forms.TabPage();
			this.linkLabelClientIniFile = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPageAccount = new System.Windows.Forms.TabPage();
			this.linkLabelAccountIniFile = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPagePaths = new System.Windows.Forms.TabPage();
			this.labelLogDebug = new System.Windows.Forms.Label();
			this.checkBoxLogDebug = new System.Windows.Forms.CheckBox();
			this.textBoxImageShadeVariation = new System.Windows.Forms.TextBox();
			this.textBoxUserRootFolder = new System.Windows.Forms.TextBox();
			this.textBoxLogsFolder = new System.Windows.Forms.TextBox();
			this.textBoxSettingsFolder = new System.Windows.Forms.TextBox();
			this.textBoxPatcherExePath = new System.Windows.Forms.TextBox();
			this.textBoxImagesFolder = new System.Windows.Forms.TextBox();
			this.labelImageShadeVariation = new System.Windows.Forms.Label();
			this.linkLabelUserConfigFile = new System.Windows.Forms.LinkLabel();
			this.checkBoxUserRootFolder = new System.Windows.Forms.CheckBox();
			this.labelUserRootFolder = new System.Windows.Forms.Label();
			this.buttonUserRootFolder = new System.Windows.Forms.Button();
			this.checkBoxSettingsFolder = new System.Windows.Forms.CheckBox();
			this.buttonLogsFolder = new System.Windows.Forms.Button();
			this.checkBoxLogsFolder = new System.Windows.Forms.CheckBox();
			this.labelLogsFolder = new System.Windows.Forms.Label();
			this.labelSettingsFolder = new System.Windows.Forms.Label();
			this.buttonImagesFolder = new System.Windows.Forms.Button();
			this.labelPatcherExePath = new System.Windows.Forms.Label();
			this.checkBoxImagesFolder = new System.Windows.Forms.CheckBox();
			this.buttonPatcherExePath = new System.Windows.Forms.Button();
			this.buttonSettingsFolder = new System.Windows.Forms.Button();
			this.labelImagesFolder = new System.Windows.Forms.Label();
			this.tabControlOptions = new System.Windows.Forms.TabControl();
			this.tabPageClient.SuspendLayout();
			this.tabPageAccount.SuspendLayout();
			this.tabPagePaths.SuspendLayout();
			this.tabControlOptions.SuspendLayout();
			this.SuspendLayout();
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
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// tabPageClient
			// 
			this.tabPageClient.BackColor = System.Drawing.Color.Transparent;
			this.tabPageClient.Controls.Add(this.linkLabelClientIniFile);
			this.tabPageClient.Controls.Add(this.label1);
			this.tabPageClient.Location = new System.Drawing.Point(4, 22);
			this.tabPageClient.Name = "tabPageClient";
			this.tabPageClient.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageClient.Size = new System.Drawing.Size(827, 459);
			this.tabPageClient.TabIndex = 1;
			this.tabPageClient.Text = "Client";
			// 
			// linkLabelClientIniFile
			// 
			this.linkLabelClientIniFile.Location = new System.Drawing.Point(37, 219);
			this.linkLabelClientIniFile.Name = "linkLabelClientIniFile";
			this.linkLabelClientIniFile.Size = new System.Drawing.Size(755, 13);
			this.linkLabelClientIniFile.TabIndex = 28;
			this.linkLabelClientIniFile.TabStop = true;
			this.linkLabelClientIniFile.Text = "linkLabel2";
			this.linkLabelClientIniFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(315, 203);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(211, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Not Yet Implemented - Edit Ini File Manually";
			// 
			// tabPageAccount
			// 
			this.tabPageAccount.BackColor = System.Drawing.Color.Transparent;
			this.tabPageAccount.Controls.Add(this.linkLabelAccountIniFile);
			this.tabPageAccount.Controls.Add(this.label2);
			this.tabPageAccount.Location = new System.Drawing.Point(4, 22);
			this.tabPageAccount.Name = "tabPageAccount";
			this.tabPageAccount.Size = new System.Drawing.Size(827, 459);
			this.tabPageAccount.TabIndex = 2;
			this.tabPageAccount.Text = "Account";
			// 
			// linkLabelAccountIniFile
			// 
			this.linkLabelAccountIniFile.Location = new System.Drawing.Point(37, 219);
			this.linkLabelAccountIniFile.Name = "linkLabelAccountIniFile";
			this.linkLabelAccountIniFile.Size = new System.Drawing.Size(755, 13);
			this.linkLabelAccountIniFile.TabIndex = 28;
			this.linkLabelAccountIniFile.TabStop = true;
			this.linkLabelAccountIniFile.Text = "linkLabel1";
			this.linkLabelAccountIniFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// tabPagePaths
			// 
			this.tabPagePaths.BackColor = System.Drawing.Color.Transparent;
			this.tabPagePaths.Controls.Add(this.labelLogDebug);
			this.tabPagePaths.Controls.Add(this.checkBoxLogDebug);
			this.tabPagePaths.Controls.Add(this.textBoxImageShadeVariation);
			this.tabPagePaths.Controls.Add(this.textBoxUserRootFolder);
			this.tabPagePaths.Controls.Add(this.textBoxLogsFolder);
			this.tabPagePaths.Controls.Add(this.textBoxSettingsFolder);
			this.tabPagePaths.Controls.Add(this.textBoxPatcherExePath);
			this.tabPagePaths.Controls.Add(this.textBoxImagesFolder);
			this.tabPagePaths.Controls.Add(this.labelImageShadeVariation);
			this.tabPagePaths.Controls.Add(this.linkLabelUserConfigFile);
			this.tabPagePaths.Controls.Add(this.checkBoxUserRootFolder);
			this.tabPagePaths.Controls.Add(this.labelUserRootFolder);
			this.tabPagePaths.Controls.Add(this.buttonUserRootFolder);
			this.tabPagePaths.Controls.Add(this.checkBoxSettingsFolder);
			this.tabPagePaths.Controls.Add(this.buttonLogsFolder);
			this.tabPagePaths.Controls.Add(this.checkBoxLogsFolder);
			this.tabPagePaths.Controls.Add(this.labelLogsFolder);
			this.tabPagePaths.Controls.Add(this.labelSettingsFolder);
			this.tabPagePaths.Controls.Add(this.buttonImagesFolder);
			this.tabPagePaths.Controls.Add(this.labelPatcherExePath);
			this.tabPagePaths.Controls.Add(this.checkBoxImagesFolder);
			this.tabPagePaths.Controls.Add(this.buttonPatcherExePath);
			this.tabPagePaths.Controls.Add(this.buttonSettingsFolder);
			this.tabPagePaths.Controls.Add(this.labelImagesFolder);
			this.tabPagePaths.Location = new System.Drawing.Point(4, 22);
			this.tabPagePaths.Name = "tabPagePaths";
			this.tabPagePaths.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePaths.Size = new System.Drawing.Size(827, 459);
			this.tabPagePaths.TabIndex = 0;
			this.tabPagePaths.Text = "Paths";
			// 
			// labelLogDebug
			// 
			this.labelLogDebug.AutoSize = true;
			this.labelLogDebug.Location = new System.Drawing.Point(6, 210);
			this.labelLogDebug.Name = "labelLogDebug";
			this.labelLogDebug.Size = new System.Drawing.Size(114, 13);
			this.labelLogDebug.TabIndex = 31;
			this.labelLogDebug.Text = "Log Debug Messages:";
			// 
			// checkBoxLogDebug
			// 
			this.checkBoxLogDebug.AutoSize = true;
			this.checkBoxLogDebug.Location = new System.Drawing.Point(141, 209);
			this.checkBoxLogDebug.Name = "checkBoxLogDebug";
			this.checkBoxLogDebug.Size = new System.Drawing.Size(65, 17);
			this.checkBoxLogDebug.TabIndex = 30;
			this.checkBoxLogDebug.Text = "Enabled";
			this.checkBoxLogDebug.UseVisualStyleBackColor = true;
			this.checkBoxLogDebug.CheckedChanged += new System.EventHandler(this.checkBoxLogDebug_CheckedChanged);
			// 
			// textBoxImageShadeVariation
			// 
			this.textBoxImageShadeVariation.Location = new System.Drawing.Point(141, 183);
			this.textBoxImageShadeVariation.Name = "textBoxImageShadeVariation";
			this.textBoxImageShadeVariation.Size = new System.Drawing.Size(93, 20);
			this.textBoxImageShadeVariation.TabIndex = 29;
			// 
			// textBoxUserRootFolder
			// 
			this.textBoxUserRootFolder.Location = new System.Drawing.Point(191, 37);
			this.textBoxUserRootFolder.Name = "textBoxUserRootFolder";
			this.textBoxUserRootFolder.ReadOnly = true;
			this.textBoxUserRootFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxUserRootFolder.TabIndex = 25;
			this.textBoxUserRootFolder.TextChanged += new System.EventHandler(this.textBoxUserRootFolder_TextChanged);
			// 
			// textBoxLogsFolder
			// 
			this.textBoxLogsFolder.Location = new System.Drawing.Point(191, 124);
			this.textBoxLogsFolder.Name = "textBoxLogsFolder";
			this.textBoxLogsFolder.ReadOnly = true;
			this.textBoxLogsFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxLogsFolder.TabIndex = 12;
			// 
			// textBoxSettingsFolder
			// 
			this.textBoxSettingsFolder.Location = new System.Drawing.Point(191, 66);
			this.textBoxSettingsFolder.Name = "textBoxSettingsFolder";
			this.textBoxSettingsFolder.ReadOnly = true;
			this.textBoxSettingsFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxSettingsFolder.TabIndex = 6;
			// 
			// textBoxPatcherExePath
			// 
			this.textBoxPatcherExePath.Location = new System.Drawing.Point(191, 8);
			this.textBoxPatcherExePath.Name = "textBoxPatcherExePath";
			this.textBoxPatcherExePath.Size = new System.Drawing.Size(556, 20);
			this.textBoxPatcherExePath.TabIndex = 3;
			// 
			// textBoxImagesFolder
			// 
			this.textBoxImagesFolder.Location = new System.Drawing.Point(191, 95);
			this.textBoxImagesFolder.Name = "textBoxImagesFolder";
			this.textBoxImagesFolder.ReadOnly = true;
			this.textBoxImagesFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxImagesFolder.TabIndex = 9;
			// 
			// labelImageShadeVariation
			// 
			this.labelImageShadeVariation.AutoSize = true;
			this.labelImageShadeVariation.Location = new System.Drawing.Point(6, 186);
			this.labelImageShadeVariation.Name = "labelImageShadeVariation";
			this.labelImageShadeVariation.Size = new System.Drawing.Size(120, 13);
			this.labelImageShadeVariation.TabIndex = 28;
			this.labelImageShadeVariation.Text = "Image Shade Variation: ";
			// 
			// linkLabelUserConfigFile
			// 
			this.linkLabelUserConfigFile.AutoSize = true;
			this.linkLabelUserConfigFile.Location = new System.Drawing.Point(14, 436);
			this.linkLabelUserConfigFile.Name = "linkLabelUserConfigFile";
			this.linkLabelUserConfigFile.Size = new System.Drawing.Size(117, 13);
			this.linkLabelUserConfigFile.TabIndex = 27;
			this.linkLabelUserConfigFile.TabStop = true;
			this.linkLabelUserConfigFile.Text = "linkLabelUserConfigFile";
			this.linkLabelUserConfigFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUserConfigFile_LinkClicked);
			// 
			// checkBoxUserRootFolder
			// 
			this.checkBoxUserRootFolder.AutoSize = true;
			this.checkBoxUserRootFolder.Checked = global::NeverClicker.Properties.Settings.Default.UserRootFolderPathIsDefault;
			this.checkBoxUserRootFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxUserRootFolder.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::NeverClicker.Properties.Settings.Default, "UserRootFolderPathIsDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxUserRootFolder.Location = new System.Drawing.Point(125, 39);
			this.checkBoxUserRootFolder.Name = "checkBoxUserRootFolder";
			this.checkBoxUserRootFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxUserRootFolder.TabIndex = 24;
			this.checkBoxUserRootFolder.Text = "Default";
			this.checkBoxUserRootFolder.UseVisualStyleBackColor = true;
			this.checkBoxUserRootFolder.CheckedChanged += new System.EventHandler(this.checkBoxUserRootFolder_CheckedChanged);
			// 
			// labelUserRootFolder
			// 
			this.labelUserRootFolder.AutoSize = true;
			this.labelUserRootFolder.Location = new System.Drawing.Point(6, 40);
			this.labelUserRootFolder.Name = "labelUserRootFolder";
			this.labelUserRootFolder.Size = new System.Drawing.Size(90, 13);
			this.labelUserRootFolder.TabIndex = 23;
			this.labelUserRootFolder.Text = "User Root Folder:";
			// 
			// buttonUserRootFolder
			// 
			this.buttonUserRootFolder.Enabled = false;
			this.buttonUserRootFolder.Location = new System.Drawing.Point(753, 35);
			this.buttonUserRootFolder.Name = "buttonUserRootFolder";
			this.buttonUserRootFolder.Size = new System.Drawing.Size(68, 23);
			this.buttonUserRootFolder.TabIndex = 26;
			this.buttonUserRootFolder.Text = "Browse...";
			this.buttonUserRootFolder.UseVisualStyleBackColor = true;
			this.buttonUserRootFolder.Click += new System.EventHandler(this.buttonUserRootFolder_Click);
			// 
			// checkBoxSettingsFolder
			// 
			this.checkBoxSettingsFolder.AutoSize = true;
			this.checkBoxSettingsFolder.Checked = global::NeverClicker.Properties.Settings.Default.SettingsFolderPathIsDefault;
			this.checkBoxSettingsFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxSettingsFolder.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::NeverClicker.Properties.Settings.Default, "SettingsFolderPathIsDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxSettingsFolder.Location = new System.Drawing.Point(125, 68);
			this.checkBoxSettingsFolder.Name = "checkBoxSettingsFolder";
			this.checkBoxSettingsFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxSettingsFolder.TabIndex = 5;
			this.checkBoxSettingsFolder.Text = "Default";
			this.checkBoxSettingsFolder.UseVisualStyleBackColor = true;
			// 
			// buttonLogsFolder
			// 
			this.buttonLogsFolder.Enabled = false;
			this.buttonLogsFolder.Location = new System.Drawing.Point(753, 122);
			this.buttonLogsFolder.Name = "buttonLogsFolder";
			this.buttonLogsFolder.Size = new System.Drawing.Size(68, 23);
			this.buttonLogsFolder.TabIndex = 13;
			this.buttonLogsFolder.Text = "Browse...";
			this.buttonLogsFolder.UseVisualStyleBackColor = true;
			// 
			// checkBoxLogsFolder
			// 
			this.checkBoxLogsFolder.AutoSize = true;
			this.checkBoxLogsFolder.Checked = global::NeverClicker.Properties.Settings.Default.LogsFolderPathIsDefault;
			this.checkBoxLogsFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxLogsFolder.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::NeverClicker.Properties.Settings.Default, "LogsFolderPathIsDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxLogsFolder.Location = new System.Drawing.Point(125, 126);
			this.checkBoxLogsFolder.Name = "checkBoxLogsFolder";
			this.checkBoxLogsFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxLogsFolder.TabIndex = 11;
			this.checkBoxLogsFolder.Text = "Default";
			this.checkBoxLogsFolder.UseVisualStyleBackColor = true;
			// 
			// labelLogsFolder
			// 
			this.labelLogsFolder.AutoSize = true;
			this.labelLogsFolder.Location = new System.Drawing.Point(23, 127);
			this.labelLogsFolder.Name = "labelLogsFolder";
			this.labelLogsFolder.Size = new System.Drawing.Size(65, 13);
			this.labelLogsFolder.TabIndex = 22;
			this.labelLogsFolder.Text = "Logs Folder:";
			// 
			// labelSettingsFolder
			// 
			this.labelSettingsFolder.AutoSize = true;
			this.labelSettingsFolder.Location = new System.Drawing.Point(23, 69);
			this.labelSettingsFolder.Name = "labelSettingsFolder";
			this.labelSettingsFolder.Size = new System.Drawing.Size(80, 13);
			this.labelSettingsFolder.TabIndex = 0;
			this.labelSettingsFolder.Text = "Settings Folder:";
			// 
			// buttonImagesFolder
			// 
			this.buttonImagesFolder.Enabled = false;
			this.buttonImagesFolder.Location = new System.Drawing.Point(753, 93);
			this.buttonImagesFolder.Name = "buttonImagesFolder";
			this.buttonImagesFolder.Size = new System.Drawing.Size(68, 23);
			this.buttonImagesFolder.TabIndex = 10;
			this.buttonImagesFolder.Text = "Browse...";
			this.buttonImagesFolder.UseVisualStyleBackColor = true;
			// 
			// labelPatcherExePath
			// 
			this.labelPatcherExePath.AutoSize = true;
			this.labelPatcherExePath.Location = new System.Drawing.Point(6, 11);
			this.labelPatcherExePath.Name = "labelPatcherExePath";
			this.labelPatcherExePath.Size = new System.Drawing.Size(133, 13);
			this.labelPatcherExePath.TabIndex = 4;
			this.labelPatcherExePath.Text = "Patcher (Neverwinter.exe):";
			// 
			// checkBoxImagesFolder
			// 
			this.checkBoxImagesFolder.AutoSize = true;
			this.checkBoxImagesFolder.Checked = global::NeverClicker.Properties.Settings.Default.ImagesFolderPathIsDefault;
			this.checkBoxImagesFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxImagesFolder.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::NeverClicker.Properties.Settings.Default, "ImagesFolderPathIsDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxImagesFolder.Location = new System.Drawing.Point(125, 97);
			this.checkBoxImagesFolder.Name = "checkBoxImagesFolder";
			this.checkBoxImagesFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxImagesFolder.TabIndex = 8;
			this.checkBoxImagesFolder.Text = "Default";
			this.checkBoxImagesFolder.UseVisualStyleBackColor = true;
			// 
			// buttonPatcherExePath
			// 
			this.buttonPatcherExePath.Location = new System.Drawing.Point(753, 6);
			this.buttonPatcherExePath.Name = "buttonPatcherExePath";
			this.buttonPatcherExePath.Size = new System.Drawing.Size(68, 23);
			this.buttonPatcherExePath.TabIndex = 4;
			this.buttonPatcherExePath.Text = "Browse...";
			this.buttonPatcherExePath.UseVisualStyleBackColor = true;
			this.buttonPatcherExePath.Click += new System.EventHandler(this.buttonPatcherExePath_Click);
			// 
			// buttonSettingsFolder
			// 
			this.buttonSettingsFolder.Enabled = false;
			this.buttonSettingsFolder.Location = new System.Drawing.Point(753, 64);
			this.buttonSettingsFolder.Name = "buttonSettingsFolder";
			this.buttonSettingsFolder.Size = new System.Drawing.Size(68, 23);
			this.buttonSettingsFolder.TabIndex = 7;
			this.buttonSettingsFolder.Text = "Browse...";
			this.buttonSettingsFolder.UseVisualStyleBackColor = true;
			// 
			// labelImagesFolder
			// 
			this.labelImagesFolder.AutoSize = true;
			this.labelImagesFolder.Location = new System.Drawing.Point(23, 98);
			this.labelImagesFolder.Name = "labelImagesFolder";
			this.labelImagesFolder.Size = new System.Drawing.Size(76, 13);
			this.labelImagesFolder.TabIndex = 10;
			this.labelImagesFolder.Text = "Images Folder:";
			// 
			// tabControlOptions
			// 
			this.tabControlOptions.Controls.Add(this.tabPagePaths);
			this.tabControlOptions.Controls.Add(this.tabPageAccount);
			this.tabControlOptions.Controls.Add(this.tabPageClient);
			this.tabControlOptions.Location = new System.Drawing.Point(12, 12);
			this.tabControlOptions.Name = "tabControlOptions";
			this.tabControlOptions.SelectedIndex = 0;
			this.tabControlOptions.Size = new System.Drawing.Size(835, 485);
			this.tabControlOptions.TabIndex = 22;
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(859, 571);
			this.Controls.Add(this.tabControlOptions);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SettingsForm";
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.tabPageClient.ResumeLayout(false);
			this.tabPageClient.PerformLayout();
			this.tabPageAccount.ResumeLayout(false);
			this.tabPageAccount.PerformLayout();
			this.tabPagePaths.ResumeLayout(false);
			this.tabPagePaths.PerformLayout();
			this.tabControlOptions.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TabPage tabPageClient;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPageAccount;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPagePaths;
		private System.Windows.Forms.TextBox textBoxImageShadeVariation;
		private System.Windows.Forms.TextBox textBoxUserRootFolder;
		private System.Windows.Forms.TextBox textBoxLogsFolder;
		private System.Windows.Forms.TextBox textBoxSettingsFolder;
		private System.Windows.Forms.TextBox textBoxPatcherExePath;
		private System.Windows.Forms.TextBox textBoxImagesFolder;
		private System.Windows.Forms.Label labelImageShadeVariation;
		private System.Windows.Forms.LinkLabel linkLabelUserConfigFile;
		private System.Windows.Forms.CheckBox checkBoxUserRootFolder;
		private System.Windows.Forms.Label labelUserRootFolder;
		private System.Windows.Forms.Button buttonUserRootFolder;
		private System.Windows.Forms.CheckBox checkBoxSettingsFolder;
		private System.Windows.Forms.Button buttonLogsFolder;
		private System.Windows.Forms.CheckBox checkBoxLogsFolder;
		private System.Windows.Forms.Label labelLogsFolder;
		private System.Windows.Forms.Label labelSettingsFolder;
		private System.Windows.Forms.Button buttonImagesFolder;
		private System.Windows.Forms.Label labelPatcherExePath;
		private System.Windows.Forms.CheckBox checkBoxImagesFolder;
		private System.Windows.Forms.Button buttonPatcherExePath;
		private System.Windows.Forms.Button buttonSettingsFolder;
		private System.Windows.Forms.Label labelImagesFolder;
		private System.Windows.Forms.TabControl tabControlOptions;
		private System.Windows.Forms.LinkLabel linkLabelClientIniFile;
		private System.Windows.Forms.LinkLabel linkLabelAccountIniFile;
		private System.Windows.Forms.Label labelLogDebug;
		private System.Windows.Forms.CheckBox checkBoxLogDebug;
	}
}