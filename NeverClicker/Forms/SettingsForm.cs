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
using System.IO;

namespace NeverClicker {
	public partial class SettingsForm : Form {
		public SettingsForm() {
			InitializeComponent();
		}
	
		private void Options_Load(object sender, EventArgs e) {
			this.textBoxPatcherExePath.Text = Settings.Default.NeverwinterExePath.ToString();
			this.textBoxUserRootFolder.Text = Settings.Default.UserRootFolderPath.ToString();			
			this.textBoxImagesFolder.Text = Settings.Default.ImagesFolderPath.ToString();
			this.textBoxSettingsFolder.Text = Settings.Default.SettingsFolderPath.ToString();
			this.textBoxLogsPath.Text = Settings.Default.LogsFolderPath.ToString();
		}

		//public static bool VerifyOptions() {
		//	//var settingsRootPath = Settings.Default["SettingsRootPath"].ToString();
		//	//var nwRootPath = Settings.Default["NeverwinterExePath"].ToString();
		//	//var imagesPath = Settings.Default["ImagesFolderPath"].ToString();
		//	//var accountIni = Settings.Default["GameAccountIniPath"].ToString();
		//	//var gameClientIni = Settings.Default["GameClientIniPath"].ToString();
		//	//var logFilePath = Settings.Default["LogFilePath"].ToString();


		//	return false; // ***** TEMP *****
		//}

		private void buttonSave_Click(object sender, EventArgs e) {
			Settings.Default.NeverwinterExePath = this.textBoxPatcherExePath.Text;
			Settings.Default.UserRootFolderPath = this.textBoxSettingsFolder.Text;

			if (SettingsManager.Save()) {
				Close();
			} else {
				MessageBox.Show("Settings Invalid. Please Retry."); // ***** TEMP *****
				SettingsManager.Failure(); // ***** DEPRICATE *****
			}			
		}

		private void buttonCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void buttonUserRootFolder_Click(object sender, EventArgs e) {
			folderBrowserDialog1.Description = "Choose settings folder location";
			//folderBrowserDialog1.RootFolder = textBoxAhkRootPath.Text;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				if (Directory.Exists(textBoxSettingsFolder.Text)) {
					textBoxSettingsFolder.Text = folderBrowserDialog1.SelectedPath;
				} else {
					MessageBox.Show("Settings folder does not exist. Please choose a valid folder.");
				}
			}
		}

		//private void validateSettingsRootPath()

		private void buttonChooseNWGameRootPath_Click(object sender, EventArgs e) {
			

		}

		private void buttonLaptop_Click(object sender, EventArgs e) {
			Settings.Default["SettingsRootPath"] = "C:\\opt\\AutoHotkey\\AHK_Public\\";
			Settings.Default["NeverwinterExePath"] = "C:\\Program Files (x86)\\Neverwinter_en\\";
			Settings.Default.Save();
			Options_Load(sender, e);
		}

		private void buttonPC_Click(object sender, EventArgs e) {
			Settings.Default["SettingsRootPath"] = "A:\\";
			Settings.Default["NeverwinterExePath"] = "C:\\Program Files (x86)\\Arc_Neverwinter\\Neverwinter_en\\";
			Settings.Default.Save();
			Options_Load(sender, e);
		}

		private void tabPagePaths_Click(object sender, EventArgs e) {

		}

		private void buttonPatcherExePath_Click(object sender, EventArgs e) {
			openFileDialog1.Title = "Select 'Neverwinter.exe' file location";
			openFileDialog1.CheckFileExists = true;

			if (File.Exists(textBoxPatcherExePath.Text)) {
				openFileDialog1.InitialDirectory = textBoxPatcherExePath.Text;
			} else {
				openFileDialog1.InitialDirectory = "C:\\Program Files (x86)\\";
			}

			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				textBoxPatcherExePath.Text = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
			}
		}
	}
}
