using System.Drawing;

namespace NeverClicker {
	public static class Globals {
		public static int CharCount = 0;
		public static string NwInvokeKey = "^i";
		public static string NwLogoutKey = "=0";
		public static string NwProfessionsWindowKey = "n";
		public static string NwInventoryKey = "i";
		public static string NwMoveForeKey = "w";
		public static string NwMoveBackKey = "s";
		public static string NwMoveLeftKey = "a";
		public static string NwMoveRightKey = "d";
		public static string NwCursorMode = "ALT";
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
