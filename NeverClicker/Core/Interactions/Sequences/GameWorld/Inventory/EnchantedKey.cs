using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool IsEnchantedKeyPending(Interactor intr) {
			DateTime KeyLastReceived;
			if (DateTime.TryParse(intr.GameAccount.GetSettingOrEmptyString("EnchKeyLastReceived", "Invocation"), out KeyLastReceived)) {
				if (KeyLastReceived >= TaskQueue.TodaysGameDate) {
					// We already have key for the day
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
			if (Game.DetermineInventoryState(intr) == InventoryState.Vip) {
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
			intr.Log("Claiming Enchanted Key.", LogEntryType.Debug);			

			// Open inventory if it has not already been pronounced open.
			if (!inventoryOpened) {
				if (!OpenInventory(intr)) {
					intr.Log("Unable to open inventory while claiming Enchanted Key for character " 
						+ charIdx + ".", LogEntryType.Fatal);
				}
				// Sometimes takes a long time for icons to load...
				intr.Wait(3000);
			}

			// If VIP tab is not active, click it's icon.
			if (!(Game.DetermineInventoryState(intr) == InventoryState.Vip)) {
				Mouse.ClickImage(intr, "InventoryTabIconVip");
				intr.WaitRand(1200, 2200);
			}

			// Ensure that we're looking at the VIP tab and the reward icon.
			if (!DetectEnchantedKeyAwaitingCollection(intr)) { return false; }

			// Get reward icon location/result:
			var iconLoc = Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon");

			// If found, click on two locations:
			if (iconLoc.Found) {
				intr.Log("VIP Claim image found, moving mouse to: " + (iconLoc.Point.X + xOfs).ToString() + ", " 
					+ (iconLoc.Point.Y + yOfs).ToString(), LogEntryType.Debug);

				// Click to the right of that image:
				Mouse.Move(intr, iconLoc.Point.X + xOfs, iconLoc.Point.Y + yOfs);
				Mouse.Click(intr,iconLoc.Point.X + xOfs, iconLoc.Point.Y + yOfs); 
				intr.Wait(900);

				// Click again just to the right of the previous spot:
				Mouse.Move(intr, iconLoc.Point.X + xOfs + 15, iconLoc.Point.Y + yOfs);
				Mouse.Click(intr,iconLoc.Point.X + xOfs + 15, iconLoc.Point.Y + yOfs); 
				intr.Wait(2500);

				// Verify that the VIP reward icon is no longer visible:
				if (!(Game.DetermineInventoryState(intr) == InventoryState.Vip)) {
					intr.Log("Inventory window VIP tab not active when it should be.", 
						LogEntryType.FatalWithScreenshot);
					return false;
				} else {
					if (Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon").Found) {
						intr.Log("Error clicking on claim button.", LogEntryType.FatalWithScreenshot);
						intr.Log("Enchanted key collected by character " + charIdx + ".", LogEntryType.Normal);
						intr.GameAccount.SaveSetting(TaskQueue.TodaysGameDate.ToString(), "EnchKeyLastReceived", "Invocation");
						return true;
					} else {
						return false;
					}
				}

				// [TODO]: Open Enchanted Key Bag
				
			} else {
				intr.Log("Failure to claim enchanted key by character " + charIdx + ".", LogEntryType.Info);

				//if (!inventoryOpened) {
				//	//Keyboard.SendKey(intr, openInventoryKey);
				//	MoveAround(intr);
				//}

				return false;
			}
		}
	}
}
