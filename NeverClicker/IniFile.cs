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
			return data[sectionName][settingName];
		}

		public int GetSettingOrZero(string settingName, string sectionName) {
			var data = ReadFile();
			int number;

			if (int.TryParse(data[sectionName][settingName], out number)) {
				return number;
			} else {
				return 0;
			}				
		}

		public void SaveSetting(string settingName, string sectionName) {
			var data = ReadFile();
			data[sectionName][settingName] = "true";
			Parser.WriteFile(IniFileName, data);
		}

		private IniData ReadFile() {
			return Parser.ReadFile(IniFileName);
		}
	}
}
