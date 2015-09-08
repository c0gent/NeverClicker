using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IniParser;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		//									     15min,   30min,   45min,   60min,   90min,
		public static int[] InvokeDelays = { 0, 900000, 1800000, 2700000, 3600000, 5400000, 0, 0 };
		//public static TimeSpan[] InvokeDelays = { 0, 900000, 1800000, 2700000, 3600000, 5400000, 0, 0 };

		public static void AutoCycle(
					Interactor intr,
					GameTaskQueue queue,
					int startDelaySec
        ) {
			intr.Wait(startDelaySec * 1000);
			if (intr.CancelSource.IsCancellationRequested) { return; }

			// ##### INITIALIZE #####
			//int charOneIdxLastInvoked = 0;
			int charsZeroIdxTotal = 0;
			
			// ##### LOAD CHARACTER COUNT AND LAST CHARACTER INVOKED FROM INI FILE #####
			try {
				//charOneIdxLastInvoked = intr.GameAccount.GetSettingOrZero("CharZeroIdxLastInvoked", "Invocation");
				charsZeroIdxTotal = intr.GameAccount.GetSettingOrZero("CharZeroIdxCount", "NwAct");
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("'lastCharInvoked, charsTotal': " + ex.ToString());
				intr.Log("Interactions::Sequences::AutoCycle(): ERROR LOADING: lastCharInvoked OR charsTotal: " + ex.ToString(), LogEntryType.Debug);
			}

			if (queue.IsEmpty()) {
				intr.Log("Auto-populating task queue: (0 -> " + (charsZeroIdxTotal).ToString() + ")");

				// <<<<< STILL HAVE TO POPULATE WITH ONE EXTRA BECAUSE OF CHARSELECT BUG IN SCRIPT >>>>>
				PopulateQueueProperly(intr, queue, charsZeroIdxTotal);
				//queue.Populate(0, (uint)charsOneIdxTotal);

				intr.UpdateQueueList(queue.TaskList);
			}

			intr.Log("Beginning AutoCycle.");
			//intr.InitOldScript();
			intr.Wait(500);
			
			// ##### BEGIN AUTOCYCLE LOOP #####
			while (!queue.IsEmpty() && !intr.CancelSource.IsCancellationRequested) {
				if (intr.CancelSource.IsCancellationRequested) { return; }

				if (IsCurfew()) {
					intr.Log("Curfew half-hour. Sleeping for 30 minutes.");
					intr.Wait(1800000); // 30 minutes
				}

				intr.Log("AutoCycle():while: Loop iteration started.", LogEntryType.Debug);
				TimeSpan nextTaskWaitTime = queue.NextTaskWaitTime();
				uint charZeroIdx = queue.NextTask.CharacterZeroIdx;
				uint charOneIdx = charZeroIdx + 1;
				string charOneIdxLabel = queue.NextTask.CharacterOneIdxLabel;
                int invokesToday = intr.GameAccount.GetSettingOrZero("InvokesToday", charOneIdxLabel);
				var invokesCompletedOn = DateTime.Now.AddDays(-99);

				try {
					invokesCompletedOn = DateTime.Parse(intr.GameAccount.GetSetting("InvokesCompleteFor", charOneIdxLabel));
				} catch (Exception ex) {					
					intr.Log("Failed to parse ini setting: InvokesCompleteFor, exception: " + ex.ToString(), LogEntryType.Debug);
					// Just carry on, the ini entry probably hasn't been created yet
				}
				
				if (nextTaskWaitTime.Ticks <= 0) { // TASK TIMER HAS MATURED -> CONTINUE
					// DETERMINE IF WE'VE ALREADY INVOKED TOO MANY TIMES TODAY
					if ((invokesToday >= 6) && (queue.NextTask.Type == GameTaskType.Invocation)) {
						if (invokesCompletedOn == TodaysGameDate()) {
							intr.Log(charOneIdxLabel + " has already invoked 6 times today. Requeuing for tomorrow", LogEntryType.Normal);
							queue.Pop();
							QueueSubsequentTask(intr, queue, invokesToday, charZeroIdx, charOneIdxLabel);

							//intr.Log("Continuing AutoCycle loop.", LogEntryType.Debug);
							continue;
						} else if (invokesCompletedOn < TodaysGameDate()) {
							intr.Log(charOneIdxLabel + ": Resetting InvokesToday to 0.", LogEntryType.Debug);
							invokesToday = 0;
							intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charOneIdxLabel);
						}
					}

					intr.Log("Attempting to process character " + charZeroIdx.ToString() + " ...");
					intr.Wait(100);

					// ##### ENTRY POINT -- INVOKING & PROCESSING CHARACTER #####
					var processingResult = ProcessCharacter(intr, charZeroIdx);					

					if (processingResult == CompletionStatus.Complete) {
						//if (intr.CancelSource.IsCancellationRequested) { return; }
						intr.Log("Task for character " + charZeroIdx.ToString() + ": Complete.");						
						queue.Pop(); // COMPLETE
						invokesToday += 1; // NEED TO DETECT THIS IN-GAME
						SaveCharacterSettings(intr, invokesToday, charZeroIdx, charOneIdxLabel);
						QueueSubsequentTask(intr, queue, invokesToday, charZeroIdx, charOneIdxLabel);
					} else if (processingResult == CompletionStatus.Immature) {
						//if (intr.CancelSource.IsCancellationRequested) { return; }
						intr.Log("Task for character " + charZeroIdx.ToString() + ": Immature.");
						intr.Log("Re-queuing task for character " + charZeroIdx.ToString() + ".");
						queue.Pop();
						QueueSubsequentTask(intr, queue, invokesToday, charZeroIdx, charOneIdxLabel);
					} else if (processingResult == CompletionStatus.Failed) {
						intr.Log("Task for character " + charZeroIdx.ToString() + ": Failed.");
					} else if (processingResult == CompletionStatus.Cancelled) {
						intr.Log("Task for character " + charZeroIdx.ToString() + ": Cancelled.");
						return;
					}
					
				} else { // TASK TIMER NOT MATURE YET -> WAIT
					intr.Wait(100);
					intr.Log("Next task matures in " + nextTaskWaitTime.TotalMinutes.ToString("F0") + " minutes.");

					TimeSpan waitDelayMs = new TimeSpan(0);

					if (nextTaskWaitTime.TotalMinutes > 8) {
						//waitDelayMs = nextTaskWaitTime + intr.RandomDelay(5, 25);
						waitDelayMs = intr.AddRandomDelay(nextTaskWaitTime);
						ProduceClientState(intr, ClientState.None);										
					} else if (nextTaskWaitTime.TotalMinutes > 1) {
						waitDelayMs = nextTaskWaitTime.Add(new TimeSpan(0, 5, 0));
						//intr.Log("Minimizing client and waiting " + waitDelayMs.TotalMinutes.ToString("F0") + " minutes.");						
						ProduceClientState(intr, ClientState.Inactive);
					}

					if (waitDelayMs.TotalMilliseconds > 1000) {
						intr.Log("Sleeping for " + waitDelayMs.TotalMinutes.ToString("F0") + " minutes before continuing...");
						intr.Wait(waitDelayMs);
						Screen.Wake(intr);
					}					
				}

				// GOTTA HAVE SOME DELAY HERE OR WE CRASH -- PROBABLY NO LONGER TRUE
				intr.Wait(100);
				//if (intr.CancelSource.IsCancellationRequested) { return; }				
			}

			intr.GameAccount.SaveSetting("0", "CharZeroIdxLastInvoked", "Invocation");
			intr.Log("AutoCycle(): Returning.", LogEntryType.Info);

			// CLOSE DOWN -- TEMPORARILY DISABLED -- TRANSITION TO USING GAMESTATE TO MANAGE
			//intr.EvaluateFunction("VigilantlyCloseClientAndExit");
		}
		
		// QueueSubsequentTask(): QUEUE FOLLOW UP TASK
		public static void QueueSubsequentTask(Interactor intr, GameTaskQueue queue, int invokesToday, uint charZeroIdx, string charOneIdxLabel) {
			DateTime charNextTaskTime = DateTime.Now;
			DateTime nextThreeThirty = NextThreeThirty();
			DateTime todaysInvokeDate = TodaysGameDate();
			
			if (invokesToday < 6) { // QUEUE FOR LATER TODAY
				// nextInvokeDelay: (Normal delay) + (3 min) + (1 sec * charIdx);
				var nextInvokeDelay = InvokeDelays[invokesToday] + 180000 + (charZeroIdx * 1000);
				charNextTaskTime = DateTime.Now.AddMilliseconds(nextInvokeDelay);

				// IF NEXT SCHEDULED TASK IS BEYOND THE 3:30 CURFEW, RESET FOR NEXT DAY
				if (charNextTaskTime > nextThreeThirty) {
					invokesToday = 6;
					charNextTaskTime = nextThreeThirty;
				}
			} else { // QUEUE FOR TOMORROW (NEXT 3:30AM)
				try {
					intr.Log("Interactions::Sequences::AutoCycle(): All daily invocation complete for character " 
						+ charZeroIdx + " on: " + todaysInvokeDate, LogEntryType.Debug);
					intr.GameAccount.SaveSetting(todaysInvokeDate.ToString(), "InvokesCompleteFor", charOneIdxLabel);
					charNextTaskTime = nextThreeThirty;
				} catch (Exception ex) {
					//System.Windows.Forms.MessageBox.Show("Error saving InvokesCompleteFor" + ex.ToString());
                    intr.Log("Error saving InvokesCompleteFor" + ex.ToString(), LogEntryType.Error);
					//System.Windows.Forms.MessageBox()
				}
			}

			try {
				intr.Log("Next task for character at: " + charNextTaskTime.ToShortTimeString() + ".");
				queue.Add(new GameTask(charNextTaskTime.AddSeconds(charZeroIdx), charZeroIdx, GameTaskType.Invocation));
				intr.UpdateQueueList(queue.TaskList);
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("Error adding new task to queue: " + ex.ToString());
                intr.Log("Error adding new task to queue: " +ex.ToString(), LogEntryType.Error);
			}
		}


		// SaveCharacterSettings(): Save relevant settings to .ini file
		public static void SaveCharacterSettings(Interactor intr, int invokesToday, uint charZeroIdx, string charOneIdxLabel) {
			// SAVE SETTINGS TO INI
			// UpdateIni() <<<<< CREATE
			try {
				//string dateTimeFormattedClassic = FormatDateTimeClassic(intr, DateTime.Now);
				intr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charOneIdxLabel);
				intr.GameAccount.SaveSetting(DateTime.Now.ToString(), "MostRecentInvocationTime", charOneIdxLabel);
				intr.GameAccount.SaveSetting(charZeroIdx.ToString(), "CharZeroIdxLastInvoked", "Invocation");
				intr.Log("Settings saved to ini for: " + charOneIdxLabel + ".", LogEntryType.Debug);
			} catch (Exception ex) {
				intr.Log("Interactions::Sequences::AutoCycle(): Problem saving settings: " + ex.ToString());
			}
		}


		// PopulateQueueProperly(): Populate queue taking in to account last invoke times
		private static void PopulateQueueProperly(Interactor intr, GameTaskQueue queue, int charOneIdxMax) {
			for (uint i = 0; i < charOneIdxMax; i++) {
				var charOneIdxLabel = "Character " + (i + 1).ToString();
				DateTime mostRecentInvTime = DateTime.Now;
				int invokesToday = 0;

				if (!DateTime.TryParse(intr.GameAccount.GetSetting("MostRecentInvocationTime", charOneIdxLabel), out mostRecentInvTime)) {
					mostRecentInvTime = DateTime.Now.AddHours(-24);
                }

				if (!int.TryParse(intr.GameAccount.GetSetting("InvokesToday", charOneIdxLabel), out invokesToday)) {
					invokesToday = 0;
				}

				DateTime taskMatureTime = mostRecentInvTime + new TimeSpan(0, 0, 0, 0, InvokeDelays[invokesToday]);

				intr.Log("Adding task to queue for character " + (i - 1).ToString() + ", matures: " + taskMatureTime.ToString(), LogEntryType.Info);

				queue.Add(new GameTask(taskMatureTime, i, GameTaskType.Invocation));
			}

		}
		
		public static bool IsCurfew() {
			var now = DateTime.Now;
			return ((now > now.Date.AddHours(3)) && (now < now.Date.AddHours(3).AddMinutes(30)));
		}
		
		public static DateTime TodaysGameDate() {
			return NextThreeThirty().Date.AddDays(-1);
		}

		public static DateTime NextThreeThirty() {
			var now = DateTime.Now;
			var todayThreeThirty = now.Date.AddHours(3).AddMinutes(30);
			return now <= todayThreeThirty ? todayThreeThirty : todayThreeThirty.AddDays(1);
		}


		// <<<<< FOR THE OLD LOG (DEPRICATE) >>>>>
		public static string FormatDateTimeClassic(Interactor intr, DateTime dt) {
			string str = "";

			try {
				str = string.Format("{0:yyyyMMddhhmmss}", dt);
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("Interactions::Sequences::FormatDateTimeClassic():" + ex.ToString());
				intr.Log("Interactions::Sequences::FormatDateTimeClassic():" + ex.ToString());
				return "00000000000000";
			}

			return str;
		}
	}

}


//DateTime dt = new DateTime(2008, 3, 9, 16, 5, 7, 123);
//String.Format("{0:y yy yyy yyyy}", dt);  // "8 08 008 2008"   year
//String.Format("{0:M MM MMM MMMM}", dt);  // "3 03 Mar March"  month
//String.Format("{0:d dd ddd dddd}", dt);  // "9 09 Sun Sunday" day
//String.Format("{0:h hh H HH}",     dt);  // "4 04 16 16"      hour 12/24
//String.Format("{0:m mm}",          dt);  // "5 05"            minute
//String.Format("{0:s ss}",          dt);  // "7 07"            second
//String.Format("{0:f ff fff ffff}", dt);  // "1 12 123 1230"   sec.fraction
//String.Format("{0:F FF FFF FFFF}", dt);  // "1 12 123 123"    without zeroes
//String.Format("{0:t tt}",          dt);  // "P PM"            A.M. or P.M.
//String.Format("{0:z zz zzz}",      dt);  // "-6 -06 -06:00"   time zone



// ##### OLD SCRIPT #####
//intr.EvaluateFunction("ActivateNeverwinter");
//intr.Wait(4000);
//intr.EvaluateFunction("AutoInvoke");
//EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad, FirstRun, VaultPurchase)
//intr.Log("AutoInvokeAsync complete.");