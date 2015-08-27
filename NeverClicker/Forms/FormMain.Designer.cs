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
			this.buttonAutoInvokeAsync = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox_var = new System.Windows.Forms.TextBox();
			this.buttonMoveMouse = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.buttonReload = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.labelFunctionParameters = new System.Windows.Forms.Label();
			this.labelFunctionName = new System.Windows.Forms.Label();
			this.labelEvaluateVariable = new System.Windows.Forms.Label();
			this.textBoxExecuteFunctionP3 = new System.Windows.Forms.TextBox();
			this.textBoxExecuteFunctionP2 = new System.Windows.Forms.TextBox();
			this.textBoxExecuteFunctionP1 = new System.Windows.Forms.TextBox();
			this.buttonExecuteFunction = new System.Windows.Forms.Button();
			this.textBoxExecuteFunction = new System.Windows.Forms.TextBox();
			this.labelLog = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.buttonOptions = new System.Windows.Forms.Button();
			this.tabControlPrimary = new System.Windows.Forms.TabControl();
			this.tabPageStatus = new System.Windows.Forms.TabPage();
			this.listBoxTaskQueue = new System.Windows.Forms.ListBox();
			this.labelTaskQueue = new System.Windows.Forms.Label();
			this.labelStatusBar = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.tabPageRaw = new System.Windows.Forms.TabPage();
			this.labelFunctionParensClose = new System.Windows.Forms.Label();
			this.labelFunctionParensOpen = new System.Windows.Forms.Label();
			this.textBoxExecuteStatement = new System.Windows.Forms.TextBox();
			this.buttonExecuteStatement = new System.Windows.Forms.Button();
			this.labelExecuteStatement = new System.Windows.Forms.Label();
			this.tabPageWindow = new System.Windows.Forms.TabPage();
			this.textBoxDetectWindow = new System.Windows.Forms.TextBox();
			this.labelDetectWindow = new System.Windows.Forms.Label();
			this.buttonDetectWindow = new System.Windows.Forms.Button();
			this.tabPageQueue = new System.Windows.Forms.TabPage();
			this.labelDelaySec = new System.Windows.Forms.Label();
			this.textBoxDelaySec = new System.Windows.Forms.TextBox();
			this.labelCharacterIdx = new System.Windows.Forms.Label();
			this.buttonNextTask = new System.Windows.Forms.Button();
			this.buttonAddCharIdx = new System.Windows.Forms.Button();
			this.textBoxAddCharIdx = new System.Windows.Forms.TextBox();
			this.tabPageOther = new System.Windows.Forms.TabPage();
			this.buttonLoadOldScript = new System.Windows.Forms.Button();
			this.buttonAutoCycle = new System.Windows.Forms.Button();
			this.tabControlPrimary.SuspendLayout();
			this.tabPageStatus.SuspendLayout();
			this.tabPageRaw.SuspendLayout();
			this.tabPageWindow.SuspendLayout();
			this.tabPageQueue.SuspendLayout();
			this.tabPageOther.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonAutoInvokeAsync
			// 
			this.buttonAutoInvokeAsync.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.buttonAutoInvokeAsync.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.buttonAutoInvokeAsync.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAutoInvokeAsync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonAutoInvokeAsync.Location = new System.Drawing.Point(16, 6);
			this.buttonAutoInvokeAsync.Name = "buttonAutoInvokeAsync";
			this.buttonAutoInvokeAsync.Size = new System.Drawing.Size(136, 23);
			this.buttonAutoInvokeAsync.TabIndex = 0;
			this.buttonAutoInvokeAsync.Text = "Begin AutoInvoke (old)";
			this.buttonAutoInvokeAsync.UseVisualStyleBackColor = false;
			this.buttonAutoInvokeAsync.Click += new System.EventHandler(this.buttonAutoInvokeAsync_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 346);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(405, 200);
			this.textBox1.TabIndex = 1;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(288, 106);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(109, 23);
			this.button3.TabIndex = 3;
			this.button3.Text = "Evaluate";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.buttonCheckVar_Click);
			// 
			// textBox_var
			// 
			this.textBox_var.Location = new System.Drawing.Point(102, 108);
			this.textBox_var.Name = "textBox_var";
			this.textBox_var.Size = new System.Drawing.Size(180, 20);
			this.textBox_var.TabIndex = 4;
			// 
			// buttonMoveMouse
			// 
			this.buttonMoveMouse.Location = new System.Drawing.Point(158, 6);
			this.buttonMoveMouse.Name = "buttonMoveMouse";
			this.buttonMoveMouse.Size = new System.Drawing.Size(105, 23);
			this.buttonMoveMouse.TabIndex = 5;
			this.buttonMoveMouse.Text = "Move Mouse";
			this.buttonMoveMouse.UseVisualStyleBackColor = true;
			this.buttonMoveMouse.Click += new System.EventHandler(this.buttonMoveMouse_Click);
			// 
			// buttonPause
			// 
			this.buttonPause.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonPause.Enabled = false;
			this.buttonPause.Location = new System.Drawing.Point(174, 592);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(75, 23);
			this.buttonPause.TabIndex = 6;
			this.buttonPause.Text = "Pause";
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Visible = false;
			this.buttonPause.Click += new System.EventHandler(this.buttonSuspend_Click);
			// 
			// buttonReload
			// 
			this.buttonReload.Location = new System.Drawing.Point(93, 592);
			this.buttonReload.Name = "buttonReload";
			this.buttonReload.Size = new System.Drawing.Size(75, 23);
			this.buttonReload.TabIndex = 7;
			this.buttonReload.Text = "Reload";
			this.buttonReload.UseVisualStyleBackColor = true;
			this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Enabled = false;
			this.buttonStop.Location = new System.Drawing.Point(12, 592);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 8;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(342, 592);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(75, 23);
			this.buttonExit.TabIndex = 11;
			this.buttonExit.Text = "Exit";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
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
			// labelEvaluateVariable
			// 
			this.labelEvaluateVariable.AutoSize = true;
			this.labelEvaluateVariable.Location = new System.Drawing.Point(6, 111);
			this.labelEvaluateVariable.Name = "labelEvaluateVariable";
			this.labelEvaluateVariable.Size = new System.Drawing.Size(79, 13);
			this.labelEvaluateVariable.TabIndex = 14;
			this.labelEvaluateVariable.Text = "Variable Name:";
			// 
			// textBoxExecuteFunctionP3
			// 
			this.textBoxExecuteFunctionP3.Location = new System.Drawing.Point(80, 59);
			this.textBoxExecuteFunctionP3.Name = "textBoxExecuteFunctionP3";
			this.textBoxExecuteFunctionP3.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunctionP3.TabIndex = 10;
			// 
			// textBoxExecuteFunctionP2
			// 
			this.textBoxExecuteFunctionP2.Location = new System.Drawing.Point(242, 33);
			this.textBoxExecuteFunctionP2.Name = "textBoxExecuteFunctionP2";
			this.textBoxExecuteFunctionP2.Size = new System.Drawing.Size(155, 20);
			this.textBoxExecuteFunctionP2.TabIndex = 9;
			// 
			// textBoxExecuteFunctionP1
			// 
			this.textBoxExecuteFunctionP1.Location = new System.Drawing.Point(80, 33);
			this.textBoxExecuteFunctionP1.Name = "textBoxExecuteFunctionP1";
			this.textBoxExecuteFunctionP1.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunctionP1.TabIndex = 8;
			// 
			// buttonExecuteFunction
			// 
			this.buttonExecuteFunction.Location = new System.Drawing.Point(288, 59);
			this.buttonExecuteFunction.Name = "buttonExecuteFunction";
			this.buttonExecuteFunction.Size = new System.Drawing.Size(109, 23);
			this.buttonExecuteFunction.TabIndex = 7;
			this.buttonExecuteFunction.Text = "Evaluate";
			this.buttonExecuteFunction.UseVisualStyleBackColor = true;
			this.buttonExecuteFunction.Click += new System.EventHandler(this.buttonExecuteFunction_Click);
			// 
			// textBoxExecuteFunction
			// 
			this.textBoxExecuteFunction.Location = new System.Drawing.Point(80, 7);
			this.textBoxExecuteFunction.Name = "textBoxExecuteFunction";
			this.textBoxExecuteFunction.Size = new System.Drawing.Size(156, 20);
			this.textBoxExecuteFunction.TabIndex = 6;
			// 
			// labelLog
			// 
			this.labelLog.AutoSize = true;
			this.labelLog.Location = new System.Drawing.Point(12, 330);
			this.labelLog.Name = "labelLog";
			this.labelLog.Size = new System.Drawing.Size(28, 13);
			this.labelLog.TabIndex = 14;
			this.labelLog.Text = "Log:";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 627);
			this.splitter1.TabIndex = 15;
			this.splitter1.TabStop = false;
			// 
			// buttonOptions
			// 
			this.buttonOptions.Location = new System.Drawing.Point(12, 552);
			this.buttonOptions.Name = "buttonOptions";
			this.buttonOptions.Size = new System.Drawing.Size(156, 23);
			this.buttonOptions.TabIndex = 16;
			this.buttonOptions.Text = "Settings";
			this.buttonOptions.UseVisualStyleBackColor = true;
			this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
			// 
			// tabControlPrimary
			// 
			this.tabControlPrimary.Controls.Add(this.tabPageStatus);
			this.tabControlPrimary.Controls.Add(this.tabPageRaw);
			this.tabControlPrimary.Controls.Add(this.tabPageWindow);
			this.tabControlPrimary.Controls.Add(this.tabPageQueue);
			this.tabControlPrimary.Controls.Add(this.tabPageOther);
			this.tabControlPrimary.Location = new System.Drawing.Point(12, 101);
			this.tabControlPrimary.Name = "tabControlPrimary";
			this.tabControlPrimary.SelectedIndex = 0;
			this.tabControlPrimary.Size = new System.Drawing.Size(405, 226);
			this.tabControlPrimary.TabIndex = 17;
			// 
			// tabPageStatus
			// 
			this.tabPageStatus.BackColor = System.Drawing.Color.Transparent;
			this.tabPageStatus.Controls.Add(this.listBoxTaskQueue);
			this.tabPageStatus.Controls.Add(this.labelTaskQueue);
			this.tabPageStatus.Controls.Add(this.labelStatusBar);
			this.tabPageStatus.Controls.Add(this.progressBar1);
			this.tabPageStatus.Location = new System.Drawing.Point(4, 22);
			this.tabPageStatus.Name = "tabPageStatus";
			this.tabPageStatus.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStatus.Size = new System.Drawing.Size(397, 200);
			this.tabPageStatus.TabIndex = 4;
			this.tabPageStatus.Text = "Status";
			// 
			// listBoxTaskQueue
			// 
			this.listBoxTaskQueue.FormattingEnabled = true;
			this.listBoxTaskQueue.Location = new System.Drawing.Point(36, 119);
			this.listBoxTaskQueue.Name = "listBoxTaskQueue";
			this.listBoxTaskQueue.Size = new System.Drawing.Size(325, 69);
			this.listBoxTaskQueue.TabIndex = 3;
			// 
			// labelTaskQueue
			// 
			this.labelTaskQueue.AutoSize = true;
			this.labelTaskQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTaskQueue.Location = new System.Drawing.Point(149, 91);
			this.labelTaskQueue.Name = "labelTaskQueue";
			this.labelTaskQueue.Size = new System.Drawing.Size(99, 20);
			this.labelTaskQueue.TabIndex = 2;
			this.labelTaskQueue.Text = "Task Queue:";
			// 
			// labelStatusBar
			// 
			this.labelStatusBar.AutoSize = true;
			this.labelStatusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelStatusBar.Location = new System.Drawing.Point(76, 19);
			this.labelStatusBar.Name = "labelStatusBar";
			this.labelStatusBar.Size = new System.Drawing.Size(242, 20);
			this.labelStatusBar.TabIndex = 1;
			this.labelStatusBar.Text = "Status Bar (not yet implemented)";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(36, 47);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(325, 23);
			this.progressBar1.TabIndex = 0;
			// 
			// tabPageRaw
			// 
			this.tabPageRaw.BackColor = System.Drawing.Color.Transparent;
			this.tabPageRaw.Controls.Add(this.labelFunctionParensClose);
			this.tabPageRaw.Controls.Add(this.labelFunctionParensOpen);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteStatement);
			this.tabPageRaw.Controls.Add(this.buttonExecuteStatement);
			this.tabPageRaw.Controls.Add(this.labelExecuteStatement);
			this.tabPageRaw.Controls.Add(this.labelFunctionParameters);
			this.tabPageRaw.Controls.Add(this.textBox_var);
			this.tabPageRaw.Controls.Add(this.labelFunctionName);
			this.tabPageRaw.Controls.Add(this.button3);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunction);
			this.tabPageRaw.Controls.Add(this.labelEvaluateVariable);
			this.tabPageRaw.Controls.Add(this.buttonExecuteFunction);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunctionP1);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunctionP2);
			this.tabPageRaw.Controls.Add(this.textBoxExecuteFunctionP3);
			this.tabPageRaw.Location = new System.Drawing.Point(4, 22);
			this.tabPageRaw.Name = "tabPageRaw";
			this.tabPageRaw.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRaw.Size = new System.Drawing.Size(397, 200);
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
			// buttonExecuteStatement
			// 
			this.buttonExecuteStatement.Location = new System.Drawing.Point(288, 150);
			this.buttonExecuteStatement.Name = "buttonExecuteStatement";
			this.buttonExecuteStatement.Size = new System.Drawing.Size(109, 23);
			this.buttonExecuteStatement.TabIndex = 17;
			this.buttonExecuteStatement.Text = "Execute";
			this.buttonExecuteStatement.UseVisualStyleBackColor = true;
			this.buttonExecuteStatement.Click += new System.EventHandler(this.buttonExecuteStatement_Click);
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
			// tabPageWindow
			// 
			this.tabPageWindow.BackColor = System.Drawing.Color.Transparent;
			this.tabPageWindow.Controls.Add(this.textBoxDetectWindow);
			this.tabPageWindow.Controls.Add(this.labelDetectWindow);
			this.tabPageWindow.Controls.Add(this.buttonDetectWindow);
			this.tabPageWindow.Location = new System.Drawing.Point(4, 22);
			this.tabPageWindow.Name = "tabPageWindow";
			this.tabPageWindow.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageWindow.Size = new System.Drawing.Size(397, 200);
			this.tabPageWindow.TabIndex = 1;
			this.tabPageWindow.Text = "Window";
			// 
			// textBoxDetectWindow
			// 
			this.textBoxDetectWindow.Location = new System.Drawing.Point(92, 13);
			this.textBoxDetectWindow.Name = "textBoxDetectWindow";
			this.textBoxDetectWindow.Size = new System.Drawing.Size(214, 20);
			this.textBoxDetectWindow.TabIndex = 2;
			this.textBoxDetectWindow.TextChanged += new System.EventHandler(this.textBoxDetectWindow_TextChanged);
			this.textBoxDetectWindow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDetectWindow_KeyPress);
			// 
			// labelDetectWindow
			// 
			this.labelDetectWindow.AutoSize = true;
			this.labelDetectWindow.Location = new System.Drawing.Point(6, 16);
			this.labelDetectWindow.Name = "labelDetectWindow";
			this.labelDetectWindow.Size = new System.Drawing.Size(80, 13);
			this.labelDetectWindow.TabIndex = 1;
			this.labelDetectWindow.Text = "Window Name:";
			// 
			// buttonDetectWindow
			// 
			this.buttonDetectWindow.Location = new System.Drawing.Point(312, 11);
			this.buttonDetectWindow.Name = "buttonDetectWindow";
			this.buttonDetectWindow.Size = new System.Drawing.Size(75, 23);
			this.buttonDetectWindow.TabIndex = 0;
			this.buttonDetectWindow.Text = "Detect";
			this.buttonDetectWindow.UseVisualStyleBackColor = true;
			this.buttonDetectWindow.Click += new System.EventHandler(this.buttonDetectWindow_Click);
			// 
			// tabPageQueue
			// 
			this.tabPageQueue.BackColor = System.Drawing.Color.Transparent;
			this.tabPageQueue.Controls.Add(this.labelDelaySec);
			this.tabPageQueue.Controls.Add(this.textBoxDelaySec);
			this.tabPageQueue.Controls.Add(this.labelCharacterIdx);
			this.tabPageQueue.Controls.Add(this.buttonNextTask);
			this.tabPageQueue.Controls.Add(this.buttonAddCharIdx);
			this.tabPageQueue.Controls.Add(this.textBoxAddCharIdx);
			this.tabPageQueue.Location = new System.Drawing.Point(4, 22);
			this.tabPageQueue.Name = "tabPageQueue";
			this.tabPageQueue.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageQueue.Size = new System.Drawing.Size(397, 200);
			this.tabPageQueue.TabIndex = 2;
			this.tabPageQueue.Text = "Queue";
			// 
			// labelDelaySec
			// 
			this.labelDelaySec.AutoSize = true;
			this.labelDelaySec.Location = new System.Drawing.Point(3, 35);
			this.labelDelaySec.Name = "labelDelaySec";
			this.labelDelaySec.Size = new System.Drawing.Size(63, 13);
			this.labelDelaySec.TabIndex = 5;
			this.labelDelaySec.Text = "Delay (sec):";
			// 
			// textBoxDelaySec
			// 
			this.textBoxDelaySec.Location = new System.Drawing.Point(77, 32);
			this.textBoxDelaySec.Name = "textBoxDelaySec";
			this.textBoxDelaySec.Size = new System.Drawing.Size(100, 20);
			this.textBoxDelaySec.TabIndex = 4;
			// 
			// labelCharacterIdx
			// 
			this.labelCharacterIdx.AutoSize = true;
			this.labelCharacterIdx.Location = new System.Drawing.Point(6, 9);
			this.labelCharacterIdx.Name = "labelCharacterIdx";
			this.labelCharacterIdx.Size = new System.Drawing.Size(56, 13);
			this.labelCharacterIdx.TabIndex = 3;
			this.labelCharacterIdx.Text = "Character:";
			// 
			// buttonNextTask
			// 
			this.buttonNextTask.Location = new System.Drawing.Point(183, 58);
			this.buttonNextTask.Name = "buttonNextTask";
			this.buttonNextTask.Size = new System.Drawing.Size(75, 23);
			this.buttonNextTask.TabIndex = 2;
			this.buttonNextTask.Text = "Pop Next Task";
			this.buttonNextTask.UseVisualStyleBackColor = true;
			this.buttonNextTask.Click += new System.EventHandler(this.buttonNextTask_Click);
			// 
			// buttonAddCharIdx
			// 
			this.buttonAddCharIdx.Location = new System.Drawing.Point(6, 58);
			this.buttonAddCharIdx.Name = "buttonAddCharIdx";
			this.buttonAddCharIdx.Size = new System.Drawing.Size(171, 23);
			this.buttonAddCharIdx.TabIndex = 1;
			this.buttonAddCharIdx.Text = "Queue Task";
			this.buttonAddCharIdx.UseVisualStyleBackColor = true;
			this.buttonAddCharIdx.Click += new System.EventHandler(this.buttonAddCharIdx_Click);
			// 
			// textBoxAddCharIdx
			// 
			this.textBoxAddCharIdx.Location = new System.Drawing.Point(77, 6);
			this.textBoxAddCharIdx.Name = "textBoxAddCharIdx";
			this.textBoxAddCharIdx.Size = new System.Drawing.Size(100, 20);
			this.textBoxAddCharIdx.TabIndex = 0;
			// 
			// tabPageOther
			// 
			this.tabPageOther.BackColor = System.Drawing.Color.Transparent;
			this.tabPageOther.Controls.Add(this.buttonAutoInvokeAsync);
			this.tabPageOther.Controls.Add(this.buttonLoadOldScript);
			this.tabPageOther.Controls.Add(this.buttonMoveMouse);
			this.tabPageOther.Location = new System.Drawing.Point(4, 22);
			this.tabPageOther.Name = "tabPageOther";
			this.tabPageOther.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageOther.Size = new System.Drawing.Size(397, 200);
			this.tabPageOther.TabIndex = 3;
			this.tabPageOther.Text = "Other";
			// 
			// buttonLoadOldScript
			// 
			this.buttonLoadOldScript.Location = new System.Drawing.Point(16, 166);
			this.buttonLoadOldScript.Name = "buttonLoadOldScript";
			this.buttonLoadOldScript.Size = new System.Drawing.Size(124, 23);
			this.buttonLoadOldScript.TabIndex = 3;
			this.buttonLoadOldScript.Text = "Load Old Script";
			this.buttonLoadOldScript.UseVisualStyleBackColor = true;
			this.buttonLoadOldScript.Click += new System.EventHandler(this.buttonLoadOldScript_Click);
			// 
			// buttonAutoCycle
			// 
			this.buttonAutoCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonAutoCycle.Location = new System.Drawing.Point(54, 31);
			this.buttonAutoCycle.Name = "buttonAutoCycle";
			this.buttonAutoCycle.Size = new System.Drawing.Size(325, 37);
			this.buttonAutoCycle.TabIndex = 18;
			this.buttonAutoCycle.Text = "Begin AutoCycle";
			this.buttonAutoCycle.UseVisualStyleBackColor = true;
			this.buttonAutoCycle.Click += new System.EventHandler(this.buttonAutoCycle_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(429, 627);
			this.Controls.Add(this.buttonAutoCycle);
			this.Controls.Add(this.tabControlPrimary);
			this.Controls.Add(this.buttonOptions);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.labelLog);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonReload);
			this.Controls.Add(this.buttonPause);
			this.Controls.Add(this.textBox1);
			this.Name = "MainForm";
			this.Text = "NeverClicker";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.tabControlPrimary.ResumeLayout(false);
			this.tabPageStatus.ResumeLayout(false);
			this.tabPageStatus.PerformLayout();
			this.tabPageRaw.ResumeLayout(false);
			this.tabPageRaw.PerformLayout();
			this.tabPageWindow.ResumeLayout(false);
			this.tabPageWindow.PerformLayout();
			this.tabPageQueue.ResumeLayout(false);
			this.tabPageQueue.PerformLayout();
			this.tabPageOther.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAutoInvokeAsync;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox_var;
        private System.Windows.Forms.Button buttonMoveMouse;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Button buttonExecuteFunction;
        private System.Windows.Forms.TextBox textBoxExecuteFunction;
        private System.Windows.Forms.TextBox textBoxExecuteFunctionP3;
        private System.Windows.Forms.TextBox textBoxExecuteFunctionP2;
        private System.Windows.Forms.TextBox textBoxExecuteFunctionP1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label labelEvaluateVariable;
        private System.Windows.Forms.Label labelFunctionParameters;
        private System.Windows.Forms.Label labelFunctionName;
        private System.Windows.Forms.Button buttonOptions;
        private System.Windows.Forms.TabControl tabControlPrimary;
        private System.Windows.Forms.TabPage tabPageRaw;
        private System.Windows.Forms.TabPage tabPageWindow;
        private System.Windows.Forms.TextBox textBoxDetectWindow;
        private System.Windows.Forms.Label labelDetectWindow;
        private System.Windows.Forms.Button buttonDetectWindow;
        private System.Windows.Forms.Button buttonLoadOldScript;
        private System.Windows.Forms.TextBox textBoxExecuteStatement;
        private System.Windows.Forms.Button buttonExecuteStatement;
        private System.Windows.Forms.Label labelExecuteStatement;
		private System.Windows.Forms.Label labelFunctionParensClose;
		private System.Windows.Forms.Label labelFunctionParensOpen;
		private System.Windows.Forms.Button buttonAutoCycle;
		private System.Windows.Forms.TabPage tabPageQueue;
		private System.Windows.Forms.Button buttonNextTask;
		private System.Windows.Forms.Button buttonAddCharIdx;
		private System.Windows.Forms.TextBox textBoxAddCharIdx;
		private System.Windows.Forms.Label labelDelaySec;
		private System.Windows.Forms.TextBox textBoxDelaySec;
		private System.Windows.Forms.Label labelCharacterIdx;
		private System.Windows.Forms.TabPage tabPageOther;
		private System.Windows.Forms.TabPage tabPageStatus;
		private System.Windows.Forms.Label labelStatusBar;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.ListBox listBoxTaskQueue;
		private System.Windows.Forms.Label labelTaskQueue;
	}
}