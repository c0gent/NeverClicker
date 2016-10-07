
using System.Drawing;

namespace NeverClicker.Interactions {
	
	public static partial class Sequences {

		//// Checks to ensure that the inventory was successfully opened.
		//public static bool InventoryIsOpen(Interactor intr) {
		//	var invState = States.DetermineInventoryState(intr);

		//	if (invState == InventoryState.None) {
		//		return false;
		//	} else {
		//		return true;
		//	}
		//}

		// Opens inventory.
		public static bool OpenInventory(Interactor intr) {
			intr.Log(LogEntryType.Debug, "Opening inventory...");
			string openInventoryKey = intr.AccountSettings.GetSettingValOr("Inventory", "GameHotkeys", Global.Default.InventoryKey);
			MoveAround(intr);
			Keyboard.SendKey(intr, openInventoryKey);

			if (intr.WaitUntil(8, WorldWindowState.Inventory, States.IsWorldWindowState, null, 1)) {
				intr.Log(LogEntryType.Debug, "Inventory is open.");
				return true;
			} else {
				intr.Log(LogEntryType.Error, "Unable to open inventory.");
				return false;
			}
		}

		// Attempts to transfer overflow items to regular inventory.
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static void TransferOverflow(Interactor intr, bool inventoryOpened, bool bagsTabActive) {
			if (!inventoryOpened) {
				OpenInventory(intr);
			}

			// If bags tab is not active, click it's icon.
			if (!bagsTabActive) {
				if (!(States.DetermineInventoryState(intr) == InventoryState.Bags)) {
					Mouse.ClickImage(intr, "InventoryTabIconBags");
					intr.WaitRand(1200, 2200);
				}
			}

			Mouse.ClickImage(intr, "InventoryOverflowTransferButton");
			intr.WaitRand(500, 800);
		}


		public static void OpenRewardBag(Interactor intr, ImageSearchResult bagImageResult) {
			const int CLICK_OFS_MAX = 25;
			Mouse.DoubleClick(intr, bagImageResult.Point.X + intr.Rand(5, CLICK_OFS_MAX), 
					bagImageResult.Point.Y + intr.Rand(5, CLICK_OFS_MAX));
		}


		// Searches inventory for Celestial Bags of Refining and opens each one. 
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static CompletionStatus OpenCelestialBags(Interactor intr, bool InventoryOpened) {
			// Open inventory:
			if (!InventoryOpened) {
				OpenInventory(intr);
			}

			// If bags tab is not active, click it's icon.
			if (!(States.DetermineInventoryState(intr) == InventoryState.Bags)) {
				var iconBags = Screen.ImageSearch(intr, "InventoryTabIconBags");
				//Mouse.ClickImage(intr, "InventoryTabIconBags");
				if (iconBags.Found) {
					Mouse.Click(intr, iconBags.Point);
					Mouse.Move(intr, iconBags.Point.X - 30, iconBags.Point.Y);
					intr.WaitRand(250, 300);
				} else {
					intr.Log(LogEntryType.Fatal, "Unable to find 'InventoryTabIconBags'.");
					return CompletionStatus.Failed;
				}
			}			

			// Determine if any Celestial Bags of Refinement are present:
			var imgCode = "InventoryCelestialBagOfRefinement";

			// First Search:
			var bagSearchResult = Screen.ImageSearch(intr, imgCode);
			//var openAnotherBtnLoc = new Point();			

			if (bagSearchResult.Found) {
				intr.Log(LogEntryType.Debug, "Opening celestial bags...");
				OpenRewardBag(intr, bagSearchResult);

				intr.Wait(800);

				// Determine 'Open Another' button location for future presses:
				var openAnotherBtnSearchResult = Screen.ImageSearch(intr, "InventoryRewardWindowOpenAnotherButton");

				if (openAnotherBtnSearchResult.Found) {
					var randOfs = new Point(intr.Rand(1, 60), intr.Rand(1, 10));
					var openAnotherBtnLoc = new Point(openAnotherBtnSearchResult.Point.X + randOfs.X, 
						openAnotherBtnSearchResult.Point.Y + randOfs.Y);
					int bagsOpened = 0;

					// Open remaining bags:
					while (Screen.ImageSearch(intr, imgCode).Found) {
						intr.WaitRand(20, 40);				
						Mouse.Click(intr, openAnotherBtnLoc);
						bagsOpened += 1;

						// Something is probably stuck:
						if (bagsOpened >= 100) {
							TransferOverflow(intr, true, true);
							break;	
						}
					}
				}
			}

			return CompletionStatus.Complete;
		}


		// Maintains inventory.
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static CompletionStatus MaintainInventory(Interactor intr, uint charIdx) {
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }	

			bool InventoryOpened = OpenInventory(intr);

			var enchKeyAvailable = DetectEnchantedKeyAwaitingCollection(intr);

			// Collect Enchanted Key:
			if (IsEnchantedKeyPending(intr)) {
				ClaimEnchantedKey(intr, charIdx, InventoryOpened);
			} else if (enchKeyAvailable) {
				ClaimEnchantedKey(intr, charIdx, InventoryOpened);
			}

			// Transfer any overflow items:
			TransferOverflow(intr, true, true);

			// Open Celestial Bags of Refinement:
			var ocbStatus = OpenCelestialBags(intr, true);

			// Transfer any overflow items again:
			TransferOverflow(intr, true, true);

			if (ocbStatus != CompletionStatus.Complete) {
				return ocbStatus;
			}

			// Finish up:
			MoveAround(intr);
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; } // necessary?
			return CompletionStatus.Complete;
		}

	}
}