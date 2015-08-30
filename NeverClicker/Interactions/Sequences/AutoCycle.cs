using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IniParser;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static int[] InvokeDelays = { 0, 900000, 1800000, 2700000, 3600000, 5400000 };

		public static void AutoCycle(
					Interactor itr,
					GameTaskQueue queue
        ) {
			// ##### INITIALIZE #####
			//int charOneIdxLastInvoked = 0;
			int charsOneIdxTotal = 0;
			
			// ##### LOAD CHARACTER COUNT AND LAST CHARACTER INVOKED FROM INI FILE #####
			try {
				//charOneIdxLastInvoked = itr.GameAccount.GetSettingOrZero("LastCharacterInvoked", "Invocation");
				charsOneIdxTotal = itr.GameAccount.GetSettingOrZero("NwCharacterCount", "NwAct");
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("'lastCharInvoked, charsTotal': " + ex.ToString());
				itr.Log("Interactions::Sequences::AutoCycle(): 'lastCharInvoked, charsTotal': " + ex.ToString(), LogEntryType.Detail);
			}

			//itr.Log("Populating Queue: (" + charOneIdxLastInvoked + " -> " + charsOneIdxTotal + ")");
			//queue.Populate(lastCharInvoked, charsTotal);
			itr.Log("Populating Queue: (0 -> " + (charsOneIdxTotal - 1).ToString() + ")");
			queue.Populate(0, (uint)charsOneIdxTotal - 1); // ***** TEMPORARY - RESETTING FOR TODAY *****

			itr.Log("Beginning AutoCycle.");
			itr.InitOldScript();
			itr.Wait(500);
			
			// ##### PROCESS CHARACTER #####
			while (!queue.IsEmpty() && !itr.CancelSource.IsCancellationRequested) {
				if (IsCurfew()) {
					itr.Log("Curfew half-hour. Sleeping for 30 minutes.");
					itr.Wait(1800000); // 30 minutes
				}

				TimeSpan nextTaskWaitTime = queue.NextTaskWaitTime();
				uint charZeroIdx = queue.NextTask.CharacterIdx;
				uint charOneIdx = charZeroIdx + 1;
				string charOneIdxSectionName = "Character " + charOneIdx.ToString();
                int invokesToday = itr.GameAccount.GetSettingOrZero("InvokesToday", charOneIdxSectionName);
				var invokesCompletedOn = DateTime.Now.AddDays(-99);
				try {
					invokesCompletedOn = DateTime.Parse(itr.GameAccount.GetSetting("InvokesCompletedOn", charOneIdxSectionName));
				} catch (Exception ex) {					
					itr.Log("Failed to parse ini setting: InvokesCompletedOn, exception: " + ex.ToString(), LogEntryType.Detail);
					// Just carry on, the ini entry probably hasn't been created yet
				}
				
				if (nextTaskWaitTime.Ticks <= 0) { // TASK TIMER HAS MATURED -> CONTINUE
					// DETERMINE IF WE'VE ALREADY INVOKED TOO MANY TIMES TODAY
					if ((invokesToday >= 6) && (queue.NextTask.Kind == TaskKind.Invocation)) {
						if (invokesCompletedOn == TodaysGameDate()) {
							itr.Log("Character " + charZeroIdx.ToString() + " has already invoked 6 times today. Requeuing for tomorrow");
							queue.Pop();
							QueueSubsequentTask(itr, queue, invokesToday, charZeroIdx, charOneIdxSectionName);
							continue;
						}
					}

					itr.Log(string.Format("Processing character: {0}.", charZeroIdx.ToString()));
					itr.Wait(1500);

					if (itr.CancelSource.IsCancellationRequested) { return; }

					// ENTRY POINT
					if (ProcessCharacter(itr, charZeroIdx)) {
						itr.Log("Completing character: " + charZeroIdx.ToString() + ".");

						invokesToday += 1; // NEED TO DETECT THIS IN-GAME

						QueueSubsequentTask(itr, queue, invokesToday, charZeroIdx, charOneIdxSectionName);
						
						// SAVE SETTINGS TO INI
						// UpdateIni() <<<<< CREATE
						try {
							string dateTimeFormatted = FormatDateTimeClassic(itr, DateTime.Now);
                            itr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", charOneIdxSectionName);
							itr.GameAccount.SaveSetting(dateTimeFormatted, "MostRecentInvocationTime", charOneIdxSectionName);
							itr.Log("Saved InvokesToday and MRITime for: " + charOneIdxSectionName + ".", LogEntryType.Detail);
						} catch (Exception ex) {
							itr.Log("Interactions::Sequences::AutoCycle(): Problem saving settings: " + ex.ToString());
                        }
						
						// ***** ENABLE BELOW ONCE WE REMOVE THIS FROM OLD SCRIPT *****
						//itr.GameAccount.SaveSetting("0", "LastCharacterInvoked", "Invocation");

						queue.Pop(); // COMPLETE

						// ########## TESTING ##########
						//itr.Wait(3000);
						//Game.ProduceClientState(itr, ClientState.Inactive);
						//itr.Wait(4000);
						//Game.ProduceClientState(itr, ClientState.CharSelect);
					} else {
						itr.Log("Error invoking character: " + charZeroIdx.ToString());
						//System.Windows.Forms.MessageBox.Show("Error invoking character: " + charZeroIdx.ToString());
					}
					
				} else { // TASK TIMER NOT MATURE YET -> WAIT
					try {
						var logStr = string.Format("Next task for character {0} in: {1}s.", charZeroIdx.ToString(), nextTaskWaitTime.TotalSeconds.ToString("00"));
                        itr.Log(logStr);						
					} catch (Exception ex) {
                        itr.Log("Interactions::Sequences::AutoCycle(): Problem formatting log string -- " + ex.ToString(), LogEntryType.Critical);						
                        throw ex;
					}

					itr.Wait(1000 + (int)nextTaskWaitTime.TotalMilliseconds);
				}

				// GOTTA HAVE SOME DELAY HERE OR WE CRASH
				itr.Wait(3000);
				if (itr.CancelSource.IsCancellationRequested) { return; }

				if (queue.NextTaskWaitTime().TotalMinutes > 1) {					
					itr.Log("AutoCycle(): Minimizing client.");
					Game.ProduceClientState(itr, ClientState.Inactive);
					//itr.Wait(600000);
				}
			}

			itr.GameAccount.SaveSetting("0", "LastCharacterInvoked", "Invocation");
			itr.Log("AutoCycle(): Returning.");

			// CLOSE DOWN -- TEMPORARILY DISABLED -- TRANSITION TO USING GAMESTATE TO MANAGE
			//itr.EvaluateFunction("VigilantlyCloseClientAndExit");

		}


		// QueueSubsequentTask(): QUEUE FOLLOW UP TASK
		public static void QueueSubsequentTask(Interactor itr, GameTaskQueue queue, int invokesToday, uint charZeroIdx, string charOneIdxSectionName) {
			DateTime charNextTaskTime = DateTime.Now;
			DateTime nextThreeThirty = NextThreeThirty();
			DateTime todaysInvokeDate = TodaysGameDate();
			
			if (invokesToday < 6) { // QUEUE FOR LATER TODAY
				var nextInvokeDelay = InvokeDelays[invokesToday] + 180000;
				charNextTaskTime = DateTime.Now.AddMilliseconds(nextInvokeDelay);

				// IF NEXT SCHEDULED TASK IS BEYOND THE 3:30 CURFEW, RESET FOR NEXT DAY
				if (charNextTaskTime > nextThreeThirty) {
					invokesToday = 6;
					charNextTaskTime = nextThreeThirty;
				}
			} else { // QUEUE FOR TOMORROW (NEXT 3:30AM)
				itr.Log("Interactions::Sequences::AutoCycle(): All daily invocation complete for character: " + charZeroIdx + ", on: " + todaysInvokeDate);
				itr.GameAccount.SaveSetting(todaysInvokeDate.ToString(), "InvokesCompletedOn", charOneIdxSectionName);
				charNextTaskTime = nextThreeThirty;
			}

			itr.Log("Next task for character at: " + charNextTaskTime.ToShortTimeString() + ".");
			queue.Add(new GameTask(charNextTaskTime, charZeroIdx, TaskKind.Invocation));
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