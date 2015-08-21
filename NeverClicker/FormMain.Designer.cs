namespace NeverClicker
{
    partial class FormMain
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
            this.buttonAutoInvoke = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox_var = new System.Windows.Forms.TextBox();
            this.buttonMoveMouse = new System.Windows.Forms.Button();
            this.buttonSuspend = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupTests = new System.Windows.Forms.GroupBox();
            this.buttonExecuteFunction = new System.Windows.Forms.Button();
            this.textBoxExecuteFunction = new System.Windows.Forms.TextBox();
            this.labelLog = new System.Windows.Forms.Label();
            this.textBoxExecuteFunctionP1 = new System.Windows.Forms.TextBox();
            this.textBoxExecuteFunctionP2 = new System.Windows.Forms.TextBox();
            this.textBoxExecuteFunctionP3 = new System.Windows.Forms.TextBox();
            this.textBoxExecuteFunctionP4 = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupTests.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAutoInvoke
            // 
            this.buttonAutoInvoke.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAutoInvoke.Location = new System.Drawing.Point(18, 12);
            this.buttonAutoInvoke.Name = "buttonAutoInvoke";
            this.buttonAutoInvoke.Size = new System.Drawing.Size(382, 33);
            this.buttonAutoInvoke.TabIndex = 0;
            this.buttonAutoInvoke.Text = "Begin Auto-Cycle";
            this.buttonAutoInvoke.UseVisualStyleBackColor = true;
            this.buttonAutoInvoke.Click += new System.EventHandler(this.buttonAutoInvoke_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 364);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(388, 192);
            this.textBox1.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(154, 100);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Check Variable";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonCheckVar_Click);
            // 
            // textBox_var
            // 
            this.textBox_var.Location = new System.Drawing.Point(5, 102);
            this.textBox_var.Name = "textBox_var";
            this.textBox_var.Size = new System.Drawing.Size(143, 20);
            this.textBox_var.TabIndex = 4;
            // 
            // buttonMoveMouse
            // 
            this.buttonMoveMouse.Location = new System.Drawing.Point(6, 19);
            this.buttonMoveMouse.Name = "buttonMoveMouse";
            this.buttonMoveMouse.Size = new System.Drawing.Size(105, 23);
            this.buttonMoveMouse.TabIndex = 5;
            this.buttonMoveMouse.Text = "Move Mouse";
            this.buttonMoveMouse.UseVisualStyleBackColor = true;
            this.buttonMoveMouse.Click += new System.EventHandler(this.buttonMoveMouse_Click);
            // 
            // buttonSuspend
            // 
            this.buttonSuspend.Location = new System.Drawing.Point(102, 563);
            this.buttonSuspend.Name = "buttonSuspend";
            this.buttonSuspend.Size = new System.Drawing.Size(84, 23);
            this.buttonSuspend.TabIndex = 6;
            this.buttonSuspend.Text = "Suspend";
            this.buttonSuspend.UseVisualStyleBackColor = true;
            this.buttonSuspend.Click += new System.EventHandler(this.buttonSuspend_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(192, 563);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(84, 23);
            this.buttonReload.TabIndex = 7;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(12, 563);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(84, 23);
            this.buttonStop.TabIndex = 8;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(324, 563);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // groupTests
            // 
            this.groupTests.Controls.Add(this.label3);
            this.groupTests.Controls.Add(this.label2);
            this.groupTests.Controls.Add(this.textBoxExecuteFunctionP4);
            this.groupTests.Controls.Add(this.textBoxExecuteFunctionP3);
            this.groupTests.Controls.Add(this.textBoxExecuteFunctionP2);
            this.groupTests.Controls.Add(this.textBoxExecuteFunctionP1);
            this.groupTests.Controls.Add(this.buttonExecuteFunction);
            this.groupTests.Controls.Add(this.textBoxExecuteFunction);
            this.groupTests.Controls.Add(this.buttonMoveMouse);
            this.groupTests.Controls.Add(this.textBox_var);
            this.groupTests.Controls.Add(this.button3);
            this.groupTests.Location = new System.Drawing.Point(12, 159);
            this.groupTests.Name = "groupTests";
            this.groupTests.Size = new System.Drawing.Size(388, 186);
            this.groupTests.TabIndex = 13;
            this.groupTests.TabStop = false;
            this.groupTests.Text = "Tests";
            this.groupTests.Enter += new System.EventHandler(this.groupTests_Enter);
            // 
            // buttonExecuteFunction
            // 
            this.buttonExecuteFunction.Location = new System.Drawing.Point(154, 157);
            this.buttonExecuteFunction.Name = "buttonExecuteFunction";
            this.buttonExecuteFunction.Size = new System.Drawing.Size(143, 23);
            this.buttonExecuteFunction.TabIndex = 7;
            this.buttonExecuteFunction.Text = "Execute Function";
            this.buttonExecuteFunction.UseVisualStyleBackColor = true;
            this.buttonExecuteFunction.Click += new System.EventHandler(this.buttonExecuteFunction_Click);
            // 
            // textBoxExecuteFunction
            // 
            this.textBoxExecuteFunction.Location = new System.Drawing.Point(6, 131);
            this.textBoxExecuteFunction.Name = "textBoxExecuteFunction";
            this.textBoxExecuteFunction.Size = new System.Drawing.Size(143, 20);
            this.textBoxExecuteFunction.TabIndex = 6;
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(15, 348);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(28, 13);
            this.labelLog.TabIndex = 14;
            this.labelLog.Text = "Log:";
            // 
            // textBoxExecuteFunctionP1
            // 
            this.textBoxExecuteFunctionP1.Location = new System.Drawing.Point(155, 131);
            this.textBoxExecuteFunctionP1.Name = "textBoxExecuteFunctionP1";
            this.textBoxExecuteFunctionP1.Size = new System.Drawing.Size(51, 20);
            this.textBoxExecuteFunctionP1.TabIndex = 8;
            // 
            // textBoxExecuteFunctionP2
            // 
            this.textBoxExecuteFunctionP2.Location = new System.Drawing.Point(212, 131);
            this.textBoxExecuteFunctionP2.Name = "textBoxExecuteFunctionP2";
            this.textBoxExecuteFunctionP2.Size = new System.Drawing.Size(51, 20);
            this.textBoxExecuteFunctionP2.TabIndex = 9;
            // 
            // textBoxExecuteFunctionP3
            // 
            this.textBoxExecuteFunctionP3.Location = new System.Drawing.Point(269, 131);
            this.textBoxExecuteFunctionP3.Name = "textBoxExecuteFunctionP3";
            this.textBoxExecuteFunctionP3.Size = new System.Drawing.Size(51, 20);
            this.textBoxExecuteFunctionP3.TabIndex = 10;
            // 
            // textBoxExecuteFunctionP4
            // 
            this.textBoxExecuteFunctionP4.Location = new System.Drawing.Point(326, 131);
            this.textBoxExecuteFunctionP4.Name = "textBoxExecuteFunctionP4";
            this.textBoxExecuteFunctionP4.Size = new System.Drawing.Size(51, 20);
            this.textBoxExecuteFunctionP4.TabIndex = 11;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 595);
            this.splitter1.TabIndex = 15;
            this.splitter1.TabStop = false;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(8, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 2);
            this.label2.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(8, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(369, 2);
            this.label3.TabIndex = 13;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 595);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.groupTests);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonReload);
            this.Controls.Add(this.buttonSuspend);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonAutoInvoke);
            this.Name = "FormMain";
            this.Text = "NeverClicker - You\'ve never clicked better than this";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupTests.ResumeLayout(false);
            this.groupTests.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAutoInvoke;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox_var;
        private System.Windows.Forms.Button buttonMoveMouse;
        private System.Windows.Forms.Button buttonSuspend;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.GroupBox groupTests;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Button buttonExecuteFunction;
        private System.Windows.Forms.TextBox textBoxExecuteFunction;
        private System.Windows.Forms.TextBox textBoxExecuteFunctionP4;
        private System.Windows.Forms.TextBox textBoxExecuteFunctionP3;
        private System.Windows.Forms.TextBox textBoxExecuteFunctionP2;
        private System.Windows.Forms.TextBox textBoxExecuteFunctionP1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
    }
}