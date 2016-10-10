using NeverClicker.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		const bool ENTER_WORLD = true;
		const int SELECT_ATTEMPTS_MAX = 10;

		public static void ProcessCharacter(
					Interactor intr,
					TaskQueue queue
		) {			
			var currentTask = queue.NextTask;
			uint charIdx = currentTask.CharIdx;
			string charLabel = intr.AccountSettings.CharNode(charIdx).GetAttribute("Name");
			//string charLabel = queue.NextTask.CharIdxLabel;
			int invokesToday = intr.AccountStates.GetCharStateOr(charIdx, "invokesToday", 0);
			bool skipInvocation = false;
			bool skipMaintInven = false;
			bool skipProfessions = false;

			DateTime invokesCompletedForDay = intr.AccountStates.GetCharStateOr(charIdx, 
				"invokesCompleteFor", Global.Default.SomeOldDate);

			if (invokesCompletedForDay == TaskQueue.TodaysGameDate) {
				// Skip invocation if it's already done.
				skipInvocation = true;
			} else if (invokesCompletedForDay < TaskQueue.TodaysGameDate && invokesToday < 2 && 
							currentTask.Kind == TaskKind.Invocation) {
				// Skip professions for the first few invokes of the day and inventory for some of the middle ones:
				intr.Log(LogEntryType.Info, "Skipping profession processing this round.");
				skipProfessions = true;

				if (invokesToday != 0) {
					intr.Log(LogEntryType.Info, "Skipping inventory processing this round.");
					skipMaintInven = true;
				}				
			}

			CompletionStatus invocationStatus = CompletionStatus.None;
			CompletionStatus professionsStatus = CompletionStatus.None;
			CompletionStatus maintStatus = CompletionStatus.None;			
			bool processingIncomplete = false;			

			intr.Log("Starting processing for character [" + charIdx + "] ...");

			// Reset `invokesToday` if `invokesCompletedOn` is 
			if (invokesToday >= 6) {
				if (currentTask.Kind == TaskKind.Invocation) {
					if (invokesCompletedForDay == TaskQueue.TodaysGameDate) {
						intr.Log(LogEntryType.Info, "Character [" + charIdx + 
							"] has already invoked 6 times today. Queuing invocation for tomorrow");
						queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);
						skipInvocation = true;
						skipMaintInven = true;
						//queue.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
						return;
					} else if (invokesCompletedForDay < TaskQueue.TodaysGameDate) {
						intr.Log(LogEntryType.Info, "Character [" + charIdx +  "]: Resetting InvokesToday to 0.");
						invokesToday = 0;
						intr.AccountStates.SaveCharState(invokesToday, charIdx, "invokesToday");
					} else {
						var errMsg = "Character [" + charIdx +  "]: Internal error. `invokesCompletedOn` is in the future.";
						intr.Log(LogEntryType.Fatal, errMsg);
						throw new Exception(errMsg);
					}
				}
			}

			// CHECK TO SEE IF THERE ARE ANY UPCOMING TASKS FOR CHARACTER IN THE NEXT 29:59 MINUTES
			// IF SO -> CHECK TO SEE IF THERE ARE ANY TASKS IN THE 29:59 (TOTAL 59:59) MINUTES AFTER THAT
			//	IF NOT -> MERGE TASKS
			//	IF SO -> CONTINUE

			if (!ProduceClientState(intr, ClientState.CharSelect, 0)) { return; }

			intr.Log(LogEntryType.Info, "ProcessCharacter(): Selecting character [" + charIdx + "] ...");

			if (!SelectCharacter(intr, charIdx, ENTER_WORLD)) {
				return;
			}

			// [DO NOT REMOVE]:
			//
			//int selectAttemptCount = 0;

			//while (!SelectCharacter(intr, charIdx, ENTER_WORLD)) {
			//	// Determine if login has been a success:
			//	if (!intr.WaitUntil(90, ClientState.InWorld, Game.IsClientState, CharSelectFailure, 0)) {
			//		ProduceClientState(intr, ClientState.CharSelect, attemptCount);
			//		SelectCharacter(intr, charIdx, enterWorld, attemptCount);
			//		//return false;
			//	}
			//	ClearDialogues(intr);

			//	selectAttemptCount += 1;

			//	if (selectAttemptCount >= SELECT_ATTEMPTS_MAX) {
			//		intr.Log("ProcessCharacter(): Fatal error selecting character [" + charIdx + "].", LogEntryType.Fatal);
			//		return;
			//	}
			//}

			// NEW PLAN:
			// SelectCharacter()
			// Determine if client state is still ClientState.CharSelect
			//    If so, FatalWithScreenShot
			//    If not, start the 'Verification' loop which will look for signs of the 'World'
			//
			//
			// END [DO NOT REMOVE]

			if (!ENTER_WORLD) {
				#pragma warning disable CS0162 // Unreachable code detected
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);
				#pragma warning restore CS0162 // Unreachable code detected
				return;
			}

			// ################################## CLEAR AND MOVE ##################################
			intr.Wait(1500);
			ClearDialogues(intr);
			MoveAround(intr);

			// ############################### INVENTORY MAINTENANCE ##############################
			if (!skipMaintInven) {
				intr.Log(LogEntryType.Info, "ProcessCharacter(): Maintaining inventory for character [" + charIdx + "] ...");
				maintStatus = MaintainInventory(intr, charIdx);
				intr.Log(LogEntryType.Info, "ProcessCharacter(): Inventory maintenance status: " + maintStatus.ToString());
			}

			// #################################### INVOCATION ####################################
			if (!skipInvocation) {
				intr.Log(LogEntryType.Info, "ProcessCharacter(): Invoking for character [" + charIdx + "] ...");
				invocationStatus = Invoke(intr, charIdx);
				intr.Log(LogEntryType.Info, "ProcessCharacter(): Invocation status: " + invocationStatus.ToString());
			}

			// ################################### PROFESSIONS ####################################
			var professionTasksProcessed = new List<ProfessionTaskResult>(9);

			if (!skipProfessions) {
				intr.Log(LogEntryType.Info, "ProcessCharacter(): Maintaining profession tasks for character [" + charIdx + "] ...");				
				professionsStatus = MaintainProfs(intr, charIdx, professionTasksProcessed);
				intr.Log(LogEntryType.Info, "ProcessCharacter(): Professions status: " + professionsStatus.ToString());
			}


			// ##################################### LOG OUT ######################################
			LogOut(intr);

			// ########################### INVOCATION QUEUE AND SETTINGS ##########################
			if (invocationStatus == CompletionStatus.Complete) {
				intr.Log(LogEntryType.Normal, "Invocation task for character [" + charIdx.ToString() + "]: Complete.");
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, true);
			} else if (invocationStatus == CompletionStatus.DayComplete) {
				intr.Log(LogEntryType.Normal, "Daily invocations for character [" + charIdx.ToString() + "]: Complete for day.");
				queue.AdvanceInvocationTask(intr, charIdx, 6, true);
			} else if (invocationStatus == CompletionStatus.Immature && currentTask.Kind == TaskKind.Invocation) {
				intr.Log(LogEntryType.Normal, "Invocation task for character [" + charIdx.ToString() + "]: Immature.");
				intr.Log(LogEntryType.Info, "Re-queuing task for character [" + charIdx.ToString() + "].");
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);				
			} else if (invocationStatus == CompletionStatus.Failed && currentTask.Kind == TaskKind.Invocation) {
				intr.Log(LogEntryType.Normal, "Invocation task for character [" + charIdx.ToString() + "]: Failed.");				
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);
				//processingIncomplete = true;
			} else if (invocationStatus == CompletionStatus.Cancelled && currentTask.Kind == TaskKind.Invocation) {
				intr.Log(LogEntryType.Normal, "Invocation task for character [" + charIdx.ToString() + "]: Cancelled.");
				//processingIncomplete = true;
			}
			
			// ######################### PROFESSIONS QUEUE AND SETTINGS ###########################			
			intr.Log(LogEntryType.Normal, "Profession task for character [" + charIdx.ToString() + "]: " + professionsStatus.ToString()
					+ ", items complete: " + professionTasksProcessed.Count);
			if (professionsStatus == CompletionStatus.Complete) {				
				foreach (ProfessionTaskResult taskResult in professionTasksProcessed) {
					queue.AdvanceProfessionsTask(intr, charIdx, taskResult.TaskId, taskResult.BonusFactor);
				}

				// SHOULD BE HANDLED BY `ADVANCEMATURED()` AND SEEMS BUGGY ANYWAY:
				//if (currentTask.Kind == TaskKind.Profession) {
				//	queue.AdvanceProfessionsTask(intr, currentTask.CharIdx, currentTask.TaskId);  // SAME
				//}
			} else if (professionsStatus == CompletionStatus.Immature && currentTask.Kind == TaskKind.Profession) {
				// UNUSED?
				// [TODO]: Remove this section.
				// SHOULD BE HANDLED BY `ADVANCEMATURED()` AND SEEMS BUGGY ANYWAY:
				queue.AdvanceProfessionsTask(intr, currentTask.CharIdx, currentTask.TaskId, currentTask.BonusFactor);
			} else if (currentTask.Kind == TaskKind.Profession) {
				// IF the status was NOT `CompletionStatus.Complete` AND the next task is a profession task for this character:
				processingIncomplete = true;
				// CANCELLED OR FAILED
				//queue.AdvanceTask(intr, currentTask.CharIdx, TaskKind.Profession, currentTask.TaskId);		// SAME
			}

			//// EVALUATE WHETHER OR NOT TO BRING THIS BACK:
			//if (!processingIncomplete && !skipProfessions && !skipInvocation) {
			//	intr.Log(LogEntryType.Normal, "Advancing all matured tasks for character {0}.", charIdx.ToString());
			//	queue.AdvanceMatured(intr, charIdx);
			//}

			if (processingIncomplete) {
				intr.Log(LogEntryType.Normal, "Processing incomplete for character [" + charIdx + "].");
			} else {
				intr.Log(LogEntryType.Normal, "Processing complete for character [" + charIdx + "].");
			}			
		}		
	}
}
