using System;
using System.Drawing;

namespace NeverClicker.Global {
	public static class Default {		
		public static int MaxCharacterCount = 52;
		public static int CharacterCount = 0;
		public static int VaultPurchase = 4;
		public static string InvokeKey = "^i";
		public static string LogoutKey = "=0";
		public static string ProfessionsWindowKey = "n";
		public static string InventoryKey = "i";
		public static string MoveForwardKey = "w";
		public static string MoveBackwardKey = "s";
		public static string MoveLeftKey = "a";
		public static string MoveRightKey = "d";
		public static string ToggleMouseCursor = "ALT";

		public static string CharLabelPrefix = "c";
		public static string SomeOldDateString = "1/1/2013 00:00:00";
		public static DateTime SomeOldDate = DateTime.Parse(SomeOldDateString);
	}

	public struct ClientCharSelectDefaults {
		public int VisibleSlots;
		public int ScrollsToAlignBottomSlot;
		public int ScrollBarTopX;
		public int ScrollBarTopY;
		public int CharSlotX;
		public int TopSlotY;

		public ClientCharSelectDefaults(Point screenDims) {
			if (screenDims == new Point(1920, 1080)) {
				VisibleSlots = 7;
				ScrollsToAlignBottomSlot = 2;
				ScrollBarTopX = 840;
				ScrollBarTopY = 108;
				CharSlotX = 700;
				TopSlotY = 138;
			} else {
				VisibleSlots = 7;
				ScrollsToAlignBottomSlot = 2;
				ScrollBarTopX = 840;
				ScrollBarTopY = 108;
				CharSlotX = 700;
				TopSlotY = 138;
			}
		}
	}

	//public static class Defaults {
	//	public static string A_DEFAULT = "Hi.";

	//	static Defaults() {

	//	}
	//}

}
