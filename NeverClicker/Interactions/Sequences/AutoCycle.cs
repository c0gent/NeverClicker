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
			int lastCharInvoked = 0;
			int charsTotal = 0;
			
			// ##### LOAD CHARACTER COUNT AND LAST CHARACTER INVOKED FROM INI FILE #####
			try {
				lastCharInvoked = itr.GameAccount.GetSettingOrZero("LastCharacterInvoked", "Invocation");
				charsTotal = itr.GameAccount.GetSettingOrZero("NwCharacterCount", "NwAct");
			} catch (Exception ex) {
				//System.Windows.Forms.MessageBox.Show("'lastCharInvoked, charsTotal': " + ex.ToString());
				itr.Log("Interactions::Sequences::AutoCycle(): 'lastCharInvoked, charsTotal': " + ex.ToString(), LogType.Detail);
			}

            queue.Populate(lastCharInvoked, charsTotal);
			itr.Log("Populating Queue: (" + lastCharInvoked + " -> " + charsTotal + ")");

			itr.Log("Beginning AutoCycle.");
			itr.InitOldScript();
			itr.Wait(500);



			// ##### OLD SCRIPT #####
			//itr.EvaluateFunction("ActivateNeverwinter");
			//itr.Wait(4000);
			//itr.EvaluateFunction("AutoInvoke");
			//EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad, FirstRun, VaultPurchase)
			//itr.Log("AutoInvokeAsync complete.");

			// ##### PROCESS CHARACTER #####
			while (!queue.IsEmpty() && !itr.CancelSource.IsCancellationRequested) {
				var nextTaskWaitTime = queue.NextTaskWaitTime();
				var charZeroIdx = queue.NextTaskCharacterIdx();
				var charOneIdx = charZeroIdx + 1;
				string characterOneIdxSection = "Character " + charOneIdx.ToString();
                var invokesToday = itr.GameAccount.GetSettingOrZero("InvokesToday", characterOneIdxSection);

				// ADD SOME LOGIC TO DECIDE WHETHER WE'VE INVOKED ENOUGH TODAY OR NOT (going below looks like)

				if (nextTaskWaitTime.Ticks <= 0) {					
                    itr.Log(string.Format("Processing character: {0}.", charZeroIdx.ToString()));
					itr.Wait(1500);

					if (itr.CancelSource.IsCancellationRequested) { return; }

					// ENTRY POINT
					if (ProcessCharacter(itr, charZeroIdx)) {
						itr.Log("Completing character: " + charZeroIdx.ToString() + ".");

						invokesToday += 1;
						DateTime charNextTaskTime = DateTime.Now;
						var now = DateTime.Now;
						var todayThreeThirty = now.Date.AddHours(3).AddMinutes(30);
						var nextThreeThirty = now <= todayThreeThirty ? todayThreeThirty : todayThreeThirty.AddDays(1);

						// REQUEUE FOLLOW UP TASK
						if (invokesToday < 6) { // QUEUE FOR LATER TODAY
							var nextInvokeDelay = InvokeDelays[invokesToday] + 180000;
							charNextTaskTime = DateTime.Now.AddMilliseconds(nextInvokeDelay);

							// IF NEXT SCHEDULED TASK IS BEYOND THE 3:30 CURFEW, RESET FOR NEXT DAY
							if (charNextTaskTime > nextThreeThirty) {
								invokesToday = 6;
								charNextTaskTime = nextThreeThirty;
							}
						} else { // QUEUE FOR TOMORROW (NEXT 3:30AM)
							charNextTaskTime = nextThreeThirty;
						}

						itr.Log("Next task for character at: " + charNextTaskTime.ToShortTimeString() + ".");
						queue.Add(new GameTask(charNextTaskTime, charZeroIdx, TaskKind.Invocation));

						try {
							string dateTimeFormatted = FormatDateTimeClassic(itr, DateTime.Now);
                            itr.GameAccount.SaveSetting(invokesToday.ToString(), "InvokesToday", characterOneIdxSection);
							itr.GameAccount.SaveSetting(dateTimeFormatted, "MostRecentInvocationTime", characterOneIdxSection);
							itr.Log("Saved InvokesToday and MRITime for: " + characterOneIdxSection + ".", LogType.Detail);
						} catch (Exception ex) {
							itr.Log("Interactions::Sequences::AutoCycle():" + ex.ToString());
                        }
						
						// ENABLE BELOW ONCE WE REMOVE THIS FROM OLD SCRIPT
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
					
				} else {
					try {
						var logStr = string.Format("Next task for character {0} in: {1}s.", charZeroIdx.ToString(), nextTaskWaitTime.TotalSeconds.ToString("00"));
                        itr.Log(logStr);						
					} catch (Exception ex) {
						itr.Log("Interactions::Sequences::AutoCycle():" + ex.ToString());
						System.Windows.Forms.MessageBox.Show("Interactions::Sequences::AutoCycle():" + ex.ToString());						
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