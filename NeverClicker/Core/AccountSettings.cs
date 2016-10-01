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
			var oldIniFileName = Settings.Default.SettingsFolderPath + SettingsForm.GAME_ACCOUNT_INI_FILE_NAME;

			if (File.Exists(oldIniFileName)) {
				if (!File.Exists(base.FileName)) {
					var oldIni = new IniFile(oldIniFileName);

					SaveSetting(oldIni.GetSettingOr("CharCount", "NwAct", Globals.CharCount),
						"CharacterCount", "General");
					SaveSetting(oldIni.GetSettingOr("NwUserName", "NwAct", ""),
						"AccountName", "General");
					SaveSetting(oldIni.GetSettingOr("NwActPwd", "NwAct", ""),
						"Password", "General");
					SaveSetting(oldIni.GetSettingOr("NwInvokeKey", "GameHotkeys", Globals.NwInvokeKey),
						"Invoke", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwLogoutKey", "GameHotkeys", Globals.NwLogoutKey),
						"Logout", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwMoveLeftKey", "GameHotkeys", Globals.NwMoveLeftKey), 
						"MoveLeft", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwMoveRightKey", "GameHotkeys", Globals.NwMoveRightKey),
						"MoveRight", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwMoveForeKey", "GameHotkeys", Globals.NwMoveForeKey),
						"MoveForward", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwMoveBackKey", "GameHotkeys", Globals.NwMoveBackKey),
						"MoveBackward", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwCursorMode", "GameHotkeys", Globals.NwCursorMode),
						"ToggleMouseCursor", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwInventoryKey", "GameHotkeys", Globals.NwInventoryKey),
						"Inventory", "GameHotkeys");
					SaveSetting(oldIni.GetSettingOr("NwProfessionsWindowKey", "GameHotkeys", Globals.NwProfessionsWindowKey),
						"Professions", "GameHotkeys");

					// [TODO]: TRANSFER PER-CHARACTER SETTINGS.
				}

				File.Move(oldIniFileName, oldIniFileName + ".OLD.txt");
			}

			base.SaveFile();
		}

	}
}
