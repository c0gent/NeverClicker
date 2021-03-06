﻿using NeverClicker.Interactions;
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
using NLog;

namespace NeverClicker {

	//public class Log {

	//}

	// A log message.
	public struct LogMessage {
		public readonly string Text;
		public readonly LogEntryType Type;
		public readonly object[] Args;

		public LogMessage(string message, params object[] args) {
			Text = message;
			Type = LogEntryType.Normal;
			Args = args;
		}

		//public LogMessage(LogEntryType type, string message) {
		//	Text = message;
		//	Type = type;
		//	Args = null;
		//}

		public LogMessage(LogEntryType type, string message, params object[] args) {
			Text = message;
			Type = type;
			Args = args;
		}
	}


	// The log entry type severity.
	public enum LogEntryType {
		Debug,
		Trace,
		Normal,
		Info,
		Warning,
		Error,
		Fatal,
		FatalWithScreenshot,
	}		

	//// [TODO]: Depricate.
	//public class LogFile {
	//	private static readonly object Locker = new object();
	//	private XmlDocument LogXmlDoc;
	//	private XmlElement SessionElement;
	//	private string LogFileName = "";
	//	private const string RootElementName = "log";
	//	private const string SessionElementName = "session";

	//	public LogFile() {
	//		// Ensure log folder exists, create otherwise:
	//		if (!Directory.Exists(Settings.Default.LogsFolderPath)) {
	//			Directory.CreateDirectory(Settings.Default.LogsFolderPath);
	//		}

	//		LogFileName = Settings.Default.LogsFolderPath + SettingsForm.LOG_FILE_NAME;
	//		LogXmlDoc = new XmlDocument();

	//		// [TODO]: CHECK LOG FILE SIZE, IF OVER 5MB, RENAME OR DELETE

	//		if (File.Exists(LogFileName)) {
	//			try {
	//				LogXmlDoc.Load(LogFileName);
	//			} catch (XmlException) {
	//				// Rename the log file and retry
	//				File.Delete(LogFileName + "_BAK");
	//				File.Move(LogFileName, LogFileName + "_BAK");
	//				//LogXmlDoc.Load(LogFileName);
	//				InitLogFile();
	//			} catch (Exception ex) {
	//				MessageBox.Show("LogFile::AppendMessage(): Error loading log file. Please rename or delete. \r\n Error info: " + ex.ToString());
	//			}

	//			// <<<<< TODO: PRUNE OLD ENTRIES >>>>>

	//			InitLogFile();
	//		} else {
	//			try {
	//				InitLogFile();
	//			} catch (Exception ex) {
	//				MessageBox.Show("LogFile::AppendMessage(): Error saving log file: " + ex.ToString());
	//			}
	//		}
	//	}

	//	/// <summary>
	//	/// Initializes a new or existing log file with a new session.
	//	/// </summary>
	//	private void InitLogFile() {
	//		var fileExists = File.Exists(LogFileName);
	//		XmlElement root;

	//		if (!fileExists) {
	//			root = LogXmlDoc.CreateElement(RootElementName);
	//			LogXmlDoc.AppendChild(root);
	//		}

	//		SessionElement = LogXmlDoc.CreateElement(SessionElementName);
	//		SessionElement.SetAttribute("id", DateTime.Now.ToFileTime().ToString());
	//		LogXmlDoc.DocumentElement.AppendChild(SessionElement);			

	//		if (!fileExists) {
	//			try {
	//				LogXmlDoc.Save(LogFileName);
	//			} catch (Exception ex) {
	//				MessageBox.Show("LogFile::AppendMessage(): Error saving xml document: " + ex.ToString());
	//			}
	//		}
	//	}

	//	public void AppendMessage(LogMessage logMessage) {
	//		lock (Locker) {
	//			var entry = (XmlElement)SessionElement.AppendChild(LogXmlDoc.CreateElement("entry"));
	//			entry.SetAttribute("time", DateTime.Now.ToString());
	//			entry.SetAttribute("type", logMessage.Type.ToString());
	//			entry.SetAttribute("text", logMessage.Text);

	//			try {
	//				LogXmlDoc.Save(LogFileName);
	//			} catch (Exception ex) {
	//				MessageBox.Show("LogFile::AppendMessage(): Error saving xml document: " + ex.ToString());
	//			}
	//		}
	//	}
	//}


	//// A log message subtext, used for detailed information.
	//public struct LogSubtext {
	//	public readonly string Title;
	//	public readonly string Message;

	//	public LogSubtext(string title, string message) {
	//		Title = title;
	//		Message = message;
	//	}


	//}

}
