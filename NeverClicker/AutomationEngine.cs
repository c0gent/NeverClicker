using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NeverClicker.Forms;

namespace NeverClicker {
	//	AUTOMATIONENGINE: MANAGE AUTOMATION STATE
	//		- ROOT OF ALL ASYNCHRONOUS OPERATIONS
	class AutomationEngine {
		private MainForm MainForm;
		//Alib.Interop.AlibEngine AlibEng;
		//String AlibAutoInvokeAsyncPath = "A:\\NW_Common.ahk";
		public Interactions.Interactor Interactor;
		GameClient.Instance Game;
		//Action GetTextBoxCallback;

		//Thread InputThread = null;
		Task MouseMoveTask = null;
		CancellationTokenSource CancelToken;

		public AutomationEngine(MainForm form) {
			MainForm = form;
			//GetTextBoxCallback = MainForm.GetTextBoxCallback();
			Interactor = new Interactions.Interactor(MainForm);
			Game = new GameClient.Instance(Interactor);
		}

		public void Log(string message) {
			MainForm.WriteTextBox(message);
			// ADD FILE LOGGING
		}

		private Progress<string> GetLogCallBack() {
			return new Progress<string>(s => Log(s));
		}

		public string GetVar(string variable) {
			return Interactor.GetVar(variable);
		}

		public async Task<string> EvaluateStatementAsync(string statement) {
			Log(String.Format("Evaluating '{0}'()...", statement));
			CancelToken = new CancellationTokenSource();
			Progress<string> progress = GetLogCallBack();
			Interactor.State = AutomationState.Running;

			await Task.Factory.StartNew(
				() => Interactor.EvaluateStatement(Interactor, progress, CancelToken.Token, statement),
				TaskCreationOptions.LongRunning
			);

			Interactor.State = AutomationState.Stopped;
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
            Log(String.Format("{0}({1}, {2}, {3}): {5}", functionName, param1, param2, param3));
			CancelToken = new CancellationTokenSource();
			Progress<string> progress = GetLogCallBack();
			Interactor.State = AutomationState.Running;

			string result = await Task.Factory.StartNew(
				() => Interactor.EvaluateFunction(Interactor, progress, CancelToken.Token, functionName, param1, param2, param3, param4),
				TaskCreationOptions.LongRunning
			);

			Interactor.State = AutomationState.Stopped;
			Log(String.Format("'{0}()' returns: '{1}'", functionName, result));
		}

		public async Task<bool> DetectWindowAsync(string windowExe) {
			string detectionParam = String.Format("ahk_exe {0}", windowExe);
			Log(String.Format("Detecting: '{0}'...", windowExe));
			CancelToken = new CancellationTokenSource();
			Progress<string> progress = GetLogCallBack();

			string result = await Task.Factory.StartNew(
				() => Interactor.EvaluateFunction(Interactor, progress, CancelToken.Token, "WinExist", windowExe, detectionParam),
				TaskCreationOptions.LongRunning
			);

			return result.Trim() != "0x0";
		}

		public void Stop() {
			Log("Stopping automation...");

			try {
				if (CancelToken != null) {
					CancelToken.Cancel();
				}
			} catch (Exception e) {
				Log(String.Format("Task cancellation status: {0}", e));
			}
			finally {
				Interactor.Reload();
				Log("AutoInvokeAsync cancelled.");
				Log("Automation functions reloaded.");
			}
		}

		public async void AutoInvoke() {
			Log("AutoInvokeAsync activated.");
			CancelToken = new CancellationTokenSource();
			Progress<string> progress = GetLogCallBack();

			Interactor.State = AutomationState.Running;

			await Task.Factory.StartNew(
				() => Interactions.Sequences.Old.OldAutoLaunchInvoke(Interactor, progress, CancelToken.Token),
				TaskCreationOptions.LongRunning
			);

			Interactor.State = AutomationState.Stopped;
			Log("AutoInvokeAsync complete.");
		}

		public async void MouseMovementTest() {
			if (MouseMoveTask == null) {
				try {
					Log("Mouse movement activated.");
					CancelToken = new CancellationTokenSource();
					Progress<string> progress = GetLogCallBack();

					await Task.Factory.StartNew(
						() => NeverClicker.Interactions.Sequences.MouseMoveTest.Start(Interactor, progress, CancelToken.Token),
						TaskCreationOptions.LongRunning
					);
				} catch {
					Log("Mouse movement cancelled.");
				}

			} else {
				Log("Mouse movement resumed.");
				return;
			}
		}

	}

	public enum AutomationState {
		Stopped,
		Running,
		Paused
	}


}
