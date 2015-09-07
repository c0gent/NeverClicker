using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void LogWaitStatus<TState>(Interactor intr, TState end, bool success) {
			if (success) {
				intr.Log("WaitUntil(): Success producing client state: "
					+ " -> " + end.ToString() + ".", LogEntryType.Info);
			} else {
				intr.Log("WaitUntil(): Failure to produce client state: "
					+ " -> " + end.ToString() + ". Re-evaluating...", LogEntryType.Info);
			}
		}
	}
}
