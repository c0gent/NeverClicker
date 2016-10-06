using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	public class AccountSettings : XmlSettingsFile {
		public AccountSettings() : base("AccountSettings") {
			base.SaveFile();
		}

		public AccountSettings(string oldIniFileName) : base("AccountSettings") {
			if (File.Exists(oldIniFileName)) {
				MigrateIniSettings(oldIniFileName);
			}

			base.SaveFile();
		}

		public string GetCharSetting(uint charIdx, string settingName) {
			var charNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			var charNode = GetOrCreateSettingNode(charNodeName, "Characters");

			var charSettingNode = charNode.SelectSingleNode(settingName);

			if (charSettingNode == null) {
				charSettingNode = Doc.CreateElement(settingName);
				charNode.AppendChild(charSettingNode);
			}

			return charSettingNode.InnerText;
		}

		public void SaveCharSetting(string settingVal, uint charIdx, string settingName) {
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

		public void SaveCharSetting(int settingVal, uint charIdx, string settingName) {
			SaveCharSetting(settingVal.ToString(), charIdx, settingName);
		}

		private void MigrateIniSettings(string oldIniFileName) {
			if (!File.Exists(base.FileName)) {
				var oldIni = new IniFile(oldIniFileName);

				SaveSetting(oldIni.GetSettingOr("CharCount", "NwAct", Global.Default.CharacterCount),
					"CharacterCount", "General");
				SaveSetting(oldIni.GetSettingOr("NwUserName", "NwAct", ""),
					"AccountName", "General");
				SaveSetting(oldIni.GetSettingOr("NwActPwd", "NwAct", ""),
					"Password", "General");
				SaveSetting(oldIni.GetSettingOr("NwInvokeKey", "GameHotkeys", Global.Default.InvokeKey),
					"Invoke", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwLogoutKey", "GameHotkeys", Global.Default.LogoutKey),
					"Logout", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveLeftKey", "GameHotkeys", Global.Default.MoveLeftKey), 
					"MoveLeft", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveRightKey", "GameHotkeys", Global.Default.MoveRightKey),
					"MoveRight", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveForeKey", "GameHotkeys", Global.Default.MoveForwardKey),
					"MoveForward", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveBackKey", "GameHotkeys", Global.Default.MoveBackwardKey),
					"MoveBackward", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwCursorMode", "GameHotkeys", Global.Default.ToggleMouseCursor),
					"ToggleMouseCursor", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwInventoryKey", "GameHotkeys", Global.Default.InventoryKey),
					"Inventory", "GameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwProfessionsWindowKey", "GameHotkeys", Global.Default.ProfessionsWindowKey),
					"Professions", "GameHotkeys");

				var charCount = GetSettingValOr("CharacterCount", "General", Global.Default.MaxCharacterCount);

				for (uint charIdx = 0; charIdx < charCount; charIdx++) {
					//[Character 48]
					//VaultPurchase = 5
					//InvokesToday = 6
					//InvokesCompleteFor = 10/1/2016 00:00:00
					//MostRecentInvocationTime = 10/1/2016 10:13:40
					//MostRecentProfTime_1 = 10/1/2016 16:19:59
					//MostRecentProfTime_2 = 10/1/2016 16:22:20
					//MostRecentProfTime_0 = 10/1/2016 05:44:26

					var charLabelZero = "Character " + charIdx.ToString();
					var charLabelOne = "Character " + (charIdx + 1).ToString();
					SaveCharSetting(charLabelOne, charIdx, "CharacterName");

					var vopItem = oldIni.GetSettingOr("VaultPurchase", charLabelZero, Global.Default.VaultPurchase);
					if (vopItem > 4) { vopItem = 4; };
					SaveCharSetting(vopItem, charIdx, "VaultOfPietyItem");
				}
			}
		}
	}
}
