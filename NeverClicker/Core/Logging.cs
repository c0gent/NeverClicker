using NeverClicker.Interactions;
using NeverClicker.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
		private XmlElement SessionElement;
		private string LogFileName = "";
		private const string RootElementName = "log";
		private const string SessionElementName = "session";

		public LogFile() {
			
			LogFileName = Settings.Default.LogsFolderPath + SettingsForm.LOG_FILE_NAME;

			if (File.Exists(LogFileName)) {
				try {
					LogXmlDoc.Load(LogFileName);

					// <<<<< TODO: PRUNE OLD ENTRIES >>>>>

					SessionElement = LogXmlDoc.CreateElement(SessionElementName);
					SessionElement.SetAttribute("id", DateTime.Now.ToFileTime().ToString());
					LogXmlDoc.DocumentElement.AppendChild(SessionElement);
					//var root = LogXmlDoc.CreateElement("Log_" + DateTime.Now.ToFileTime().ToString());
					//LogXmlDoc.AppendChild(root);
				} catch (Exception ex) {
					MessageBox.Show("LogFile::AppendMessage(): Error loading log file. Please rename or delete. \r\n Error info: " + ex.ToString());
				}
			} else {
				try {
					var root = LogXmlDoc.CreateElement(RootElementName);
					LogXmlDoc.AppendChild(root);
					//SessionElement = LogXmlDoc.CreateElement(SessionPrefix + DateTime.Now.ToFileTime().ToString());
					SessionElement = LogXmlDoc.CreateElement(SessionElementName);
					SessionElement.SetAttribute("id", DateTime.Now.ToFileTime().ToString());
					LogXmlDoc.DocumentElement.AppendChild(SessionElement);
					LogXmlDoc.Save(LogFileName);
				} catch (Exception ex) {
					MessageBox.Show("LogFile::AppendMessage(): Error saving log file: " + ex.ToString());
				}
			}

			//MessageBox.Show("LogFile::LogFile(): Loading " + LogFileName);

			//try {
			//	LogXmlDoc.Load(LogFileName);
			//} catch (XmlException) {
			//	try {
			//		Directory.CreateDirectory(Settings.Default.LogsFolderPath);
			//		File.Delete(LogFileName);
			//		LogXmlDoc.Load(LogFileName);
			//	} catch (Exception ex) {
			//		MessageBox.Show("Error loading log file: " + ex.ToString());
			//	}
			//}
			//var root = LogXmlDoc.CreateElement("log_entries");
			//LogXmlDoc.AppendChild(root);
		}

		public void AppendMessage(LogMessage logMessage) {
			lock (Locker) {
				//var el = (XmlElement)LogXmlDoc.DocumentElement.AppendChild(LogXmlDoc.CreateElement("entry"));
				var entry = (XmlElement)SessionElement.AppendChild(LogXmlDoc.CreateElement("entry"));
				entry.SetAttribute("time", System.Security.SecurityElement.Escape(DateTime.Now.ToString()));
				entry.SetAttribute("type", System.Security.SecurityElement.Escape(logMessage.Type.ToString()));
				entry.SetAttribute("text", System.Security.SecurityElement.Escape(logMessage.Text).ToString());
				try {
					LogXmlDoc.Save(LogFileName);
				} catch (Exception ex) {
					MessageBox.Show("LogFile::AppendMessage(): Error saving xml document: " + ex.ToString());
				}
			}
		}


		//this.LogFileName = Properties.Settings.Default.LogsFolderPath;
		//if (File.Exists(LogFileName)) {
		//	LogXmlDoc.Load(LogFileName);
		//	var root = LogXmlDoc.CreateElement("log_entries");
		//	LogXmlDoc.AppendChild(root);
		//} else {

		//}

		//var lfPath = Settings.Default.LogsFolderPath.ToString();


		//if (InitLogFile(lfName)) {
		//	LogFileName = lfName;
		//}

		//public static bool InitLogFile(string logFileName) {
		//	//string logFileName = ;

				
		//	//if (!Directory.Exists(logFolder)) {
		//	//	//Directory.CreateDirectory(logFolder);
		//	//	return false;
		//	//}

		//	//var logFileName = logFolder + "\\" + SettingsManager.LOG_FILE_NAME;

		//	if (File.Exists(logFileName)) {
		//		try {
		//			LogXmlDoc.Load(logFileName);
		//		} catch (XmlException) {
		//			return false;
		//		}
		//		var root = LogXmlDoc.CreateElement("log_entries");
		//		LogXmlDoc.AppendChild(root);
		//		return true;
		//	} else {
		//		return false;
		//		//try {
		//		//	var root = LogXmlDoc.CreateElement("log_entries");
		//		//	LogXmlDoc.AppendChild(root);
		//		//	LogXmlDoc.Save(logFileName);
					
		//		//} catch (Exception ex) {
		//		//	MessageBox.Show("LogFile::AppendMessage(): Error saving xml document: " + ex.ToString());
		//		//}				
		//	}
		//}
		
	}

	public struct LogMessage {
		public readonly string Text;
		public readonly LogEntryType Type;

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
		Debug,
		Info,
		Warning,
		Error,		
		Fatal,
		FatalWithScreenshot,
	}		
}

//namespace NeverClicker.Interactions {
//	public static partial class Sequences {
//		public static void LogWaitResult<T, U>(Interactor intr, T start, U end, bool success) {
//			if (success) {
//				intr.Log("logging.cs::Interactions::Sequences::LogSuccess(): Producing client state: "
//					+ start.ToString() + " -> " + end.ToString() + " success.", LogEntryType.Debug);
//			} else {
//				intr.Log("logging.cs::Interactions::Sequences::LogFailure(): Producing client state: " 
//					+ start.ToString() + " -> " + end.ToString() + " failure. Re-evaluating...", LogEntryType.Debug);
//			}
//		}

//		public static void LogSuccess<T, U>(Interactor intr, T start, U end) {
//			intr.Log("logging.cs::Interactions::Sequences::LogSuccess(): Producing client state: " + start.ToString() + " -> " + end.ToString() + " success.", LogEntryType.Debug);
//		}

//		public static void LogFailure<T, U>(Interactor intr, T start, U end) {
//			intr.Log("logging.cs::Interactions::Sequences::LogFailure(): Producing client state: " + start.ToString() + " -> " + end.ToString() + " failure. Re-evaluating...", LogEntryType.Debug);
//		}
//	}
//}

