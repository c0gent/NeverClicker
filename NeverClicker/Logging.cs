using NeverClicker.Interactions;
using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NeverClicker {
	public class LogFile {
		private static readonly object Locker = new object();
		private static XmlDocument LogXmlDoc = new XmlDocument();
		private string LogFileName = "";

		public LogFile() {
			this.LogFileName = Properties.Settings.Default.LogsFolderPath.ToString();

			if (File.Exists(LogFileName))
				LogXmlDoc.Load(LogFileName);
			else {
				var root = LogXmlDoc.CreateElement("messages");
				LogXmlDoc.AppendChild(root);
			}
		}

		public void AppendMessage(LogMessage logMessage) {
			lock (Locker) {
				var el = (XmlElement)LogXmlDoc.DocumentElement.AppendChild(LogXmlDoc.CreateElement("entry"));
				el.SetAttribute("time", System.Security.SecurityElement.Escape(DateTime.Now.ToString()));
				el.SetAttribute("type", System.Security.SecurityElement.Escape(logMessage.Type.ToString()));
				el.SetAttribute("message", System.Security.SecurityElement.Escape(logMessage.Text).ToString());
				try {
					LogXmlDoc.Save(LogFileName);
				} catch (Exception ex) {
					MessageBox.Show("LogFile::AppendMessage(): Error saving xml document: " + ex.ToString());
				}
			}
		}
	}

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
		Debug,
		Normal,
		Warning,
		Error,
		Fatal
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

