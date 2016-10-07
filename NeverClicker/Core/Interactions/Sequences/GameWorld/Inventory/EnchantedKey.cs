using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool IsEnchantedKeyPending(Interactor intr) {
			DateTime KeyLastReceived;
			if (DateTime.TryParse(intr.AccountStates.GetSettingValOr("EnchKeyLastReceived", "Invocation", ""), out KeyLastReceived)) {
				if (KeyLastReceived >= TaskQueue.TodaysGameDate) {
					// We already have key for the day
					//intr.Log(LogEntryType.Trace, "")
					return false;
				}
			}
			return true;
		}

		// Determines if an Enchanted Key is available to claim unexpectedly.
		//
		// [TODO]: Need error handling channels if detected states don't align with expected.
		//
		public static bool DetectEnchantedKeyAwaitingCollection(Interactor intr) {
			if (States.DetermineInventoryState(intr) == InventoryState.Vip) {
				return Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon").Found;
			} else {
				return false;
			}
		}

		// Claims an Enchanted Key if at all possible.
		public static bool ClaimEnchantedKey(Interactor intr, uint charIdx, bool inventoryOpened) {
			// Offset between reward icon and claim button:
			int xOfs = 222;
			int yOfs = 12;

			intr.Wait(1000);
			intr.Log(LogEntryType.Debug, "Claiming Enchanted Key.");			

			// Open inventory if it has not already been pronounced open.
			if (!inventoryOpened) {
				if (!OpenInventory(intr)) {
					intr.Log(LogEntryType.Fatal, "Unable to open inventory while claiming Enchanted Key for character " 
						+ charIdx + ".");
				}
				// Sometimes takes a long time for icons to load...
				intr.Wait(3000);
			}

			// If VIP tab is not active, click it's icon.
			if (!States.IsInventoryState(intr, InventoryState.Vip)) {
				Mouse.ClickImage(intr, "InventoryTabIconVip");
				intr.Wait(1400);

				// Ensure that we're looking at the VIP tab.
				if (States.DetermineInventoryState(intr) != InventoryState.Vip) {
					intr.Log(LogEntryType.FatalWithScreenshot, "Unable to select VIP Tab in Inventory window.");
					return false;
				}
			}			

			// Get reward icon location/result:
			var iconLoc = Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon");

			// If found, click on two locations:
			if (iconLoc.Found) {
				intr.Log(LogEntryType.Debug, "VIP Claim image found, moving mouse to: " + (iconLoc.Point.X + xOfs).ToString() + ", " 
					+ (iconLoc.Point.Y + yOfs).ToString());

				// Click to the right of that image:
				Mouse.Move(intr, iconLoc.Point.X + xOfs, iconLoc.Point.Y + yOfs);
				Mouse.Click(intr,iconLoc.Point.X + xOfs, iconLoc.Point.Y + yOfs); 
				intr.Wait(900);

				// Click again just to the right of the previous spot:
				Mouse.Move(intr, iconLoc.Point.X + xOfs + 15, iconLoc.Point.Y + yOfs);
				Mouse.Click(intr,iconLoc.Point.X + xOfs + 15, iconLoc.Point.Y + yOfs); 
				intr.Wait(2500);

				// Verify that the VIP reward icon is no longer visible:
				if (!(States.DetermineInventoryState(intr) == InventoryState.Vip)) {
					intr.Log(LogEntryType.FatalWithScreenshot, "Inventory window VIP tab not active when it should be.");
					return false;
				} else {
					if (Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon").Found) {
						intr.Log(LogEntryType.FatalWithScreenshot, "Error clicking on claim button.");
						return false;
					} else {
						intr.Log(LogEntryType.Normal, "Enchanted key collected on character " + charIdx + ".");
						intr.AccountStates.SaveSetting(TaskQueue.TodaysGameDate.ToString(), "EnchKeyLastReceived", "Invocation");
						return true;
					}
				}

				// [TODO]: Open Enchanted Key Bag
				
			} else {
				if (States.DetermineInventoryState(intr) == InventoryState.Vip) {
					intr.Log(LogEntryType.Error, "Failure to claim enchanted key on character " + charIdx + ".");
					intr.Log(LogEntryType.Error, "Assuming key was manually collected and continuing.");
					intr.AccountStates.SaveSetting(TaskQueue.TodaysGameDate.ToString(), "EnchKeyLastReceived", "Invocation");
				}
				//if (!inventoryOpened) {
				//	//Keyboard.SendKey(intr, openInventoryKey);
				//	MoveAround(intr);
				//}

				return false;
			}
		}
	}
}
