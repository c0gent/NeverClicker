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
using System.Configuration;
using System.Diagnostics;

namespace NeverClicker {
	public partial class SettingsForm : Form {
		MainForm MainForm;

		public SettingsForm(MainForm mainForm) {
			InitializeComponent();
			this.MainForm = mainForm;
		}
	
		private void Options_Load(object sender, EventArgs e) {
			//this.textBoxPatcherExePath.Text = Settings.Default.NeverwinterExePath.ToString();
			//this.textBoxUserRootFolder.Text = Settings.Default.UserRootFolderPath.ToString();			
			//this.textBoxImagesFolder.Text = Settings.Default.ImagesFolderPath.ToString();
			//this.textBoxSettingsFolder.Text = Settings.Default.SettingsFolderPath.ToString();
			//this.textBoxLogsPath.Text = Settings.Default.LogsFolderPath.ToString();
			
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
			this.linkLabelUserConfigFile.Text = config.FilePath;

			// ##### PATCHER #####
			if (SettingsManager.PatcherExePathIsValid()) {
				this.textBoxPatcherExePath.Text = Settings.Default.NeverwinterExePath;
			} else {
				this.textBoxPatcherExePath.Text = "";
            }

			// ##### USER ROOT #####
			if (Directory.Exists(Settings.Default.UserRootFolderPath)) {
				this.textBoxUserRootFolder.Text = Settings.Default.UserRootFolderPath;
			} else {
				this.textBoxUserRootFolder.Text = SettingsManager.DefaultUserRootFolder;
			}

			// ##### IMAGES #####
			//if (Directory.Exists(Settings.Default.UserRootFolderPath)) {
			//	this.textBoxImagesFolder.Text = Settings.Default.ImagesFolderPath;
			//} else {
			//	this.textBoxImagesFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.IMAGES_SUBPATH_DEFAULT;
			//}
			this.textBoxImagesFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.IMAGES_SUBPATH_DEFAULT;

			// ##### SETTINGS #####
			//if (Directory.Exists(Settings.Default.SettingsFolderPath)) {
			//	this.textBoxSettingsFolder.Text = Settings.Default.SettingsFolderPath;
			//} else {
			//	this.textBoxSettingsFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.SETTINGS_SUBPATH_DEFAULT;
			//}
			this.textBoxSettingsFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.SETTINGS_SUBPATH_DEFAULT;

			// ##### LOGS #####
			//if (Directory.Exists(Settings.Default.LogsFolderPath)) {
			//	this.textBoxLogsFolder.Text = Settings.Default.LogsFolderPath;
			//} else {
			//	this.textBoxLogsFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.LOGS_SUBPATH_DEFAULT;
			//}
			this.textBoxLogsFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.LOGS_SUBPATH_DEFAULT;
		}


		private void buttonSave_Click(object sender, EventArgs e) {
			Settings.Default.NeverwinterExePath = this.textBoxPatcherExePath.Text;
			Settings.Default.UserRootFolderPath = this.textBoxUserRootFolder.Text;
			Settings.Default.ImagesFolderPath = this.textBoxImagesFolder.Text;
			Settings.Default.SettingsFolderPath = this.textBoxSettingsFolder.Text;
			Settings.Default.LogsFolderPath = this.textBoxLogsFolder.Text;

			//Settings.Default.Save();

			if (SettingsManager.Save()) {
				Settings.Default.Save();								
				Close();
				MainForm.ReloadSettings();
			} else {
				// ***** BUILD SETTINGS FILES *****
				MessageBox.Show("Settings invalid. Can not save."); // ***** TEMP *****
				SettingsManager.Failure(); // ***** DEPRICATE *****
			}			
		}


		private void buttonCancel_Click(object sender, EventArgs e) {
			Close();
		}


		private void buttonPatcherExePath_Click(object sender, EventArgs e) {
			openFileDialog1.Title = "Select 'Neverwinter.exe' file location";
			//"Text files (*.txt)|*.txt|All files (*.*)|*.*"
			openFileDialog1.Filter = "Neverwinter Patcher (Neverwinter.exe)|Neverwinter.exe";
			openFileDialog1.CheckFileExists = true;

			if (File.Exists(textBoxPatcherExePath.Text)) {
				openFileDialog1.InitialDirectory = textBoxPatcherExePath.Text;
			} else {
				openFileDialog1.InitialDirectory = "C:\\Program Files (x86)\\";
			}

			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				//textBoxPatcherExePath.Text = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
				textBoxPatcherExePath.Text = openFileDialog1.FileName;
			}
		}


		private void buttonUserRootFolder_Click(object sender, EventArgs e) {
			folderBrowserDialog1.Description = "Choose settings folder location";
			//folderBrowserDialog1.RootFolder = textBoxAhkRootPath.Text;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				if (Directory.Exists(textBoxSettingsFolder.Text)) {
					textBoxUserRootFolder.Text = folderBrowserDialog1.SelectedPath;
					Settings.Default.UserRootFolderPath = folderBrowserDialog1.SelectedPath;
					//SettingsManager.InitUserFolders();
				} else {
					MessageBox.Show("Settings folder does not exist. Please choose a valid folder.");
				}
			}
		}
				

		private void checkBoxUserRootFolder_CheckedChanged(object sender, EventArgs e) {
			this.textBoxUserRootFolder.ReadOnly = this.checkBoxUserRootFolder.Checked;
			this.buttonUserRootFolder.Enabled = !this.checkBoxUserRootFolder.Checked;

			if (this.checkBoxUserRootFolder.Checked) {
				this.textBoxUserRootFolder.Text = SettingsManager.DefaultUserRootFolder;
			}
        }

		private void textBoxUserRootFolder_TextChanged(object sender, EventArgs e) {
			this.textBoxImagesFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.IMAGES_SUBPATH_DEFAULT;
			this.textBoxSettingsFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.SETTINGS_SUBPATH_DEFAULT;
			this.textBoxLogsFolder.Text = this.textBoxUserRootFolder.Text + SettingsManager.LOGS_SUBPATH_DEFAULT;
		}

		private void linkLabelUserConfigFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(Path.GetDirectoryName(linkLabelUserConfigFile.Text));
		}



		//private void checkBoxSettingsFolder_CheckedChanged(object sender, EventArgs e) {
		//	bool boxChecked = this.checkBoxSettingsFolder.Checked;

		//          this.textBoxSettingsFolder.ReadOnly = boxChecked;
		//	this.buttonSettingsFolder.Enabled = !boxChecked;

		//	if (boxChecked) {
		//		this.textBoxSettingsFolder.Text = SettingsManager.DefaultUserRootFolder + SettingsManager.SETTINGS_SUBPATH_DEFAULT;
		//	}
		//}

		//private void validateSettingsRootPath()

		//private void buttonLaptop_Click(object sender, EventArgs e) {
		//	Settings.Default["SettingsRootPath"] = "C:\\opt\\AutoHotkey\\AHK_Public\\";
		//	Settings.Default["NeverwinterExePath"] = "C:\\Program Files (x86)\\Neverwinter_en\\";
		//	Settings.Default.Save();
		//	Options_Load(sender, e);
		//}

		//private void buttonPC_Click(object sender, EventArgs e) {
		//	Settings.Default["SettingsRootPath"] = "A:\\";
		//	Settings.Default["NeverwinterExePath"] = "C:\\Program Files (x86)\\Arc_Neverwinter\\Neverwinter_en\\";
		//	Settings.Default.Save();
		//	Options_Load(sender, e);
		//}
	}
}
