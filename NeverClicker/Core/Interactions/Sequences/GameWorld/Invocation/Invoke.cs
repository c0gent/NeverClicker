using NeverClicker.Core;
using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public const bool ALWAYS_REDEEM = false;
		public const VaultOfPietyItem REDEMPTION_ITEM = VaultOfPietyItem.CofferOfCelestialArtifactEquipment;

		public static CompletionStatus Invoke(Interactor intr, uint charIdx, bool enchKeyIsPending) {
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }			

			if (ALWAYS_REDEEM) {
				#pragma warning disable CS0162 // Unreachable code detected
				Redeem(intr, REDEMPTION_ITEM);
				#pragma warning restore CS0162 // Unreachable code detected
			}

			string invokeKey = intr.GameAccount.GetSettingValOr("NwInvokeKey", "GameHotkeys", Globals.NwInvokeKey);

			///////// MOVED TO INVENTORY MANAGEMENT:
			//string openInventoryKey = intr.GameAccount.GetSettingOrEmpty("NwInventoryKey", "GameHotkeys");
			//// Collect Enchanted Key
			//if (enchKeyIsPending) {
			//	if (!ClaimEnchantedKey(intr)) {
			//		// ***** Can Remove This *****
			//		if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }
			//		intr.Log("Unable to collect enchanted key for character " + charIdx + ".", 
			//			LogEntryType.Fatal);
			//	}
			//}

			// Invocation Attempt (first):
			Keyboard.SendKey(intr, invokeKey);
			intr.Wait(100);

			if (Screen.ImageSearch(intr, "InvocationMaximumBlessings").Found) {
				intr.Log("Maximum blessings reached for character " + charIdx 
					+ ". Redeeming through Vault of Piety...", LogEntryType.Info);
				if (Redeem(intr, REDEMPTION_ITEM)) {
					MoveAround(intr);
					intr.Log("Redeeming Vault of Piety...", LogEntryType.Debug);
					// Invocation Attempt:
					Keyboard.SendKey(intr, invokeKey);
				} else {
					intr.Log("Unable to invoke: Error collecting Vault of Piety rewards for character " + charIdx + ".", LogEntryType.Error);
					return CompletionStatus.Failed;
				}
			}

			if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionWindowTitle").Found) {
				intr.Wait(900);

				if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionInvokeReady").Found) {
					intr.Wait(2000);
					// Invocation Attempt:
					Keyboard.SendKey(intr, invokeKey);
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionDoneForDay").Found) {
					intr.Log("Unable to invoke: Invocation already finished for the day on character " + charIdx + ".");
					return CompletionStatus.DayComplete;
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionPatience").Found) {
					intr.Log("Unable to invoke: Still waiting to invoke on character " + charIdx + ".");
					return CompletionStatus.Immature;
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionNotInRestZone").Found) {
					intr.Log("Unable to invoke: Character " + charIdx + " not in rest zone.", LogEntryType.Error);
					return CompletionStatus.Complete;			
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionItemsInOverflow").Found) {
					intr.Log("Unable to invoke: Items in overflow bag are preventing invocation for character " 
						+ charIdx + ". Attempting to move to regular inventory...", LogEntryType.Error);

					// Attempt to transfer overflow items to regular inventory:
					//
					// [NOTE]: Possibly Redundant. 
					// - Determine if it is possible for new items to be added to inventory
					//   between inventory management and here.
					//
					TransferOverflow(intr, false, false);
					MoveAround(intr);

					// Invocation Attempt:
					Keyboard.SendKey(intr, invokeKey);
				} else {
					intr.Log("Unable to invoke for character " + charIdx 
						+ "." + "[IN0]", LogEntryType.FatalWithScreenshot);
					intr.Wait(30000);
					return CompletionStatus.Failed;
				}
            }

			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }
			intr.Wait(3500);			

			if (!intr.WaitUntil(3, DialogueBoxState.InvocationSuccess, Game.IsDialogueBoxState, null, 0)) {
				// Invocation Attempt:
				Keyboard.SendKey(intr, invokeKey);				
				intr.Wait(1500);
				MoveAround(intr);
			}

			// [FIXME]: Not sure why this is here still or why it was needed:
			if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionWindowTitle").Found) {
				intr.Log("Closing Rewards of Devotion window and reassessing invocation success...", LogEntryType.Debug);
				//Mouse.ClickImage(intr, "InvocationRewardsOfDevotionCloseButton");
			}

			if (!intr.WaitUntil(9, DialogueBoxState.InvocationSuccess, Game.IsDialogueBoxState, null, 0)) {
				// Invocation Attempt: Display invocation screen for the screenshot:
				Keyboard.SendKey(intr, invokeKey);				
				intr.Wait(1500);

				intr.Log("Unable to invoke for character " + charIdx 
						+ "." + "[FN1]", LogEntryType.FatalWithScreenshot);

				intr.Wait(30000);
				MoveAround(intr);	
				return CompletionStatus.Failed;
			}

			intr.Log("Invocation successful. Continuing...", LogEntryType.Info);

			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }

			return CompletionStatus.Complete;
		}
	}
}
