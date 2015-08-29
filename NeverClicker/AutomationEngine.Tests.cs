using NeverClicker.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NeverClicker {
	partial class AutomationEngine {

		public void InitOldScript() {
			Itr.Run(GetLogProgress());
			Itr.InitOldScript();
			Itr.Stop();
		}


		public void Timer() {
			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			aTimer.Interval = 5000;
			aTimer.Enabled = true;

			//Console.WriteLine("Press \'q\' to quit the sample.");
			//while (Console.Read() != 'q') ;
		}

		// Specify what you want to happen when the Elapsed event is raised.
		private void OnTimedEvent(object source, ElapsedEventArgs e) {
			Log("Hello World!");
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
				Log(string.Format("Processing next character: {0}.", nextTask.CharacterIdx.ToString()));
			} else {
				Log("Task queue is empty.");
			}

			//EXECUTE TASK
			// SET TIMER FOR NEXT TASK
		}


		//
		//	INTERACTOR
		//


		public async void MouseMovementTest() {
			//if (MouseMoveTask == null) {
			try {
				//CancelToken = new CancellationTokenSource();
				//Progress<string> log = GetLogProgress();
				Log("Mouse movement activated.");
				Itr.Run(GetLogProgress());

				await Task.Factory.StartNew(
					() => Sequences.MouseMoveTest(Itr),
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


		public string GetVar(string variable) {
			return Itr.GetVar(variable);
		}


		public async void EvaluateFunction(
						string functionName,
						string param1 = null,
						string param2 = null,
						string param3 = null,
						string param4 = null
		) {
			//CancelToken = new CancellationTokenSource();
			//Progress<string> log = GetLogProgress();
			//Interactor.State = AutomationState.Running;

			

			//CancelSource = new CancellationTokenSource();
			//.Log(String.Format("{0}({1}, {2}, {3}): {4}", functionName, param1, param2, param3));

			//System.Windows.Forms.MessageBox.Show("test0");
			Itr.Run(GetLogProgress());


			string result = "";

            try { 
				result = await Task.Factory.StartNew(
					() => Itr.EvaluateFunction(functionName, param1, param2, param3, param4),
					TaskCreationOptions.LongRunning
				);
			} catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
			}

			Itr.Stop();
			//Interactor.State = AutomationState.Stopped;
			Log(String.Format("'{0}()' returns: '{1}'", functionName, result));
		}


		public async void ExecuteStatementAsync(string statement) {
			//Progress<string> log = GetLogProgress();
			//Interactor.State = AutomationState.Running;
			//CancelSource = new CancellationTokenSource();

			Log(String.Format("Evaluating '{0}'()...", statement));
			Itr.Run(GetLogProgress());

			try {
				await Task.Factory.StartNew(
					() => Itr.ExecuteStatement(statement),
					TaskCreationOptions.LongRunning
				);
			} catch (Exception ex) {
				System.Windows.Forms.MessageBox.Show(ex.ToString());
			}
			

			//Interactor.State = AutomationState.Stopped;
			Itr.Stop();
			Log(string.Format("'{0}' complete.", statement));

		}


		public async Task<bool> DetectWindowAsync(string windowExe) {
			Log(string.Format("Detecting: '{0}'...", windowExe));
			var result = await Run(() => Screen.WindowDetectExist(Itr, windowExe));
			return result;
		}

		public async void WindowMinimize(string windowExe) {
			Log(string.Format("Minimizing: '{0}'...", windowExe));
			await Run(() => Screen.WindowMinimize(Itr, windowExe));
		}

		public async void WindowActivate(string windowExe) {
			Log(string.Format("Activating: '{0}'...", windowExe));
			await Run(() => Screen.WindowActivate(Itr, windowExe));
		}

		public async void WindowKill(string windowExe) {
			Log(string.Format("Activating: '{0}'...", windowExe));
			await Run(() => Screen.WindowKill(Itr, windowExe));
		}

		public async void ImageSearch(string imgCode) {
			Itr.Run(GetLogProgress());

			ImageSearchResult searchResult;

			searchResult = await Task.Factory.StartNew(
				() => Interactions.Screen.ImageSearch(Itr, imgCode),
				TaskCreationOptions.LongRunning
			);

			if (searchResult.Found) {
				Log("Image found at: " + searchResult.Point.ToString());
			} else {
				Log("Image not found.");
			}

			Itr.Stop();
		}


		public void AutoInvokeOld() {
			Log("Depricated.");
			//CancelToken = new CancellationTokenSource();
			//Progress<string> log = GetLogProgress();

			//Interactor.State = AutomationState.Running;

			//await Task.Factory.StartNew(
			//	() => Interactions.Sequences.Old.AutoLaunchInvoke(Interactor, log, CancelToken.Token),
			//	TaskCreationOptions.LongRunning
			//);

			//Interactor.State = AutomationState.Stopped;
			//Log("Depricated.");
		}


	}
}
