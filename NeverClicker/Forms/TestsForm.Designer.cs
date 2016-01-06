namespace NeverClicker.Forms {
	partial class TestsForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestsForm));
			this.tabControlPrimary = new System.Windows.Forms.TabControl();
			this.tabPageStatus = new System.Windows.Forms.TabPage();
			this.textBoxFileName = new System.Windows.Forms.TextBox();
			this.labelFileName = new System.Windows.Forms.Label();
			this.groupBoxSettings = new System.Windows.Forms.GroupBox();
			this.textBoxReadSettingValue3 = new System.Windows.Forms.TextBox();
			this.textBoxReadSettingValue2 = new System.Windows.Forms.TextBox();
			this.labelSettingValue3 = new System.Windows.Forms.Label();
			this.textBoxSettingValue3 = new System.Windows.Forms.TextBox();
			this.labelSettingValue2 = new System.Windows.Forms.Label();
			this.textBoxSettingValue2 = new System.Windows.Forms.TextBox();
			this.textBoxReadSettingValue = new System.Windows.Forms.TextBox();
			this.textBoxSettingName = new System.Windows.Forms.TextBox();
			this.labelSettingValue = new System.Windows.Forms.Label();
			this.buttonSaveSetting = new System.Windows.Forms.Button();
			this.textBoxSettingValue = new System.Windows.Forms.TextBox();
			this.labelSettingName = new System.Windows.Forms.Label();
			this.buttonReadSetting = new System.Windows.Forms.Button();
			this.tabPageWindow = new System.Windows.Forms.TabPage();
			this.buttonClickImage = new System.Windows.Forms.Button();
			this.buttonWindowActivate = new System.Windows.Forms.Button();
			this.buttonWindowKill = new System.Windows.Forms.Button();
			this.buttonWindowMinimize = new System.Windows.Forms.Button();
			this.labelFindImage1 = new System.Windows.Forms.Label();
			this.textBoxFindImage = new System.Windows.Forms.TextBox();
			this.textBoxDetectWindow = new System.Windows.Forms.TextBox();
			this.buttonFindImage = new System.Windows.Forms.Button();
			this.labelDetectWindow = new System.Windows.Forms.Label();
			this.buttonWindowDetect = new System.Windows.Forms.Button();
			this.tabPageQueue = new System.Windows.Forms.TabPage();
			this.labelGameTaskType = new System.Windows.Forms.Label();
			this.comboBoxGameTaskType = new System.Windows.Forms.ComboBox();
			this.labelGameTaskDelaySec = new System.Windows.Forms.Label();
			this.textBoxGameTaskDelaySec = new System.Windows.Forms.TextBox();
			this.textBoxGameTaskCharIdx = new System.Windows.Forms.TextBox();
			this.labelGameTaskCharacterIdx = new System.Windows.Forms.Label();
			this.buttonNextTask = new System.Windows.Forms.Button();
			this.buttonAddCharIdx = new System.Windows.Forms.Button();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.textBoxTestReadValue = new System.Windows.Forms.TextBox();
			this.textBoxTestReadName = new System.Windows.Forms.TextBox();
			this.textBoxTestFileName = new System.Windows.Forms.TextBox();
			this.textBoxTestFileContents = new System.Windows.Forms.TextBox();
			this.textBoxTestStoreName = new System.Windows.Forms.TextBox();
			this.textBoxTestStoreValue = new System.Windows.Forms.TextBox();
			this.buttonTestReadValue = new System.Windows.Forms.Button();
			this.labelTestProperty = new System.Windows.Forms.Label();
			this.labelTestFileName = new System.Windows.Forms.Label();
			this.buttonTestStore = new System.Windows.Forms.Button();
			this.tabPageInput = new System.Windows.Forms.TabPage();
			this.buttonSendKeys = new System.Windows.Forms.Button();
			this.labelSendKeys = new System.Windows.Forms.Label();
			this.textBoxSendKeys = new System.Windows.Forms.TextBox();
			this.buttonMoveMouse = new System.Windows.Forms.Button();
			this.tabPageRaw = new System.Windows.Forms.TabPage();
			this.labelFunctionParensClose = new System.Windows.Forms.Label();
			this.labelFunctionParensOpen = new System.Windows.Forms.Label();
			this.textBoxExecuteStatement = new System.Windows.Forms.TextBox();
			this.textBox_var = new System.Windows.Forms.TextBox();
			this.textBoxExecuteFunction = new System.Windows.Forms.TextBox();
			this.textBoxExecuteFunctionP1 = new System.Windows.Forms.TextBox();
			this.textBoxExecuteFunctionP2 = new System.Windows.Forms.TextBox();
			this.textBoxExecuteFunctionP3 = new System.Windows.Forms.TextBox();
			this.buttonExecuteStatement = new System.Windows.Forms.Button();
			this.labelExecuteStatement = new System.Windows.Forms.Label();
			this.labelFunctionParameters = new System.Windows.Forms.Label();
			this.labelFunctionName = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.labelEvaluateVariable = new System.Windows.Forms.Label();
			this.buttonEvaluateFunction = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.tabControlPrimary.SuspendLayout();
			this.tabPageStatus.SuspendLayout();
			this.groupBoxSettings.SuspendLayout();
			this.tabPageWindow.SuspendLayout();
			this.tabPageQueue.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.tabPageInput.SuspendLayout();
			this.tabPageRaw.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlPrimary
			// 
			this.tabControlPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlPrimary.Controls.Add(this.tabPageStatus);
			this.tabControlPrimary.Controls.Add(this.tabPageWindow);
			this.tabControlPrimary.Controls.Add(this.tabPageQueue);
			this.tabControlPrimary.Controls.Add(this.tabPageInput);
			this.tabControlPrimary.Controls.Add(this.tabPageRaw);
			this.tabControlPrimary.Controls.Add(this.tabPageSettings);
			this.tabControlPrimary.Location = new System.Drawing.Point(9, 9);
			this.tabControlPrimary.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlPrimary.Name = "tabControlPrimary";
			this.tabControlPrimary.Padding = new System.Drawing.Point(0, 0);
			this.tabControlPrimary.SelectedIndex = 0;
			this.tabControlPrimary.Size = new System.Drawing.Size(493, 340);
			this.tabControlPrimary.TabIndex = 18;
			// 
			// tabPageStatus
			// 
			this.tabPageStatus.BackColor = System.Drawing.Color.Transparent;
			this.tabPageStatus.Controls.Add(this.textBoxFileName);
			this.tabPageStatus.Controls.Add(this.labelFileName);
			this.tabPageStatus.Controls.Add(this.groupBoxSettings);
			this.tabPageStatus.Location = new System.Drawing.Point(4, 22);
			this.tabPageStatus.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageStatus.Name = "tabPageStatus";
			this.tabPageStatus.Size = new System.Drawing.Size(485, 314);
			this.tabPageStatus.TabIndex = 4;
			this.tabPageStatus.Text = "Config";
			// 
			// textBoxFileName
			// 
			this.textBoxFileName.Location = new System.Drawing.Point(96, 12);
			this.textBoxFileName.Name = "textBoxFileName";
			this.textBoxFileName.ReadOnly = true;
			this.textBoxFileName.Size = new System.Drawing.Size(383, 20);
			this.textBoxFileName.TabIndex = 7;
			// 
			// labelFileName
			// 
			this.labelFileName.AutoSize = true;
			this.labelFileName.Location = new System.Drawing.Point(13, 12);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(57, 13);
			this.labelFileName.TabIndex = 8;
			this.labelFileName.Text = "File Name:";
			// 
			// groupBoxSettings
			// 
			this.groupBoxSettings.Controls.Add(this.textBoxReadSettingValue3);
			this.groupBoxSettings.Controls.Add(this.textBoxReadSettingValue2);
			this.groupBoxSettings.Controls.Add(this.labelSettingValue3);
			this.groupBoxSettings.Controls.Add(this.textBoxSettingValue3);
			this.groupBoxSettings.Controls.Add(this.labelSettingValue2);
			this.groupBoxSettings.Controls.Add(this.textBoxSettingValue2);
			this.groupBoxSettings.Controls.Add(this.textBoxReadSettingValue);
			this.groupBoxSettings.Controls.Add(this.textBoxSettingName);
			this.groupBoxSettings.Controls.Add(this.labelSettingValue);
			this.groupBoxSettings.Controls.Add(this.buttonSaveSetting);
			this.groupBoxSettings.Controls.Add(this.textBoxSettingValue);
			this.groupBoxSettings.Controls.Add(this.labelSettingName);
			this.groupBoxSettings.Controls.Add(this.buttonReadSetting);
			this.groupBoxSettings.Location = new System.Drawing.Point(9, 51);
			this.groupBoxSettings.Name = "groupBoxSettings";
			this.groupBoxSettings.Size = new System.Drawing.Size(470, 239);
			this.groupBoxSettings.TabIndex = 6;
			this.groupBoxSettings.TabStop = false;
			this.groupBoxSettings.Text = "Settings";
			// 
			// textBoxReadSettingValue3
			// 
			this.textBoxReadSettingValue3.Location = new System.Drawing.Point(224, 101);
			this.textBoxReadSettingValue3.Name = "textBoxReadSettingValue3";
			this.textBoxReadSettingValue3.ReadOnly = true;
			this.textBoxReadSettingValue3.Size = new System.Drawing.Size(110, 20);
			this.textBoxReadSettingValue3.TabIndex = 12;
			// 
			// textBoxReadSettingValue2
			// 
			this.textBoxReadSettingValue2.Location = new System.Drawing.Point(224, 75);
			this.textBoxReadSettingValue2.Name = "textBoxReadSettingValue2";
			this.textBoxReadSettingValue2.ReadOnly = true;
			this.textBoxReadSettingValue2.Size = new System.Drawing.Size(110, 20);
			this.textBoxReadSettingValue2.TabIndex = 11;
			// 
			// labelSettingValue3
			// 
			this.labelSettingValue3.AutoSize = true;
			this.labelSettingValue3.Location = new System.Drawing.Point(49, 104);
			this.labelSettingValue3.Name = "labelSettingValue3";
			this.labelSettingValue3.Size = new System.Drawing.Size(46, 13);
			this.labelSettingValue3.TabIndex = 10;
			this.labelSettingValue3.Text = "Value 3:";
			// 
			// textBoxSettingValue3
			// 
			this.textBoxSettingValue3.Location = new System.Drawing.Point(110, 101);
			this.textBoxSettingValue3.Name = "textBoxSettingValue3";
			this.textBoxSettingValue3.Size = new System.Drawing.Size(108, 20);
			this.textBoxSettingValue3.TabIndex = 9;
			// 
			// labelSettingValue2
			// 
			this.labelSettingValue2.AutoSize = true;
			this.labelSettingValue2.Location = new System.Drawing.Point(49, 78);
			this.labelSettingValue2.Name = "labelSettingValue2";
			this.labelSettingValue2.Size = new System.Drawing.Size(46, 13);
			this.labelSettingValue2.TabIndex = 8;
			this.labelSettingValue2.Text = "Value 2:";
			// 
			// textBoxSettingValue2
			// 
			this.textBoxSettingValue2.Location = new System.Drawing.Point(110, 75);
			this.textBoxSettingValue2.Name = "textBoxSettingValue2";
			this.textBoxSettingValue2.Size = new System.Drawing.Size(108, 20);
			this.textBoxSettingValue2.TabIndex = 7;
			// 
			// textBoxReadSettingValue
			// 
			this.textBoxReadSettingValue.Location = new System.Drawing.Point(224, 49);
			this.textBoxReadSettingValue.Name = "textBoxReadSettingValue";
			this.textBoxReadSettingValue.ReadOnly = true;
			this.textBoxReadSettingValue.Size = new System.Drawing.Size(110, 20);
			this.textBoxReadSettingValue.TabIndex = 6;
			// 
			// textBoxSettingName
			// 
			this.textBoxSettingName.Location = new System.Drawing.Point(87, 23);
			this.textBoxSettingName.Name = "textBoxSettingName";
			this.textBoxSettingName.Size = new System.Drawing.Size(212, 20);
			this.textBoxSettingName.TabIndex = 1;
			// 
			// labelSettingValue
			// 
			this.labelSettingValue.AutoSize = true;
			this.labelSettingValue.Location = new System.Drawing.Point(49, 52);
			this.labelSettingValue.Name = "labelSettingValue";
			this.labelSettingValue.Size = new System.Drawing.Size(46, 13);
			this.labelSettingValue.TabIndex = 5;
			this.labelSettingValue.Text = "Value 1:";
			// 
			// buttonSaveSetting
			// 
			this.buttonSaveSetting.Location = new System.Drawing.Point(52, 127);
			this.buttonSaveSetting.Name = "buttonSaveSetting";
			this.buttonSaveSetting.Size = new System.Drawing.Size(75, 23);
			this.buttonSaveSetting.TabIndex = 0;
			this.buttonSaveSetting.Text = "Save";
			this.buttonSaveSetting.UseVisualStyleBackColor = true;
			this.buttonSaveSetting.Click += new System.EventHandler(this.buttonSaveSetting_Click);
			// 
			// textBoxSettingValue
			// 
			this.textBoxSettingValue.Location = new System.Drawing.Point(110, 49);
			this.textBoxSettingValue.Name = "textBoxSettingValue";
			this.textBoxSettingValue.Size = new System.Drawing.Size(108, 20);
			this.textBoxSettingValue.TabIndex = 4;
			// 
			// labelSettingName
			// 
			this.labelSettingName.AutoSize = true;
			this.labelSettingName.Location = new System.Drawing.Point(26, 26);
			this.labelSettingName.Name = "labelSettingName";
			this.labelSettingName.Size = new System.Drawing.Size(38, 13);
			this.labelSettingName.TabIndex = 2;
			this.labelSettingName.Text = "Name:";
			// 
			// buttonReadSetting
			// 
			this.buttonReadSetting.Location = new System.Drawing.Point(305, 21);
			this.buttonReadSetting.Name = "buttonReadSetting";
			this.buttonReadSetting.Size = new System.Drawing.Size(75, 23);
			this.buttonReadSetting.TabIndex = 3;
			this.buttonReadSetting.Text = "Read";
			this.buttonReadSetting.UseVisualStyleBackColor = true;
			this.buttonReadSetting.Click += new System.EventHandler(this.buttonReadSetting_Click);
			// 
			// tabPageWindow
			// 
			this.tabPageWindow.BackColor = System.Drawing.Color.Transparent;
			this.tabPageWindow.Controls.Add(this.buttonClickImage);
			this.tabPageWindow.Controls.Add(this.buttonWindowActivate);
			this.tabPageWindow.Controls.Add(this.buttonWindowKill);
			this.tabPageWindow.Controls.Add(this.buttonWindowMinimize);
			this.tabPageWindow.Controls.Add(this.labelFindImage1);
			this.tabPageWindow.Controls.Add(this.textBoxFindImage);
			this.tabPageWindow.Controls.Add(this.textBoxDetectWindow);
			this.tabPageWindow.Controls.Add(this.buttonFindImage);
			this.tabPageWindow.Controls.Add(this.labelDetectWindow);
			this.tabPageWindow.Controls.Add(this.buttonWindowDetect);
			this.tabPageWindow.Location = new System.Drawing.Point(4, 22);
			this.tabPageWindow.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageWindow.Name = "tabPageWindow";
			this.tabPageWindow.Size = new System.Drawing.Size(485, 314);
			this.tabPageWindow.TabIndex = 1;
			this.tabPageWindow.Text = "Window";
			// 
			// buttonClickImage
			// 
			this.buttonClickImage.Location = new System.Drawing.Point(398, 11);
			this.buttonClickImage.Name = "buttonClickImage";
			this.buttonClickImage.Size = new System.Drawing.Size(75, 23);
			this.buttonClickImage.TabIndex = 10;
			this.buttonClickImage.Text = "Click";
			this.buttonClickImage.UseVisualStyleBackColor = true;
			this.buttonClickImage.Click += new System.EventHandler(this.buttonClickImage_Click);
			// 
			// buttonWindowActivate
			// 
			this.buttonWindowActivate.Location = new System.Drawing.Point(398, 104);
			this.buttonWindowActivate.Name = "buttonWindowActivate";
			this.buttonWindowActivate.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowActivate.TabIndex = 9;
			this.buttonWindowActivate.Text = "Activate";
			this.buttonWindowActivate.UseVisualStyleBackColor = true;
			this.buttonWindowActivate.Click += new System.EventHandler(this.buttonWindowActivate_Click);
			// 
			// buttonWindowKill
			// 
			this.buttonWindowKill.Location = new System.Drawing.Point(398, 130);
			this.buttonWindowKill.Name = "buttonWindowKill";
			this.buttonWindowKill.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowKill.TabIndex = 8;
			this.buttonWindowKill.Text = "Kill";
			this.buttonWindowKill.UseVisualStyleBackColor = true;
			this.buttonWindowKill.Click += new System.EventHandler(this.buttonWindowKill_Click);
			// 
			// buttonWindowMinimize
			// 
			this.buttonWindowMinimize.Location = new System.Drawing.Point(317, 130);
			this.buttonWindowMinimize.Name = "buttonWindowMinimize";
			this.buttonWindowMinimize.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowMinimize.TabIndex = 7;
			this.buttonWindowMinimize.Text = "Minimize";
			this.buttonWindowMinimize.UseVisualStyleBackColor = true;
			this.buttonWindowMinimize.Click += new System.EventHandler(this.buttonWindowMinimize_Click);
			// 
			// labelFindImage1
			// 
			this.labelFindImage1.AutoSize = true;
			this.labelFindImage1.Location = new System.Drawing.Point(9, 13);
			this.labelFindImage1.Name = "labelFindImage1";
			this.labelFindImage1.Size = new System.Drawing.Size(67, 13);
			this.labelFindImage1.TabIndex = 6;
			this.labelFindImage1.Text = "Image Code:";
			// 
			// textBoxFindImage
			// 
			this.textBoxFindImage.Location = new System.Drawing.Point(104, 13);
			this.textBoxFindImage.Name = "textBoxFindImage";
			this.textBoxFindImage.Size = new System.Drawing.Size(207, 20);
			this.textBoxFindImage.TabIndex = 5;
			// 
			// textBoxDetectWindow
			// 
			this.textBoxDetectWindow.Location = new System.Drawing.Point(104, 106);
			this.textBoxDetectWindow.Name = "textBoxDetectWindow";
			this.textBoxDetectWindow.Size = new System.Drawing.Size(207, 20);
			this.textBoxDetectWindow.TabIndex = 2;
			this.textBoxDetectWindow.TextChanged += new System.EventHandler(this.textBoxDetectWindow_TextChanged);
			// 
			// buttonFindImage
			// 
			this.buttonFindImage.Location = new System.Drawing.Point(317, 11);
			this.buttonFindImage.Name = "buttonFindImage";
			this.buttonFindImage.Size = new System.Drawing.Size(75, 23);
			this.buttonFindImage.TabIndex = 4;
			this.buttonFindImage.Text = "Find";
			this.buttonFindImage.UseVisualStyleBackColor = true;
			this.buttonFindImage.Click += new System.EventHandler(this.buttonFindImage_Click);
			// 
			// labelDetectWindow
			// 
			this.labelDetectWindow.AutoSize = true;
			this.labelDetectWindow.Location = new System.Drawing.Point(9, 106);
			this.labelDetectWindow.Name = "labelDetectWindow";
			this.labelDetectWindow.Size = new System.Drawing.Size(73, 13);
			this.labelDetectWindow.TabIndex = 1;
			this.labelDetectWindow.Text = "Window EXE:";
			// 
			// buttonWindowDetect
			// 
			this.buttonWindowDetect.Location = new System.Drawing.Point(317, 104);
			this.buttonWindowDetect.Name = "buttonWindowDetect";
			this.buttonWindowDetect.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowDetect.TabIndex = 19;
			this.buttonWindowDetect.Text = "Detect";
			this.buttonWindowDetect.UseVisualStyleBackColor = true;
			this.buttonWindowDetect.Click += new System.EventHandler(this.buttonWindowDetect_Click);
			// 
			// tabPageQueue
			// 
			this.tabPageQueue.BackColor = System.Drawing.Color.Transparent;
			this.tabPageQueue.Controls.Add(this.labelGameTaskType);
			this.tabPageQueue.Controls.Add(this.comboBoxGameTaskType);
			this.tabPageQueue.Controls.Add(this.labelGameTaskDelaySec);
			this.tabPageQueue.Controls.Add(this.textBoxGameTaskDelaySec);
			this.tabPageQueue.Controls.Add(this.textBoxGameTaskCharIdx);
			this.tabPageQueue.Controls.Add(this.labelGameTaskCharacterIdx);
			this.tabPageQueue.Controls.Add(this.buttonNextTask);
			this.tabPageQueue.Controls.Add(this.buttonAddCharIdx);
			this.tabPageQueue.Location = new System.Drawing.Point(4, 22);
			this.tabPageQueue.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageQueue.Name = "tabPageQueue";
			this.tabPageQueue.Size = new System.Drawing.Size(485, 314);
			this.tabPageQueue.TabIndex = 2;
			this.tabPageQueue.Text = "Queue";
			// 
			// labelGameTaskType
			// 
			this.labelGameTaskType.AutoSize = true;
			this.labelGameTaskType.Location = new System.Drawing.Point(7, 62);
			this.labelGameTaskType.Name = "labelGameTaskType";
			this.labelGameTaskType.Size = new System.Drawing.Size(34, 13);
			this.labelGameTaskType.TabIndex = 7;
			this.labelGameTaskType.Text = "Type:";
			// 
			// comboBoxGameTaskType
			// 
			this.comboBoxGameTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGameTaskType.FormattingEnabled = true;
			this.comboBoxGameTaskType.Location = new System.Drawing.Point(81, 62);
			this.comboBoxGameTaskType.Name = "comboBoxGameTaskType";
			this.comboBoxGameTaskType.Size = new System.Drawing.Size(121, 21);
			this.comboBoxGameTaskType.TabIndex = 6;
			// 
			// labelGameTaskDelaySec
			// 
			this.labelGameTaskDelaySec.AutoSize = true;
			this.labelGameTaskDelaySec.Location = new System.Drawing.Point(7, 36);
			this.labelGameTaskDelaySec.Name = "labelGameTaskDelaySec";
			this.labelGameTaskDelaySec.Size = new System.Drawing.Size(63, 13);
			this.labelGameTaskDelaySec.TabIndex = 5;
			this.labelGameTaskDelaySec.Text = "Delay (sec):";
			// 
			// textBoxGameTaskDelaySec
			// 
			this.textBoxGameTaskDelaySec.Location = new System.Drawing.Point(81, 36);
			this.textBoxGameTaskDelaySec.Name = "textBoxGameTaskDelaySec";
			this.textBoxGameTaskDelaySec.Size = new System.Drawing.Size(121, 20);
			this.textBoxGameTaskDelaySec.TabIndex = 4;
			// 
			// textBoxGameTaskCharIdx
			// 
			this.textBoxGameTaskCharIdx.Location = new System.Drawing.Point(81, 10);
			this.textBoxGameTaskCharIdx.Name = "textBoxGameTaskCharIdx";
			this.textBoxGameTaskCharIdx.Size = new System.Drawing.Size(178, 20);
			this.textBoxGameTaskCharIdx.TabIndex = 0;
			// 
			// labelGameTaskCharacterIdx
			// 
			this.labelGameTaskCharacterIdx.AutoSize = true;
			this.labelGameTaskCharacterIdx.Location = new System.Drawing.Point(7, 10);
			this.labelGameTaskCharacterIdx.Name = "labelGameTaskCharacterIdx";
			this.labelGameTaskCharacterIdx.Size = new System.Drawing.Size(56, 13);
			this.labelGameTaskCharacterIdx.TabIndex = 3;
			this.labelGameTaskCharacterIdx.Text = "Character:";
			// 
			// buttonNextTask
			// 
			this.buttonNextTask.Location = new System.Drawing.Point(184, 94);
			this.buttonNextTask.Name = "buttonNextTask";
			this.buttonNextTask.Size = new System.Drawing.Size(75, 23);
			this.buttonNextTask.TabIndex = 2;
			this.buttonNextTask.Text = "Pop Next Task";
			this.buttonNextTask.UseVisualStyleBackColor = true;
			this.buttonNextTask.Click += new System.EventHandler(this.buttonNextTask_Click);
			// 
			// buttonAddCharIdx
			// 
			this.buttonAddCharIdx.Location = new System.Drawing.Point(7, 94);
			this.buttonAddCharIdx.Name = "buttonAddCharIdx";
			this.buttonAddCharIdx.Size = new System.Drawing.Size(171, 23);
			this.buttonAddCharIdx.TabIndex = 1;
			this.buttonAddCharIdx.Text = "Queue Task";
			this.buttonAddCharIdx.UseVisualStyleBackColor = true;
			this.buttonAddCharIdx.Click += new System.EventHandler(this.buttonAddCharIdx_Click);
			// 
			// tabPageSettings
			// 
			this.tabPageSettings.BackColor = System.Drawing.Color.Transparent;
			this.tabPageSettings.Controls.Add(this.textBoxTestReadValue);
			this.tabPageSettings.Controls.Add(this.textBoxTestReadName);
			this.tabPageSettings.Controls.Add(this.textBoxTestFileName);
			this.tabPageSettings.Controls.Add(this.textBoxTestFileContents);
			this.tabPageSettings.Controls.Add(this.textBoxTestStoreName);
			this.tabPageSettings.Controls.Add(this.textBoxTestStoreValue);
			this.tabPageSettings.Controls.Add(this.buttonTestReadValue);
			this.tabPageSettings.Controls.Add(this.labelTestProperty);
			this.tabPageSettings.Controls.Add(this.labelTestFileName);
			this.tabPageSettings.Controls.Add(this.buttonTestStore);
			this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSettings.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageSettings.Name = "tabPageSettings";
			this.tabPageSettings.Size = new System.Drawing.Size(485, 314);
			this.tabPageSettings.TabIndex = 5;
			this.tabPageSettings.Text = "Settings (disabled)";
			// 
			// textBoxTestReadValue
			// 
			this.textBoxTestReadValue.Location = new System.Drawing.Point(338, 65);
			this.textBoxTestReadValue.Name = "textBoxTestReadValue";
			this.textBoxTestReadValue.Size = new System.Drawing.Size(100, 20);
			this.textBoxTestReadValue.TabIndex = 19;
			// 
			// textBoxTestReadName
			// 
			this.textBoxTestReadName.Location = new System.Drawing.Point(78, 65);
			this.textBoxTestReadName.Name = "textBoxTestReadName";
			this.textBoxTestReadName.Size = new System.Drawing.Size(173, 20);
			this.textBoxTestReadName.TabIndex = 18;
			// 
			// textBoxTestFileName
			// 
			this.textBoxTestFileName.Location = new System.Drawing.Point(78, 11);
			this.textBoxTestFileName.Name = "textBoxTestFileName";
			this.textBoxTestFileName.Size = new System.Drawing.Size(360, 20);
			this.textBoxTestFileName.TabIndex = 14;
			// 
			// textBoxTestFileContents
			// 
			this.textBoxTestFileContents.Location = new System.Drawing.Point(9, 91);
			this.textBoxTestFileContents.Multiline = true;
			this.textBoxTestFileContents.Name = "textBoxTestFileContents";
			this.textBoxTestFileContents.Size = new System.Drawing.Size(429, 87);
			this.textBoxTestFileContents.TabIndex = 13;
			// 
			// textBoxTestStoreName
			// 
			this.textBoxTestStoreName.Location = new System.Drawing.Point(78, 39);
			this.textBoxTestStoreName.Name = "textBoxTestStoreName";
			this.textBoxTestStoreName.Size = new System.Drawing.Size(173, 20);
			this.textBoxTestStoreName.TabIndex = 12;
			// 
			// textBoxTestStoreValue
			// 
			this.textBoxTestStoreValue.Location = new System.Drawing.Point(257, 39);
			this.textBoxTestStoreValue.Name = "textBoxTestStoreValue";
			this.textBoxTestStoreValue.Size = new System.Drawing.Size(100, 20);
			this.textBoxTestStoreValue.TabIndex = 11;
			// 
			// buttonTestReadValue
			// 
			this.buttonTestReadValue.Location = new System.Drawing.Point(257, 63);
			this.buttonTestReadValue.Name = "buttonTestReadValue";
			this.buttonTestReadValue.Size = new System.Drawing.Size(75, 23);
			this.buttonTestReadValue.TabIndex = 17;
			this.buttonTestReadValue.Text = "Read";
			this.buttonTestReadValue.UseVisualStyleBackColor = true;
			// 
			// labelTestProperty
			// 
			this.labelTestProperty.AutoSize = true;
			this.labelTestProperty.Location = new System.Drawing.Point(9, 42);
			this.labelTestProperty.Name = "labelTestProperty";
			this.labelTestProperty.Size = new System.Drawing.Size(49, 13);
			this.labelTestProperty.TabIndex = 16;
			this.labelTestProperty.Text = "Property:";
			// 
			// labelTestFileName
			// 
			this.labelTestFileName.AutoSize = true;
			this.labelTestFileName.Location = new System.Drawing.Point(9, 14);
			this.labelTestFileName.Name = "labelTestFileName";
			this.labelTestFileName.Size = new System.Drawing.Size(52, 13);
			this.labelTestFileName.TabIndex = 15;
			this.labelTestFileName.Text = "Yaml File:";
			// 
			// buttonTestStore
			// 
			this.buttonTestStore.Location = new System.Drawing.Point(363, 37);
			this.buttonTestStore.Name = "buttonTestStore";
			this.buttonTestStore.Size = new System.Drawing.Size(75, 23);
			this.buttonTestStore.TabIndex = 10;
			this.buttonTestStore.Text = "Store";
			this.buttonTestStore.UseVisualStyleBackColor = true;
			// 
			// tabPageInput
			// 
			this.tabPageInput.BackColor = System.Drawing.Color.Transparent;
			this.tabPageInput.Controls.Add(this.buttonSendKeys);
			this.tabPageInput.Controls.Add(this.labelSendKeys);
			this.tabPageInput.Controls.Add(this.textBoxSendKeys);
			this.tabPageInput.Controls.Add(this.buttonMoveMouse);
			this.tabPageInput.Location = new System.Drawing.Point(4, 22);
			this.tabPageInput.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageInput.Name = "tabPageInput";
			this.tabPageInput.Size = new System.Drawing.Size(485, 314);
			this.tabPageInput.TabIndex = 3;
			this.tabPageInput.Text = "Input";
			// 
			// buttonSendKeys
			// 
			this.buttonSendKeys.Location = new System.Drawing.Point(326, 6);
			this.buttonSendKeys.Name = "buttonSendKeys";
			this.buttonSendKeys.Size = new System.Drawing.Size(65, 23);
			this.buttonSendKeys.TabIndex = 8;
			this.buttonSendKeys.Text = "Send";
			this.buttonSendKeys.UseVisualStyleBackColor = true;
			this.buttonSendKeys.Click += new System.EventHandler(this.buttonSendKeys_Click);
			// 
			// labelSendKeys
			// 
			this.labelSendKeys.AutoSize = true;
			this.labelSendKeys.Location = new System.Drawing.Point(3, 8);
			this.labelSendKeys.Name = "labelSendKeys";
			this.labelSendKeys.Size = new System.Drawing.Size(61, 13);
			this.labelSendKeys.TabIndex = 7;
			this.labelSendKeys.Text = "Send Keys:";
			// 
			// textBoxSendKeys
			// 
			this.textBoxSendKeys.Location = new System.Drawing.Point(73, 8);
			this.textBoxSendKeys.Name = "textBoxSendKeys";
			this.textBoxSendKeys.Size = new System.Drawing.Size(247, 20);
			this.textBoxSendKeys.TabIndex = 6;
			// 
			// buttonMoveMouse
			// 
			this.buttonMoveMouse.Location = new System.Drawing.Point(9, 267);
			this.buttonMoveMouse.Name = "buttonMoveMouse";
			this.buttonMoveMouse.Size = new System.Drawing.Size(105, 23);
			this.buttonMoveMouse.TabIndex = 5;
			this.buttonMoveMouse.Text = "Move Mouse";
			this.buttonMoveMouse.UseVisualStyleBackColor = true;
			// 
			// tabPageRaw
			// 
			this.tabPageRaw.BackColor = System.Drawing.Color.Transparent;
			this.tabPageRaw.Controls.Add(this.labelFunctionParensClose);
			this.tabPageRaw.Controls.Add(this.labelFunctionParensOpen);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteStatement);
			this.tabPageRaw.Controls.Add(this.textBox_var);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunction);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunctionP1);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunctionP2);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunctionP3);
			this.tabPageRaw.Controls.Add(this.buttonExecuteStatement);
			this.tabPageRaw.Controls.Add(this.labelExecuteStatement);
			this.tabPageRaw.Controls.Add(this.labelFunctionParameters);
			this.tabPageRaw.Controls.Add(this.labelFunctionName);
			this.tabPageRaw.Controls.Add(this.button3);
			this.tabPageRaw.Controls.Add(this.labelEvaluateVariable);
			this.tabPageRaw.Controls.Add(this.buttonEvaluateFunction);
			this.tabPageRaw.Location = new System.Drawing.Point(4, 22);
			this.tabPageRaw.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageRaw.Name = "tabPageRaw";
			this.tabPageRaw.Size = new System.Drawing.Size(485, 314);
			this.tabPageRaw.TabIndex = 0;
			this.tabPageRaw.Text = "Raw";
			// 
			// labelFunctionParensClose
			// 
			this.labelFunctionParensClose.AutoSize = true;
			this.labelFunctionParensClose.Location = new System.Drawing.Point(244, 64);
			this.labelFunctionParensClose.Name = "labelFunctionParensClose";
			this.labelFunctionParensClose.Size = new System.Drawing.Size(10, 13);
			this.labelFunctionParensClose.TabIndex = 21;
			this.labelFunctionParensClose.Text = ")";
			// 
			// labelFunctionParensOpen
			// 
			this.labelFunctionParensOpen.AutoSize = true;
			this.labelFunctionParensOpen.Location = new System.Drawing.Point(241, 12);
			this.labelFunctionParensOpen.Name = "labelFunctionParensOpen";
			this.labelFunctionParensOpen.Size = new System.Drawing.Size(10, 13);
			this.labelFunctionParensOpen.TabIndex = 20;
			this.labelFunctionParensOpen.Text = "(";
			// 
			// textBoxExecuteStatement
			// 
			this.textBoxExecuteStatement.Location = new System.Drawing.Point(189, 196);
			this.textBoxExecuteStatement.Name = "textBoxExecuteStatement";
			this.textBoxExecuteStatement.Size = new System.Drawing.Size(98, 20);
			this.textBoxExecuteStatement.TabIndex = 18;
			this.textBoxExecuteStatement.Visible = false;
			// 
			// textBox_var
			// 
			this.textBox_var.Location = new System.Drawing.Point(189, 152);
			this.textBox_var.Name = "textBox_var";
			this.textBox_var.Size = new System.Drawing.Size(98, 20);
			this.textBox_var.TabIndex = 4;
			this.textBox_var.Visible = false;
			// 
			// textBoxExecuteFunction
			// 
			this.textBoxExecuteFunction.Location = new System.Drawing.Point(85, 12);
			this.textBoxExecuteFunction.Name = "textBoxExecuteFunction";
			this.textBoxExecuteFunction.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunction.TabIndex = 6;
			// 
			// textBoxExecuteFunctionP1
			// 
			this.textBoxExecuteFunctionP1.Location = new System.Drawing.Point(85, 38);
			this.textBoxExecuteFunctionP1.Name = "textBoxExecuteFunctionP1";
			this.textBoxExecuteFunctionP1.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunctionP1.TabIndex = 8;
			// 
			// textBoxExecuteFunctionP2
			// 
			this.textBoxExecuteFunctionP2.Location = new System.Drawing.Point(247, 38);
			this.textBoxExecuteFunctionP2.Name = "textBoxExecuteFunctionP2";
			this.textBoxExecuteFunctionP2.Size = new System.Drawing.Size(155, 20);
			this.textBoxExecuteFunctionP2.TabIndex = 9;
			// 
			// textBoxExecuteFunctionP3
			// 
			this.textBoxExecuteFunctionP3.Location = new System.Drawing.Point(85, 64);
			this.textBoxExecuteFunctionP3.Name = "textBoxExecuteFunctionP3";
			this.textBoxExecuteFunctionP3.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunctionP3.TabIndex = 10;
			// 
			// buttonExecuteStatement
			// 
			this.buttonExecuteStatement.Location = new System.Drawing.Point(293, 194);
			this.buttonExecuteStatement.Name = "buttonExecuteStatement";
			this.buttonExecuteStatement.Size = new System.Drawing.Size(109, 23);
			this.buttonExecuteStatement.TabIndex = 17;
			this.buttonExecuteStatement.Text = "Execute";
			this.buttonExecuteStatement.UseVisualStyleBackColor = true;
			this.buttonExecuteStatement.Visible = false;
			this.buttonExecuteStatement.Click += new System.EventHandler(this.buttonExecuteStatement_Click);
			// 
			// labelExecuteStatement
			// 
			this.labelExecuteStatement.AutoSize = true;
			this.labelExecuteStatement.Location = new System.Drawing.Point(8, 196);
			this.labelExecuteStatement.Name = "labelExecuteStatement";
			this.labelExecuteStatement.Size = new System.Drawing.Size(136, 13);
			this.labelExecuteStatement.TabIndex = 19;
			this.labelExecuteStatement.Text = "Statement (not hooked up):";
			this.labelExecuteStatement.Visible = false;
			// 
			// labelFunctionParameters
			// 
			this.labelFunctionParameters.AutoSize = true;
			this.labelFunctionParameters.Location = new System.Drawing.Point(8, 38);
			this.labelFunctionParameters.Name = "labelFunctionParameters";
			this.labelFunctionParameters.Size = new System.Drawing.Size(63, 13);
			this.labelFunctionParameters.TabIndex = 16;
			this.labelFunctionParameters.Text = "Parameters:";
			// 
			// labelFunctionName
			// 
			this.labelFunctionName.AutoSize = true;
			this.labelFunctionName.Location = new System.Drawing.Point(8, 12);
			this.labelFunctionName.Name = "labelFunctionName";
			this.labelFunctionName.Size = new System.Drawing.Size(51, 13);
			this.labelFunctionName.TabIndex = 15;
			this.labelFunctionName.Text = "Function:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(293, 150);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(109, 23);
			this.button3.TabIndex = 3;
			this.button3.Text = "Evaluate";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Visible = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// labelEvaluateVariable
			// 
			this.labelEvaluateVariable.AutoSize = true;
			this.labelEvaluateVariable.Location = new System.Drawing.Point(8, 152);
			this.labelEvaluateVariable.Name = "labelEvaluateVariable";
			this.labelEvaluateVariable.Size = new System.Drawing.Size(157, 13);
			this.labelEvaluateVariable.TabIndex = 14;
			this.labelEvaluateVariable.Text = "Variable Name (not hooked up):";
			this.labelEvaluateVariable.Visible = false;
			// 
			// buttonEvaluateFunction
			// 
			this.buttonEvaluateFunction.Location = new System.Drawing.Point(293, 64);
			this.buttonEvaluateFunction.Name = "buttonEvaluateFunction";
			this.buttonEvaluateFunction.Size = new System.Drawing.Size(109, 23);
			this.buttonEvaluateFunction.TabIndex = 7;
			this.buttonEvaluateFunction.Text = "Evaluate";
			this.buttonEvaluateFunction.UseVisualStyleBackColor = true;
			this.buttonEvaluateFunction.Click += new System.EventHandler(this.buttonExecuteFunction_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(424, 362);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// TestsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(511, 397);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.tabControlPrimary);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TestsForm";
			this.Text = "Tests";
			this.Load += new System.EventHandler(this.Tests_Load);
			this.tabControlPrimary.ResumeLayout(false);
			this.tabPageStatus.ResumeLayout(false);
			this.tabPageStatus.PerformLayout();
			this.groupBoxSettings.ResumeLayout(false);
			this.groupBoxSettings.PerformLayout();
			this.tabPageWindow.ResumeLayout(false);
			this.tabPageWindow.PerformLayout();
			this.tabPageQueue.ResumeLayout(false);
			this.tabPageQueue.PerformLayout();
			this.tabPageSettings.ResumeLayout(false);
			this.tabPageSettings.PerformLayout();
			this.tabPageInput.ResumeLayout(false);
			this.tabPageInput.PerformLayout();
			this.tabPageRaw.ResumeLayout(false);
			this.tabPageRaw.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControlPrimary;
		private System.Windows.Forms.TabPage tabPageStatus;
		private System.Windows.Forms.TabPage tabPageRaw;
		private System.Windows.Forms.Label labelFunctionParensClose;
		private System.Windows.Forms.Label labelFunctionParensOpen;
		private System.Windows.Forms.TextBox textBoxExecuteStatement;
		private System.Windows.Forms.TextBox textBox_var;
		private System.Windows.Forms.TextBox textBoxExecuteFunction;
		private System.Windows.Forms.TextBox textBoxExecuteFunctionP1;
		private System.Windows.Forms.TextBox textBoxExecuteFunctionP2;
		private System.Windows.Forms.TextBox textBoxExecuteFunctionP3;
		private System.Windows.Forms.Button buttonExecuteStatement;
		private System.Windows.Forms.Label labelExecuteStatement;
		private System.Windows.Forms.Label labelFunctionParameters;
		private System.Windows.Forms.Label labelFunctionName;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label labelEvaluateVariable;
		private System.Windows.Forms.Button buttonEvaluateFunction;
		private System.Windows.Forms.TabPage tabPageWindow;
		private System.Windows.Forms.Button buttonWindowActivate;
		private System.Windows.Forms.Button buttonWindowKill;
		private System.Windows.Forms.Button buttonWindowMinimize;
		private System.Windows.Forms.Label labelFindImage1;
		private System.Windows.Forms.TextBox textBoxFindImage;
		private System.Windows.Forms.TextBox textBoxDetectWindow;
		private System.Windows.Forms.Button buttonFindImage;
		private System.Windows.Forms.Label labelDetectWindow;
		private System.Windows.Forms.Button buttonWindowDetect;
		private System.Windows.Forms.TabPage tabPageQueue;
		private System.Windows.Forms.Label labelGameTaskType;
		private System.Windows.Forms.ComboBox comboBoxGameTaskType;
		private System.Windows.Forms.Label labelGameTaskDelaySec;
		private System.Windows.Forms.TextBox textBoxGameTaskDelaySec;
		private System.Windows.Forms.TextBox textBoxGameTaskCharIdx;
		private System.Windows.Forms.Label labelGameTaskCharacterIdx;
		private System.Windows.Forms.Button buttonNextTask;
		private System.Windows.Forms.Button buttonAddCharIdx;
		private System.Windows.Forms.TabPage tabPageInput;
		private System.Windows.Forms.Button buttonSendKeys;
		private System.Windows.Forms.Label labelSendKeys;
		private System.Windows.Forms.TextBox textBoxSendKeys;
		private System.Windows.Forms.Button buttonMoveMouse;
		private System.Windows.Forms.Button buttonClickImage;
		private System.Windows.Forms.Button buttonSaveSetting;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.GroupBox groupBoxSettings;
		private System.Windows.Forms.TextBox textBoxSettingName;
		private System.Windows.Forms.Label labelSettingValue;
		private System.Windows.Forms.TextBox textBoxSettingValue;
		private System.Windows.Forms.Label labelSettingName;
		private System.Windows.Forms.Button buttonReadSetting;
		private System.Windows.Forms.TextBox textBoxFileName;
		private System.Windows.Forms.Label labelFileName;
		private System.Windows.Forms.Label labelSettingValue3;
		private System.Windows.Forms.TextBox textBoxSettingValue3;
		private System.Windows.Forms.Label labelSettingValue2;
		private System.Windows.Forms.TextBox textBoxSettingValue2;
		private System.Windows.Forms.TextBox textBoxReadSettingValue3;
		private System.Windows.Forms.TextBox textBoxReadSettingValue2;
		private System.Windows.Forms.TextBox textBoxReadSettingValue;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.TextBox textBoxTestReadValue;
		private System.Windows.Forms.TextBox textBoxTestReadName;
		private System.Windows.Forms.TextBox textBoxTestFileName;
		private System.Windows.Forms.TextBox textBoxTestFileContents;
		private System.Windows.Forms.TextBox textBoxTestStoreName;
		private System.Windows.Forms.TextBox textBoxTestStoreValue;
		private System.Windows.Forms.Button buttonTestReadValue;
		private System.Windows.Forms.Label labelTestProperty;
		private System.Windows.Forms.Label labelTestFileName;
		private System.Windows.Forms.Button buttonTestStore;
	}
}