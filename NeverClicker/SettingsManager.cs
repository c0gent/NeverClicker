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
using System.Xml;

namespace NeverClicker {
	class SettingsManager {
		const string IMAGES_FOLDER_NAME = "Images";
		const string SETTINGS_FOLDER_NAME = "Settings";
		const string LOGS_FOLDER_NAME = "Logs";
		const string ASSETS_FOLDER_NAME = "Assets";

		public const string IMAGES_SUBPATH = "\\" + IMAGES_FOLDER_NAME;
		public const string SETTINGS_SUBPATH = "\\" + SETTINGS_FOLDER_NAME;
		public const string LOGS_SUBPATH = "\\" + LOGS_FOLDER_NAME;
		public const string ASSETS_SUBPATH = "\\" + ASSETS_FOLDER_NAME;

		public const string BUILTIN_IMAGES_SUBPATH = "\\" + "Default" + IMAGES_FOLDER_NAME;
		public const string BUILTIN_SETTINGS_SUBPATH = "\\" + "Default" + SETTINGS_FOLDER_NAME;
		//public const string DEFAULT_LOGS_SUBPATH = "\\" + LOGS_FOLDER_NAME;
		//public const string DEFAULT_ASSETS_SUBPATH = "\\" + ASSETS_FOLDER_NAME;

		public const string GAME_ACCOUNT_INI_FILE_NAME = "\\NeverClicker_GameAccount.ini";
		public const string GAME_CLIENT_INI_FILE_NAME = "\\NeverClicker_GameClient.ini";
		public const string LOG_FILE_NAME = "\\NeverClicker_Log.txt";
		public const string OLD_AHK_SCRIPT_FILE_NAME = "\\NW_Common.ahk";

		private static readonly object Locker = new object();
		private static XmlDocument LogXmlDoc = new XmlDocument();

		public static string ProgramRootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Lo‌​cation);
		public static string DefaultUserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NeverClicker";


		public SettingsManager() {
			//ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
			//configMap.ExeConfigFilename = @"d:\test\justAConfigFile.config.whateverYouLikeExtension";
			//Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);


		}

		//public static string DefaultUserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NeverClicker";
		

		//public static bool SettingsAreValid() {			
		//	bool settingsValid = true;

		//	settingsValid &= PatcherExePathIsValid();
		//	settingsValid &= UserRootFolderPathIsValid();
		//	settingsValid &= InitSettingsFiles();
		//	settingsValid &= ImagesFolderIsValid();
		//	settingsValid &= LogsFolderIsValid();
			
		//	return settingsValid;
		//}

		

		//public static bool UserRootFolderPathIsValid() {
		//	if (Directory.Exists(Settings.Default.UserRootFolderPath)) {
		//		return true;
		//	} else {
		//		MessageBox.Show("Neverwinter.exe path: '" + Settings.Default.NeverwinterExePath + "' is invalid.");
		//		return false;
		//		//Directory.CreateDirectory(DefaultUserRootFolder);
		//		//return true;
		//	}
		//}

		//public static void InitUserFolders() {
		//	string newFolder = "";

		//	if (Directory.Exists(Settings.Default.UserRootFolderPath)) {
		//		newFolder = Settings.Default.UserRootFolderPath;               
		//	} else {
		//		newFolder = DefaultUserRootFolder;
		//	}

		//	MessageBox.Show("Creating settings folder: '" + Settings.Default.UserRootFolderPath);
		//	try {
		//		Directory.CreateDirectory(newFolder);
		//		Directory.CreateDirectory(newFolder + IMAGES_SUBPATH_DEFAULT);
		//		Directory.CreateDirectory(newFolder + SETTINGS_SUBPATH_DEFAULT);
		//		Directory.CreateDirectory(newFolder + LOGS_SUBPATH_DEFAULT);
		//		Directory.CreateDirectory(newFolder + ASSETS_SUBPATH_DEFAULT);

		//		Settings.Default.UserRootFolderPath = newFolder;
		//		Settings.Default.ImagesFolderPath = newFolder + IMAGES_SUBPATH_DEFAULT;
		//		Settings.Default.SettingsFolderPath = newFolder + SETTINGS_SUBPATH_DEFAULT;
		//		Settings.Default.LogsFolderPath = newFolder + LOGS_SUBPATH_DEFAULT;

		//		Settings.Default.Save();
		//	} catch (Exception ex) {
		//		MessageBox.Show("Error creating folder: " + newFolder + ". Info: " + ex.ToString());
		//	}						
		//}

		public static bool InitSettingsFiles() {
			var settingsFolderPath = Settings.Default.SettingsFolderPath;
			bool valid = true;

			if (Directory.Exists(settingsFolderPath)) {
				if (File.Exists(settingsFolderPath + "\\" + GAME_ACCOUNT_INI_FILE_NAME)) {
					//MessageBox.Show(GAME_ACCOUNT_INI_FILE_NAME + " exists.");					
				} else {
					MessageBox.Show(GAME_ACCOUNT_INI_FILE_NAME + " not found");
					valid = false;
					// CREATE OR COPY INI FILE
				}

				// VERIFY VALIDITY AND RECREATE IF NECESSARY

				if (File.Exists(settingsFolderPath + "\\" + GAME_CLIENT_INI_FILE_NAME)) {
					//MessageBox.Show(GAME_CLIENT_INI_FILE_NAME + " exists.");
				} else {
					MessageBox.Show(GAME_CLIENT_INI_FILE_NAME + " not found");
					valid = false;
					// CREATE OR COPY INI FILE
				}
			} else {
				MessageBox.Show("Error: settings folder: '" + settingsFolderPath + "' not found.");
				valid = false;

				// ACTUALLY CREATE THEM

			}
			// LOAD UP BOTH/ALL INI FILES AND VERIFY THAT THEY ARE VALID;
			//	IF THEY ARE NOT VALID:
			//		CREATE THE DIRECTORIES
			//		CREATE THE FILES
			//		CREATE THE SECTIONS
			return valid;
		}

		//public void CreateUserRootSubpaths() {

		//}

		public static bool ImagesFolderIsValid() {
			if (Directory.Exists(Settings.Default.ImagesFolderPath)) {
				return true;
			} else {
				MessageBox.Show("Images folder does not exist.");
				// CREATE DIRECTORY
				// COPY IMAGE FILES
				// VERIFY THEIR EXISTENCE
				// RETURN TRUE;
				return false;
			}
			// CHECK THAT IMAGE FILES EXIST
			// PARSE INI?			
		}

		public static bool LogsFolderIsValid() {
			//return LogFile.InitLogFile(Settings.Default.LogsFolderPath);
			if (Directory.Exists(Settings.Default.LogsFolderPath)) {
				return true;
			} else {
				MessageBox.Show("Logs folder does not exist.");
				return false;
			}
		}

		//public static void Failure() { // ***** DEPRICATE *****
		//	//if (System.Windows.Forms.Application.MessageLoop) {
		//	//	// WinForms app
		//	//	MessageBox.Show("SettingsManager::Failure(): DEBUG: Settings invalid. Exiting application.");
		//	//	System.Windows.Forms.Application.Exit();
		//	//}
		//}

		//public static bool Save() {
		//	if (SettingsAreValid()) {
		//		Settings.Default.Save();
		//		return true;
		//	} else {
		//		try {
		//			Directory.CreateDirectory(Settings.Default.LogsFolderPath);
		//			Settings.Default.Save();
		//			return true;
		//		} catch (Exception ex) {
		//			MessageBox.Show("Unable to save settings -- Error creating folders: " + ex.ToString());
		//			return false;
		//		}			
		//	}
		//}

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