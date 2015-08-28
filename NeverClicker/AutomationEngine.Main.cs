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

namespace NeverClicker {
	//	AUTOMATIONENGINE: MANAGE AUTOMATION STATE
	//		- ROOT OF ALL ASYNCHRONOUS OPERATIONS
	partial class AutomationEngine {
		private MainForm MainForm;
		private Interactor Itr;
		//GameClient. GameClientInstance;
		GameTaskQueue Queue;
		//Task MouseMoveTask = null;
		//private CancellationTokenSource CancelSource;

		public AutomationEngine(MainForm form) {
			MainForm = form;
			Itr = new Interactor(MainForm);
            Queue = new GameTaskQueue();
			var lastChar = Itr.GameAccount.GetSettingOrZero("LastCharacterInvoked", "Invocation");
            Queue.Populate(lastChar, Itr.GameAccount.GetSettingOrZero("NwCharacterCount", "NwAct"));	
		}


		//private Progress<string> GetLogCallBack() {
		//	return new Progress<string>(s => MainForm.Log(s));
		//}


		private Progress<LogMessage> GetLogProgress() {
			return new Progress<LogMessage>(l => MainForm.Log(l));
		}


		public async Task<TResult> Run<TResult>(Func<TResult> action) {
			Itr.Run(GetLogProgress());
			var result = await Task.Factory.StartNew(action, TaskCreationOptions.LongRunning);
			Itr.Stop();
			return result;
		}


		public async Task Run(Action action) {
			Itr.Run(GetLogProgress());
			await Task.Factory.StartNew(action, TaskCreationOptions.LongRunning);
			Itr.Stop();
		}


		public void Stop(MainForm mainForm) {
			MainForm.Log("Stopping automation...");			

			try {
				//Itr.Stop();
				Itr.CancelSource.Cancel();
				//if (CancelToken != null) {					
				//	CancelToken.Cancel();
				//}
			} catch (Exception e) {
				MainForm.Log(string.Format("Task cancellation status: {0}", e));
			}
			finally {
				//Interactor.Reload();
				mainForm.SetButtonStateStopped();
				MainForm.Log("AutoInvoke cancelled.");
				MainForm.Log("Automation functions reloaded.");
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


		public async void AutoCycle(MainForm mainForm) {
			//CancelToken = new CancellationTokenSource();
			//Progress<string> log = GetLogProgress);
			//Interactor.State = AutomationState.Running;
			MainForm.Log("AutoCycle activated.");
			
			await Run(() => Sequences.AutoCycle(Itr, Queue));

			//Interactor.Run(GetLogProgress());
			//await Task.Factory.StartNew(
			//	() => Sequences.AutoCycle(Interactor, Queue),
			//	TaskCreationOptions.LongRunning
			//);
			//Interactor.Stop();
			
			mainForm.SetButtonStateStopped();
			MainForm.Log("AutoCycle complete.");
		}
	}
}
