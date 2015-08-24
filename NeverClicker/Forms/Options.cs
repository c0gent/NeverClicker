using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeverClicker.Properties;
using NeverClicker.Forms;

namespace NeverClicker {
	public partial class Options : Form {
		public Options() {
			InitializeComponent();
		}
	
		private void Options_Load(object sender, EventArgs e) {
			textBoxAhkRootPath.Text = Settings.Default["ScriptRootPath"].ToString();
			textBoxNwRootPath.Text = Settings.Default["NeverwinterExeLocation"].ToString();
		}

		private void buttonSave_Click(object sender, EventArgs e) {
			Settings.Default["ScriptRootPath"] = textBoxAhkRootPath.Text;
			Settings.Default["NeverwinterExeLocation"] = textBoxNwRootPath.Text;
			Settings.Default.Save();
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void buttonChooseAhkRootPath_Click(object sender, EventArgs e) {
			folderBrowserDialog1.Description = "Choose NW_Common.ahk folder location";
			//folderBrowserDialog1.RootFolder = textBoxAhkRootPath.Text;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				textBoxAhkRootPath.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void buttonChooseNWGameRootPath_Click(object sender, EventArgs e) {
			openFileDialog1.Title = "Select 'Neverwinter.exe' file location";
			//openFileDialog1.InitialDirectory = "C:\\Program Files (x86)\\";
			openFileDialog1.InitialDirectory = textBoxNwRootPath.Text;
			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				textBoxNwRootPath.Text = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
			}

		}

		private void buttonLaptop_Click(object sender, EventArgs e) {
			Settings.Default["ScriptRootPath"] = "C:\\opt\\AutoHotkey\\AHK_Public\\";
			Settings.Default["NeverwinterExeLocation"] = "C:\\Program Files (x86)\\Neverwinter_en\\";
			Settings.Default.Save();
			Options_Load(sender, e);
		}

		private void buttonPC_Click(object sender, EventArgs e) {
			Settings.Default["ScriptRootPath"] = "A:\\";
			Settings.Default["NeverwinterExeLocation"] = "C:\\Program Files (x86)\\Arc_Neverwinter\\Neverwinter_en\\";
			Settings.Default.Save();
			Options_Load(sender, e);
		}
	}
}
