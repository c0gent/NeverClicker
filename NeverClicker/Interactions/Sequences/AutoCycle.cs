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
					Interactions.Interactor interactor,
					GameTaskQueue queue
        ) {

			interactor.Log("Beginning AutoCycle.");
			interactor.InitOldScript();
			interactor.Wait(500);

			while (!queue.IsEmpty() && !interactor.CancelSource.IsCancellationRequested) {
				var nextTaskWaitTime = queue.NextTaskWaitTime();
				var charId = queue.NextTaskCharacterIdx();

				if (nextTaskWaitTime.Ticks <= 0) {					
                    interactor.Log("Processing character: {0}", charId.ToString());
					interactor.Wait(1000);

					// ACTIVATE
					interactor.EvaluateFunction("ActivateNeverwinter");
					interactor.Wait(4000);

					// PROCESS CHARACTER
					interactor.EvaluateFunction("AutoInvoke");
					interactor.ProgressLog.Report("AutoInvokeAsync complete.");

					queue.Pop(); // COMPLETE
				} else {
					try {						
						interactor.Log("Next task for character {0} in: {1}s.",
							charId.ToString(), nextTaskWaitTime.TotalSeconds.ToString("00"));						
					} catch (Exception ex) {
						interactor.Log(ex.ToString());
					}

					interactor.Wait(1000 + (int)nextTaskWaitTime.TotalMilliseconds);
				}
			}

			// CLOSE DOWN
			interactor.EvaluateFunction("VigilantlyCloseClientAndExit");
			
		}
	}
}
