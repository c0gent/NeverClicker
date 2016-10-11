using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IniParser;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		const bool RESET_DAY = false;
		
		public static void AutoCycle(
					Interactor intr,
					int startDelaySec)
        {
			if (intr.CancelSource.IsCancellationRequested) { return; }

			var queue = new TaskQueue();
			int charsTotal = intr.AccountSettings.GetSettingValOr("characterCount", "general", 0);

			if (queue.IsEmpty) {
				intr.Log("Populating task queue: (0 -> " + (charsTotal).ToString() + ")");
				queue.Populate(intr, charsTotal, RESET_DAY);
				intr.UpdateQueueList(queue.ListClone(intr));
			}

			intr.Log("Starting AutoCycle in " + startDelaySec.ToString() + " seconds...");
			intr.Wait(startDelaySec * 1000);
			if (intr.CancelSource.IsCancellationRequested) { return; }
			intr.Log("Beginning AutoCycle.");
			
			
			// ##### BEGIN AUTOCYCLE LOOP #####
			while (!queue.IsEmpty && !intr.CancelSource.IsCancellationRequested) {
				if (IsCurfew()) {
					int sleepTime = intr.WaitRand(300000, 1800000);
					intr.Log("Curfew time. Sleeping for " + (sleepTime / 60000).ToString() + " minutes.");					
				}

				intr.Log(LogEntryType.Debug, "AutoCycle(): Loop iteration starting.");
				
				TimeSpan nextTaskMatureDelay = queue.NextTaskMatureDelay();
				intr.Log(LogEntryType.Debug, "AutoCycle(): Next task mature delay: " + nextTaskMatureDelay);
				
				if (nextTaskMatureDelay.Ticks <= 0) { 
					// TASK TIMER HAS MATURED -> CONTINUE
					// ##### ENTRY POINT -- INVOKING & PROCESSING CHARACTER #####
					ProcessCharacter(intr, queue);					
				} else { 
					// TASK TIMER NOT MATURE YET -> WAIT
					intr.Wait(1000);
					intr.Log("Next task matures in " + nextTaskMatureDelay.TotalMinutes.ToString("F0") + " minutes.");

					TimeSpan waitDelay = nextTaskMatureDelay;

					if (nextTaskMatureDelay.TotalMinutes > 8) {
						if (queue.NextTask.Kind == TaskKind.Profession) {
							waitDelay = nextTaskMatureDelay + intr.RandomDelay(9, 25);
						} else {
							waitDelay = nextTaskMatureDelay + intr.RandomDelay(9, 15);
						}

						ProduceClientState(intr, ClientState.None, 0);										
					} else if (nextTaskMatureDelay.TotalSeconds > 1) {
						// Delay more than 1 sec, let the train catch up...
						ProduceClientState(intr, ClientState.Inactive, 0);
						waitDelay = nextTaskMatureDelay + intr.RandomDelay(5, 11);
						intr.Log("Minimizing client and waiting " + waitDelay.TotalMinutes.ToString("F0") + " minutes.");
					}

					if (waitDelay.TotalSeconds > 1) {
						intr.Log("Sleeping for " + waitDelay.TotalMinutes.ToString("F0") + " minutes before continuing...");
						intr.Wait(waitDelay);
						Screen.Wake(intr);
					}					
				}
				
				intr.Wait(100);
			}

			intr.Log(LogEntryType.Info, "Autocycle complete.");
		}
		
				
		public static bool IsCurfew() {
			var utcNow = DateTime.UtcNow;
			return ((utcNow > utcNow.Date.AddHours(10)) && (utcNow < utcNow.Date.AddHours(10).AddMinutes(10)));
		}
	}
}

