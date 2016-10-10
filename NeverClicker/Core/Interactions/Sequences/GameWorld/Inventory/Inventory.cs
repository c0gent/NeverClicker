
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
			string openInventoryKey = intr.AccountSettings.GetSettingValOr("inventory", "gameHotkeys", Global.Default.InventoryKey);
			MoveAround(intr);
			Keyboard.SendKey(intr, openInventoryKey);
			intr.Wait(300);

			if (intr.WaitUntil(8, WorldWindowState.Inventory, States.IsWorldWindowState, null, 1)) {
				intr.Log(LogEntryType.Debug, "Inventory is open.");
				return true;
			} else {
				intr.Log(LogEntryType.Info, "Unable to open inventory. Could be a random glitch.");
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

			if (Mouse.ClickImage(intr, "InventoryOverflowTransferButton")) {
				intr.WaitRand(500, 800);
			}
		}


		public static void OpenRewardBag(Interactor intr, ImageSearchResult bagImageResult) {
			const int CLICK_OFS_MAX = 25;
			Mouse.DoubleClick(intr, bagImageResult.Point.X + intr.Rand(5, CLICK_OFS_MAX), 
					bagImageResult.Point.Y + intr.Rand(5, CLICK_OFS_MAX));
		}


		// Searches inventory for rewards such as Celestial Bags of Refining and opens each one. 
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static CompletionStatus OpenRewardBags(Interactor intr, string RewardIconImgCode, bool InventoryOpened) {
			// Open inventory:
			if (!InventoryOpened) {
				OpenInventory(intr);
			}

			// If bags tab is not active, click it's icon.
			if (!(States.IsInventoryState(intr, InventoryState.Bags))) {
				var iconBags = Screen.ImageSearch(intr, "InventoryTabIconBags");
				//Mouse.ClickImage(intr, "InventoryTabIconBags");
				if (iconBags.Found) {
					Mouse.Click(intr, iconBags.Point);
					Mouse.Move(intr, iconBags.Point.X - 30, iconBags.Point.Y);
					intr.WaitRand(50, 100);
				} else {
					intr.Log(LogEntryType.Info, "Unable to find inventory tab: 'Bags'. Probably just a random timing error.");
					return CompletionStatus.Failed;
				}
			}			

			//
			//var imgCode = "InventoryCelestialBagOfRefinement";
			int afterSearchDelay = 3200;

			// Determine if any reward bags are present:
			// First Search:
			var bagSearchResult = Screen.ImageSearch(intr, RewardIconImgCode);
			//var openAnotherBtnLoc = new Point();			

			if (bagSearchResult.Found) {
				intr.Log(LogEntryType.Debug, "Found reward icon with image code: '{0}'.", RewardIconImgCode);

				// Find a place to move the mouse out of the way to:
				var iconBags = Screen.ImageSearch(intr, "InventoryTabIconBags");
				if (!iconBags.Found) { intr.Log(LogEntryType.Error, "Error determining inventory state."); }

				intr.Log(LogEntryType.Debug, "Opening reward bags...");
				OpenRewardBag(intr, bagSearchResult);
				intr.Wait(100);

				// Move mouse out of the way:
				Mouse.Move(intr, iconBags.Point.X - 30, iconBags.Point.Y);
				intr.Wait(afterSearchDelay);

				// Determine 'Open Another' button location for future presses:
				var openAnotherBtnSearchResult = Screen.ImageSearch(intr, 
					"InventoryRewardWindowOpenAnotherButton_2", "InventoryRewardWindowOpenAnotherButton");

				if (openAnotherBtnSearchResult.Found) {					
					var randOfs = new Point(intr.Rand(1, 60), intr.Rand(1, 10));
					var openAnotherBtnLoc = new Point(openAnotherBtnSearchResult.Point.X + randOfs.X, 
						openAnotherBtnSearchResult.Point.Y + randOfs.Y);
					int bagsOpened = 0;

					// Open remaining bags:
					while (true) {						
						Mouse.Click(intr, openAnotherBtnLoc);
						intr.WaitRand(20, 40);
						bagsOpened += 1;

						if (!Screen.ImageSearch(intr, RewardIconImgCode).Found) {
							break;
						}

						// Something is probably stuck:
						if (bagsOpened >= 100) {
							TransferOverflow(intr, true, true);
							break;	
						}
					}
				} else {
					// 'Open Another' button not found, just click them individually:
					while (true) {
						// Move mouse out of the way:
						Mouse.Move(intr, iconBags.Point.X - 30, iconBags.Point.Y);

						// Search for bag icon:
						bagSearchResult = Screen.ImageSearch(intr, RewardIconImgCode);
						if (!bagSearchResult.Found) { break; }					

						// Open bag:
						OpenRewardBag(intr, bagSearchResult);
						intr.Wait(afterSearchDelay);
					}

				}
			} else {
				intr.Log(LogEntryType.Debug, "No reward icons with image code: '{0}' detected.", RewardIconImgCode);
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
			if (IsEnchantedKeyTimerExpired(intr)) {
				intr.Log(LogEntryType.Debug, "Enchanted key timer has expired. Attempting to collect...");
				ClaimEnchantedKey(intr, charIdx, InventoryOpened);
			} else if (enchKeyAvailable) {
				intr.Log(LogEntryType.Debug, "Enchanted key detected as awaiting collection. Attempting to collect...");
				ClaimEnchantedKey(intr, charIdx, InventoryOpened);
			}

			// Transfer any overflow items:
			TransferOverflow(intr, true, true);

			// Open Celestial Bags of Refinement:
			var ocbStatus = OpenRewardBags(intr, "InventoryCelestialBagOfRefinement", true);

			if (ocbStatus != CompletionStatus.Complete) {
				intr.Log(LogEntryType.Error, "Unable to open celestial bags of refinement.");
				return ocbStatus;
			}

			// Open VIP Account Reward bags:
			var varStatus = OpenRewardBags(intr, "InventoryVipAccountRewardBag", true);

			if (varStatus != CompletionStatus.Complete) {
				intr.Log(LogEntryType.Error, "Unable to open vip account reward bags.");
				return varStatus;
			}

			// Attempt to transfer any overflow items again just in case:
			TransferOverflow(intr, true, true);			

			// Finish up:
			MoveAround(intr);
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; } // necessary?
			return CompletionStatus.Complete;
		}

	}
}