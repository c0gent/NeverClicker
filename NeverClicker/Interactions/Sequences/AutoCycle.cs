using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IniParser;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		
		public static void AutoCycle(
					Interactor intr,
					TaskQueue queue,
					int startDelaySec
        ) {
			intr.Wait(startDelaySec * 1000);
			if (intr.CancelSource.IsCancellationRequested) { return; }

			int charsZeroIdxTotal = intr.GameAccount.GetSettingOrZero("CharCount", "NwAct");
			
			if (queue.IsEmpty) {
				intr.Log("Auto-populating task queue: (0 -> " + (charsZeroIdxTotal).ToString() + ")");
				queue.Populate(intr, charsZeroIdxTotal);
				intr.UpdateQueueList(queue.ListClone());
			}

			intr.Log("Beginning AutoCycle.");
			intr.Wait(500);
			
			// ##### BEGIN AUTOCYCLE LOOP #####
			while (!queue.IsEmpty && !intr.CancelSource.IsCancellationRequested) {
				if (IsCurfew()) {
					int sleepTime = intr.WaitRand(300000, 1800000);
					intr.Log("Curfew time. Sleeping for " + (sleepTime / 60000).ToString() + " minutes.");					
				}

				intr.Log("AutoCycle():while: Loop iteration started.", LogEntryType.Debug);
				TimeSpan nextTaskWaitTime = queue.NextTaskWaitTime();				
				
				if (nextTaskWaitTime.Ticks <= 0) { // TASK TIMER HAS MATURED -> CONTINUE
					// ##### ENTRY POINT -- INVOKING & PROCESSING CHARACTER #####
					ProcessCharacter(intr, queue);										
					
				} else { // TASK TIMER NOT MATURE YET -> WAIT
					intr.Wait(100);
					intr.Log("Next task matures in " + nextTaskWaitTime.TotalMinutes.ToString("F0") + " minutes.");

					TimeSpan waitDelayMs = new TimeSpan(0);

					if (nextTaskWaitTime.TotalMinutes > 8) {
						//waitDelayMs = nextTaskWaitTime + intr.RandomDelay(5, 25);
						waitDelayMs = nextTaskWaitTime + intr.RandomDelay(5, 15);
						ProduceClientState(intr, ClientState.None);										
					} else if (nextTaskWaitTime.TotalMinutes > 1) {
						waitDelayMs = nextTaskWaitTime.Add(new TimeSpan(0, intr.Rand(1, 2), 0));
						//intr.Log("Minimizing client and waiting " + waitDelayMs.TotalMinutes.ToString("F0") + " minutes.");						
						ProduceClientState(intr, ClientState.Inactive);
					}

					if (waitDelayMs.TotalMilliseconds > 1000) {
						intr.Log("Sleeping for " + waitDelayMs.TotalMinutes.ToString("F0") + " minutes before continuing...");
						intr.Wait(waitDelayMs);
						Screen.Wake(intr);
					}					
				}
				
				intr.Wait(100);
				//if (intr.CancelSource.IsCancellationRequested) { return; }				
			}

			//intr.GameAccount.SaveSetting("0", "CharZeroIdxLastInvoked", "Invocation");
			//intr.Log("AutoCycle(): Returning.", LogEntryType.Info);

			// CLOSE DOWN -- TEMPORARILY DISABLED -- TRANSITION TO USING GAMESTATE TO MANAGE
			//intr.EvaluateFunction("VigilantlyCloseClientAndExit");
		}
		
				
		public static bool IsCurfew() {
			var utcNow = DateTime.UtcNow;
			return ((utcNow > utcNow.Date.AddHours(10)) && (utcNow < utcNow.Date.AddHours(10).AddMinutes(10)));
		}
		

		// <<<<< FOR THE OLD LOG (DEPRICATE) >>>>>
		//public static string FormatDateTimeClassic(Interactor intr, DateTime dt) {
		//	string str = "";

		//	try {
		//		str = string.Format("{0:yyyyMMddhhmmss}", dt);
		//	} catch (Exception ex) {
		//		//System.Windows.Forms.MessageBox.Show("Interactions::Sequences::FormatDateTimeClassic():" + ex.ToString());
		//		intr.Log("Interactions::Sequences::FormatDateTimeClassic():" + ex.ToString());
		//		return "00000000000000";
		//	}

		//	return str;
		//}
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