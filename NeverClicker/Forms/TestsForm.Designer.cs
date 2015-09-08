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
			this.buttonExecuteFunction = new System.Windows.Forms.Button();
			this.tabPageWindow = new System.Windows.Forms.TabPage();
			this.buttonWindowActivate = new System.Windows.Forms.Button();
			this.buttonWindowKill = new System.Windows.Forms.Button();
			this.buttonWindowMinimize = new System.Windows.Forms.Button();
			this.labelFindImage1 = new System.Windows.Forms.Label();
			this.textBoxFindImage = new System.Windows.Forms.TextBox();
			this.textBoxDetectWindow = new System.Windows.Forms.TextBox();
			this.buttonFindImage = new System.Windows.Forms.Button();
			this.labelFindImage2 = new System.Windows.Forms.Label();
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
			this.tabPageOther = new System.Windows.Forms.TabPage();
			this.buttonSendKeys = new System.Windows.Forms.Button();
			this.labelSendKeys = new System.Windows.Forms.Label();
			this.textBoxSendKeys = new System.Windows.Forms.TextBox();
			this.buttonLoadOldScript = new System.Windows.Forms.Button();
			this.buttonMoveMouse = new System.Windows.Forms.Button();
			this.buttonClickImage = new System.Windows.Forms.Button();
			this.tabControlPrimary.SuspendLayout();
			this.tabPageRaw.SuspendLayout();
			this.tabPageWindow.SuspendLayout();
			this.tabPageQueue.SuspendLayout();
			this.tabPageOther.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlPrimary
			// 
			this.tabControlPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlPrimary.Controls.Add(this.tabPageStatus);
			this.tabControlPrimary.Controls.Add(this.tabPageRaw);
			this.tabControlPrimary.Controls.Add(this.tabPageWindow);
			this.tabControlPrimary.Controls.Add(this.tabPageQueue);
			this.tabControlPrimary.Controls.Add(this.tabPageOther);
			this.tabControlPrimary.Location = new System.Drawing.Point(9, 9);
			this.tabControlPrimary.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlPrimary.Name = "tabControlPrimary";
			this.tabControlPrimary.Padding = new System.Drawing.Point(0, 0);
			this.tabControlPrimary.SelectedIndex = 0;
			this.tabControlPrimary.Size = new System.Drawing.Size(493, 322);
			this.tabControlPrimary.TabIndex = 18;
			// 
			// tabPageStatus
			// 
			this.tabPageStatus.BackColor = System.Drawing.Color.Transparent;
			this.tabPageStatus.Location = new System.Drawing.Point(4, 22);
			this.tabPageStatus.Name = "tabPageStatus";
			this.tabPageStatus.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStatus.Size = new System.Drawing.Size(485, 296);
			this.tabPageStatus.TabIndex = 4;
			this.tabPageStatus.Text = "Status";
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
			this.tabPageRaw.Controls.Add(this.buttonExecuteFunction);
			this.tabPageRaw.Location = new System.Drawing.Point(4, 22);
			this.tabPageRaw.Name = "tabPageRaw";
			this.tabPageRaw.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRaw.Size = new System.Drawing.Size(485, 296);
			this.tabPageRaw.TabIndex = 0;
			this.tabPageRaw.Text = "Raw";
			// 
			// labelFunctionParensClose
			// 
			this.labelFunctionParensClose.AutoSize = true;
			this.labelFunctionParensClose.Location = new System.Drawing.Point(242, 62);
			this.labelFunctionParensClose.Name = "labelFunctionParensClose";
			this.labelFunctionParensClose.Size = new System.Drawing.Size(10, 13);
			this.labelFunctionParensClose.TabIndex = 21;
			this.labelFunctionParensClose.Text = ")";
			// 
			// labelFunctionParensOpen
			// 
			this.labelFunctionParensOpen.AutoSize = true;
			this.labelFunctionParensOpen.Location = new System.Drawing.Point(239, 10);
			this.labelFunctionParensOpen.Name = "labelFunctionParensOpen";
			this.labelFunctionParensOpen.Size = new System.Drawing.Size(10, 13);
			this.labelFunctionParensOpen.TabIndex = 20;
			this.labelFunctionParensOpen.Text = "(";
			// 
			// textBoxExecuteStatement
			// 
			this.textBoxExecuteStatement.Location = new System.Drawing.Point(80, 152);
			this.textBoxExecuteStatement.Name = "textBoxExecuteStatement";
			this.textBoxExecuteStatement.Size = new System.Drawing.Size(202, 20);
			this.textBoxExecuteStatement.TabIndex = 18;
			// 
			// textBox_var
			// 
			this.textBox_var.Location = new System.Drawing.Point(102, 108);
			this.textBox_var.Name = "textBox_var";
			this.textBox_var.Size = new System.Drawing.Size(180, 20);
			this.textBox_var.TabIndex = 4;
			// 
			// textBoxExecuteFunction
			// 
			this.textBoxExecuteFunction.Location = new System.Drawing.Point(80, 7);
			this.textBoxExecuteFunction.Name = "textBoxExecuteFunction";
			this.textBoxExecuteFunction.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunction.TabIndex = 6;
			// 
			// textBoxExecuteFunctionP1
			// 
			this.textBoxExecuteFunctionP1.Location = new System.Drawing.Point(80, 33);
			this.textBoxExecuteFunctionP1.Name = "textBoxExecuteFunctionP1";
			this.textBoxExecuteFunctionP1.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunctionP1.TabIndex = 8;
			// 
			// textBoxExecuteFunctionP2
			// 
			this.textBoxExecuteFunctionP2.Location = new System.Drawing.Point(242, 33);
			this.textBoxExecuteFunctionP2.Name = "textBoxExecuteFunctionP2";
			this.textBoxExecuteFunctionP2.Size = new System.Drawing.Size(155, 20);
			this.textBoxExecuteFunctionP2.TabIndex = 9;
			// 
			// textBoxExecuteFunctionP3
			// 
			this.textBoxExecuteFunctionP3.Location = new System.Drawing.Point(80, 59);
			this.textBoxExecuteFunctionP3.Name = "textBoxExecuteFunctionP3";
			this.textBoxExecuteFunctionP3.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunctionP3.TabIndex = 10;
			// 
			// buttonExecuteStatement
			// 
			this.buttonExecuteStatement.Location = new System.Drawing.Point(288, 150);
			this.buttonExecuteStatement.Name = "buttonExecuteStatement";
			this.buttonExecuteStatement.Size = new System.Drawing.Size(109, 23);
			this.buttonExecuteStatement.TabIndex = 17;
			this.buttonExecuteStatement.Text = "Execute";
			this.buttonExecuteStatement.UseVisualStyleBackColor = true;
			// 
			// labelExecuteStatement
			// 
			this.labelExecuteStatement.AutoSize = true;
			this.labelExecuteStatement.Location = new System.Drawing.Point(6, 155);
			this.labelExecuteStatement.Name = "labelExecuteStatement";
			this.labelExecuteStatement.Size = new System.Drawing.Size(58, 13);
			this.labelExecuteStatement.TabIndex = 19;
			this.labelExecuteStatement.Text = "Statement:";
			// 
			// labelFunctionParameters
			// 
			this.labelFunctionParameters.AutoSize = true;
			this.labelFunctionParameters.Location = new System.Drawing.Point(6, 36);
			this.labelFunctionParameters.Name = "labelFunctionParameters";
			this.labelFunctionParameters.Size = new System.Drawing.Size(63, 13);
			this.labelFunctionParameters.TabIndex = 16;
			this.labelFunctionParameters.Text = "Parameters:";
			// 
			// labelFunctionName
			// 
			this.labelFunctionName.AutoSize = true;
			this.labelFunctionName.Location = new System.Drawing.Point(6, 10);
			this.labelFunctionName.Name = "labelFunctionName";
			this.labelFunctionName.Size = new System.Drawing.Size(51, 13);
			this.labelFunctionName.TabIndex = 15;
			this.labelFunctionName.Text = "Function:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(288, 106);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(109, 23);
			this.button3.TabIndex = 3;
			this.button3.Text = "Evaluate";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// labelEvaluateVariable
			// 
			this.labelEvaluateVariable.AutoSize = true;
			this.labelEvaluateVariable.Location = new System.Drawing.Point(6, 111);
			this.labelEvaluateVariable.Name = "labelEvaluateVariable";
			this.labelEvaluateVariable.Size = new System.Drawing.Size(79, 13);
			this.labelEvaluateVariable.TabIndex = 14;
			this.labelEvaluateVariable.Text = "Variable Name:";
			// 
			// buttonExecuteFunction
			// 
			this.buttonExecuteFunction.Location = new System.Drawing.Point(288, 59);
			this.buttonExecuteFunction.Name = "buttonExecuteFunction";
			this.buttonExecuteFunction.Size = new System.Drawing.Size(109, 23);
			this.buttonExecuteFunction.TabIndex = 7;
			this.buttonExecuteFunction.Text = "Evaluate";
			this.buttonExecuteFunction.UseVisualStyleBackColor = true;
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
			this.tabPageWindow.Controls.Add(this.labelFindImage2);
			this.tabPageWindow.Controls.Add(this.labelDetectWindow);
			this.tabPageWindow.Controls.Add(this.buttonWindowDetect);
			this.tabPageWindow.Location = new System.Drawing.Point(4, 22);
			this.tabPageWindow.Name = "tabPageWindow";
			this.tabPageWindow.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageWindow.Size = new System.Drawing.Size(485, 296);
			this.tabPageWindow.TabIndex = 1;
			this.tabPageWindow.Text = "Window";
			// 
			// buttonWindowActivate
			// 
			this.buttonWindowActivate.Location = new System.Drawing.Point(150, 132);
			this.buttonWindowActivate.Name = "buttonWindowActivate";
			this.buttonWindowActivate.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowActivate.TabIndex = 9;
			this.buttonWindowActivate.Text = "Activate";
			this.buttonWindowActivate.UseVisualStyleBackColor = true;
			// 
			// buttonWindowKill
			// 
			this.buttonWindowKill.Location = new System.Drawing.Point(312, 132);
			this.buttonWindowKill.Name = "buttonWindowKill";
			this.buttonWindowKill.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowKill.TabIndex = 8;
			this.buttonWindowKill.Text = "Kill";
			this.buttonWindowKill.UseVisualStyleBackColor = true;
			// 
			// buttonWindowMinimize
			// 
			this.buttonWindowMinimize.Location = new System.Drawing.Point(231, 132);
			this.buttonWindowMinimize.Name = "buttonWindowMinimize";
			this.buttonWindowMinimize.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowMinimize.TabIndex = 7;
			this.buttonWindowMinimize.Text = "Minimize";
			this.buttonWindowMinimize.UseVisualStyleBackColor = true;
			// 
			// labelFindImage1
			// 
			this.labelFindImage1.AutoSize = true;
			this.labelFindImage1.Location = new System.Drawing.Point(8, 16);
			this.labelFindImage1.Name = "labelFindImage1";
			this.labelFindImage1.Size = new System.Drawing.Size(84, 13);
			this.labelFindImage1.TabIndex = 6;
			this.labelFindImage1.Text = "INI Image Code:";
			// 
			// textBoxFindImage
			// 
			this.textBoxFindImage.Location = new System.Drawing.Point(98, 13);
			this.textBoxFindImage.Name = "textBoxFindImage";
			this.textBoxFindImage.Size = new System.Drawing.Size(144, 20);
			this.textBoxFindImage.TabIndex = 5;
			// 
			// textBoxDetectWindow
			// 
			this.textBoxDetectWindow.Location = new System.Drawing.Point(98, 106);
			this.textBoxDetectWindow.Name = "textBoxDetectWindow";
			this.textBoxDetectWindow.Size = new System.Drawing.Size(289, 20);
			this.textBoxDetectWindow.TabIndex = 2;
			// 
			// buttonFindImage
			// 
			this.buttonFindImage.Location = new System.Drawing.Point(312, 11);
			this.buttonFindImage.Name = "buttonFindImage";
			this.buttonFindImage.Size = new System.Drawing.Size(75, 23);
			this.buttonFindImage.TabIndex = 4;
			this.buttonFindImage.Text = "Find";
			this.buttonFindImage.UseVisualStyleBackColor = true;
			this.buttonFindImage.Click += new System.EventHandler(this.buttonFindImage_Click);
			// 
			// labelFindImage2
			// 
			this.labelFindImage2.AutoSize = true;
			this.labelFindImage2.Location = new System.Drawing.Point(248, 16);
			this.labelFindImage2.Name = "labelFindImage2";
			this.labelFindImage2.Size = new System.Drawing.Size(58, 13);
			this.labelFindImage2.TabIndex = 3;
			this.labelFindImage2.Text = "_ImageFile";
			// 
			// labelDetectWindow
			// 
			this.labelDetectWindow.AutoSize = true;
			this.labelDetectWindow.Location = new System.Drawing.Point(6, 109);
			this.labelDetectWindow.Name = "labelDetectWindow";
			this.labelDetectWindow.Size = new System.Drawing.Size(73, 13);
			this.labelDetectWindow.TabIndex = 1;
			this.labelDetectWindow.Text = "Window EXE:";
			// 
			// buttonWindowDetect
			// 
			this.buttonWindowDetect.Location = new System.Drawing.Point(69, 132);
			this.buttonWindowDetect.Name = "buttonWindowDetect";
			this.buttonWindowDetect.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowDetect.TabIndex = 0;
			this.buttonWindowDetect.Text = "Detect";
			this.buttonWindowDetect.UseVisualStyleBackColor = true;
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
			this.tabPageQueue.Name = "tabPageQueue";
			this.tabPageQueue.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageQueue.Size = new System.Drawing.Size(485, 296);
			this.tabPageQueue.TabIndex = 2;
			this.tabPageQueue.Text = "Queue";
			// 
			// labelGameTaskType
			// 
			this.labelGameTaskType.AutoSize = true;
			this.labelGameTaskType.Location = new System.Drawing.Point(6, 61);
			this.labelGameTaskType.Name = "labelGameTaskType";
			this.labelGameTaskType.Size = new System.Drawing.Size(34, 13);
			this.labelGameTaskType.TabIndex = 7;
			this.labelGameTaskType.Text = "Type:";
			// 
			// comboBoxGameTaskType
			// 
			this.comboBoxGameTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGameTaskType.FormattingEnabled = true;
			this.comboBoxGameTaskType.Location = new System.Drawing.Point(77, 58);
			this.comboBoxGameTaskType.Name = "comboBoxGameTaskType";
			this.comboBoxGameTaskType.Size = new System.Drawing.Size(121, 21);
			this.comboBoxGameTaskType.TabIndex = 6;
			// 
			// labelGameTaskDelaySec
			// 
			this.labelGameTaskDelaySec.AutoSize = true;
			this.labelGameTaskDelaySec.Location = new System.Drawing.Point(6, 35);
			this.labelGameTaskDelaySec.Name = "labelGameTaskDelaySec";
			this.labelGameTaskDelaySec.Size = new System.Drawing.Size(63, 13);
			this.labelGameTaskDelaySec.TabIndex = 5;
			this.labelGameTaskDelaySec.Text = "Delay (sec):";
			// 
			// textBoxGameTaskDelaySec
			// 
			this.textBoxGameTaskDelaySec.Location = new System.Drawing.Point(77, 32);
			this.textBoxGameTaskDelaySec.Name = "textBoxGameTaskDelaySec";
			this.textBoxGameTaskDelaySec.Size = new System.Drawing.Size(121, 20);
			this.textBoxGameTaskDelaySec.TabIndex = 4;
			// 
			// textBoxGameTaskCharIdx
			// 
			this.textBoxGameTaskCharIdx.Location = new System.Drawing.Point(77, 6);
			this.textBoxGameTaskCharIdx.Name = "textBoxGameTaskCharIdx";
			this.textBoxGameTaskCharIdx.Size = new System.Drawing.Size(178, 20);
			this.textBoxGameTaskCharIdx.TabIndex = 0;
			// 
			// labelGameTaskCharacterIdx
			// 
			this.labelGameTaskCharacterIdx.AutoSize = true;
			this.labelGameTaskCharacterIdx.Location = new System.Drawing.Point(6, 9);
			this.labelGameTaskCharacterIdx.Name = "labelGameTaskCharacterIdx";
			this.labelGameTaskCharacterIdx.Size = new System.Drawing.Size(56, 13);
			this.labelGameTaskCharacterIdx.TabIndex = 3;
			this.labelGameTaskCharacterIdx.Text = "Character:";
			// 
			// buttonNextTask
			// 
			this.buttonNextTask.Location = new System.Drawing.Point(180, 90);
			this.buttonNextTask.Name = "buttonNextTask";
			this.buttonNextTask.Size = new System.Drawing.Size(75, 23);
			this.buttonNextTask.TabIndex = 2;
			this.buttonNextTask.Text = "Pop Next Task";
			this.buttonNextTask.UseVisualStyleBackColor = true;
			// 
			// buttonAddCharIdx
			// 
			this.buttonAddCharIdx.Location = new System.Drawing.Point(3, 90);
			this.buttonAddCharIdx.Name = "buttonAddCharIdx";
			this.buttonAddCharIdx.Size = new System.Drawing.Size(171, 23);
			this.buttonAddCharIdx.TabIndex = 1;
			this.buttonAddCharIdx.Text = "Queue Task";
			this.buttonAddCharIdx.UseVisualStyleBackColor = true;
			// 
			// tabPageOther
			// 
			this.tabPageOther.BackColor = System.Drawing.Color.Transparent;
			this.tabPageOther.Controls.Add(this.buttonSendKeys);
			this.tabPageOther.Controls.Add(this.labelSendKeys);
			this.tabPageOther.Controls.Add(this.textBoxSendKeys);
			this.tabPageOther.Controls.Add(this.buttonLoadOldScript);
			this.tabPageOther.Controls.Add(this.buttonMoveMouse);
			this.tabPageOther.Location = new System.Drawing.Point(4, 22);
			this.tabPageOther.Name = "tabPageOther";
			this.tabPageOther.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageOther.Size = new System.Drawing.Size(485, 296);
			this.tabPageOther.TabIndex = 3;
			this.tabPageOther.Text = "Other";
			// 
			// buttonSendKeys
			// 
			this.buttonSendKeys.Location = new System.Drawing.Point(326, 6);
			this.buttonSendKeys.Name = "buttonSendKeys";
			this.buttonSendKeys.Size = new System.Drawing.Size(65, 23);
			this.buttonSendKeys.TabIndex = 8;
			this.buttonSendKeys.Text = "Send";
			this.buttonSendKeys.UseVisualStyleBackColor = true;
			// 
			// labelSendKeys
			// 
			this.labelSendKeys.AutoSize = true;
			this.labelSendKeys.Location = new System.Drawing.Point(6, 11);
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
			// buttonLoadOldScript
			// 
			this.buttonLoadOldScript.Location = new System.Drawing.Point(267, 171);
			this.buttonLoadOldScript.Name = "buttonLoadOldScript";
			this.buttonLoadOldScript.Size = new System.Drawing.Size(124, 23);
			this.buttonLoadOldScript.TabIndex = 3;
			this.buttonLoadOldScript.Text = "Load Old Script";
			this.buttonLoadOldScript.UseVisualStyleBackColor = true;
			// 
			// buttonMoveMouse
			// 
			this.buttonMoveMouse.Location = new System.Drawing.Point(3, 171);
			this.buttonMoveMouse.Name = "buttonMoveMouse";
			this.buttonMoveMouse.Size = new System.Drawing.Size(105, 23);
			this.buttonMoveMouse.TabIndex = 5;
			this.buttonMoveMouse.Text = "Move Mouse";
			this.buttonMoveMouse.UseVisualStyleBackColor = true;
			// 
			// buttonClickImage
			// 
			this.buttonClickImage.Location = new System.Drawing.Point(393, 11);
			this.buttonClickImage.Name = "buttonClickImage";
			this.buttonClickImage.Size = new System.Drawing.Size(75, 23);
			this.buttonClickImage.TabIndex = 10;
			this.buttonClickImage.Text = "Click";
			this.buttonClickImage.UseVisualStyleBackColor = true;
			this.buttonClickImage.Click += new System.EventHandler(this.buttonClickImage_Click);
			// 
			// TestsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(511, 340);
			this.Controls.Add(this.tabControlPrimary);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TestsForm";
			this.Text = "Tests";
			this.Load += new System.EventHandler(this.Tests_Load);
			this.tabControlPrimary.ResumeLayout(false);
			this.tabPageRaw.ResumeLayout(false);
			this.tabPageRaw.PerformLayout();
			this.tabPageWindow.ResumeLayout(false);
			this.tabPageWindow.PerformLayout();
			this.tabPageQueue.ResumeLayout(false);
			this.tabPageQueue.PerformLayout();
			this.tabPageOther.ResumeLayout(false);
			this.tabPageOther.PerformLayout();
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
		private System.Windows.Forms.Button buttonExecuteFunction;
		private System.Windows.Forms.TabPage tabPageWindow;
		private System.Windows.Forms.Button buttonWindowActivate;
		private System.Windows.Forms.Button buttonWindowKill;
		private System.Windows.Forms.Button buttonWindowMinimize;
		private System.Windows.Forms.Label labelFindImage1;
		private System.Windows.Forms.TextBox textBoxFindImage;
		private System.Windows.Forms.TextBox textBoxDetectWindow;
		private System.Windows.Forms.Button buttonFindImage;
		private System.Windows.Forms.Label labelFindImage2;
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
		private System.Windows.Forms.TabPage tabPageOther;
		private System.Windows.Forms.Button buttonSendKeys;
		private System.Windows.Forms.Label labelSendKeys;
		private System.Windows.Forms.TextBox textBoxSendKeys;
		private System.Windows.Forms.Button buttonLoadOldScript;
		private System.Windows.Forms.Button buttonMoveMouse;
		private System.Windows.Forms.Button buttonClickImage;
	}
}