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

namespace NeverClicker {
	//	AUTOMATIONENGINE: MANAGE AUTOMATION STATE
	//		- ROOT OF ALL ASYNCHRONOUS OPERATIONS
	partial class AutomationEngine {
		const bool SHOW_DEBUG_LOG_MESSAGES_IN_TEXTBOX = false; // make user setting
		const bool PRINT_DEBUG_LOG_MESSAGES_TO_LOG_FILE = false; // make user setting

		private MainForm MainForm;
		private Interactor Itr;
		//GameClient. GameClientInstance;
		GameTaskQueue Queue;
		LogFile LogFile;
		//Task MouseMoveTask = null;
		//private CancellationTokenSource CancelSource;
		//private static readonly object Locker = new object();
		//private static XmlDocument LogXmlDoc = new XmlDocument();
		//private string LogFileName = "";
				
		
		public AutomationEngine(MainForm form) {
			this.MainForm = form;
			Itr = new Interactor(MainForm);
            Queue = new GameTaskQueue();
			LogFile = new LogFile();

			//this.MainForm.BindListBox(Queue.TaskList);
			
			//LogFileName = Settings.Default.LogFilePath.ToString();

			//if (File.Exists(LogFileName))
			//	LogXmlDoc.Load(LogFileName);
			//else {
			//	var root = LogXmlDoc.CreateElement("messages");
			//	LogXmlDoc.AppendChild(root);
			//}
		}
		
		public void Log(string message) {
			//textBox1.AppendText(message);
			Log(new LogMessage(message));
		}
		
		public void Log(LogMessage logMessage) {			
			switch (logMessage.Type) {
				case LogEntryType.Warning:
				case LogEntryType.Normal:
					LogFile.AppendMessage(logMessage);
					MainForm.WriteLine(logMessage.Text);					
					break;
				case LogEntryType.Error:
				case LogEntryType.Fatal:
					LogFile.AppendMessage(logMessage);
					MainForm.WriteLine(logMessage.Text);
					MessageBox.Show(logMessage.Text);
					break;
				case LogEntryType.Debug:
					LogFile.AppendMessage(logMessage);
					if (SHOW_DEBUG_LOG_MESSAGES_IN_TEXTBOX) { MainForm.WriteLine(logMessage.Text); }
					if (PRINT_DEBUG_LOG_MESSAGES_TO_LOG_FILE) { LogFile.AppendMessage(logMessage); }
					break;				
			}
		}

		private Progress<LogMessage> GetLogProgress() {
			return new Progress<LogMessage>(l => Log(l));
		}

		private Progress<SortedList<long, GameTask>> GetTaskQueueProgress() {
			return new Progress<SortedList<long, GameTask>>(sl => MainForm.RefreshTaskQueue(sl));
		}

		public async Task<TResult> Run<TResult>(Func<TResult> action) {
			//Itr.Run(GetLogProgress());
			try {
				//var result = await Task.Factory.StartNew(action, TaskCreationOptions.LongRunning);
				var result = await Task.Factory.StartNew(action, Itr.Start(GetLogProgress()), 
					TaskCreationOptions.LongRunning, TaskScheduler.Current);
				Itr.Stop();
				return result;
			} catch (Exception ex) {
				Log(ex.ToString());
				MessageBox.Show(ex.ToString());
				throw ex;
			}
		}
		
		public async Task Run(Action action) {
			MainForm.SetButtonStateRunning();
			try {
				//await Task.Factory.StartNew(action, Itr.Start(GetLogProgress(), GetTaskQueueProgress()),
				await Task.Factory.StartNew(action, Itr.Start(GetLogProgress()),
					TaskCreationOptions.LongRunning, TaskScheduler.Current);
			} catch (Exception ex) {
				Log(ex.ToString());
				MessageBox.Show(ex.ToString());
				throw ex;
			} finally {				
				Itr.Stop();
				Log("Automation engine stopped. There may be outstanding tasks yet to terminate.");
				MainForm.SetButtonStateStopped();
			}
		}

		public void Stop() {
			try {
				Log("Stopping automation...");
				Itr.CancelSource.Cancel();
			} catch (Exception ex) {
				Log(new LogMessage("Task cancellation error: " + ex, LogEntryType.Error));
			} 
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

		public async void AutoCycle() {
			Log("AutoCycle activated.");			
			await Run(() => Sequences.AutoCycle(Itr, Queue));
			Log("AutoCycle terminated.");
		}
	}
}
