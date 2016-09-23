

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		// Attempts to transfer overflow items to regular inventory.
		public static void TransferOverflow(Interactor intr) {
			string openInventoryKey = intr.GameAccount.GetSettingOrEmpty("NwInventoryKey", "GameHotkeys");

			intr.Wait(2000);
			MoveAround(intr);
			Keyboard.SendKey(intr, openInventoryKey);
			intr.WaitRand(2000, 3000);
			Mouse.ClickImage(intr, "InventoryOverflowTransferButton");
			intr.WaitRand(1500, 2500);
			MoveAround(intr);
		}

		// Searches inventory for Celestial Bags of Refining and opens each one.
		public static void OpenCelestialBags(Interactor intr) {
			string openInventoryKey = intr.GameAccount.GetSettingOrEmpty("NwInventoryKey", "GameHotkeys");

			intr.Wait(2000);
			MoveAround(intr);
			Keyboard.SendKey(intr, openInventoryKey);

		}


		// TODO: COMPLETE ME
		public static bool InventoryIsOpen(Interactor intr) {

			// Screen.ImageSearch(intr, "")

			return true;

		}
	}
}