using NeverClicker.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NeverClicker {
	public struct LogMessage {
		public string Text;
		public LogEntryType Type;

		public LogMessage(string message) {
			Text = message;
			Type = LogEntryType.Normal;
		}

		public LogMessage(string message, LogEntryType type) {
			Text = message;
			Type = type;
		}
	}

	public enum LogEntryType {
		Normal,
		Detail,
		Critical
	}		
}

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void LogSuccess<T, U>(Interactor itr, T start, U end) {
			itr.Log("ProduceClientState(): " + start.ToString() + " -> " + end.ToString() + "success.");
		}

		public static void LogFailure<T, U>(Interactor itr, T start, U end) {
			itr.Log("ProduceClientState(): " + start.ToString() + " -> " + end.ToString() + "failure. Re-evaluating...");
		}
	}
}

