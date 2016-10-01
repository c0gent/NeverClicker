using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	public class ClientSettings : XmlSettingsFile {
		public ClientSettings() : base("ClientSettings") {
			var oldIniFileName = Settings.Default.SettingsFolderPath + SettingsForm.GAME_CLIENT_INI_FILE_NAME;

			if (File.Exists(oldIniFileName)) {
				if (!File.Exists(base.FileName)) {
					var oldIni = new IniFile(oldIniFileName);
					var charSel = new ClientCharSelectDefaults(new Point(1920, 1080));

					SaveSetting(oldIni.GetSettingOr("CharacterSelectScrollBarTopX", "ClickLocations", charSel.ScrollBarTopX),
						"ScrollBarTopX", "CharacterSelect");
					SaveSetting(oldIni.GetSettingOr("CharacterSelectScrollBarTopY", "ClickLocations", charSel.ScrollBarTopY),
						"ScrollBarTopY", "CharacterSelect");
					SaveSetting(oldIni.GetSettingOr("CharSlotX", "ClickLocations", charSel.CharSlotX),
						"CharSlotX", "CharacterSelect");
					SaveSetting(oldIni.GetSettingOr("TopSlotY", "ClickLocations", charSel.TopSlotY),
						"TopSlotY", "CharacterSelect");
					SaveSetting(oldIni.GetSettingOr("VisibleCharacterSelectSlots", "KeyBindAndUi", charSel.VisibleSlots),
						"VisibleSlots", "CharacterSelect");
					SaveSetting(oldIni.GetSettingOr("ScrollsToAlignBottomSlot", "KeyBindAndUi", charSel.ScrollsToAlignBottomSlot),
						"ScrollsToAlignBottomSlot", "CharacterSelect");
				}

				File.Move(oldIniFileName, oldIniFileName + ".OLD.txt");
			}

			base.SaveFile();
		} 


		public void CharSelectDefaults() {

			
		}
	}
}
