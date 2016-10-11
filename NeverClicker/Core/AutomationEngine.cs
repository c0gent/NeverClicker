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
		private MainForm MainForm;
		private Interactor Itr;
		//TaskQueue StoredQueue;
		//LogFile LogFile;
		
		public AutomationEngine(MainForm form) {
			this.MainForm = form;

			try {
				Itr = new Interactor();
				//Queue = new TaskQueue();
				//StoredQueue = null;
				//LogFile = new LogFile();
			} catch (Exception ex) {
				MessageBox.Show(MainForm, "Error initializing AutomationEngine: " + ex.ToString());
			}
		}

		//public void InitOldScript_DEPRICATING() {
		//	Itr.Start(GetLogProgress(), GetTaskQueueProgress());
		//	Itr.Stop();
		//}

		public void LogProgress(string message) {
			MainForm.WriteLine(message);
		}

		public void LogError(string message) {
			MainForm.AppendError(message);
		}

		private Progress<LogMessage> GetLogProgress() {
			return new Progress<LogMessage>(l => MainForm.WriteLine(l.Text));
		}

		private Progress<LogMessage> GetLogError() {
			return new Progress<LogMessage>(l => MainForm.AppendError(l.Text));
		}

		//private Progress<ImmutableSortedDictionary<long, GameTask>> GetTaskQueueProgress() {
		//	return new Progress<ImmutableSortedDictionary<long, GameTask>>(sl => MainForm.RefreshTaskQueue(sl));
		//}

		private Progress<ImmutableArray<TaskDisplay>> GetTaskQueueProgress() {
			return new Progress<ImmutableArray<TaskDisplay>>(sl => MainForm.RefreshTaskQueue(sl));
		}

		public async Task<TResult> Run<TResult>(Func<TResult> action) {
			MainForm.SetButtonStateRunning();
			try {
				var result = await Task.Factory.StartNew(action, Itr.Start(GetLogProgress(), GetLogError(),
					GetTaskQueueProgress()), 
					TaskCreationOptions.LongRunning, TaskScheduler.Current);
				Itr.Stop();
				return result;
			} catch (Exception ex) {
				LogError(new LogMessage(ex.ToString(), LogEntryType.Fatal).Text);
				throw ex;
			} finally {
				Itr.Stop();
				LogProgress("Stopping running tasks...");
				MainForm.SetButtonStateStopped();
			}
		}
		
		public async Task Run(Action action) {
			Func<bool> func = () => { action(); return false; };
			await Run(func);
		}

		public CancellationTokenSource Stop() {
			try {
				LogProgress("Stopping automation engine...");
				Itr.CancelSource.Cancel();
				return Itr.CancelSource;
			} catch (Exception ex) {
				LogError(new LogMessage("Task cancellation error: " + ex, LogEntryType.Error).Text);
				return null;
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

		public async Task AutoCycle(int startDelaySec) {
			//await Run(() => Sequences.AutoCycle(Itr, Queue, startDelaySec));
			await Run(() => Sequences.AutoCycle(Itr, startDelaySec));
			LogProgress("AutoCycle stopped.");
		}
	}
}
