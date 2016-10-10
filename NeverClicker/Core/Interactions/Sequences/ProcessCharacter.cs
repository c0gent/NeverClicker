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
			uint charIdx = queue.NextTask.CharIdx;
			string charLabel = queue.NextTask.CharIdxLabel;
			int invokesToday = intr.AccountStates.GetCharStateOr(charIdx, "InvokesToday", 0);

			//DateTime invokesCompletedOn;

			//if (!DateTime.TryParse(intr.AccountStates.GetCharState(charIdx, "InvokesCompleteFor"), out invokesCompletedOn)) {
			//	invokesCompletedOn = DateTime.Parse(Global.Default.SomeOldDateString);
			//}

			DateTime invokesCompletedOn = intr.AccountStates.GetCharStateOr(charIdx, 
				"InvokesCompleteFor", Global.Default.SomeOldDate);

			CompletionStatus invocationStatus = CompletionStatus.None;
			CompletionStatus professionsStatus = CompletionStatus.None;
			CompletionStatus maintStatus = CompletionStatus.None;			
			bool processingIncomplete = false;

			intr.Log("Starting processing for character " + charIdx + " ...");

			// Reset `invokesToday` if `invokesCompletedOn` is 
			if (invokesToday >= 6) {
				if (queue.NextTask.Kind == TaskKind.Invocation) {
					if (invokesCompletedOn == TaskQueue.TodaysGameDate) {
						intr.Log(LogEntryType.Info, charLabel + " has already invoked 6 times today. Queuing invocation for tomorrow");
						queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);
						//queue.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
						return;
					} else if (invokesCompletedOn < TaskQueue.TodaysGameDate) {
						intr.Log(LogEntryType.Info, charLabel + ": Resetting InvokesToday to 0.");
						invokesToday = 0;
						intr.AccountStates.SaveCharState(invokesToday, charIdx, "InvokesToday");
					} else {
						var errMsg = charLabel + ": Internal error. `invokesCompletedOn` is in the future.";
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

			intr.Log(LogEntryType.Info, "ProcessCharacter(): Selecting character " + charIdx + " ...");

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
			//		intr.Log("ProcessCharacter(): Fatal error selecting character " + charIdx + ".", LogEntryType.Fatal);
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
			intr.Log(LogEntryType.Info, "ProcessCharacter(): Maintaining inventory for character " + charIdx + " ...");
			maintStatus = MaintainInventory(intr, charIdx);
			intr.Log(LogEntryType.Info, "ProcessCharacter(): Inventory maintenance status: " + maintStatus.ToString());

			// #################################### INVOCATION ####################################
			intr.Log(LogEntryType.Info, "ProcessCharacter(): Invoking for character " + charIdx + " ...");
			invocationStatus = Invoke(intr, charIdx);
			intr.Log(LogEntryType.Info, "ProcessCharacter(): Invocation status: " + invocationStatus.ToString());

			// ################################### PROFESSIONS ####################################
			intr.Log(LogEntryType.Info, "ProcessCharacter(): Maintaining profession tasks for character " + charIdx + " ...");
			var professionTasksProcessed = new List<ProfessionTaskResult>(9);
			professionsStatus = MaintainProfs(intr, charLabel, professionTasksProcessed);
			intr.Log(LogEntryType.Info, "ProcessCharacter(): Professions status: " + professionsStatus.ToString());


			// ##################################### LOG OUT ######################################
			LogOut(intr);

			// ########################### INVOCATION QUEUE AND SETTINGS ##########################
			if (invocationStatus == CompletionStatus.Complete) {
				intr.Log(LogEntryType.Normal, "Invocation task for character " + charIdx.ToString() + ": Complete.");
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, true);
			} else if (invocationStatus == CompletionStatus.DayComplete) {
				intr.Log(LogEntryType.Normal, "Daily invocations for character " + charIdx.ToString() + ": Complete for day.");
				queue.AdvanceInvocationTask(intr, charIdx, 6, true);
			} else if (invocationStatus == CompletionStatus.Immature && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log(LogEntryType.Normal, "Invocation task for character " + charIdx.ToString() + ": Immature.");
				intr.Log(LogEntryType.Info, "Re-queuing task for character " + charIdx.ToString() + ".");
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);				
			} else if (invocationStatus == CompletionStatus.Failed && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log(LogEntryType.Normal, "Invocation task for character " + charIdx.ToString() + ": Failed.");				
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);
				//processingIncomplete = true;
			} else if (invocationStatus == CompletionStatus.Cancelled && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log(LogEntryType.Normal, "Invocation task for character " + charIdx.ToString() + ": Cancelled.");
				//processingIncomplete = true;
			}

			// ######################### PROFESSIONS QUEUE AND SETTINGS ###########################
			intr.Log(LogEntryType.Normal, "Profession task for character " + charIdx.ToString() + ": " + professionsStatus.ToString()
					+ ", items complete: " + professionTasksProcessed.Count);
			if (professionsStatus == CompletionStatus.Complete) {
				foreach (ProfessionTaskResult taskResult in professionTasksProcessed) {
					queue.AdvanceProfessionsTask(intr, charIdx, taskResult.TaskId, taskResult.BonusFactor);
				}

				// SHOULD BE HANDLED BY `ADVANCEMATURED()` AND SEEMS BUGGY ANYWAY:
				//if (queue.NextTask.Kind == TaskKind.Profession) {
				//	queue.AdvanceProfessionsTask(intr, queue.NextTask.CharIdx, queue.NextTask.TaskId);  // SAME
				//}
			} else if (professionsStatus == CompletionStatus.Immature && queue.NextTask.Kind == TaskKind.Profession) {
				// UNUSED?
				// [TODO]: Remove this section.
				// SHOULD BE HANDLED BY `ADVANCEMATURED()` AND SEEMS BUGGY ANYWAY:
				//queue.AdvanceProfessionsTask(intr, queue.NextTask.CharIdx, queue.NextTask.TaskId);      // SAME
			} else if (queue.NextTask.Kind == TaskKind.Profession && queue.NextTask.CharIdx == charIdx) {
				// IF the status was NOT `CompletionStatus.Complete` AND the next task is a profession task for this character:
				processingIncomplete = true;
				// CANCELLED OR FAILED
				//queue.AdvanceTask(intr, queue.NextTask.CharIdx, TaskKind.Profession, queue.NextTask.TaskId);		// SAME
			}

			if (!processingIncomplete) {
				intr.Log("Advancing all matured tasks for character " + charIdx.ToString() + ".");
				queue.AdvanceMatured(intr, charIdx);
			}

			intr.Log(LogEntryType.Normal, "Processing complete for character " + charIdx + ".");
		}		
	}
}
