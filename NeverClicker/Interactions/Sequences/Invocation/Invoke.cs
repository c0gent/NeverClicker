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
			public const int REDEMPTION_ITEM = 5;

		public static CompletionStatus Invoke(Interactor intr, uint charIdx, bool enchKeyIsPending) {
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }			

			if (ALWAYS_REDEEM) {
				#pragma warning disable CS0162 // Unreachable code detected
				Redeem(intr, REDEMPTION_ITEM);
				#pragma warning restore CS0162 // Unreachable code detected
			}

			string invokeKey = intr.GameAccount.GetSettingOrEmpty("NwInvokeKey", "GameHotkeys");
			string openInventoryKey = intr.GameAccount.GetSettingOrEmpty("NwInventoryKey", "GameHotkeys");

			// Collect Enchanted Key
			if (enchKeyIsPending) {
				if (!ClaimEnchantedKey(intr)) {
					// ***** Can Remove This *****
					if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }
					intr.Log("Unable to collect enchanted key for character " + charIdx + ".", 
						LogEntryType.FatalWithScreenshot);
				}
			}			

			// First Invoke Attempt
			Keyboard.SendKey(intr, invokeKey);

			if (Screen.ImageSearch(intr, "InvocationMaximumBlessings").Found) {
				intr.Log("Maximum blessings reached for character " + charIdx 
					+ ". Redeeming through Vault of Piety...", LogEntryType.Info);
				Redeem(intr, REDEMPTION_ITEM);
				//intr.ExecuteStatement("MoveAround()");
				MoveAround(intr);
				//intr.Log("Redeeming Vault of Piety...", LogEntryType.Info);
				Keyboard.SendKey(intr, invokeKey);
			}

			if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionWindowTitle").Found) {
				intr.Wait(900);

				if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionInvokeReady").Found) {
					intr.Wait(2000);
					//Invoke
					Keyboard.SendKey(intr, invokeKey);
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionDoneForDay").Found) {
					intr.Log("Invocation already finished for the day on character " + charIdx);
					return CompletionStatus.DayComplete;
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionPatience").Found) {
					intr.Log("Still waiting to invoke on character " + charIdx);
					return CompletionStatus.Immature;
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionNotInRestZone").Found) {
					intr.Log("Character " + charIdx + " not in rest zone.", LogEntryType.Fatal);
					return CompletionStatus.Complete;			
				} else if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionItemsInOverflow").Found) {
					intr.Log("Items in overflow bag are preventing invocation for character " 
						+ charIdx + ". Attempting to move to regular inventory...", LogEntryType.Error);
					intr.Wait(2000);
					MoveAround(intr);	
					Keyboard.SendKey(intr, openInventoryKey);
					intr.Wait(3000);
					Mouse.ClickImage(intr, "InventoryOverflowTransferButton");
					intr.Wait(2000);
					MoveAround(intr);
					Keyboard.SendKey(intr, invokeKey);
				} else {
					intr.Log("[INITIAL_0]NEEDS HANDLING -- Unable to invoke for character " + charIdx 
						+ ".", LogEntryType.FatalWithScreenshot);
					//ClearDialogues(intr);
					intr.Wait(30000);
					return CompletionStatus.Failed;
				}
            }

			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }
			intr.Wait(3500);			

			if (!intr.WaitUntil(3, DialogueBoxState.InvocationSuccess, Game.IsDialogueBoxState, null, 0)) {				
				Keyboard.SendKey(intr, invokeKey);				
				intr.Wait(1500);
				MoveAround(intr);		
			}

			if (Screen.ImageSearch(intr, "InvocationRewardsOfDevotionWindowTitle").Found) {
				intr.Log("Closing Rewards of Devotion window and reassessing invocation success...", LogEntryType.Debug);
				//intr.ExecuteStatement("FindAndClick(\"InvocationRewardsOfDevotionCloseButton.png\")");
				//Mouse.ClickImage(intr, "InvocationRewardsOfDevotionCloseButton");
			}

			if (!intr.WaitUntil(9, DialogueBoxState.InvocationSuccess, Game.IsDialogueBoxState, null, 0)) {
				intr.Log("[FINAL_1]NEEDS HANDLING -- Unable to invoke.", LogEntryType.FatalWithScreenshot);
				//intr.SaveErrorScreenshot();				
				intr.Wait(30000);	// TEMP
				return CompletionStatus.Failed;
			}

			intr.Log("Invocation successful. Continuing...", LogEntryType.Info);

			// BRING BACK ALTAR DEPLOYMENT:

			//if (FindMaxBlessings()) {
			//	Redeem(vault_purchase)
			//}
			//if (couldn't invoke) {
			//             MoveAround()
			//             DeployAltar()
			//}
			//Sleep 200
			//Send {% NwInvokeKey %}

			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }

			return CompletionStatus.Complete;
		}
	}
}


//Invoke(first_run, vault_purchase) {
//	global
//	invo_poss := 0
//    invo_started:= 0
//    invo_altar_deployed:= 0
//    if (first_run) {
//		Sleep 500 + Ran(200)
//        MoveAround()
//        Sleep 500 + Ran(200)
//    }

//	while (invo_started == 0) {
//		MoveAround()
//        Sleep 500
//        invo_poss += FindInvokePossible()

//		; Send {% NwInvokeKey %}
//		; Sleep 200
//		 ; Send {% NwInvokeKey %}
//		; Sleep 200

//		Sleep % BeforeInvokeDelay %

//		Send {% NwInvokeKey % down}
//		sleep 180
//        Send {% NwInvokeKey % up}
//		sleep % AfterInvokeDelay %


//		if (invo_poss < 1) {
//			sleep 500
//            invo_poss += FindInvokePossible()
//        }

//		Sleep 100

//		if (FindMaxBlessings()) {
//			; == Max Blessing Windows Found == ---Maybe Move this to ConfirmInvoke()-- -
//Redeem(vault_purchase)
//            LogAppend("[CWA Purchased]")
//            invo_started:= 0
//            continue
//        } else if (0) {
//			; == Unreachable Code ==
//            continue
//        } else {
//			; == Normal Situation ==
//            if (invo_poss < 1) {
//				sleep 500
//                invo_poss += FindInvokePossible()
//            }

//			if (invo_poss > 0) {
//				; == FindInvokePossible() was true ==
//invo_started := 1
//                LogAppend("[Invoke Started]")
//            } else {
//				; == FindInvokePossible() was false, let's find out why ==
//                LogAppend("[Invoke(): Checking CrashCheckRestart()")
//                if (CrashCheckRestart()) {
//				invo_started:= 0
//                    continue
//                }

//				if (invo_altar_deployed) {
//					LogAppend("[Altar Deployed but Cannot Invoke]")
//                    return 0
//                }

//				MoveAround()

//				if (DeployAltar()) {
//					LogAppend("[Altar Deployed]")
//                    invo_altar_deployed:= 1
//                } else {
//					LogAppend("[Altar Not Found - Invoke Failed]")
//                    return 0
//                }

//			invo_started:= 0
//                continue
//            }
//			Sleep 150 + Ran(20)
//            invo_started:= 1
//        }
//	}

//	Sleep 200
//    Send {% NwInvokeKey %}

//	return 1
//}