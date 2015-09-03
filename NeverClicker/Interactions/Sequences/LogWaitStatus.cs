using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void LogWaitStatus<TState>(Interactor itr, TState end, bool success) {
			if (success) {
				itr.Log("logging.cs::Interactions::Sequences::LogSuccess(): Success producing client state: "
					+ " -> " + end.ToString() + ".", LogEntryType.Debug);
			} else {
				itr.Log("logging.cs::Interactions::Sequences::LogFailure(): Failure to produce client state: "
					+ " -> " + end.ToString() + ". Re-evaluating...", LogEntryType.Debug);
			}
		}
	}
}
