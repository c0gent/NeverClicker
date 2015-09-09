using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static void ProcessCharacter(
					Interactor intr,
					TaskQueue queue
		) {			
			uint charZeroIdx = queue.NextTask.CharacterZeroIdx;
			uint charOneIdx = charZeroIdx + 1;
			string charZeroIdxLabel = queue.NextTask.CharZeroIdxLabel;
            int invokesToday = intr.GameAccount.GetSettingOrZero("InvokesToday", charZeroIdxLabel);
			DateTime invokesCompletedOn;
			DateTime.TryParse(intr.GameAccount.GetSetting("InvokesCompleteFor", charZeroIdxLabel), out invokesCompletedOn);
			CompletionStatus invokeStatus = CompletionStatus.None;

			intr.Log("Starting processing for character " + charZeroIdx + " ...", LogEntryType.Normal);


			if ((invokesToday >= 6) && (queue.NextTask.Type == GameTaskType.Invocation)) {
				if (invokesCompletedOn == TaskQueue.TodaysGameDate()) {
					intr.Log(charZeroIdxLabel + " has already invoked 6 times today. Queuing invocation for tomorrow", LogEntryType.Normal);
					queue.Pop();
					queue.QueueSubsequentTask(intr, invokesToday, charZeroIdx);
				} else if (invokesCompletedOn < TaskQueue.TodaysGameDate()) {
					intr.Log(charZeroIdxLabel + ": Resetting InvokesToday to 0.", LogEntryType.Debug);
					invokesToday = 0;
					intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charZeroIdxLabel);
				}
			}

			if (!ProduceClientState(intr, ClientState.CharSelect)) { return; }

			if (intr.CancelSource.IsCancellationRequested) { return; }

			intr.Log("ProcessCharacter(): Selecting character " + charZeroIdx + " ...", LogEntryType.Info);
			if (!SelectCharacter(intr, charZeroIdx)) { return; }
			

			// ################################### INVOCATION #####################################
			intr.Log("ProcessCharacter(): Invoking for character " + charZeroIdx + " ...", LogEntryType.Info);
			if (invokesToday < 6) {
				invokeStatus = Invoke(intr);
			}			

			intr.Log("ProcessCharacter(): Completion status: " + invokeStatus.ToString(), LogEntryType.Info);

			if (invokeStatus == CompletionStatus.Complete || invokeStatus == CompletionStatus.DayComplete) {
				//if (intr.CancelSource.IsCancellationRequested) { return; }
				intr.Log("Task for character " + charZeroIdx.ToString() + ": Complete.", LogEntryType.Normal);						
				queue.Pop(); // COMPLETE
				if (invokeStatus == CompletionStatus.DayComplete) {
					invokesToday = 6;
					intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charZeroIdxLabel);
				} else {
					invokesToday += 1; // NEED TO DETECT THIS IN-GAME
				}
				SaveCharacterSettings(intr, invokesToday, charZeroIdx);
				queue.QueueSubsequentTask(intr, invokesToday, charZeroIdx);
			} else if (invokeStatus == CompletionStatus.Immature) {
				//if (intr.CancelSource.IsCancellationRequested) { return; }
				intr.Log("Task for character " + charZeroIdx.ToString() + ": Immature.", LogEntryType.Normal);
				intr.Log("Re-queuing task for character " + charZeroIdx.ToString() + ".", LogEntryType.Normal);
				queue.Pop();
				queue.QueueSubsequentTask(intr, invokesToday, charZeroIdx);
			} else if (invokeStatus == CompletionStatus.Failed) {
				intr.Log("Task for character " + charZeroIdx.ToString() + ": Failed.", LogEntryType.Normal);
			} else if (invokeStatus == CompletionStatus.Cancelled) {
				intr.Log("Task for character " + charZeroIdx.ToString() + ": Cancelled.", LogEntryType.Normal);
			}


			// ################################## PROFESSIONS #####################################
			intr.Log("ProcessCharacter(): Maintaining profession tasks for character " + charZeroIdx + " ...", LogEntryType.Info);

			MaintainProfs(intr);






			// #################################### LOGOUT ########################################
			LogOut(intr);
		}

		// SaveCharacterSettings(): Save relevant settings to .ini file
		public static void SaveCharacterSettings(Interactor intr, int invokesToday, uint charZeroIdx) {
			// SAVE SETTINGS TO INI
			// UpdateIni() <<<<< CREATE
			string charZeroIdxLabel = "Character " + charZeroIdx.ToString();

			try {
				//string dateTimeFormattedClassic = FormatDateTimeClassic(intr, DateTime.Now);
				intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charZeroIdxLabel);
				intr.GameAccount.SaveSetting(DateTime.Now.ToString(), "MostRecentInvocationTime", charZeroIdxLabel);
				intr.GameAccount.SaveSetting(charZeroIdx.ToString(), "CharZeroIdxLastInvoked", "Invocation");
				intr.Log("Settings saved to ini for: " + charZeroIdxLabel + ".", LogEntryType.Debug);
			} catch (Exception ex) {
				intr.Log("Interactions::Sequences::AutoCycle(): Problem saving settings: " + ex.ToString(), LogEntryType.Error);
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