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
			uint charIdx = queue.NextTask.CharIdx;
			//uint charOneIdx = charZeroIdx + 1;
			string charLabel = queue.NextTask.CharZeroIdxLabel;
            int invokesToday = intr.GameAccount.GetSettingOrZero("InvokesToday", charLabel);
			DateTime invokesCompletedOn;
			DateTime.TryParse(intr.GameAccount.GetSettingOrEmpty("InvokesCompleteFor", charLabel), out invokesCompletedOn);
			CompletionStatus invocationStatus = CompletionStatus.None;
			CompletionStatus professionsStatus = CompletionStatus.None;

			bool processingFailure = false;

			intr.Log("Starting processing for character " + charIdx + " ...", LogEntryType.Normal);

			if ((invokesToday >= 6) && (queue.NextTask.Kind == TaskKind.Invocation)) {
				if (invokesCompletedOn == TaskQueue.TodaysGameDate()) {
					intr.Log(charLabel + " has already invoked 6 times today. Queuing invocation for tomorrow", LogEntryType.Info);
					queue.Pop();
					queue.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
					return;
				} else if (invokesCompletedOn < TaskQueue.TodaysGameDate()) {
					intr.Log(charLabel + ": Resetting InvokesToday to 0.", LogEntryType.Info);
					invokesToday = 0;
					intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charLabel);
				}
			}

			// CHECK TO SEE IF THERE ARE ANY UPCOMING TASKS FOR CHARACTER IN THE NEXT 29:59 MINUTES
			// IF SO -> CHECK TO SEE IF THERE ARE ANY TASKS IN THE 29:59 (TOTAL 59:59) MINUTES AFTER THAT
			//	IF NOT -> MERGE TASKS
			//	IF SO -> CONTINUE

			if (!ProduceClientState(intr, ClientState.CharSelect)) { return; }

			if (intr.CancelSource.IsCancellationRequested) { return; }

			intr.Log("ProcessCharacter(): Selecting character " + charIdx + " ...", LogEntryType.Info);
			if (!SelectCharacter(intr, charIdx)) { return; }

			// ################################### CLEAR AND MOVE #####################################
			intr.Wait(1000);
			ClearOkButtons(intr);
			intr.Wait(200);	
			MoveAround(intr);
								
			// ################################### INVOCATION #####################################
			intr.Log("ProcessCharacter(): Invoking for character " + charIdx + " ...", LogEntryType.Info);
			invocationStatus = Invoke(intr);
			intr.Log("ProcessCharacter(): Invocation status: " + invocationStatus.ToString(), LogEntryType.Info);

			//if (queue.NextTask.Type == GameTaskType.Invocation) {
				
			//} else {
			//	invocationStatus = CompletionStatus.None;
			//}
			

			// ################################## PROFESSIONS #####################################
			intr.Log("ProcessCharacter(): Maintaining profession tasks for character " + charIdx + " ...", LogEntryType.Info);

			var completionList = new List<int>();

			professionsStatus = MaintainProfs(intr, charLabel, completionList);
			intr.Log("ProcessCharacter(): Professions status: " + professionsStatus.ToString(), LogEntryType.Info);

			
			// ###################################### LOGOUT ######################################
			LogOut(intr);


			// ########################### INVOCATION QUEUE AND SETTINGS ##########################

			if (invocationStatus == CompletionStatus.Complete || invocationStatus == CompletionStatus.DayComplete) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Complete.", LogEntryType.Normal);						
				
				if (invocationStatus == CompletionStatus.DayComplete) {
					invokesToday = 6;
					intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charLabel);
				} else {
					invokesToday += 1; // NEED TO DETECT THIS IN-GAME
				}
								
				//queue.Pop(); // COMPLETE
				//queue.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
				SaveInvocationSettings(intr, invokesToday, charIdx);
				queue.AdvanceTask(intr, charIdx, TaskKind.Invocation, true);
			} else if (invocationStatus == CompletionStatus.Immature && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Immature.", LogEntryType.Normal);
				intr.Log("Re-queuing task for character " + charIdx.ToString() + ".", LogEntryType.Info);
				
				//queue.Pop();
				//queue.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
				//if ( && queue.NextTask.Type == TaskKind.Invocation) {
					queue.AdvanceTask(intr, charIdx, TaskKind.Invocation, false);
				//}
			} else if (invocationStatus == CompletionStatus.Failed && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Failed.", LogEntryType.Normal);
				processingFailure = true;
				//queue.Pop();
				//queue.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
				//if (&& queue.NextTask.Type == TaskKind.Invocation) {
				queue.AdvanceTask(intr, charIdx, TaskKind.Invocation, false);
				//}
			} else if (invocationStatus == CompletionStatus.Cancelled && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Cancelled.", LogEntryType.Normal);
				processingFailure = true;
			}

			// ######################### PROFESSIONS QUEUE AND SETTINGS ###########################
			intr.Log("Profession task for character " + charIdx.ToString() + ": " + professionsStatus.ToString() 
					+ ", items complete: " + completionList.Count, LogEntryType.Normal);
			if (professionsStatus == CompletionStatus.Complete) {
				foreach (int taskId in completionList) {
					queue.AdvanceTask(intr, charIdx, TaskKind.Profession, taskId);
				}

				if (queue.NextTask.Kind == TaskKind.Profession) {
					queue.AdvanceTask(intr, queue.NextTask.CharIdx, TaskKind.Profession, queue.NextTask.TaskId);	// SAME
				}
			} else if (professionsStatus == CompletionStatus.Immature && queue.NextTask.Kind == TaskKind.Profession) {	// UNUSED
				queue.AdvanceTask(intr, queue.NextTask.CharIdx, TaskKind.Profession, queue.NextTask.TaskId);		// SAME
			} else if (queue.NextTask.Kind == TaskKind.Profession) {
				processingFailure = true;
				// CANCELLED OR FAILED
				//queue.AdvanceTask(intr, queue.NextTask.CharIdx, TaskKind.Profession, queue.NextTask.TaskId);		// SAME
			}

			intr.Log("Advancing all matured tasks for character " + charIdx.ToString() + ".");
			if (!processingFailure) {
				queue.AdvanceMatured(intr, charIdx);
			}

			intr.Log("Processing complete for character " + charIdx + ".", LogEntryType.Normal);
		}


		// SaveCharacterSettings(): Save relevant settings to .ini file
		public static void SaveInvocationSettings(Interactor intr, int invokesToday, uint charIdx) {
			// SAVE SETTINGS TO INI
			// UpdateIni() <<<<< CREATE
			string charLabel = "Character " + charIdx.ToString();

			try {
				//string dateTimeFormattedClassic = FormatDateTimeClassic(intr, DateTime.Now);
				intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charLabel);
				intr.GameAccount.SaveSetting(DateTime.Now.ToString(), "MostRecentInvocationTime", charLabel);
				intr.GameAccount.SaveSetting(charIdx.ToString(), "CharZeroIdxLastInvoked", "Invocation");
				intr.Log("Settings saved to ini for: " + charLabel + ".", LogEntryType.Debug);
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