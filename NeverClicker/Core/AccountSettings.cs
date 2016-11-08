using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NeverClicker {
	public class AccountSettings : XmlSettingsFile {
		public static string CharactersNodeName = "characters";
		public ImmutableArray<string> CharNames;

		public AccountSettings() : base("accountSettings") {
			base.SaveFile();
			PopulateCharNames();
		}

		public AccountSettings(string oldIniFileName) : base("accountSettings") {
			if (File.Exists(oldIniFileName)) {
				MigrateIniSettings(oldIniFileName);
			}

			base.SaveFile();
			PopulateCharNames();
		}

		private void PopulateCharNames() {
			var charCount = GetSettingValOr("characterCount", "general", Global.Default.CharacterCount);
			ImmutableArray<string>.Builder charNamesBuilder = ImmutableArray.CreateBuilder<string>(charCount);

			for (uint i = 0; i < charCount; i++) {
				charNamesBuilder.Add(CharNode(i).GetAttribute("name"));
			}

			this.CharNames = charNamesBuilder.ToImmutable();
		}

		public XmlElement CharNode(uint charIdx) {
			var charsNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			return GetOrCreateSettingNode(charsNodeName, CharactersNodeName);
		}

		public string GetCharSetting(uint charIdx, string settingName) {
			var charNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			var charNode = GetOrCreateSettingNode(charNodeName, CharactersNodeName);

			var charSettingNode = charNode.SelectSingleNode(settingName);

			if (charSettingNode == null) {
				charSettingNode = Doc.CreateElement(settingName);
				charNode.AppendChild(charSettingNode);
			}

			return charSettingNode.InnerText;
		}

		public void SaveCharSetting(string settingVal, uint charIdx, string settingName) {
			var charNodeName = Global.Default.CharLabelPrefix + charIdx.ToString();
			var charNode = GetOrCreateSettingNode(charNodeName, CharactersNodeName);

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
					"characterCount", "general");
				SaveSetting(oldIni.GetSettingOr("NwUserName", "NwAct", ""),
					"accountName", "general");
				SaveSetting(oldIni.GetSettingOr("NwActPwd", "NwAct", ""),
					"password", "general");
				SaveSetting(oldIni.GetSettingOr("NwInvokeKey", "GameHotkeys", Global.Default.InvokeKey),
					"invoke", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwLogoutKey", "GameHotkeys", Global.Default.LogoutKey),
					"logout", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveLeftKey", "GameHotkeys", Global.Default.MoveLeftKey), 
					"moveLeft", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveRightKey", "GameHotkeys", Global.Default.MoveRightKey),
					"moveRight", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveForeKey", "GameHotkeys", Global.Default.MoveForwardKey),
					"moveForward", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwMoveBackKey", "GameHotkeys", Global.Default.MoveBackwardKey),
					"moveBackward", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwCursorMode", "GameHotkeys", Global.Default.ToggleMouseCursor),
					"toggleMouseCursor", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwInventoryKey", "GameHotkeys", Global.Default.InventoryKey),
					"inventory", "gameHotkeys");
				SaveSetting(oldIni.GetSettingOr("NwProfessionsWindowKey", "GameHotkeys", Global.Default.ProfessionsWindowKey),
					"professions", "gameHotkeys");

				var charCount = GetSettingValOr("characterCount", "general", Global.Default.CharacterCount);

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
					var charLabelOne = "Character #" + (charIdx + 1).ToString();
					//SaveCharSetting(charLabelOne, charIdx, "CharacterName");
					CharNode(charIdx).SetAttribute("name", charLabelOne);

					var vopItem = oldIni.GetSettingOr("VaultPurchase", charLabelZero, Global.Default.VaultPurchase);
					if (vopItem > 4) { vopItem = 4; };
					SaveCharSetting(vopItem, charIdx, "vaultOfPietyItem");
				}
			}
		}
	}
}
