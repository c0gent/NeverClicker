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
			this.labelSettingsFolder = new System.Windows.Forms.Label();
			this.textBoxSettingsFolder = new System.Windows.Forms.TextBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxPatcherExePath = new System.Windows.Forms.TextBox();
			this.labelPatcherExePath = new System.Windows.Forms.Label();
			this.buttonSettingsFolder = new System.Windows.Forms.Button();
			this.buttonPatcherExePath = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.buttonImagesFolder = new System.Windows.Forms.Button();
			this.textBoxImagesFolder = new System.Windows.Forms.TextBox();
			this.labelImagesFolder = new System.Windows.Forms.Label();
			this.checkBoxImagesFolder = new System.Windows.Forms.CheckBox();
			this.tabControlOptions = new System.Windows.Forms.TabControl();
			this.tabPagePaths = new System.Windows.Forms.TabPage();
			this.textBoxImageShadeVariation = new System.Windows.Forms.TextBox();
			this.labelImageShadeVariation = new System.Windows.Forms.Label();
			this.linkLabelUserConfigFile = new System.Windows.Forms.LinkLabel();
			this.checkBoxUserRootFolder = new System.Windows.Forms.CheckBox();
			this.textBoxUserRootFolder = new System.Windows.Forms.TextBox();
			this.labelUserRootFolder = new System.Windows.Forms.Label();
			this.buttonUserRootFolder = new System.Windows.Forms.Button();
			this.checkBoxSettingsFolder = new System.Windows.Forms.CheckBox();
			this.buttonLogsFolder = new System.Windows.Forms.Button();
			this.checkBoxLogsFolder = new System.Windows.Forms.CheckBox();
			this.textBoxLogsFolder = new System.Windows.Forms.TextBox();
			this.labelLogsFolder = new System.Windows.Forms.Label();
			this.tabPageClient = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.Account = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPageTesting = new System.Windows.Forms.TabPage();
			this.textBoxTestReadValue = new System.Windows.Forms.TextBox();
			this.textBoxTestReadName = new System.Windows.Forms.TextBox();
			this.buttonTestReadValue = new System.Windows.Forms.Button();
			this.labelTestProperty = new System.Windows.Forms.Label();
			this.labelTestFileName = new System.Windows.Forms.Label();
			this.textBoxTestFileName = new System.Windows.Forms.TextBox();
			this.textBoxTestFileContents = new System.Windows.Forms.TextBox();
			this.textBoxTestStoreName = new System.Windows.Forms.TextBox();
			this.textBoxTestStoreValue = new System.Windows.Forms.TextBox();
			this.buttonTestStore = new System.Windows.Forms.Button();
			this.tabControlOptions.SuspendLayout();
			this.tabPagePaths.SuspendLayout();
			this.tabPageClient.SuspendLayout();
			this.Account.SuspendLayout();
			this.tabPageTesting.SuspendLayout();
			this.SuspendLayout();
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
			// textBoxSettingsFolder
			// 
			this.textBoxSettingsFolder.Location = new System.Drawing.Point(191, 66);
			this.textBoxSettingsFolder.Name = "textBoxSettingsFolder";
			this.textBoxSettingsFolder.ReadOnly = true;
			this.textBoxSettingsFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxSettingsFolder.TabIndex = 6;
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
			// textBoxPatcherExePath
			// 
			this.textBoxPatcherExePath.Location = new System.Drawing.Point(191, 8);
			this.textBoxPatcherExePath.Name = "textBoxPatcherExePath";
			this.textBoxPatcherExePath.Size = new System.Drawing.Size(556, 20);
			this.textBoxPatcherExePath.TabIndex = 3;
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
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
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
			// textBoxImagesFolder
			// 
			this.textBoxImagesFolder.Location = new System.Drawing.Point(191, 95);
			this.textBoxImagesFolder.Name = "textBoxImagesFolder";
			this.textBoxImagesFolder.ReadOnly = true;
			this.textBoxImagesFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxImagesFolder.TabIndex = 9;
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
			// checkBoxImagesFolder
			// 
			this.checkBoxImagesFolder.AutoSize = true;
			this.checkBoxImagesFolder.Checked = true;
			this.checkBoxImagesFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxImagesFolder.Enabled = false;
			this.checkBoxImagesFolder.Location = new System.Drawing.Point(125, 97);
			this.checkBoxImagesFolder.Name = "checkBoxImagesFolder";
			this.checkBoxImagesFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxImagesFolder.TabIndex = 8;
			this.checkBoxImagesFolder.Text = "Default";
			this.checkBoxImagesFolder.UseVisualStyleBackColor = true;
			// 
			// tabControlOptions
			// 
			this.tabControlOptions.Controls.Add(this.tabPagePaths);
			this.tabControlOptions.Controls.Add(this.tabPageClient);
			this.tabControlOptions.Controls.Add(this.Account);
			this.tabControlOptions.Controls.Add(this.tabPageTesting);
			this.tabControlOptions.Location = new System.Drawing.Point(12, 12);
			this.tabControlOptions.Name = "tabControlOptions";
			this.tabControlOptions.SelectedIndex = 0;
			this.tabControlOptions.Size = new System.Drawing.Size(835, 485);
			this.tabControlOptions.TabIndex = 22;
			this.tabControlOptions.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlOptions_Selected);
			// 
			// tabPagePaths
			// 
			this.tabPagePaths.BackColor = System.Drawing.Color.Transparent;
			this.tabPagePaths.Controls.Add(this.textBoxImageShadeVariation);
			this.tabPagePaths.Controls.Add(this.labelImageShadeVariation);
			this.tabPagePaths.Controls.Add(this.linkLabelUserConfigFile);
			this.tabPagePaths.Controls.Add(this.checkBoxUserRootFolder);
			this.tabPagePaths.Controls.Add(this.textBoxUserRootFolder);
			this.tabPagePaths.Controls.Add(this.labelUserRootFolder);
			this.tabPagePaths.Controls.Add(this.buttonUserRootFolder);
			this.tabPagePaths.Controls.Add(this.checkBoxSettingsFolder);
			this.tabPagePaths.Controls.Add(this.buttonLogsFolder);
			this.tabPagePaths.Controls.Add(this.checkBoxLogsFolder);
			this.tabPagePaths.Controls.Add(this.textBoxLogsFolder);
			this.tabPagePaths.Controls.Add(this.labelLogsFolder);
			this.tabPagePaths.Controls.Add(this.textBoxSettingsFolder);
			this.tabPagePaths.Controls.Add(this.labelSettingsFolder);
			this.tabPagePaths.Controls.Add(this.buttonImagesFolder);
			this.tabPagePaths.Controls.Add(this.labelPatcherExePath);
			this.tabPagePaths.Controls.Add(this.checkBoxImagesFolder);
			this.tabPagePaths.Controls.Add(this.buttonPatcherExePath);
			this.tabPagePaths.Controls.Add(this.textBoxPatcherExePath);
			this.tabPagePaths.Controls.Add(this.buttonSettingsFolder);
			this.tabPagePaths.Controls.Add(this.labelImagesFolder);
			this.tabPagePaths.Controls.Add(this.textBoxImagesFolder);
			this.tabPagePaths.Location = new System.Drawing.Point(4, 22);
			this.tabPagePaths.Name = "tabPagePaths";
			this.tabPagePaths.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePaths.Size = new System.Drawing.Size(827, 459);
			this.tabPagePaths.TabIndex = 0;
			this.tabPagePaths.Text = "Paths";
			// 
			// textBoxImageShadeVariation
			// 
			this.textBoxImageShadeVariation.Location = new System.Drawing.Point(191, 183);
			this.textBoxImageShadeVariation.Name = "textBoxImageShadeVariation";
			this.textBoxImageShadeVariation.Size = new System.Drawing.Size(93, 20);
			this.textBoxImageShadeVariation.TabIndex = 29;
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
			this.checkBoxUserRootFolder.Checked = true;
			this.checkBoxUserRootFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxUserRootFolder.Location = new System.Drawing.Point(125, 39);
			this.checkBoxUserRootFolder.Name = "checkBoxUserRootFolder";
			this.checkBoxUserRootFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxUserRootFolder.TabIndex = 24;
			this.checkBoxUserRootFolder.Text = "Default";
			this.checkBoxUserRootFolder.UseVisualStyleBackColor = true;
			this.checkBoxUserRootFolder.CheckedChanged += new System.EventHandler(this.checkBoxUserRootFolder_CheckedChanged);
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
			this.checkBoxSettingsFolder.Checked = true;
			this.checkBoxSettingsFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxSettingsFolder.Enabled = false;
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
			this.checkBoxLogsFolder.Checked = true;
			this.checkBoxLogsFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxLogsFolder.Enabled = false;
			this.checkBoxLogsFolder.Location = new System.Drawing.Point(125, 126);
			this.checkBoxLogsFolder.Name = "checkBoxLogsFolder";
			this.checkBoxLogsFolder.Size = new System.Drawing.Size(60, 17);
			this.checkBoxLogsFolder.TabIndex = 11;
			this.checkBoxLogsFolder.Text = "Default";
			this.checkBoxLogsFolder.UseVisualStyleBackColor = true;
			// 
			// textBoxLogsFolder
			// 
			this.textBoxLogsFolder.Location = new System.Drawing.Point(191, 124);
			this.textBoxLogsFolder.Name = "textBoxLogsFolder";
			this.textBoxLogsFolder.ReadOnly = true;
			this.textBoxLogsFolder.Size = new System.Drawing.Size(556, 20);
			this.textBoxLogsFolder.TabIndex = 12;
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
			// tabPageTesting
			// 
			this.tabPageTesting.BackColor = System.Drawing.Color.Transparent;
			this.tabPageTesting.Controls.Add(this.textBoxTestReadValue);
			this.tabPageTesting.Controls.Add(this.textBoxTestReadName);
			this.tabPageTesting.Controls.Add(this.buttonTestReadValue);
			this.tabPageTesting.Controls.Add(this.labelTestProperty);
			this.tabPageTesting.Controls.Add(this.labelTestFileName);
			this.tabPageTesting.Controls.Add(this.textBoxTestFileName);
			this.tabPageTesting.Controls.Add(this.textBoxTestFileContents);
			this.tabPageTesting.Controls.Add(this.textBoxTestStoreName);
			this.tabPageTesting.Controls.Add(this.textBoxTestStoreValue);
			this.tabPageTesting.Controls.Add(this.buttonTestStore);
			this.tabPageTesting.Location = new System.Drawing.Point(4, 22);
			this.tabPageTesting.Name = "tabPageTesting";
			this.tabPageTesting.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTesting.Size = new System.Drawing.Size(827, 459);
			this.tabPageTesting.TabIndex = 3;
			this.tabPageTesting.Text = "Testing";
			// 
			// textBoxTestReadValue
			// 
			this.textBoxTestReadValue.Location = new System.Drawing.Point(335, 201);
			this.textBoxTestReadValue.Name = "textBoxTestReadValue";
			this.textBoxTestReadValue.Size = new System.Drawing.Size(100, 20);
			this.textBoxTestReadValue.TabIndex = 9;
			// 
			// textBoxTestReadName
			// 
			this.textBoxTestReadName.Location = new System.Drawing.Point(75, 201);
			this.textBoxTestReadName.Name = "textBoxTestReadName";
			this.textBoxTestReadName.Size = new System.Drawing.Size(173, 20);
			this.textBoxTestReadName.TabIndex = 8;
			// 
			// buttonTestReadValue
			// 
			this.buttonTestReadValue.Location = new System.Drawing.Point(254, 199);
			this.buttonTestReadValue.Name = "buttonTestReadValue";
			this.buttonTestReadValue.Size = new System.Drawing.Size(75, 23);
			this.buttonTestReadValue.TabIndex = 7;
			this.buttonTestReadValue.Text = "Read";
			this.buttonTestReadValue.UseVisualStyleBackColor = true;
			// 
			// labelTestProperty
			// 
			this.labelTestProperty.AutoSize = true;
			this.labelTestProperty.Location = new System.Drawing.Point(6, 178);
			this.labelTestProperty.Name = "labelTestProperty";
			this.labelTestProperty.Size = new System.Drawing.Size(49, 13);
			this.labelTestProperty.TabIndex = 6;
			this.labelTestProperty.Text = "Property:";
			// 
			// labelTestFileName
			// 
			this.labelTestFileName.AutoSize = true;
			this.labelTestFileName.Location = new System.Drawing.Point(6, 150);
			this.labelTestFileName.Name = "labelTestFileName";
			this.labelTestFileName.Size = new System.Drawing.Size(52, 13);
			this.labelTestFileName.TabIndex = 5;
			this.labelTestFileName.Text = "Yaml File:";
			// 
			// textBoxTestFileName
			// 
			this.textBoxTestFileName.Location = new System.Drawing.Point(75, 147);
			this.textBoxTestFileName.Name = "textBoxTestFileName";
			this.textBoxTestFileName.Size = new System.Drawing.Size(360, 20);
			this.textBoxTestFileName.TabIndex = 4;
			// 
			// textBoxTestFileContents
			// 
			this.textBoxTestFileContents.Location = new System.Drawing.Point(6, 227);
			this.textBoxTestFileContents.Multiline = true;
			this.textBoxTestFileContents.Name = "textBoxTestFileContents";
			this.textBoxTestFileContents.Size = new System.Drawing.Size(815, 226);
			this.textBoxTestFileContents.TabIndex = 3;
			// 
			// textBoxTestStoreName
			// 
			this.textBoxTestStoreName.Location = new System.Drawing.Point(75, 175);
			this.textBoxTestStoreName.Name = "textBoxTestStoreName";
			this.textBoxTestStoreName.Size = new System.Drawing.Size(173, 20);
			this.textBoxTestStoreName.TabIndex = 2;
			// 
			// textBoxTestStoreValue
			// 
			this.textBoxTestStoreValue.Location = new System.Drawing.Point(254, 175);
			this.textBoxTestStoreValue.Name = "textBoxTestStoreValue";
			this.textBoxTestStoreValue.Size = new System.Drawing.Size(100, 20);
			this.textBoxTestStoreValue.TabIndex = 1;
			// 
			// buttonTestStore
			// 
			this.buttonTestStore.Location = new System.Drawing.Point(360, 173);
			this.buttonTestStore.Name = "buttonTestStore";
			this.buttonTestStore.Size = new System.Drawing.Size(75, 23);
			this.buttonTestStore.TabIndex = 0;
			this.buttonTestStore.Text = "Store";
			this.buttonTestStore.UseVisualStyleBackColor = true;
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
			this.Load += new System.EventHandler(this.Options_Load);
			this.tabControlOptions.ResumeLayout(false);
			this.tabPagePaths.ResumeLayout(false);
			this.tabPagePaths.PerformLayout();
			this.tabPageClient.ResumeLayout(false);
			this.tabPageClient.PerformLayout();
			this.Account.ResumeLayout(false);
			this.Account.PerformLayout();
			this.tabPageTesting.ResumeLayout(false);
			this.tabPageTesting.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelSettingsFolder;
        private System.Windows.Forms.TextBox textBoxSettingsFolder;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxPatcherExePath;
        private System.Windows.Forms.Label labelPatcherExePath;
        private System.Windows.Forms.Button buttonSettingsFolder;
        private System.Windows.Forms.Button buttonPatcherExePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button buttonImagesFolder;
		private System.Windows.Forms.TextBox textBoxImagesFolder;
		private System.Windows.Forms.Label labelImagesFolder;
		private System.Windows.Forms.CheckBox checkBoxImagesFolder;
		private System.Windows.Forms.TabControl tabControlOptions;
		private System.Windows.Forms.TabPage tabPagePaths;
		private System.Windows.Forms.TabPage tabPageClient;
		private System.Windows.Forms.TabPage Account;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonLogsFolder;
		private System.Windows.Forms.CheckBox checkBoxLogsFolder;
		private System.Windows.Forms.TextBox textBoxLogsFolder;
		private System.Windows.Forms.Label labelLogsFolder;
		private System.Windows.Forms.CheckBox checkBoxSettingsFolder;
		private System.Windows.Forms.CheckBox checkBoxUserRootFolder;
		private System.Windows.Forms.TextBox textBoxUserRootFolder;
		private System.Windows.Forms.Label labelUserRootFolder;
		private System.Windows.Forms.Button buttonUserRootFolder;
		private System.Windows.Forms.LinkLabel linkLabelUserConfigFile;
		private System.Windows.Forms.TextBox textBoxImageShadeVariation;
		private System.Windows.Forms.Label labelImageShadeVariation;
		private System.Windows.Forms.TabPage tabPageTesting;
		private System.Windows.Forms.TextBox textBoxTestFileContents;
		private System.Windows.Forms.TextBox textBoxTestStoreName;
		private System.Windows.Forms.TextBox textBoxTestStoreValue;
		private System.Windows.Forms.Button buttonTestStore;
		private System.Windows.Forms.Label labelTestProperty;
		private System.Windows.Forms.Label labelTestFileName;
		private System.Windows.Forms.TextBox textBoxTestFileName;
		private System.Windows.Forms.TextBox textBoxTestReadValue;
		private System.Windows.Forms.TextBox textBoxTestReadName;
		private System.Windows.Forms.Button buttonTestReadValue;
	}
}