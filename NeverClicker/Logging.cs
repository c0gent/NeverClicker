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
		Detail,
		Critical
	}

	//public static class FileLog {
	//	private static readonly object Locker = new object();
	//	private static XmlDocument _doc = new XmlDocument();
	//	private const string LogFileName = "NeverClicker_Log_New.txt";

	//	static void Main(string[] args) {
	//		if (File.Exists())
	//			_doc.Load("logs.txt");
	//		else {
	//			var root = _doc.CreateElement("hosts");
	//			_doc.AppendChild(root);
	//		}
	//		for (int i = 0; i < 100; i++) {
	//			new Thread(new ThreadStart(DoSomeWork)).Start();
	//		}
	//	}
	//	static void DoSomeWork() {
	//		/*

	//		 * Here you will build log messages

	//		 */
	//		Log("192.168.1.15", "alive");
	//	}
	//	static void Log(string hostname, string state) {
	//		lock (Locker) {
	//			var el = (XmlElement)_doc.DocumentElement.AppendChild(_doc.CreateElement("host"));
	//			el.SetAttribute("Hostname", hostname);
	//			el.AppendChild(_doc.CreateElement("State")).InnerText = state;
	//			_doc.Save("logs.txt");
	//		}
	//	}

	//}
}
