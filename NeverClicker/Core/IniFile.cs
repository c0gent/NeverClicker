using IniParser;
using IniParser.Model;
using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeverClicker {
	public class IniFile {
		private FileIniDataParser Parser = new FileIniDataParser();
		private IniData Data;
		private string IniFileName;

		public IniFile(string fileName) {
			// ***** ADD A CHECK TO MAKE SURE FILES EXIST AND THROW EXCEPTION HERE RATHER THAN LATER *****
			if (File.Exists(fileName)) {
				//MessageBox.Show(fileName + " exists.");
				IniFileName = fileName;
			} else {
				var builtinSettingsFolder = SettingsForm.ProgramRootFolder + SettingsForm.BUILTIN_SETTINGS_SUBPATH;
				//SettingsForm.DirectoryCopy(builtinSettingsFolder, Settings.Default.SettingsFolderPath, false);
				File.Copy(builtinSettingsFolder + "\\" + IniFileName, 
					Settings.Default.SettingsFolderPath + "\\" + IniFileName);
				//MessageBox.Show("Cannot find default settings file: " +  fileName + 
				//	" does not exist.", "NeverClicker Settings File Error");
				return;
			}

			try {
				Data = Parser.ReadFile(IniFileName);
			} catch (Exception ex) {
				MessageBox.Show("Error reading ini file: '" + IniFileName + "' -- Error information: " + ex.ToString());
				return;
			}
		}

		public bool TryGetSetting(string settingName, string sectionName, out string settingVal) {
			try {
				settingVal = Data[sectionName][settingName].Trim();
				if (string.IsNullOrWhiteSpace(settingVal)) {
					settingVal = "";
					return false;
				} else {
					return true;
				}
			} catch (NullReferenceException) {
				settingVal = "";
				return false;
			}
		}

		public string GetSettingOrEmpty(string settingName, string sectionName) {
			string settingVal = null;

			try {
				settingVal = Data[sectionName][settingName];
			} catch (NullReferenceException) {
				settingVal = null;
			}

			if (settingVal != null) {
				return settingVal.Trim();					
			} else {
				return "";
			}				
		}

		public int GetSettingOrZero(string settingName, string sectionName) {
			//IniData data;

			//try {
			//	Data = Parser.ReadFile(IniFileName);
			//} catch (Exception) {
			//	MessageBox.Show("Problem loading ini files. Please check settings.");
			//	return 0;
			//}
			
			int number;
			string settingVal = null;

			try {
				settingVal = Data[sectionName][settingName];
			} catch (NullReferenceException) {
				settingVal = null;
			}

			if (settingVal == null) {
				return 0;
			}			

			if (int.TryParse(Data[sectionName][settingName], out number)) {
				return number;
			} else {
				return 0;
			}				
		}

		public bool SettingExists(string settingName, string sectionName) {
			string settingVal = null;

			try {
				settingVal = Data[sectionName][settingName];
			} catch (NullReferenceException) {
				settingVal = null;
			}

			if (settingVal != null) {
				return true;
			} else {
				return false;
			}
		}

		public bool SaveSetting(string settingVal, string settingName, string sectionName) {
			try {
				Data[sectionName][settingName] = settingVal;
				Parser.WriteFile(IniFileName, Data);
				return true;
			} catch (Exception) {
				if (!Data.Sections.ContainsSection(sectionName)) {
					Data.Sections.AddSection(sectionName);
				}

				if (!Data.Sections.GetSectionData(sectionName).Keys.ContainsKey(settingName)) {					
					try {
						Data.Sections.GetSectionData(sectionName).Keys.AddKey(settingName, settingVal);
						return true;
					} catch (Exception ex) {
						MessageBox.Show("Error writing ini file: '" + IniFileName + "' -- Error information: " + ex.ToString());
						return false;
					}
				} else {
					System.Diagnostics.Debug.Fail("Unreachable code reached");
					throw new InvalidOperationException();
				}
			}
		}

		public bool RemoveSetting(string settingName, string sectionName) {
			try {
				Data[sectionName].RemoveKey(settingName);
				Parser.WriteFile(IniFileName, Data);
			} catch (Exception ex) {
				MessageBox.Show("Failed to remove ini file setting: " + ex.ToString());
				return false;
			}

			return true;
		}

		//private IniData ReadFile() {
		//	return 
		//}
	}

	class InvalidIniSettingSectionException : Exception {
		public InvalidIniSettingSectionException() : base("Invalid setting or section designation.") { }
		public InvalidIniSettingSectionException(string message) : base("Invalid setting or section designation: " + message) { }
		public InvalidIniSettingSectionException(string message, Exception inner) : base(message, inner) { }
	}
}
