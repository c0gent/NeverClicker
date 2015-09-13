using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void MouseMoveTest(Interactor intr) {
			int sleepDuration = 3000;
			int loopIterations = 3;

			var coordinateList = new List<Point>();
			coordinateList.Add(new Point(1, 1));
			coordinateList.Add(new Point(800, 20));
			coordinateList.Add(new Point(20, 800));

			for (uint i = 0; i < loopIterations; i++) {
				foreach (var p in coordinateList) {
					if (intr.CancelSource.Token.IsCancellationRequested) {
						intr.Log("Attempting to cancel mouse movement test.");
						break;
					}
					//WriteTextBox("Moving to (1, 1).");
					intr.Log(String.Format("Moving to ({0}, {1}).", p.X, p.Y));

					intr.MoveMouseCursor(p, false);
					//alibEng.Exec("SendEvent {Click 1, 1, 0}");
					//alibEng.Exec("Sleep 3000");
					//Thread.Sleep(2000);
					//log.Report("Moving to (800, 800).");
					Task.Delay(sleepDuration).Wait();
					//cancelToken.ThrowIfCancellationRequested();
					//if (cancelToken.IsCancellationRequested) { break; }
				}

				////WriteTextBox("Moving to (800, 800).");
				//log.Report("Moving to (800, 800).");
				//alibEng.Exec("SendEvent {Click 800, 800, 0}");
				////alibEng.Exec("Sleep 3000");
				////Thread.Sleep(2000);
				//await Task.Delay(3000);
				////cancelToken.ThrowIfCancellationRequested();
				//if (cancelToken.IsCancellationRequested) { break; }
			}

			intr.Log("Mouse movement complete.");

		}
	}
}
