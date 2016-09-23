using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool IsEnchantedKeyPending(Interactor intr) {
			DateTime KeyLastReceived;
			if (DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty("EnchKeyLastReceived", "Invocation"), out KeyLastReceived)) {
				if (KeyLastReceived >= TaskQueue.TodaysGameDate) {
					// We already have key for the day
					return false;
				}
			}
			return true;
		}

		public static bool ClaimEnchantedKey(Interactor intr) {
			int xOfs = 222;
			int yOfs = 12;
			string openInventoryKey = intr.GameAccount.GetSettingOrEmpty("NwInventoryKey", "GameHotkeys");
			
			intr.Wait(5000);
			intr.Log("Claiming Enchanted Key.", LogEntryType.Debug);

			// Open Inventory:
			intr.Log("Opening inventory...", LogEntryType.Debug);
			Keyboard.SendKey(intr, openInventoryKey);

			// Check that inventory is open:
			intr.Wait(2000);
			// TODO: VERIFY INVENTORY OPEN

			// Sometimes takes a long time for icons to load...
			intr.Wait(6000);

			// Check for VIP Account Rewards icon:
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
				intr.Wait(3500);

				// [FIXME]: BRING BACK SOME SORT OF VERIFICATION
				//if (Screen.ImageSearch(intr, "InventoryVipAccountRewardsIcon").Found) {
				//	intr.Log("Error clicking on claim button.", LogEntryType.FatalWithScreenshot);
				//	Keyboard.SendKey(intr, openInventoryKey);
				//	return false;
				//}

				// Click main bag area tab:

				// Find icon for the key thing:
				// Double click it:
				//Mouse.ClickImage(intr, "InventoryClaimVipAccountRewards");

				// Click open or whatever from the dialogue:

				// Close everything down (inventory closes below):				

				intr.Log("Key claimed, closing inventory...", LogEntryType.Debug);
				Keyboard.SendKey(intr, openInventoryKey);
				intr.GameAccount.SaveSetting(TaskQueue.TodaysGameDate.ToString(), "EnchKeyLastReceived", "Invocation");
				return true;
			} else {
				intr.Log("Failure to claim key, closing inventory...", LogEntryType.Debug);
				Keyboard.SendKey(intr, openInventoryKey);
				return false;
			}			
		}
	}
}
