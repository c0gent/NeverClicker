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
using System.Xml;

namespace NeverClicker {
	public partial class SettingsForm: Form {
		MainForm MainForm;

		const string IMAGES_FOLDER_NAME = "Images";
		const string SETTINGS_FOLDER_NAME = "Settings";
		const string LOGS_FOLDER_NAME = "Logs";

		const string BUILTIN_ASSETS_FOLDER_NAME = "Assets";
		const string BUILTIN_DEFAULTS_FOLDER_NAME = BUILTIN_ASSETS_FOLDER_NAME + "\\" + "Default";

		public const string BUILTIN_IMAGES_SUBPATH = "\\" + BUILTIN_DEFAULTS_FOLDER_NAME + "\\" + IMAGES_FOLDER_NAME;
		public const string BUILTIN_SETTINGS_SUBPATH = "\\" + BUILTIN_DEFAULTS_FOLDER_NAME + "\\" + SETTINGS_FOLDER_NAME;

		public const string IMAGES_SUBPATH = "\\" + IMAGES_FOLDER_NAME;
		public const string SETTINGS_SUBPATH = "\\" + SETTINGS_FOLDER_NAME;
		public const string LOGS_SUBPATH = "\\" + LOGS_FOLDER_NAME;

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


		//public SettingsForm() {
		//	InitializeComponent();
		//}


		public SettingsForm(MainForm mainForm) {
			InitializeComponent();
			this.MainForm = mainForm;

			Settings.Default.Upgrade();

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
			//MessageBox.Show(this, config.FilePath);
			this.linkLabelUserConfigFile.Text = config.FilePath;
			this.linkLabelAccountIniFile.Text = Settings.Default.SettingsFolderPath;
			this.linkLabelClientIniFile.Text = Settings.Default.SettingsFolderPath;

			if (Settings.Default.NeverClickerConfigValid) {
				this.textBoxPatcherExePath.Text = Settings.Default.NeverwinterExePath;
				this.textBoxUserRootFolder.Text = Settings.Default.UserRootFolderPath;
				this.textBoxImagesFolder.Text = Settings.Default.ImagesFolderPath;
				this.textBoxSettingsFolder.Text = Settings.Default.SettingsFolderPath;
				this.textBoxLogsFolder.Text = Settings.Default.LogsFolderPath;
				this.textBoxImageShadeVariation.Text = Settings.Default.ImageShadeVariation.ToString();

			} else if (Settings.Default.NeverClickerFirstRun) {
				this.textBoxPatcherExePath.Text = "";
				this.textBoxUserRootFolder.Text = DefaultUserRootFolder;
				this.textBoxImagesFolder.Text = this.textBoxUserRootFolder.Text + IMAGES_SUBPATH;
				this.textBoxSettingsFolder.Text = this.textBoxUserRootFolder.Text + SETTINGS_SUBPATH;
				this.textBoxLogsFolder.Text = this.textBoxUserRootFolder.Text + LOGS_SUBPATH;
				this.textBoxImageShadeVariation.Text = "60";
			}

			this.checkBoxLogDebug.Checked = Settings.Default.LogDebugMessages;
		}

		private void SettingsForm_Shown(object sender, EventArgs e) {
			this.checkBoxBeginOnStartup.Checked = Settings.Default.BeginOnStartup;
		}

		private void SettingsForm_Load(object sender, EventArgs e) { }

		public bool ValidateNeverwinterExePath() {
			if (File.Exists(this.textBoxPatcherExePath.Text)) {
				return true;
			} else {
				MessageBox.Show(this, "Neverwinter.exe path: '" + this.textBoxPatcherExePath.Text + "' is invalid.");
				return false;
			}
		}

		private bool ValidateCreateFolder(string textBoxText, bool isDefault) {
			return ValidateCreateFolder(textBoxText, isDefault, false, "");
		}

		private bool ValidateCreateFolder(string textBoxText, bool isDefault, bool copyBuiltin, string builtinSubpath) {
			if (Directory.Exists(textBoxText)) {
				return true;
			} else {
				//if (Settings.Default.NeverClickerFirstRun && isDefault) {
				//	if (copyBuiltin) {
				//		DirectoryCopy(ProgramRootFolder + builtinSubpath,
				//			textBoxText, true);
				//	} else {
				//		Directory.CreateDirectory(textBoxText);
				//	}

				//	return true;
				//} else {
				//	MessageBox.Show(this, "Folder path: '" + textBoxText + "' not found.");
				//	return false;
				//}

				// If directory doesn't exist, just create it:
				if (copyBuiltin) {
					DirectoryCopy(ProgramRootFolder + builtinSubpath,
					textBoxText, true);
				} else {
					Directory.CreateDirectory(textBoxText);
				}

				return true;
			}
		}

		public bool ValidateUserRootFolderPath() {
			return this.ValidateCreateFolder(this.textBoxUserRootFolder.Text, Settings.Default.UserRootFolderPathIsDefault);
		}

		public bool ValidateSettingsFolderPath() {
			return this.ValidateCreateFolder(this.textBoxSettingsFolder.Text, 
				Settings.Default.SettingsFolderPathIsDefault, 
				true,
				SETTINGS_SUBPATH
            );
		}

		public bool ValidateImagesFolderPath() {
			return this.ValidateCreateFolder(
				this.textBoxImagesFolder.Text,
				Settings.Default.ImagesFolderPathIsDefault,
				true,
				IMAGES_SUBPATH
            );
		}

		public bool ValidateLogsFolderPath() {
			return this.ValidateCreateFolder(this.textBoxLogsFolder.Text, Settings.Default.LogsFolderPathIsDefault);
		}

		public bool ValidateImageShadeVariation() {
			ushort imageShadeVariation = 0;
			bool parseSuccess = ushort.TryParse(this.textBoxImageShadeVariation.Text, out imageShadeVariation);

			if ((parseSuccess) && (imageShadeVariation <= 255)) {
				return true;
			} else {
				MessageBox.Show(this, "Image shade variation must be a number between 0 and 255.");
				return false;
			}
		}

		
		public bool ValidateAllSettings() {
			bool settingsValid = true;

			settingsValid &= ValidateNeverwinterExePath();

			if (ValidateUserRootFolderPath()) {
				settingsValid &= ValidateSettingsFolderPath();
				settingsValid &= ValidateImagesFolderPath();
				settingsValid &= ValidateLogsFolderPath();
			} else {
				settingsValid = false;
			}

			settingsValid &= ValidateImageShadeVariation();

			return settingsValid;
		}		


		private void buttonSave_Click(object sender, EventArgs e) {
			//ushort imageShadeVariation = 0;
			//bool parseSuccess = ushort.TryParse(this.textBoxImageShadeVariation.Text, out imageShadeVariation);

			//if ((parseSuccess) && (imageShadeVariation <= 255)) {
			//	Settings.Default.ImageShadeVariation = imageShadeVariation;
			//} else {
			//	MessageBox.Show(this, "Image shade variation must be a number between 0 and 255.");
			//	saveSuccess = false;
			//}

			//if (!this.Save()) {
			//	saveSuccess = false;
			//}

			bool saveSuccess = this.Save();

			if (saveSuccess) {
				//Settings.Default.Save();				
				MainForm.ReloadSettings();
				Close();
			} 
			//else {
			//	// ***** BUILD SETTINGS FILES *****
			//	//MessageBox.Show(this, "Settings invalid. Can not save."); // ***** TEMP *****
			//	//Failure(); // ***** DEPRICATE *****
			//}
		}

		public bool Save() {
			if (this.ValidateAllSettings()) {
				Settings.Default.NeverwinterExePath = this.textBoxPatcherExePath.Text;
				Settings.Default.UserRootFolderPath = this.textBoxUserRootFolder.Text;
				Settings.Default.ImagesFolderPath = this.textBoxImagesFolder.Text;
				Settings.Default.SettingsFolderPath = this.textBoxSettingsFolder.Text;
				Settings.Default.LogsFolderPath = this.textBoxLogsFolder.Text;

				ushort imageShadeVariation = 0;
				ushort.TryParse(this.textBoxImageShadeVariation.Text, out imageShadeVariation);
				Settings.Default.ImageShadeVariation = imageShadeVariation;

				Settings.Default.NeverClickerFirstRun = false;
				Settings.Default.NeverClickerConfigValid = true;

				Settings.Default.Save();
				return true;
			} else {
				MessageBox.Show(this, "Unable to save settings. Please try again.", "NeverClicker Settings Error");
				return false;		
			}
		}


		private void buttonCancel_Click(object sender, EventArgs e) {
			Settings.Default.Reload();
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
					//InitUserFolders();
				} else {
					MessageBox.Show(this, "Settings folder does not exist. Please choose a valid folder.", "NeverClicker Error");
				}
			}
		}


		private void checkBoxUserRootFolder_CheckedChanged(object sender, EventArgs e) {
			var chkd = this.checkBoxUserRootFolder.Checked;

			// TODO: CLEAN THIS UP LATER --- CHECKBOXES FOR THESE ITEMS ARE CURRENTLY PRETTY IRRELEVANT
			// this.checkBoxSettingsFolder.Enabled = !chkd;
			// this.checkBoxImagesFolder.Enabled = !chkd;
			// this.checkBoxLogsFolder.Enabled = !chkd;
			


			this.textBoxUserRootFolder.ReadOnly = chkd;
			this.textBoxUserRootFolder.Enabled = !chkd;
			this.buttonUserRootFolder.Enabled = !chkd;

			if (chkd) {
				this.textBoxUserRootFolder.Text = DefaultUserRootFolder;
			}
		}

		private void textBoxUserRootFolder_TextChanged(object sender, EventArgs e) {
			this.textBoxImagesFolder.Text = this.textBoxUserRootFolder.Text + IMAGES_SUBPATH;
			this.textBoxSettingsFolder.Text = this.textBoxUserRootFolder.Text + SETTINGS_SUBPATH;
			this.textBoxLogsFolder.Text = this.textBoxUserRootFolder.Text + LOGS_SUBPATH;
		}

		private void linkLabelUserConfigFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(Path.GetDirectoryName(linkLabelUserConfigFile.Text));
		}

		public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs) {
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);
			DirectoryInfo[] dirs = dir.GetDirectories();

			if (!dir.Exists) {
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			// If the destination directory doesn't exist, create it. 
			if (!Directory.Exists(destDirName)) {
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files) {
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, false);
			}

			// If copying subdirectories, copy them and their contents to new location. 
			if (copySubDirs) {
				foreach (DirectoryInfo subdir in dirs) {
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}

		private void checkBoxLogDebug_CheckedChanged(object sender, EventArgs e) {
			Settings.Default.LogDebugMessages = this.checkBoxLogDebug.Checked;
			//Settings.Default.Save();
		}

		private void linkLabelAccountIniFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(linkLabelAccountIniFile.Text);
		}

		private void linkLabelClientIniFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(linkLabelClientIniFile.Text);
		}

		private void checkBoxBeginOnStartup_CheckedChanged(object sender, EventArgs e) {
			Settings.Default.BeginOnStartup = this.checkBoxBeginOnStartup.Checked;
			Settings.Default.Save();
		}		


		//private void checkBoxSettingsFolder_CheckedChanged(object sender, EventArgs e) {
		//	bool boxChecked = this.checkBoxSettingsFolder.Checked;
		//  this.textBoxSettingsFolder.ReadOnly = boxChecked;
		//	this.buttonSettingsFolder.Enabled = !boxChecked;

		//	if (boxChecked) {
		//		this.textBoxSettingsFolder.Text = DefaultUserRootFolder + SETTINGS_SUBPATH_DEFAULT;
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



//// ##### PATCHER #####
//if (PatcherExePathIsValid()) {
//	this.textBoxPatcherExePath.Text = Settings.Default.NeverwinterExePath;
//} else {
//	this.textBoxPatcherExePath.Text = "";
//}

//// ##### USER ROOT #####
//if (Directory.Exists(Settings.Default.UserRootFolderPath)) {
//	this.textBoxUserRootFolder.Text = Settings.Default.UserRootFolderPath;
//} else {
//	this.textBoxUserRootFolder.Text = DefaultUserRootFolder;
//}

//// ##### IMAGES #####
////if (Directory.Exists(Settings.Default.UserRootFolderPath)) {
////	this.textBoxImagesFolder.Text = Settings.Default.ImagesFolderPath;
////} else {
////	this.textBoxImagesFolder.Text = this.textBoxUserRootFolder.Text + IMAGES_SUBPATH_DEFAULT;
////}
//if (ImagesFolderIsValid()) {
//	this.textBoxImagesFolder.Text = Settings.Default.ImagesFolderPath;
//} else {
//	this.textBoxImagesFolder.Text = this.textBoxUserRootFolder.Text + IMAGES_SUBPATH_DEFAULT;
//}

//// ##### SETTINGS #####
//if (Directory.Exists(Settings.Default.SettingsFolderPath)) {
//	this.textBoxSettingsFolder.Text = Settings.Default.SettingsFolderPath;
//} else {
//	this.textBoxSettingsFolder.Text = this.textBoxUserRootFolder.Text + SETTINGS_SUBPATH_DEFAULT;
//}
////this.textBoxSettingsFolder.Text = this.textBoxUserRootFolder.Text + SETTINGS_SUBPATH_DEFAULT;

//// ##### LOGS #####
//if (Directory.Exists(Settings.Default.LogsFolderPath)) {
//	this.textBoxLogsFolder.Text = Settings.Default.LogsFolderPath;
//} else {
//	this.textBoxLogsFolder.Text = this.textBoxUserRootFolder.Text + LOGS_SUBPATH_DEFAULT;
//}
////this.textBoxLogsFolder.Text = this.textBoxUserRootFolder.Text + LOGS_SUBPATH_DEFAULT;


//this.textBoxImageShadeVariation.Text = Settings.Default.ImageShadeVariation.ToString();