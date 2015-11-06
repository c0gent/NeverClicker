using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NeverClicker.Forms;
using System.Timers;
using NeverClicker.Interactions;
using System.Configuration;
using System.Xml;
using NeverClicker.Properties;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Immutable;

namespace NeverClicker {
	//	AUTOMATIONENGINE: MANAGE AUTOMATION STATE
	//		- ROOT OF ALL ASYNCHRONOUS OPERATIONS
	public partial class AutomationEngine {
		//const bool SHOW_DEBUG_LOG_MESSAGES_IN_TEXTBOX = false; 
		//const bool PRINT_DEBUG_LOG_MESSAGES_TO_LOG_FILE = false; // make a user setting

		private MainForm MainForm;
		private Interactor Itr;
		//GameClient. GameClientInstance;
		TaskQueue Queue;
		LogFile LogFile;
		//Task MouseMoveTask = null;
		//private CancellationTokenSource CancelSource;
		//private static readonly object Locker = new object();
		//private static XmlDocument LogXmlDoc = new XmlDocument();
		//private string LogFileName = "";
				
		
		public AutomationEngine(MainForm form) {
			this.MainForm = form;

			try {
				Itr = new Interactor();
				Queue = new TaskQueue();
				LogFile = new LogFile();
			} catch (Exception ex) {
				//MainForm.WriteLine(ex.ToString());
				MessageBox.Show(MainForm, "Error initializing AutomationEngine: " + ex.ToString());
			}

			//if (!SettingsManager.SettingsAreValid()) {
			//	MainForm.SettingsInvalid();
			//}

			//this.MainForm.BindListBox(Queue.TaskList);

			//LogFileName = Settings.Default.LogFilePath.ToString();

			//if (File.Exists(LogFileName))
			//	LogXmlDoc.Load(LogFileName);
			//else {
			//	var root = LogXmlDoc.CreateElement("messages");
			//	LogXmlDoc.AppendChild(root);
			//}
		}

		//public void Init() {
			
		//}

		public void InitOldScript_DEPRICATING() {
			Itr.Start(GetLogProgress(), GetTaskQueueProgress());
			//Itr.Start(GetLogProgress());
			//Itr.InitOldScript();
			Itr.Stop();
		}

		public void Log(string message) {
			//textBox1.AppendText(message);
			Log(new LogMessage(message));
		}
		
		public void Log(LogMessage logMessage) {			
			switch (logMessage.Type) {
				case LogEntryType.FatalWithScreenshot:					
					LogFile.AppendMessage(logMessage);
					MainForm.WriteLine(logMessage.Text);
					MessageBox.Show(MainForm, logMessage.Text + " -- " 
						+ SaveErrorScreenshot(), "NeverClicker - " + logMessage.Text);
					break;
				case LogEntryType.Fatal:					
					LogFile.AppendMessage(logMessage);
					MainForm.WriteLine(logMessage.Text);
					MessageBox.Show(MainForm, logMessage.Text, "NeverClicker - " + logMessage.Text);
					break;
				case LogEntryType.Error:
				case LogEntryType.Warning:
				case LogEntryType.Normal:
					LogFile.AppendMessage(logMessage);
					MainForm.WriteLine(logMessage.Text);
					break;				
				case LogEntryType.Info:
					LogFile.AppendMessage(logMessage);
					break;
				case LogEntryType.Debug:
					if (Settings.Default.LogDebugMessages) { LogFile.AppendMessage(logMessage); }
					break;				
			}
		}

		private string SaveErrorScreenshot() {			
			var errorImageFileName = Settings.Default.LogsFolderPath + @"\" + "FATAL_ERROR_"
				+ DateTime.Now.ToFileTime().ToString() + ".png";
			var errMsg = "FATAL ERROR: Please investigate! Image file: " + errorImageFileName;
			//Log(new LogMessage(errMsg, LogEntryType.Fatal));
			ScreenCapture sc = new ScreenCapture();
			Image img = sc.CaptureScreen();
			img.Save(errorImageFileName, ImageFormat.Png);
			return errMsg;
		}

		private Progress<LogMessage> GetLogProgress() {
			return new Progress<LogMessage>(l => Log(l));
		}

		private Progress<ImmutableSortedDictionary<long, GameTask>> GetTaskQueueProgress() {
			return new Progress<ImmutableSortedDictionary<long, GameTask>>(sl => MainForm.RefreshTaskQueue(sl));
		}

		public async Task<TResult> Run<TResult>(Func<TResult> action) {
			MainForm.SetButtonStateRunning();
			try {
				var result = await Task.Factory.StartNew(action, Itr.Start(GetLogProgress(), GetTaskQueueProgress()), 
					TaskCreationOptions.LongRunning, TaskScheduler.Current);
				Itr.Stop();
				return result;
			} catch (Exception ex) {
				Log(ex.ToString());
				MessageBox.Show(MainForm, ex.ToString());
				throw ex;
			} finally {
				Itr.Stop();
				Log("Stopping running tasks...");
				MainForm.SetButtonStateStopped();
			}
		}
		
		public async Task Run(Action action) {
			Func<bool> func = () => { action(); return false; };
			await Run(func);

			//MainForm.SetButtonStateRunning();
			//try {
			//	await Task.Factory.StartNew(action, Itr.Start(GetLogProgress(), GetTaskQueueProgress()),
			//		TaskCreationOptions.LongRunning, TaskScheduler.Current);
			//} catch (Exception ex) {
			//	Log(ex.ToString());
			//	MessageBox.Show(ex.ToString());
			//	throw ex;
			//} finally {				
			//	Itr.Stop();
			//	Log("Automation engine stopped. There may be outstanding tasks yet to terminate.");
			//	MainForm.SetButtonStateStopped();
			//}
		}

		public void Stop() {
			try {
				Log("Stopping automation engine...");
				Itr.CancelSource.Cancel();
			} catch (Exception ex) {
				Log(new LogMessage("Task cancellation error: " + ex, LogEntryType.Error));
			} 
		}

		public void ReloadSettings() {
			Itr.LoadSettings();
		}


		public void Reload() {
			Itr.Reload();
		}

		public AutomationState TogglePause() {
			if (Itr.State == AutomationState.Running) {
				Itr.Pause();
			} else if (Itr.State == AutomationState.Paused) {
				Itr.Unpause();
			}
			return Itr.State;
		}

		public async void AutoCycle(int startDelaySec) {
			//Log("AutoCycle activated.");			
			await Run(() => Sequences.AutoCycle(Itr, Queue, startDelaySec));
			Log("AutoCycle stopped.");
		}
	}
}
