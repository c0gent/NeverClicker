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
		//private IniData Data;
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
		}

		public string GetSetting(string settingName, string sectionName) {
			try {
				var data = Parser.ReadFile(IniFileName);
				if (data[sectionName][settingName] == null) {
					//throw new InvalidIniSettingSectionException("settingName: " + settingName + ", sectionName: " + sectionName);
					return "";
				} else {
					return data[sectionName][settingName].Trim();
				}
			} catch (Exception) {
				MessageBox.Show("Problem loading ini files. Please check settings.");
				return "";
			}					
		}

		public int GetSettingOrZero(string settingName, string sectionName) {
			IniData data;

			try {
				data = Parser.ReadFile(IniFileName);
			} catch (Exception) {
				MessageBox.Show("Problem loading ini files. Please check settings.");
				return 0;
			}
			
			int number;
			string settingString = null;

			try {
				settingString = data[sectionName][settingName];
			} catch (NullReferenceException) {
				settingString = null;
			}

			if (settingString == null) {
				return 0;
			}			

			if (int.TryParse(data[sectionName][settingName], out number)) {
				return number;
			} else {
				return 0;
			}				
		}

		public void SaveSetting(string settingVal, string settingName, string sectionName) {
			IniData data;

			try {
				data = Parser.ReadFile(IniFileName);
			} catch (Exception ex) {
				MessageBox.Show("Error reading ini file: '" + IniFileName + "' -- Error information: " + ex.ToString());
				return;
			}

			try {
				data[sectionName][settingName] = settingVal;
				Parser.WriteFile(IniFileName, data);
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
