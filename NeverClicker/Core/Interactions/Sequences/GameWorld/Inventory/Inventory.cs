
namespace NeverClicker.Interactions {
	
	public static partial class Sequences {

		// Checks to ensure that the inventory was successfully opened.
		//
		// [TODO]: COMPLETE ME
		//
		public static bool InventoryIsOpen(Interactor intr) {
			var invState = Game.DetermineInventoryState(intr);

			if (invState == InventoryState.Unknown || invState == InventoryState.None) {
				return false;
			} else {
				return true;
			}
		}

		// Opens inventory.
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static bool OpenInventory(Interactor intr) {
			intr.Log("Opening inventory...", LogEntryType.Debug);
			string openInventoryKey = intr.GameAccount.GetSettingOrEmpty("NwInventoryKey", "GameHotkeys");
			MoveAround(intr);
			Keyboard.SendKey(intr, openInventoryKey);
			intr.Wait(2500);

			return InventoryIsOpen(intr);
		}

		// Attempts to transfer overflow items to regular inventory.
		//
		// [TODO]: Need checks on inventory window status.
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static void TransferOverflow(Interactor intr, bool inventoryOpened, bool bagsTabActive) {
			if (!inventoryOpened) {
				OpenInventory(intr);
			}

			// If bags tab is not active, click it's icon.
			if (!bagsTabActive) {
				if (!(Game.DetermineInventoryState(intr) == InventoryState.Bags)) {
					Mouse.ClickImage(intr, "InventoryTabIconBags");
					intr.WaitRand(1200, 2200);
				}
			}

			Mouse.ClickImage(intr, "InventoryOverflowTransferButton");
			intr.WaitRand(500, 800);
		}


		// Searches inventory for Celestial Bags of Refining and opens each one. 
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static CompletionStatus OpenCelestialBags(Interactor intr, bool InventoryOpened) {
			const int CLICK_OFS_MAX = 25;

			// Open inventory:
			if (!InventoryOpened) {
				OpenInventory(intr);
			}

			// If bags tab is not active, click it's icon.
			if (!(Game.DetermineInventoryState(intr) == InventoryState.Bags)) {
				Mouse.ClickImage(intr, "InventoryTabIconBags");
				intr.WaitRand(700, 2200);
			}	

			// Transfer any overflow items:
			TransferOverflow(intr, true, true);

			// Determine if any Celestial Bags of Refinement are present:
			var imgCode = "InventoryCelestialBagOfRefinement";

			// First Search:
			var ir = Screen.ImageSearch(intr, imgCode);

			if (ir.Found) {
				intr.Log("Opening celestial bags...", LogEntryType.Debug);
			}

			while (ir.Found) {
				Mouse.DoubleClick(intr, ir.Point.X + intr.Rand(5, CLICK_OFS_MAX), 
					ir.Point.Y + intr.Rand(5, CLICK_OFS_MAX));
				intr.WaitRand(50, 120);

				// Subsequent Search(es):
				//
				// [TODO]: Replace subsequent clicking of icons with clicking of the "Open Another" button.
				//
				ir = Screen.ImageSearch(intr, imgCode);
			}

			return CompletionStatus.Complete;
		}


		// Maintains inventory.
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static CompletionStatus MaintainInventory(Interactor intr, uint charIdx, bool enchKeyPendingCollection) {
			bool InventoryOpened = OpenInventory(intr);

			var enchKeyAvailable = DetectEnchantedKeyAwaitingCollection(intr);

			// Collect Enchanted Key:
			if (enchKeyPendingCollection) {
				if (ClaimEnchantedKey(intr, charIdx, InventoryOpened)) {
				} else {
					intr.Log("Unable to collect enchanted key for character " + charIdx + ".",
						LogEntryType.Fatal);
				}
			} else if (enchKeyAvailable) {
				ClaimEnchantedKey(intr, charIdx, InventoryOpened);
			}

			// Open Celestial Bags of Refinement:
			var ocbStatus = OpenCelestialBags(intr, true);

			if (ocbStatus != CompletionStatus.Complete) {
				return ocbStatus;
			}

			// Transfer any additional overflow items:
			TransferOverflow(intr, true, true);

			// Finish up:
			MoveAround(intr);
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; } // necessary?
			return CompletionStatus.Complete;
		}

	}
}