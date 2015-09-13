using IniParser;
using IniParser.Model;
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
				MessageBox.Show(fileName + " does not exist.");
				return;
			}

			try {
				Data = Parser.ReadFile(IniFileName);
			} catch (Exception ex) {
				MessageBox.Show("Error reading ini file: '" + IniFileName + "' -- Error information: " + ex.ToString());
				return;
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

		public void SaveSetting(string settingVal, string settingName, string sectionName) {
			//IniData data;

			//try {
			//	data = Parser.ReadFile(IniFileName);
			//} catch (Exception ex) {
			//	MessageBox.Show("Error reading ini file: '" + IniFileName + "' -- Error information: " + ex.ToString());
			//	return;
			//}

			try {
				Data[sectionName][settingName] = settingVal;
				Parser.WriteFile(IniFileName, Data);
			} catch (Exception ex) {
				MessageBox.Show("Error writing ini file: '" + IniFileName + "' -- Error information: " + ex.ToString());
			}

			//if (data[sectionName][settingName] == null) {
			//	data[sectionName][settingName] = settingVal;
			//	//throw new InvalidIniSettingSectionException("settingName: " + settingName + ", sectionName: " + sectionName);
			//} else {
			//	data[sectionName][settingName] = settingVal;
			//	Parser.WriteFile(IniFileName, data);
			//}
		}

		public bool RemoveSetting(string settingName, string sectionName) {
			try {
				Data[sectionName].RemoveKey(settingName);
				Parser.WriteFile(IniFileName, Data);
			} catch (Exception) {
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
