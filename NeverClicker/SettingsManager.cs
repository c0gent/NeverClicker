using NeverClicker.Forms;
using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeverClicker {
	class SettingsManager {
		const string IMAGES_FOLDER_NAME = "Images";
		const string SETTINGS_FOLDER_NAME = "Settings";
		const string LOGS_FOLDER_NAME = "Logs";
		const string ASSETS_FOLDER_NAME = "Assets";

		const string IMAGES_SUBPATH_DEFAULT = IMAGES_FOLDER_NAME + "\\";
		const string SETTINGS_SUBPATH_DEFAULT = SETTINGS_FOLDER_NAME + "\\";
		const string LOGS_SUBPATH_DEFAULT = LOGS_FOLDER_NAME + "\\";
		const string ASSETS_SUBPATH_DEFAULT = ASSETS_FOLDER_NAME + "\\";

		const string GAME_ACCOUNT_INI_FILE_NAME = "NeverClicker_GameAccount.ini";
		const string GAME_CLIENT_INI_FILE_NAME = "NeverClicker_GameClient.ini";
		const string LOG_FILE_NAME = "NeverClicker_Log.txt";
		const string OLD_AHK_SCRIPT_FILE_NAME = "NW_Common.ahk";

		string NeverClickerUserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NeverClicker";

		public static bool SettingsAreValid() {
			bool settingsValid = true;

			settingsValid &= PatcherExePathIsValid();
			settingsValid &= RootFolderPathIsValid();
			settingsValid &= InitSettingsFiles();
			settingsValid &= InitImages();
			settingsValid &= InitLogs();

			return settingsValid;
		}

		public static bool PatcherExePathIsValid() {
			return File.Exists(Settings.Default.NeverwinterExePath);
		}

		public static bool RootFolderPathIsValid() {
			return Directory.Exists(Settings.Default.UserRootFolderPath);
		}

		public static bool InitSettingsFiles() {

			return false;
		}

		public static bool InitImages() {

			return false;
		}

		public static bool InitLogs() {

			return false;
		}

		public static void Failure() { // ***** DEPRICATE *****
			//if (System.Windows.Forms.Application.MessageLoop) {
			//	// WinForms app
			//	MessageBox.Show("SettingsManager::Failure(): DEBUG: Settings invalid. Exiting application.");
			//	System.Windows.Forms.Application.Exit();
			//}
		}

		public static bool Save() {			
			Settings.Default.Save();
			return SettingsAreValid(); // ***** TEMP *****
		}

	}
}



//public static bool Save() {
//	////if (textBoxSettingsFolder.Text.EndsWith("\\")) {
//	////	Settings.Default["SettingsRootPath"] = textBoxSettingsFolder.Text;
//	////} else {
//	////	Settings.Default["SettingsRootPath"] = textBoxSettingsFolder.Text + "\\";
//	////}



//	//Settings.Default["NeverwinterExePath"] = textBoxNwExePath.Text;

//	//if (checkBoxImagesFolder.Checked) {
//	//	Settings.Default["ImagesFolderPath"] = string.Format("{0}{1}\\", Settings.Default["SettingsRootPath"], IMAGES_FOLDER);
//	//}

//	//if (checkBoxGameClientIni.Checked) {
//	//	Settings.Default["GameAccountIniPath"] = string.Format("{0}{1}", Settings.Default["SettingsRootPath"], GAME_ACCOUNT_INI);
//	//}

//	//if (checkBoxAccountIni.Checked) {
//	//	Settings.Default["GameClientIniPath"] = string.Format("{0}{1}", Settings.Default["SettingsRootPath"], GAME_CLIENT_INI);
//	//}

//	//if (checkBoxLogsPath.Checked) {
//	//	Settings.Default["LogFilePath"] = string.Format("{0}{1}", Settings.Default["SettingsRootPath"], LOG_FILE);
//	//}

//	Settings.Default.NeverwinterExePath = this.textBoxPatcherExePath.Text;
//	Settings.Default.UserRootFolderPath = this.textBoxSettingsFolder.Text;

//	if (SettingsManager.SettingsAreValid()) {
//		SettingsManager.Save();
//		Close();
//	} else {
//		// CREATE THE FILES ETC...
//		MessageBox.Show("Settings Invalid. Please Retry."); // ***** TEMP *****
//		SettingsManager.Failure();
//	}

//	Settings.Default.Save();


//	return SettingsAreValid(); // ***** TEMP *****
//}

//}