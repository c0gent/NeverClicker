using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker {
	public struct LogMessage {
		public string Text;
		public LogType Type;

		public LogMessage(string message) {
			Text = message;
			Type = LogType.Normal;
		}

		public LogMessage(string message, LogType type) {
			Text = message;
			Type = type;
		}
	}

	public enum LogType {
		Normal,
		Detail
	}
}
