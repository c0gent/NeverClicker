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
		public ClientSettings() : base("clientSettings") {
			base.SaveFile();
		}

		public ClientSettings(string oldIniFileName) : base("clientSettings") {
			if (File.Exists(oldIniFileName)) {
				MigrateSettings(oldIniFileName);
			}

			base.SaveFile();
		} 

		private void MigrateSettings(string oldIniFileName) {
			if (!File.Exists(base.FileName)) {
				var oldIni = new IniFile(oldIniFileName);
				var charSel = new Global.ClientCharSelectDefaults(new Point(1920, 1080));

				SaveSetting(oldIni.GetSettingOr("CharacterSelectScrollBarTopX", "ClickLocations", charSel.ScrollBarTopX),
					"scrollBarTopX", "characterSelect");
				SaveSetting(oldIni.GetSettingOr("CharacterSelectScrollBarTopY", "ClickLocations", charSel.ScrollBarTopY),
					"scrollBarTopY", "characterSelect");
				SaveSetting(oldIni.GetSettingOr("CharSlotX", "ClickLocations", charSel.CharSlotX),
					"charSlotX", "characterSelect");
				SaveSetting(oldIni.GetSettingOr("TopSlotY", "ClickLocations", charSel.TopSlotY),
					"topSlotY", "characterSelect");
				SaveSetting(oldIni.GetSettingOr("VisibleCharacterSelectSlots", "KeyBindAndUi", charSel.VisibleSlots),
					"visibleSlots", "characterSelect");
				SaveSetting(oldIni.GetSettingOr("ScrollsToAlignBottomSlot", "KeyBindAndUi", charSel.ScrollsToAlignBottomSlot),
					"scrollsToAlignBottomSlot", "characterSelect");
			}			
		}
	}
}
