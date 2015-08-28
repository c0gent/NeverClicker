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
					Interactor itr,
					GameTaskQueue queue
        ) {

			itr.Log("Beginning AutoCycle.");
			itr.InitOldScript();
			itr.Wait(500);

			// ACTIVATE
			//itr.EvaluateFunction("ActivateNeverwinter");
			//itr.Wait(4000);

			// PROCESS CHARACTER
			//itr.EvaluateFunction("AutoInvoke");
			//EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad, FirstRun, VaultPurchase)
			//itr.Log("AutoInvokeAsync complete.");

			while (!queue.IsEmpty() && !itr.CancelSource.IsCancellationRequested) {
				var nextTaskWaitTime = queue.NextTaskWaitTime();
				var charId = queue.NextTaskCharacterIdx();

				if (nextTaskWaitTime.Ticks <= 0) {					
                    itr.Log(string.Format("Processing character: {0}.", charId.ToString()));
					itr.Wait(1500);

					if (itr.CancelSource.IsCancellationRequested) { return; }

					if (ProcessCharacter(itr, charId)) {
						itr.Log("Completing character: " + charId.ToString() + ".");
						queue.Pop(); // COMPLETE
					} else {
						itr.Log("Error invoking character: " + charId.ToString());
						System.Windows.Forms.MessageBox.Show("Error invoking character: " + charId.ToString());
						break;
					}

					
				} else {
					try {
						var logStr = string.Format("Next task for character {0} in: {1}s.", charId.ToString(), nextTaskWaitTime.TotalSeconds.ToString("00"));
                        itr.Log(logStr);						
					} catch (Exception ex) {
						itr.Log(ex.ToString());
						System.Windows.Forms.MessageBox.Show(ex.ToString());
						throw ex;
					}

					itr.Wait(1000 + (int)nextTaskWaitTime.TotalMilliseconds);
				}

				// GOTTA HAVE SOME DELAY HERE OR WE CRASH
				if (itr.CancelSource.IsCancellationRequested) { return; }
				itr.Wait(3000);
			}

			itr.Log("AutoCycle(): Returning.");

			// CLOSE DOWN -- TEMPORARILY DISABLED
			//itr.EvaluateFunction("VigilantlyCloseClientAndExit");

		}
	}
}
