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
					Interactor itr,
					GameTaskQueue queue
        ) {
			// ##### INITIALIZE #####
			//int charOneIdxLastInvoked = 0;
			int charsZeroIdxTotal = 0;
			
			// ##### LOAD CHARACTER COUNT AND LAST CHARACTER INVOKED FROM INI FILE #####
			try {
				//charOneIdxLastInvoked = itr.GameAccount.GetSettingOrZero("CharZeroIdxLastInvoked", "Invocation");
				charsZeroIdxTotal = itr.GameAccount.GetSettingOrZero("CharZeroIdxCount", "NwAct");
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("'lastCharInvoked, charsTotal': " + ex.ToString());
				itr.Log("Interactions::Sequences::AutoCycle(): ERROR LOADING: lastCharInvoked OR charsTotal: " + ex.ToString(), LogEntryType.Debug);
			}

			if (queue.IsEmpty()) {
				itr.Log("Auto-populating task queue: (0 -> " + (charsZeroIdxTotal).ToString() + ")");

				// <<<<< STILL HAVE TO POPULATE WITH ONE EXTRA BECAUSE OF CHARSELECT BUG IN SCRIPT >>>>>
				PopulateQueueProperly(itr, queue, charsZeroIdxTotal);
				//queue.Populate(0, (uint)charsOneIdxTotal);

				itr.UpdateQueueList(queue.TaskList);
			}

			itr.Log("Beginning AutoCycle.");
			itr.InitOldScript();
			itr.Wait(500);
			
			// ##### BEGIN AUTOCYCLE LOOP #####
			while (!queue.IsEmpty() && !itr.CancelSource.IsCancellationRequested) {
				if (itr.CancelSource.IsCancellationRequested) { return; }

				if (IsCurfew()) {
					itr.Log("Curfew half-hour. Sleeping for 30 minutes.");
					itr.Wait(1800000); // 30 minutes
				}

				itr.Log("Starting AutoInvoke loop.");
				TimeSpan nextTaskWaitTime = queue.NextTaskWaitTime();
				uint charZeroIdx = queue.NextTask.CharacterZeroIdx;
				uint charOneIdx = charZeroIdx + 1;
				string charOneIdxLabel = queue.NextTask.CharacterOneIdxLabel;
                int invokesToday = itr.GameAccount.GetSettingOrZero("InvokesToday", charOneIdxLabel);
				var invokesCompletedOn = DateTime.Now.AddDays(-99);

				try {
					invokesCompletedOn = DateTime.Parse(itr.GameAccount.GetSetting("InvokesCompleteFor", charOneIdxLabel));
				} catch (Exception ex) {					
					itr.Log("Failed to parse ini setting: InvokesCompleteFor, exception: " + ex.ToString(), LogEntryType.Debug);
					// Just carry on, the ini entry probably hasn't been created yet
				}
				
				if (nextTaskWaitTime.Ticks <= 0) { // TASK TIMER HAS MATURED -> CONTINUE
					// DETERMINE IF WE'VE ALREADY INVOKED TOO MANY TIMES TODAY
					if ((invokesToday >= 6) && (queue.NextTask.Type == GameTaskType.Invocation)) {
						if (invokesCompletedOn == TodaysGameDate()) {
							itr.Log(charOneIdxLabel + " has already invoked 6 times today. Requeuing for tomorrow");
							queue.Pop();
							QueueSubsequentTask(itr, queue, invokesToday, charZeroIdx, charOneIdxLabel);

							itr.Log("Continuing AutoCycle loop.");
							continue;
						} else if (invokesCompletedOn < TodaysGameDate()) {
							itr.Log(charOneIdxLabel + ": Resetting InvokesToday to 0.");
							invokesToday = 0;
							itr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charOneIdxLabel);
						}
					}

					itr.Log(string.Format("Processing character: {0}.", charZeroIdx.ToString()));
					itr.Wait(1500);

					if (itr.CancelSource.IsCancellationRequested) { return; }

					// ##### ENTRY POINT -- INVOKING & PROCESSING CHARACTER #####
					if (ProcessCharacter(itr, charZeroIdx)) {
						if (itr.CancelSource.IsCancellationRequested) { return; }
						itr.Log("Completing character: " + charZeroIdx.ToString() + ".");
						invokesToday += 1; // NEED TO DETECT THIS IN-GAME
						queue.Pop(); // COMPLETE
						SaveCharacterSettings(itr, invokesToday, charZeroIdx, charOneIdxLabel);
						QueueSubsequentTask(itr, queue, invokesToday, charZeroIdx, charOneIdxLabel);										
					} else {
						itr.Log("Task for character: " + charZeroIdx.ToString() + " incomplete.");
					}
					
				} else { // TASK TIMER NOT MATURE YET -> WAIT
					itr.Wait(500);
					TimeSpan waitDelayMs = new TimeSpan();

					if (nextTaskWaitTime.TotalMinutes > 10) {
						waitDelayMs = nextTaskWaitTime + RandomDelay(5, 25);
						itr.Log("AutoCycle(): Closing client and waiting " + waitDelayMs.TotalMinutes.ToString() + " minutes.");
						ProduceClientState(itr, ClientState.None);
					} else if (nextTaskWaitTime.TotalMinutes > 1) {
						waitDelayMs = nextTaskWaitTime;
						itr.Log("AutoCycle(): Minimizing client and waiting " + waitDelayMs.TotalMinutes.ToString() + " minutes.");						
						ProduceClientState(itr, ClientState.Inactive);
					}

					itr.Wait(waitDelayMs);
				}

				// GOTTA HAVE SOME DELAY HERE OR WE CRASH
				itr.Wait(3000);
				//if (itr.CancelSource.IsCancellationRequested) { return; }				
			}

			itr.GameAccount.SaveSetting("0", "CharZeroIdxLastInvoked", "Invocation");
			itr.Log("AutoCycle(): Returning.");

			// CLOSE DOWN -- TEMPORARILY DISABLED -- TRANSITION TO USING GAMESTATE TO MANAGE
			//itr.EvaluateFunction("VigilantlyCloseClientAndExit");

		}

		
		// QueueSubsequentTask(): QUEUE FOLLOW UP TASK
		public static void QueueSubsequentTask(Interactor itr, GameTaskQueue queue, int invokesToday, uint charZeroIdx, string charOneIdxLabel) {
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
					itr.Log("Interactions::Sequences::AutoCycle(): All daily invocation complete for character: " + charZeroIdx + ", on: " + todaysInvokeDate);
					itr.GameAccount.SaveSetting(todaysInvokeDate.ToString(), "InvokesCompleteFor", charOneIdxLabel);
					charNextTaskTime = nextThreeThirty;
				} catch (Exception ex) {
					//System.Windows.Forms.MessageBox.Show("Error saving InvokesCompleteFor" + ex.ToString());
                    itr.Log("Error saving InvokesCompleteFor" + ex.ToString(), LogEntryType.Error);
					//System.Windows.Forms.MessageBox()
				}
			}

			try {
				itr.Log("Next task for character at: " + charNextTaskTime.ToShortTimeString() + ".");
				queue.Add(new GameTask(charNextTaskTime.AddSeconds(charZeroIdx), charZeroIdx, GameTaskType.Invocation));
				itr.UpdateQueueList(queue.TaskList);
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("Error adding new task to queue: " + ex.ToString());
                itr.Log("Error adding new task to queue: " +ex.ToString(), LogEntryType.Error);
			}
		}


		// SaveCharacterSettings(): Save relevant settings to .ini file
		public static void SaveCharacterSettings(Interactor itr, int invokesToday, uint charZeroIdx, string charOneIdxLabel) {
			// SAVE SETTINGS TO INI
			// UpdateIni() <<<<< CREATE
			try {
				//string dateTimeFormattedClassic = FormatDateTimeClassic(itr, DateTime.Now);
				itr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charOneIdxLabel);
				itr.GameAccount.SaveSetting(DateTime.Now.ToString(), "MostRecentInvocationTime", charOneIdxLabel);
				itr.GameAccount.SaveSetting(charZeroIdx.ToString(), "CharZeroIdxLastInvoked", "Invocation");
				itr.Log("Settings saved to ini for: " + charOneIdxLabel + ".", LogEntryType.Debug);
			} catch (Exception ex) {
				itr.Log("Interactions::Sequences::AutoCycle(): Problem saving settings: " + ex.ToString());
			}
		}


		// PopulateQueueProperly(): Populate queue taking in to account last invoke times
		private static void PopulateQueueProperly(Interactor itr, GameTaskQueue queue, int charOneIdxMax) {
			for (uint i = 0; i < charOneIdxMax; i++) {
				var charOneIdxLabel = "Character " + (i + 1).ToString();
				DateTime mostRecentInvTime = DateTime.Now;
				int invokesToday = 0;

				if (!DateTime.TryParse(itr.GameAccount.GetSetting("MostRecentInvocationTime", charOneIdxLabel), out mostRecentInvTime)) {
					mostRecentInvTime = DateTime.Now.AddHours(-24);
                }

				if (!int.TryParse(itr.GameAccount.GetSetting("InvokesToday", charOneIdxLabel), out invokesToday)) {
					invokesToday = 0;
				}

				DateTime taskMatureTime = mostRecentInvTime + new TimeSpan(0, 0, 0, 0, InvokeDelays[invokesToday]);

				itr.Log("Adding task to queue for character " + (i - 1).ToString() + ", matures: " + taskMatureTime.ToString(), LogEntryType.Debug);

				queue.Add(new GameTask(taskMatureTime, i, GameTaskType.Invocation));
			}

		}

		public static TimeSpan RandomDelay(int minutesMin, int minutesMax) {
			int rnd = new Random().Next(minutesMin, minutesMax);
			return new TimeSpan(0, rnd, 1);
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
		public static string FormatDateTimeClassic(Interactor itr, DateTime dt) {
			string str = "";

			try {
				str = string.Format("{0:yyyyMMddhhmmss}", dt);
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("Interactions::Sequences::FormatDateTimeClassic():" + ex.ToString());
				itr.Log("Interactions::Sequences::FormatDateTimeClassic():" + ex.ToString());
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
//itr.EvaluateFunction("ActivateNeverwinter");
//itr.Wait(4000);
//itr.EvaluateFunction("AutoInvoke");
//EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad, FirstRun, VaultPurchase)
//itr.Log("AutoInvokeAsync complete.");