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
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textBoxExecuteStatement = new System.Windows.Forms.TextBox();
			this.buttonExecuteStatement = new System.Windows.Forms.Button();
			this.labelExecuteStatement = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.buttonLoadOldScript = new System.Windows.Forms.Button();
			this.textBoxDetectWindow = new System.Windows.Forms.TextBox();
			this.labelDetectWindow = new System.Windows.Forms.Label();
			this.buttonDetectWindow = new System.Windows.Forms.Button();
			this.labelFunctionParensOpen = new System.Windows.Forms.Label();
			this.labelFunctionParensClose = new System.Windows.Forms.Label();
			this.tabControlPrimary.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonAutoInvokeAsync
			// 
			this.buttonAutoInvokeAsync.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.buttonAutoInvokeAsync.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.buttonAutoInvokeAsync.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAutoInvokeAsync.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonAutoInvokeAsync.Location = new System.Drawing.Point(46, 32);
			this.buttonAutoInvokeAsync.Name = "buttonAutoInvokeAsync";
			this.buttonAutoInvokeAsync.Size = new System.Drawing.Size(342, 33);
			this.buttonAutoInvokeAsync.TabIndex = 0;
			this.buttonAutoInvokeAsync.Text = "Begin Auto-Cycle";
			this.buttonAutoInvokeAsync.UseVisualStyleBackColor = false;
			this.buttonAutoInvokeAsync.Click += new System.EventHandler(this.buttonAutoInvokeAsync_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 355);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(407, 170);
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
			this.buttonMoveMouse.Location = new System.Drawing.Point(314, 531);
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
			this.buttonPause.Location = new System.Drawing.Point(93, 586);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(75, 23);
			this.buttonPause.TabIndex = 6;
			this.buttonPause.Text = "Pause";
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.buttonSuspend_Click);
			// 
			// buttonReload
			// 
			this.buttonReload.Location = new System.Drawing.Point(174, 586);
			this.buttonReload.Name = "buttonReload";
			this.buttonReload.Size = new System.Drawing.Size(75, 23);
			this.buttonReload.TabIndex = 7;
			this.buttonReload.Text = "Reload";
			this.buttonReload.UseVisualStyleBackColor = true;
			this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(12, 586);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 8;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(344, 586);
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
			this.labelLog.Location = new System.Drawing.Point(12, 341);
			this.labelLog.Name = "labelLog";
			this.labelLog.Size = new System.Drawing.Size(28, 13);
			this.labelLog.TabIndex = 14;
			this.labelLog.Text = "Log:";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 621);
			this.splitter1.TabIndex = 15;
			this.splitter1.TabStop = false;
			// 
			// buttonOptions
			// 
			this.buttonOptions.Location = new System.Drawing.Point(12, 531);
			this.buttonOptions.Name = "buttonOptions";
			this.buttonOptions.Size = new System.Drawing.Size(75, 23);
			this.buttonOptions.TabIndex = 16;
			this.buttonOptions.Text = "Options";
			this.buttonOptions.UseVisualStyleBackColor = true;
			this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
			// 
			// tabControlPrimary
			// 
			this.tabControlPrimary.Controls.Add(this.tabPage1);
			this.tabControlPrimary.Controls.Add(this.tabPage2);
			this.tabControlPrimary.Location = new System.Drawing.Point(12, 100);
			this.tabControlPrimary.Name = "tabControlPrimary";
			this.tabControlPrimary.SelectedIndex = 0;
			this.tabControlPrimary.Size = new System.Drawing.Size(411, 221);
			this.tabControlPrimary.TabIndex = 17;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.Transparent;
			this.tabPage1.Controls.Add(this.labelFunctionParensClose);
			this.tabPage1.Controls.Add(this.labelFunctionParensOpen);
			this.tabPage1.Controls.Add(this.textBoxExecuteStatement);
			this.tabPage1.Controls.Add(this.buttonExecuteStatement);
			this.tabPage1.Controls.Add(this.labelExecuteStatement);
			this.tabPage1.Controls.Add(this.labelFunctionParameters);
			this.tabPage1.Controls.Add(this.textBox_var);
			this.tabPage1.Controls.Add(this.labelFunctionName);
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.textBoxExecuteFunction);
			this.tabPage1.Controls.Add(this.labelEvaluateVariable);
			this.tabPage1.Controls.Add(this.buttonExecuteFunction);
			this.tabPage1.Controls.Add(this.textBoxExecuteFunctionP1);
			this.tabPage1.Controls.Add(this.textBoxExecuteFunctionP2);
			this.tabPage1.Controls.Add(this.textBoxExecuteFunctionP3);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(403, 195);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Raw Commands";
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
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.Transparent;
			this.tabPage2.Controls.Add(this.textBoxDetectWindow);
			this.tabPage2.Controls.Add(this.labelDetectWindow);
			this.tabPage2.Controls.Add(this.buttonDetectWindow);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(403, 229);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Window Detection";
			// 
			// buttonLoadOldScript
			// 
			this.buttonLoadOldScript.Location = new System.Drawing.Point(93, 531);
			this.buttonLoadOldScript.Name = "buttonLoadOldScript";
			this.buttonLoadOldScript.Size = new System.Drawing.Size(124, 23);
			this.buttonLoadOldScript.TabIndex = 3;
			this.buttonLoadOldScript.Text = "Load Old Script";
			this.buttonLoadOldScript.UseVisualStyleBackColor = true;
			this.buttonLoadOldScript.Click += new System.EventHandler(this.buttonLoadOldScript_Click);
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
			// labelFunctionParensOpen
			// 
			this.labelFunctionParensOpen.AutoSize = true;
			this.labelFunctionParensOpen.Location = new System.Drawing.Point(239, 10);
			this.labelFunctionParensOpen.Name = "labelFunctionParensOpen";
			this.labelFunctionParensOpen.Size = new System.Drawing.Size(10, 13);
			this.labelFunctionParensOpen.TabIndex = 20;
			this.labelFunctionParensOpen.Text = "(";
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(429, 621);
			this.Controls.Add(this.buttonLoadOldScript);
			this.Controls.Add(this.tabControlPrimary);
			this.Controls.Add(this.buttonOptions);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.buttonMoveMouse);
			this.Controls.Add(this.labelLog);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonReload);
			this.Controls.Add(this.buttonPause);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.buttonAutoInvokeAsync);
			this.Name = "MainForm";
			this.Text = "NeverClicker";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabControlPrimary.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxDetectWindow;
        private System.Windows.Forms.Label labelDetectWindow;
        private System.Windows.Forms.Button buttonDetectWindow;
        private System.Windows.Forms.Button buttonLoadOldScript;
        private System.Windows.Forms.TextBox textBoxExecuteStatement;
        private System.Windows.Forms.Button buttonExecuteStatement;
        private System.Windows.Forms.Label labelExecuteStatement;
		private System.Windows.Forms.Label labelFunctionParensClose;
		private System.Windows.Forms.Label labelFunctionParensOpen;
	}
}