﻿using NeverClicker.Core;
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
		public const bool DEBUG_ALWAYS_REDEEM = false;
		public const VaultOfPietyItem DEFAULT_REDEMPTION_ITEM = VaultOfPietyItem.CofferOfCelestialArtifactEquipment;

		public static CompletionStatus Invoke(Interactor intr, uint charIdx, bool enchKeyIsPending) {
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }			

			string invokeKey = intr.AccountSettings.GetSettingValOr("Invoke", "GameHotkeys", Global.Default.InvokeKey);

			// Invocation Attempt (first):
			Keyboard.SendKey(intr, invokeKey);
			intr.Wait(100);

			if (Screen.ImageSearch(intr, "InvocationMaximumBlessings").Found || DEBUG_ALWAYS_REDEEM) {
				intr.Log(LogEntryType.Info, "Maximum blessings reached for character " + charIdx 
					+ ". Redeeming through Vault of Piety...");

				VaultOfPietyItem vopItem;

				if (!Enum.TryParse(intr.AccountSettings.GetCharSetting(charIdx, "VaultOfPietyItem"), out vopItem)) {					
					vopItem = DEFAULT_REDEMPTION_ITEM;
				}

				intr.Log(LogEntryType.Debug, "VaultOfPietyItem: " + vopItem.ToString());

				if (Redeem(intr, vopItem)) {
					MoveAround(intr);
					intr.Log(LogEntryType.Debug, "Redeeming Vault of Piety...");
					// Invocation Attempt:
					Keyboard.SendKey(intr, invokeKey);
				} else {
					intr.Log(LogEntryType.Error, "Unable to invoke: Error collecting Vault of Piety rewards for character " + charIdx + ".");
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
					intr.Log(LogEntryType.Error, "Unable to invoke: Character " + charIdx + " not in rest zone.");
					return CompletionStatus.Complete;			
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionItemsInOverflow").Found) {
					intr.Log(LogEntryType.Error, "Unable to invoke: Items in overflow bag are preventing invocation for character " 
						+ charIdx + ". Attempting to move to regular inventory...");

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
					intr.Log(LogEntryType.FatalWithScreenshot, "Unable to invoke for character " + charIdx 
						+ "." + "[IN0]");
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

			//// [FIXME]: Not sure why this is here still or why it was needed:
			//if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionWindowTitle").Found) {
			//	intr.Log("Closing Rewards of Devotion window and reassessing invocation success...", LogEntryType.Debug);
			//	//Mouse.ClickImage(intr, "InvocationRewardsOfDevotionCloseButton");
			//}

			if (!intr.WaitUntil(9, DialogueBoxState.InvocationSuccess, Game.IsDialogueBoxState, null, 0)) {
				// Invocation Attempt Failure -- Display invocation screen for the screenshot:
				Keyboard.SendKey(intr, invokeKey);				
				intr.Wait(1500);

				intr.Log(LogEntryType.FatalWithScreenshot, "Unable to invoke for character " + charIdx 
						+ "." + "[FN1]");

				intr.Wait(30000);
				MoveAround(intr);	
				return CompletionStatus.Failed;
			}

			intr.Log(LogEntryType.Info, "Invocation successful. Continuing...");

			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }

			return CompletionStatus.Complete;
		}
	}
}
