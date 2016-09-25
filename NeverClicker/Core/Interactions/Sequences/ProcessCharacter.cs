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
			string charLabel = queue.NextTask.CharZeroIdxLabel;
			int invokesToday = intr.GameAccount.GetSettingOrZero("InvokesToday", charLabel);
			DateTime invokesCompletedOn;
			DateTime.TryParse(intr.GameAccount.GetSettingOrEmptyString("InvokesCompleteFor", charLabel), out invokesCompletedOn);
			CompletionStatus invocationStatus = CompletionStatus.None;
			CompletionStatus professionsStatus = CompletionStatus.None;
			CompletionStatus maintStatus = CompletionStatus.None;
			var professionsCompleted = new List<int>();
			bool processingIncomplete = false;
			bool enchKeyIsPending = IsEnchantedKeyPending(intr);

			intr.Log("Starting processing for character " + charIdx + " ...", LogEntryType.Normal);
			
			if ((invokesToday >= 6)) {
				if (invokesCompletedOn == TaskQueue.TodaysGameDate) {
					intr.Log(charLabel + " has already invoked 6 times today. Queuing invocation for tomorrow", LogEntryType.Info);
					queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);
					//queue.QueueSubsequentInvocationTask(intr, charIdx, invokesToday);
					return;
				} else if (invokesCompletedOn < TaskQueue.TodaysGameDate) {
					intr.Log(charLabel + ": Resetting InvokesToday to 0.", LogEntryType.Info);
					invokesToday = 0;
					intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charLabel);
				}
			} else {
				if (queue.NextTask.Kind == TaskKind.Professions) {
					queue.PostponeUntilNextInvoke(intr, charIdx);
				}
			}


			// CHECK TO SEE IF THERE ARE ANY UPCOMING TASKS FOR CHARACTER IN THE NEXT 29:59 MINUTES
			// IF SO -> CHECK TO SEE IF THERE ARE ANY TASKS IN THE 29:59 (TOTAL 59:59) MINUTES AFTER THAT
			//	IF NOT -> MERGE TASKS
			//	IF SO -> CONTINUE

			if (!ProduceClientState(intr, ClientState.CharSelect, 0)) { return; }

			intr.Log("ProcessCharacter(): Selecting character " + charIdx + " ...", LogEntryType.Info);

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
			intr.Log("ProcessCharacter(): Maintaining inventory for character " + charIdx + " ...", LogEntryType.Info);
			maintStatus = MaintainInventory(intr, charIdx, enchKeyIsPending);
			intr.Log("ProcessCharacter(): Inventory maintenance status: " + maintStatus.ToString(), LogEntryType.Info);

			// #################################### INVOCATION ####################################
			intr.Log("ProcessCharacter(): Invoking for character " + charIdx + " ...", LogEntryType.Info);
			invocationStatus = Invoke(intr, charIdx, enchKeyIsPending);
			intr.Log("ProcessCharacter(): Invocation status: " + invocationStatus.ToString(), LogEntryType.Info);

			// ################################### PROFESSIONS ####################################
			intr.Log("ProcessCharacter(): Maintaining profession tasks for character " + charIdx + " ...", LogEntryType.Info);
			professionsStatus = MaintainProfs(intr, charLabel, professionsCompleted);
			intr.Log("ProcessCharacter(): Professions status: " + professionsStatus.ToString(), LogEntryType.Info);


			// ##################################### LOG OUT ######################################
			LogOut(intr);

			// ########################### INVOCATION QUEUE AND SETTINGS ##########################
			if (invocationStatus == CompletionStatus.Complete) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Complete.", LogEntryType.Normal);
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, true);
			} else if (invocationStatus == CompletionStatus.DayComplete) {
				intr.Log("Daily invocations for character " + charIdx.ToString() + ": Complete for day.", LogEntryType.Normal);
				queue.AdvanceInvocationTask(intr, charIdx, 6, true);
			} else if (invocationStatus == CompletionStatus.Immature && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Immature.", LogEntryType.Normal);
				intr.Log("Re-queuing task for character " + charIdx.ToString() + ".", LogEntryType.Info);
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);				
			} else if (invocationStatus == CompletionStatus.Failed && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Failed.", LogEntryType.Normal);				
				queue.AdvanceInvocationTask(intr, charIdx, invokesToday, false);
				//processingIncomplete = true;
			} else if (invocationStatus == CompletionStatus.Cancelled && queue.NextTask.Kind == TaskKind.Invocation) {
				intr.Log("Invocation task for character " + charIdx.ToString() + ": Cancelled.", LogEntryType.Normal);
				//processingIncomplete = true;
			}

			// ######################### PROFESSIONS QUEUE AND SETTINGS ###########################
			intr.Log("Profession task for character " + charIdx.ToString() + ": " + professionsStatus.ToString()
					+ ", items complete: " + professionsCompleted.Count, LogEntryType.Normal);
			if (professionsStatus == CompletionStatus.Complete) {
				foreach (int taskId in professionsCompleted) {
					queue.AdvanceProfessionsTask(intr, charIdx, taskId);
				}

				if (queue.NextTask.Kind == TaskKind.Professions) {
					queue.AdvanceProfessionsTask(intr, queue.NextTask.CharIdx, queue.NextTask.TaskId);  // SAME
				}
			} else if (professionsStatus == CompletionStatus.Immature && queue.NextTask.Kind == TaskKind.Professions) { // UNUSED
				queue.AdvanceProfessionsTask(intr, queue.NextTask.CharIdx, queue.NextTask.TaskId);      // SAME
			} else if (queue.NextTask.Kind == TaskKind.Professions) {
				processingIncomplete = true;
				// CANCELLED OR FAILED
				//queue.AdvanceTask(intr, queue.NextTask.CharIdx, TaskKind.Profession, queue.NextTask.TaskId);		// SAME
			}

			if (!processingIncomplete) {
				intr.Log("Advancing all matured tasks for character " + charIdx.ToString() + ".");
				queue.AdvanceMatured(intr, charIdx);
			}

			intr.Log("Processing complete for character " + charIdx + ".", LogEntryType.Normal);
		}

		
	}
}
