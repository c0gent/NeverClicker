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
	class AutomationEngine {
		private MainForm MainForm;
		private Interactor Interactor;
		//GameClient. GameClientInstance;
		GameTaskQueue Queue;
		//Task MouseMoveTask = null;
		//private CancellationTokenSource CancelSource;

		public AutomationEngine(MainForm form) {
			MainForm = form;
			Interactor = new Interactor(MainForm);
			//GameClientInstance = new GameClient.Instance(Interactor);

            Queue = new GameTaskQueue();
			Queue.Populate(Interactor.GameAccount.GetSettingOrZero("NwCharacterCount", "NwAct"));	
		}

		public void Log(string message) {
			MainForm.Log(message);
			// ADD FILE LOGGING
		}

		private Progress<string> GetLogCallBack() {
			return new Progress<string>(s => Log(s));
		}

		public void AddGameTask(int charIdx, int delaySec) {

			var dateTime = DateTime.Now.AddSeconds(delaySec);
			var taskKind = TaskKind.Invocation;

			GameTask gameTask = new GameTask(
				dateTime, charIdx, taskKind
			);

			try {
				Log(String.Format("Adding task with charIdx: {0}, dateTime: {1}, taskKind: {2}", charIdx, dateTime, taskKind));
				Queue.Add(gameTask);
			} catch (Exception exc) {
				Log(exc.ToString());
			}
		}

		public void ProcessNextGameTask() {
			GameTask nextTask;

			if (!Queue.IsEmpty()) {
				nextTask = Queue.Pop();
				Log(String.Format("Processing next character: {0}.", nextTask.CharacterIdx.ToString()));
			} else {
				Log(String.Format("Task queue is empty."));
			}
			
			//EXECUTE TASK

			// SET TIMER FOR NEXT TASK

		}

		
		public string GetVar(string variable) {
			return Interactor.GetVar(variable);
		}

		public async Task<string> EvaluateStatementAsync(string statement) {
			//Progress<string> log = GetLogCallBack();
			//Interactor.State = AutomationState.Running;
			//CancelSource = new CancellationTokenSource();

			Log(String.Format("Evaluating '{0}'()...", statement));
			Interactor.Run(GetLogCallBack());

			await Task.Factory.StartNew(
				() => Interactor.ExecuteStatement(statement),
				TaskCreationOptions.LongRunning
			);

			//Interactor.State = AutomationState.Stopped;
			Interactor.Stop();
			//Log(String.Format("{0}() returns: {1}", functionName, result));
			return String.Format("'{0}' complete.", statement);

		}

		public async void EvaluateFunction(
			string functionName,
			string param1 = null,
			string param2 = null,
			string param3 = null,
			string param4 = null
		) {
			//CancelToken = new CancellationTokenSource();
			//Progress<string> log = GetLogCallBack();
			//Interactor.State = AutomationState.Running;

			//CancelSource = new CancellationTokenSource();
			Log(String.Format("{0}({1}, {2}, {3}): {5}", functionName, param1, param2, param3));
			Interactor.Run(GetLogCallBack());

			string result = await Task.Factory.StartNew(
				() => Interactor.EvaluateFunction(functionName, param1, param2, param3, param4),
				TaskCreationOptions.LongRunning
			);

			Interactor.Stop();
			//Interactor.State = AutomationState.Stopped;
			Log(String.Format("'{0}()' returns: '{1}'", functionName, result));
		}

		public async Task<bool> DetectWindowAsync(string windowExe) {
			//string detectionParam = String.Format("ahk_exe {0}", windowExe);
			//CancelToken = new CancellationTokenSource();
			//Progress<string> log = GetLogCallBack();

			Log(String.Format("Detecting: '{0}'...", windowExe));
			Interactor.Run(GetLogCallBack());

			var result = await Task.Factory.StartNew(
				() => Interactor.WindowDetectExists(windowExe),
				TaskCreationOptions.LongRunning
			);

			Interactor.Stop();
			return result;
		}

		public void Stop(MainForm mainForm) {
			Log("Stopping automation...");			

			try {
				Interactor.Stop();
				//if (CancelToken != null) {					
				//	CancelToken.Cancel();
				//}
			} catch (Exception e) {
				Log(string.Format("Task cancellation status: {0}", e));
			}
			finally {
				//Interactor.Reload();
				mainForm.SetButtonStateStopped();
				Log("AutoInvoke cancelled.");
				Log("Automation functions reloaded.");
			}
		}

		public void InitOldScript() {
			Interactor.Run(GetLogCallBack());
			Interactor.InitOldScript();
			Interactor.Stop();
		}

		public void AutoInvokeOld() {
			Log("Depricated.");
			//CancelToken = new CancellationTokenSource();
			//Progress<string> log = GetLogCallBack();

			//Interactor.State = AutomationState.Running;

			//await Task.Factory.StartNew(
			//	() => Interactions.Sequences.Old.AutoLaunchInvoke(Interactor, log, CancelToken.Token),
			//	TaskCreationOptions.LongRunning
			//);

			//Interactor.State = AutomationState.Stopped;
			//Log("Depricated.");
		}

		//	NEW CYCLE ACTIVATION ENTRY POINT
		public async void AutoCycle(MainForm mainForm) {
			//CancelToken = new CancellationTokenSource();
			//Progress<string> log = GetLogCallBack();
			//Interactor.State = AutomationState.Running;
			Log("AutoCycle activated.");
			Interactor.Run(GetLogCallBack());

			try {
				await Task.Factory.StartNew(
					() => Sequences.AutoCycle(Interactor, Queue),
					TaskCreationOptions.LongRunning
				);
			} catch (System.Reflection.TargetInvocationException) {
				Log("AutomationEngine::AutoCycle(): System.Reflection.TargetInvocationException caught.");
			}

			//Interactor.State = AutomationState.Stopped;
			Interactor.Stop();
			mainForm.SetButtonStateStopped();
			Log("AutoCycle complete.");
		}

		public async void MouseMovementTest() {
			//if (MouseMoveTask == null) {
			try {
				//CancelToken = new CancellationTokenSource();
				//Progress<string> log = GetLogCallBack();
				Log("Mouse movement activated.");
				Interactor.Run(GetLogCallBack());

				await Task.Factory.StartNew(
					() => Sequences.MouseMoveTest(Interactor),
					TaskCreationOptions.LongRunning
				);
			} catch {
				Log("Mouse movement cancelled.");
			}

			//} else {
			//	Log("Mouse movement resumed.");
			//	return;
			//}
		}

		public void Timer() {
			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			aTimer.Interval = 5000;
			aTimer.Enabled = true;

			//Console.WriteLine("Press \'q\' to quit the sample.");
			//while (Console.Read() != 'q') ;
		}

		public void Reload() {
			Interactor.Reload();
		}

		public AutomationState TogglePause() {
			if (Interactor.State == AutomationState.Running) {
				Interactor.Pause();
			} else if (Interactor.State == AutomationState.Paused) {
				Interactor.Unpause();
			}
			return Interactor.State;
		}

		// Specify what you want to happen when the Elapsed event is raised.
		private void OnTimedEvent(object source, ElapsedEventArgs e) {
			Log("Hello World!");
		}

	}

}
