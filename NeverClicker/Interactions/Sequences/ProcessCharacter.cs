using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static CompletionStatus ProcessCharacter(
					Interactor intr,
					uint charZeroIdx
		) {
			intr.Log("ProcessCharacter(): Starting processing for character " + charZeroIdx + " ...", LogEntryType.Info);

			if (ProduceClientState(intr, ClientState.CharSelect)) {
				if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }

				//uint vaultPurchase = 5; // CONVERT TO SETTING

				// <<<<< FIX THIS LATER - GET RID OF CharOneIdx >>>>>
				var charOneIdx = (charZeroIdx + 1).ToString();

				// BREAK UP
				//var cmdString = "EnterWorldInvoke(1, 0, " + charOneIdx + ", 0, 0, 5)";
				//intr.Log(cmdString, LogEntryType.Debug);
				//intr.ExecuteStatement(cmdString);

				intr.Log("ProcessCharacter(): Selecting character " + charZeroIdx + " ...", LogEntryType.Info);
				if (!SelectCharacter(intr, charZeroIdx)) { return CompletionStatus.Failed; }
				
				var invokeStatus = Invoke(intr);
				
				//ProduceClientState(intr, ClientState.CharSelect);
				LogOut(intr);

				intr.Log("ProcessCharacter(): Completion status: " + invokeStatus.ToString(), LogEntryType.Info);
				return invokeStatus;
			} else {
				return CompletionStatus.Failed;
			}		
		}

	}
}

//EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad := 0, FirstRun := 0, VaultPurchase := 5) {
//	global

//	; invo_gap:= A_Now
//   ; invo_gap -= MostRecentInvocationTime, Minutes
//   invo_gap := 60
//    LogAppend("[EnterWorldInvoke(): invoke_mode:".invoke_mode. ", MostRecentInvocationTime:".MostRecentInvocationTime. ", CurrentCharacter:".CurrentCharacter. ", AutoUiBindLoad:".AutoUiBindLoad. ", FirstRun:".FirstRun. ", VaultPurchase:".VaultPurchase. ", invo_gap:".invo_gap. "]")

//	if ((invo_gap > 15) || (invoke_mode == 2)) {
//		SelectCharacter(CurrentCharacter, 1, 0)

//		While(!(FindLoggedIn())) {


//			; ##### IF IT'S OUR SECOND TRY, CLOSE POP-UP DIALOGUES
//			if (A_Index > 2) {
//				LogAppend("[EnterWorldInvoke(): (FindLoggedIn() = '".FindLoggedIn(). "')  (Attempt: ".A_Index. ")]")
//                ClearOkPopupBullshit()
//                ClearSafeLogin()
//            }

//		   ; ##### IF WE'VE BEEN AT IT FOR 10 TRIES, CHECK TO SEE IF WE'VE CRASHED
//			if ((A_Index >= 15)) {
//				LogAppend("[EnterWorldInvoke(): Checking CrashCheckRestart()]")
//                if (CrashCheckRestart()) {
//					Sleep 5000
//                    SelectCharacter(CurrentCharacter, 1, 0)
//                    continue
//                }
//			}

//		   ; ##### GIVE UP AND ABORT
//			if (A_Index == 30) {
//			err_str:= "[*** AutoInvoke(): Error Logging In. Aborting. ***]"
//                LogAppend(err_str)

//				Sleep 5000
//                return 0
//            }

//		   ; ##### SLEEP BEFORE NEXT 'ARE WE LOGGED IN' CHECK
//			Sleep 1200
//        }
//		; #####
//		; #####	END ENTER WORLD
//		; #####
		
		
//		; ##### LOAD BINDINGS AND UI IF NEEDED
//		if (AutoUiBindLoad) {
//			if (FirstRun) {
//				; *****NOTE: WHAT IS FIRSTRUN ALL ABOUT? DO WE NEED IT?
//			   LoadBindAndUi(1)
//            } else {
//				LoadBindAndUi(0)
//            }
//		}

//		; #####
//		; #####	BEGIN INVOKE - INVOCATION ENTRY POINT
//		; #####
		
//		While(FindLoggedIn()) {

//			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): PRE-INVOKE]")

//			Invoke(FirstRun, VaultPurchase)

//			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): POST-INVOKE]")



//			if (invoke_mode == 2) {
//				LogAppend("[*****TRACE: ENTERWORLDINVOKE(): REEDEEMING]")
//                Redeem(3)
//            }

//		   ; #####	CLOSE ALL POP-UPS AND LOOP UNTIL THAT IS DONE
//			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): CLEARING POPUPS]")

//			if (ClearOkPopupBullshit()) {
//				LogAppend("[*****TRACE: ENTERWORLDINVOKE(): AT LEAST ONE POPUP WAS CLEARED, ATTEMPTING TO INVOKE AGAIN]")
//                continue
//            } else {
//				LogAppend("[*****TRACE: ENTERWORLDINVOKE(): NO POPUPS WERE FOUND, COMPLETED]")
//                break
//            }

//			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): POST-INVOKE POST-CLEAR-POPUPS]")
//        }

//		LogAppend("[*****TRACE: ENTERWORLDINVOKE(): PRE-LOGOUT]")

//		Logout()

//		LogAppend("[*****TRACE: ENTERWORLDINVOKE(): POST-LOGOUT]")

//	} else {

//		if ((invo_gap <= 15) || (invoke_mode == 3)) {
//			if (invo_gap <= 15) {
//				LogAppend("[Character ".CurrentCharacter. " has been invoked within the last 15 minutes. Skipping.]")
//            }

//			if (SelectCharacter(CurrentCharacter, 1, 1) == 1) {
//				LogAppend("[*** Character ".CurrentCharacter. " was not correctly skipped. ***]")
//            }

//			UpdateInvokeLog = 0
//        }
//	}


//char_label:= "Character ".CurrentCharacter
//; LoadSetting("MostRecentInvocationTime", tmp_fakedate, char_label, as_ini)

//	if (UpdateInvokeLog) {
//		; IniWrite, % A_Now %, % as_ini %, % char_label %, MostRecentInvocationTime
//			   ; IniWrite, % invo_gap %, % as_ini %, % char_label %, PrevInvoTimeGap
//					  LogAppend("[Logging Out]")
//    }

//	; IniWrite, % CurrentCharacter %, % as_ini %, Invocation, LastCharacterInvoked

//		 LogAppend("[EnterWorldInvoke(): Returning 1]")

//	return 1
//}