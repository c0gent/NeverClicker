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
		const string IMAGES_FOLDER = "NeverClicker_Images";
		const string GAME_ACCOUNT_INI = "NeverClicker_GameAccount.ini";
		const string GAME_CLIENT_INI = "NeverClicker_GameClient.ini";
		const string LOG_FILE = "NeverClicker_Log.txt";

		public Options() {
			InitializeComponent();
		}
	
		private void Options_Load(object sender, EventArgs e) {
			textBoxSettingsRootPath.Text = Settings.Default["SettingsRootPath"].ToString();
			textBoxNwRootPath.Text = Settings.Default["NeverwinterExePath"].ToString();
			textBoxImagesFolder.Text = Settings.Default["ImagesFolderPath"].ToString();
			textBoxAccountIni.Text = Settings.Default["GameAccountIniPath"].ToString();
			textBoxGameClientIni.Text = Settings.Default["GameClientIniPath"].ToString();
			textBoxLogFilePath.Text = Settings.Default["LogFilePath"].ToString();
		}

		private void buttonSave_Click(object sender, EventArgs e) {
			Settings.Default["SettingsRootPath"] = textBoxSettingsRootPath.Text;
			Settings.Default["NeverwinterExePath"] = textBoxNwRootPath.Text;

			if (checkBoxImagesFolder.Checked) {
				Settings.Default["ImagesFolderPath"] = string.Format("{0}{1}{2}", Settings.Default["SettingsRootPath"], IMAGES_FOLDER, "\\");
			}

			if (checkBoxGameClientIni.Checked) {
				Settings.Default["GameAccountIniPath"] = string.Format("{0}{1}", Settings.Default["SettingsRootPath"], GAME_ACCOUNT_INI);
			}

			if (checkBoxAccountIni.Checked) {
				Settings.Default["GameClientIniPath"] = string.Format("{0}{1}", Settings.Default["SettingsRootPath"], GAME_CLIENT_INI);
			}

			if (checkBoxLogFilePath.Checked) {
				Settings.Default["LogFilePath"] = string.Format("{0}{1}", Settings.Default["SettingsRootPath"], LOG_FILE);
			}

			Settings.Default.Save();
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void buttonChooseAhkRootPath_Click(object sender, EventArgs e) {
			folderBrowserDialog1.Description = "Choose settings folder location";
			//folderBrowserDialog1.RootFolder = textBoxAhkRootPath.Text;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				textBoxSettingsRootPath.Text = folderBrowserDialog1.SelectedPath;
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

	}
}
