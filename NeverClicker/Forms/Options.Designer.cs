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
            this.textBoxAhkRootPath = new System.Windows.Forms.TextBox();
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
            this.SuspendLayout();
            // 
            // labelAhkRootPath
            // 
            this.labelAhkRootPath.AutoSize = true;
            this.labelAhkRootPath.Location = new System.Drawing.Point(14, 15);
            this.labelAhkRootPath.Name = "labelAhkRootPath";
            this.labelAhkRootPath.Size = new System.Drawing.Size(121, 13);
            this.labelAhkRootPath.TabIndex = 0;
            this.labelAhkRootPath.Text = "NW_Common.ahk path:";
            // 
            // textBoxAhkRootPath
            // 
            this.textBoxAhkRootPath.Location = new System.Drawing.Point(141, 12);
            this.textBoxAhkRootPath.Name = "textBoxAhkRootPath";
            this.textBoxAhkRootPath.Size = new System.Drawing.Size(442, 20);
            this.textBoxAhkRootPath.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(508, 227);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(589, 227);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxNwRootPath
            // 
            this.textBoxNwRootPath.Location = new System.Drawing.Point(141, 38);
            this.textBoxNwRootPath.Name = "textBoxNwRootPath";
            this.textBoxNwRootPath.Size = new System.Drawing.Size(442, 20);
            this.textBoxNwRootPath.TabIndex = 5;
            // 
            // labelNwRootPath
            // 
            this.labelNwRootPath.AutoSize = true;
            this.labelNwRootPath.Location = new System.Drawing.Point(14, 41);
            this.labelNwRootPath.Name = "labelNwRootPath";
            this.labelNwRootPath.Size = new System.Drawing.Size(111, 13);
            this.labelNwRootPath.TabIndex = 4;
            this.labelNwRootPath.Text = "Neverwinter.exe path:";
            // 
            // buttonChooseAhkRootPath
            // 
            this.buttonChooseAhkRootPath.Location = new System.Drawing.Point(580, 10);
            this.buttonChooseAhkRootPath.Name = "buttonChooseAhkRootPath";
            this.buttonChooseAhkRootPath.Size = new System.Drawing.Size(75, 23);
            this.buttonChooseAhkRootPath.TabIndex = 6;
            this.buttonChooseAhkRootPath.Text = "browse...";
            this.buttonChooseAhkRootPath.UseVisualStyleBackColor = true;
            this.buttonChooseAhkRootPath.Click += new System.EventHandler(this.buttonChooseAhkRootPath_Click);
            // 
            // buttonChooseNWGameRootPath
            // 
            this.buttonChooseNWGameRootPath.Location = new System.Drawing.Point(580, 36);
            this.buttonChooseNWGameRootPath.Name = "buttonChooseNWGameRootPath";
            this.buttonChooseNWGameRootPath.Size = new System.Drawing.Size(75, 23);
            this.buttonChooseNWGameRootPath.TabIndex = 7;
            this.buttonChooseNWGameRootPath.Text = "browse...";
            this.buttonChooseNWGameRootPath.UseVisualStyleBackColor = true;
            this.buttonChooseNWGameRootPath.Click += new System.EventHandler(this.buttonChooseNWGameRootPath_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonPC
            // 
            this.buttonPC.Location = new System.Drawing.Point(236, 227);
            this.buttonPC.Name = "buttonPC";
            this.buttonPC.Size = new System.Drawing.Size(131, 23);
            this.buttonPC.TabIndex = 8;
            this.buttonPC.Text = "Load PC Defaults";
            this.buttonPC.UseVisualStyleBackColor = true;
            this.buttonPC.Click += new System.EventHandler(this.buttonPC_Click);
            // 
            // buttonLaptop
            // 
            this.buttonLaptop.Location = new System.Drawing.Point(93, 227);
            this.buttonLaptop.Name = "buttonLaptop";
            this.buttonLaptop.Size = new System.Drawing.Size(137, 23);
            this.buttonLaptop.TabIndex = 9;
            this.buttonLaptop.Text = "Load Laptop Defaults";
            this.buttonLaptop.UseVisualStyleBackColor = true;
            this.buttonLaptop.Click += new System.EventHandler(this.buttonLaptop_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 262);
            this.Controls.Add(this.buttonLaptop);
            this.Controls.Add(this.buttonPC);
            this.Controls.Add(this.buttonChooseNWGameRootPath);
            this.Controls.Add(this.buttonChooseAhkRootPath);
            this.Controls.Add(this.textBoxNwRootPath);
            this.Controls.Add(this.labelNwRootPath);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxAhkRootPath);
            this.Controls.Add(this.labelAhkRootPath);
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAhkRootPath;
        private System.Windows.Forms.TextBox textBoxAhkRootPath;
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
    }
}