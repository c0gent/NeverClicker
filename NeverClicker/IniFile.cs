using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	public class IniFile {
		private FileIniDataParser Parser = new FileIniDataParser();
		//private IniData Data;
		private string IniFileName;

		public IniFile(string iniFileName) {
			IniFileName = iniFileName;
		}

		public string GetSetting(string settingName, string sectionName) {
			var data = ReadFile();

			if (data[sectionName][settingName] == null) {
				throw new InvalidIniSettingSectionException("settingName: " + settingName + ", sectionName: " + sectionName);
			} else {
				return data[sectionName][settingName];
			}		
		}

		public int GetSettingOrZero(string settingName, string sectionName) {
			var data = ReadFile();
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
			var data = ReadFile();
			data[sectionName][settingName] = settingVal;
			Parser.WriteFile(IniFileName, data);

			//if (data[sectionName][settingName] == null) {
			//	data[sectionName][settingName] = settingVal;
			//	//throw new InvalidIniSettingSectionException("settingName: " + settingName + ", sectionName: " + sectionName);
			//} else {
			//	data[sectionName][settingName] = settingVal;
			//	Parser.WriteFile(IniFileName, data);
			//}
		}

		private IniData ReadFile() {
			return Parser.ReadFile(IniFileName);
		}
	}

	class InvalidIniSettingSectionException : Exception {
		public InvalidIniSettingSectionException() : base("Invalid setting or section designation.") { }
		public InvalidIniSettingSectionException(string message) : base("Invalid setting or section designation: " + message) { }
		public InvalidIniSettingSectionException(string message, Exception inner) : base(message, inner) { }
	}
}
