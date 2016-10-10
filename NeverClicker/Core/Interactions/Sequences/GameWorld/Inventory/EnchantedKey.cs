using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool IsEnchantedKeyTimerExpired(Interactor intr) {
			DateTime KeyLastReceived;
			if (DateTime.TryParse(intr.AccountStates.GetSettingValOr("EnchKeyLastReceived", "Invocation", ""), out KeyLastReceived)) {
				if (KeyLastReceived.AddDays(1) >= DateTime.Now) {
					return false;
				}
			} else {
				intr.Log(LogEntryType.Fatal, "Sequences::IsEnchantedKeyTimerExpired(): Unable to parse 'EnchKeyLastReceived'.");
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
			intr.Log(LogEntryType.Debug, "Attempting to claim Enchanted Key...");

			// Open inventory if it has not already been pronounced open:
			if (!inventoryOpened) {
				intr.Log(LogEntryType.Debug, "Opening inventory...");
				if (!OpenInventory(intr)) {					
					intr.Log(LogEntryType.Fatal, "Unable to open inventory while claiming Enchanted Key for character " 
						+ charIdx + ".");
				} else {
					intr.Log(LogEntryType.Debug, "Inventory has been opened...");
				}
				// Sometimes takes a long time for icons to load...
				intr.Wait(3000);
			}

			// // If VIP tab is not active, click it's icon:
			//if (!States.IsInventoryState(intr, InventoryState.Vip)) {
			//	intr.Log(LogEntryType.Debug, "VIP tab is not active, activating now...");
			//	var tabIcon = Screen.ImageSearch(intr, "InventoryTabIconVip");

			//	if (tabIcon.Found) {
			//		Mouse.Click(intr, tabIcon.Point);
			//	} else {
			//		intr.Log(LogEntryType.FatalWithScreenshot, "Unable to find inventory VIP icon to click.");
			//		return false;
			//	}

			//	intr.Wait(1800);

			//	// Move mouse out of the way:
			//	Mouse.Move(intr, tabIcon.Point.X - 30, tabIcon.Point.Y);

			//	// Ensure that we're looking at the VIP tab:
			//	if (!States.IsInventoryState(intr, InventoryState.Vip)) {
			//		intr.Log(LogEntryType.FatalWithScreenshot, "Unable to select VIP Tab in Inventory window.");
			//		return false;
			//	}
			//} else {
			//	intr.Log(LogEntryType.Debug, "VIP Tab is active.");	
			//}

			// Click VIP tab icon:
			var vipTabIcon = Screen.ImageSearch(intr, "InventoryTabIconVip");

			if (vipTabIcon.Found) {
				Mouse.Click(intr, vipTabIcon.Point);
			} else {
				intr.Log(LogEntryType.FatalWithScreenshot, "Unable to find inventory VIP icon to click.");
				return false;
			}

			// Move mouse out of the way:
			Mouse.Move(intr, vipTabIcon.Point.X - 30, vipTabIcon.Point.Y);

			// Ensure that we're looking at the VIP tab:
			if (!States.IsInventoryState(intr, InventoryState.Vip)) {
				intr.Log(LogEntryType.FatalWithScreenshot, "Unable to select VIP Tab in Inventory window.");
				return false;				
			} else {
				intr.Log(LogEntryType.Debug, "VIP Tab is active.");	
			}

			// Wait to make sure VIP tab page is drawn:
			intr.Wait(1800);

			// Get reward icon location/result:
			var iconLoc = Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon");

			// If found, click on two locations:
			if (iconLoc.Found) {
				intr.Log(LogEntryType.Debug, "VIP Claim image found, moving mouse to: ({0},{1}) to claim...",
					(iconLoc.Point.X + xOfs),
					(iconLoc.Point.Y + yOfs));

				// Click to the right of that image:
				Mouse.Move(intr, iconLoc.Point.X + xOfs, iconLoc.Point.Y + yOfs);
				Mouse.Click(intr,iconLoc.Point.X + xOfs, iconLoc.Point.Y + yOfs); 
				intr.Wait(900);

				// Click again just to the right of the previous spot:
				Mouse.Move(intr, iconLoc.Point.X + xOfs + 15, iconLoc.Point.Y + yOfs);
				Mouse.Click(intr,iconLoc.Point.X + xOfs + 15, iconLoc.Point.Y + yOfs); 
				intr.Wait(2500);

				// Verify that the VIP reward icon is no longer visible:
				if (Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon").Found) {
					intr.Log(LogEntryType.FatalWithScreenshot, "Error clicking on claim button.");
					return false;
				} else {
					intr.Log(LogEntryType.Normal, "Enchanted key collected on character " + charIdx + ".");
					intr.AccountStates.SaveSetting(DateTime.Now.ToString("o"), "EnchKeyLastReceived", "Invocation");
					return true;
				}

				// [TODO]: Open Enchanted Key Bag
				
			} else {
				//if (States.DetermineInventoryState(intr) == InventoryState.Vip) {
				//	intr.Log(LogEntryType.Error, "Failure to claim enchanted key on character " + charIdx + ".");
				//	intr.Log(LogEntryType.Error, "Assuming key was manually collected and continuing.");
				//	intr.AccountStates.SaveSetting(TaskQueue.TodaysGameDate.ToString(), "EnchKeyLastReceived", "Invocation");
				//}
				//if (!inventoryOpened) {
				//	//Keyboard.SendKey(intr, openInventoryKey);
				//	MoveAround(intr);
				//}

				// If things didn't work, just advance it anyway with an error message.
				intr.Log(LogEntryType.Error, "Failure to claim enchanted key on character " + charIdx + ".");
				intr.Log(LogEntryType.Error, "Assuming key was manually collected and continuing.");
				intr.AccountStates.SaveSetting(DateTime.Now.ToString("o"), "EnchKeyLastReceived", "Invocation");

				return false;
			}
		}
	}
}
