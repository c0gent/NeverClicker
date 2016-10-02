using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeverClicker {
	public class AccountStates : XmlSettingsFile {
		public AccountStates() : base("AccountStates") {
			base.SaveFile();
		}

		public AccountStates(string oldIniFileName) : base("AccountStates") {
			if (File.Exists(oldIniFileName)) {
				MigrateIniSettings(oldIniFileName);
			}

			base.SaveFile();
		}

		// Gets a `String` state value.
		public string GetCharState(uint charIdx, string settingName) {
			var charNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			var charNode = GetOrCreateSettingNode(charNodeName, "Characters");

			var charSettingNode = charNode.SelectSingleNode(settingName);

			if (charSettingNode == null) {
				charSettingNode = Doc.CreateElement(settingName);
				charNode.AppendChild(charSettingNode);
			}

			return charSettingNode.InnerText;
		}

		// Gets a `String` state value or a provided default.
		public string GetCharStateOr(uint charIdx, string settingName, string valDefault) {
			string valCurrent = GetCharState(charIdx, settingName);

			if (string.IsNullOrWhiteSpace(valCurrent)) {
				SaveCharState(valDefault, charIdx, settingName);
				return valDefault;
			} else {
				return valCurrent;
			}
		}

		// Gets an `int` state value or a provided default.
		public int GetCharStateOr(uint charIdx, string settingName, int valDefault) {
			string valCurrent = GetCharState(charIdx, settingName);
			int valResult;

			if(!int.TryParse(valCurrent, out valResult)) {
				SaveCharState(valDefault, charIdx, settingName);
				valResult = valDefault;
			}

			return valResult;
		}

		// Gets a `DateTime` state value or a provided default.
		public DateTime GetCharStateOr(uint charIdx, string settingName, DateTime valDefault) {
			string valCurrent = GetCharState(charIdx, settingName);
			DateTime valResult;

			if(!DateTime.TryParse(valCurrent, out valResult)) {
				SaveCharState(valDefault, charIdx, settingName);
				valResult = valDefault;
			}

			return valResult;
		}

		// Saves a `String` state value.
		public void SaveCharState(string settingVal, uint charIdx, string settingName) {
			var charNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			var charNode = GetOrCreateSettingNode(charNodeName, "Characters");

			var charSettingNode = charNode.SelectSingleNode(settingName);

			if (charSettingNode == null) {
				charSettingNode = Doc.CreateElement(settingName);
				charNode.AppendChild(charSettingNode);
			}

			charSettingNode.InnerText = settingVal;
			SaveFile();
		}

		// Saves a `Int32` state value.
		public void SaveCharState(int settingVal, uint charIdx, string settingName) {
			SaveCharState(settingVal.ToString(), charIdx, settingName);
		}

		// Saves a `DateTime` state value.
		public void SaveCharState(DateTime settingVal, uint charIdx, string settingName) {
			SaveCharState(settingVal.ToString(), charIdx, settingName);
		}

		// Migrates old ini settings.
		private void MigrateIniSettings(string oldIniFileName) {
			if (!File.Exists(base.FileName)) {
				var oldIni = new IniFile(oldIniFileName);

				for (uint charIdx = 0; charIdx < Global.Default.MaxCharacterCount; charIdx++) {
					string charLabelZero = "Character " + charIdx.ToString();

					if (oldIni.SectionExists(charLabelZero)) {
						SaveCharState(oldIni.GetSettingOr("InvokesToday", charLabelZero, 0),
							charIdx, "InvokesToday");
						SaveCharState(oldIni.GetSettingOr("InvokesCompleteFor", charLabelZero, Global.Default.SomeOldDateString),
							charIdx, "InvokesCompleteFor");
						SaveCharState(oldIni.GetSettingOr("MostRecentInvocationTime", charLabelZero, Global.Default.SomeOldDateString),
							charIdx, "MostRecentInvocationTime");
						SaveCharState(oldIni.GetSettingOr("MostRecentProfTime_0", charLabelZero, Global.Default.SomeOldDateString),
							charIdx, "MostRecentProfTime_0");
						SaveCharState(oldIni.GetSettingOr("MostRecentProfTime_1", charLabelZero, Global.Default.SomeOldDateString),
							charIdx, "MostRecentProfTime_1");
						SaveCharState(oldIni.GetSettingOr("MostRecentProfTime_2", charLabelZero, Global.Default.SomeOldDateString),
							charIdx, "MostRecentProfTime_2");
					}
						
				}
			}
		}
	}
}
