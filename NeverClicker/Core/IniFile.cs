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
			if (File.Exists(fileName)) {
				IniFileName = fileName;
			} else {
				var builtinSettingsFolder = SettingsForm.ProgramRootFolder + SettingsForm.BUILTIN_SETTINGS_SUBPATH;
				File.Copy(builtinSettingsFolder + "\\" + IniFileName, 
					Settings.Default.SettingsFolderPath + "\\" + IniFileName);
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

		public bool TryGetSetting(string settingName, string sectionName, out int settingValInt) {
			string settingValString;

			if (TryGetSetting(settingName, sectionName, out settingValString)) {
				if (int.TryParse(settingValString, out settingValInt)) {
					return true;
				} else {
					return false;
				}
 			} else {
				settingValInt = 0;
				return false;
			}
		}

		public string GetSettingOrEmptyString(string settingName, string sectionName) {
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
			return GetSettingOr(settingName, sectionName, 0);
		}

		public string GetSettingOr(string settingName, string sectionName, string dflt_val) {
			string settingVal = null;

			try {
				settingVal = Data[sectionName][settingName];
			} catch (NullReferenceException) {
				settingVal = null;
			}

			if (settingVal != null) {
				return settingVal.Trim();					
			} else {
				return dflt_val;
			}	
		}
		
		public int GetSettingOr(string settingName, string sectionName, int dflt_val) {
			int setting_val;
			string settingVal = null;
			int return_val = dflt_val;

			try {
				settingVal = Data[sectionName][settingName];
			} catch (NullReferenceException) {
				settingVal = null;
			}

			if (settingVal == null) {
				return_val = dflt_val;
			}			

			if (int.TryParse(Data[sectionName][settingName], out setting_val)) {
				return_val = setting_val;
			} else {
				return_val = dflt_val;
			}

			return return_val;
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
	}

	class InvalidIniSettingSectionException : Exception {
		public InvalidIniSettingSectionException() : base("Invalid setting or section designation.") { }
		public InvalidIniSettingSectionException(string message) : base("Invalid setting or section designation: " + message) { }
		public InvalidIniSettingSectionException(string message, Exception inner) : base(message, inner) { }
	}
}
