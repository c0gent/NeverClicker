using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using Alib;
using NeverClicker.Forms;
using NeverClicker.Properties;
using IniParser;
using System.Configuration;

namespace NeverClicker.Interactions {
	// INTERACTOR: MANAGES ALIBENGINE
	public class Interactor {
		private AlibEngine AlibEng;
		string NwCommonFileName;
		static private uint MaxFileLoadAttempts = 5;
		public AutomationState State { get; private set; } = AutomationState.Stopped;
		//public IProgress<string> ProgressLog { get; private set; }
		public IProgress<LogMessage> ProgressLog { get; private set; }
		public CancellationTokenSource CancelSource { get; private set; }
		public IniFile GameAccount = new IniFile(Settings.Default["GameAccountIniPath"].ToString());
		public IniFile GameClient = new IniFile(Settings.Default["GameClientIniPath"].ToString());

		public Interactor(MainForm mainForm) {
			InitAlibEng();

			bool settingsNotSet = false; 

			foreach (SettingsProperty setting in Settings.Default.Properties) {
				//mainForm.Log();
                if (string.IsNullOrWhiteSpace(Settings.Default[setting.Name].ToString())) {					
					settingsNotSet = true;
					break;
				}
			}

			if (settingsNotSet) {
				mainForm.SettingsNotSet();
			}
		}

		private void InitAlibEng() {
			AlibEng = new AlibEngine();
			//AlibEng.Exec("CoordMode, Mouse, Screen");
			//AlibEng.Exec("CoordMode, Pixel, Screen");
		}

		//public void Log(string message, params string[] args) {
		//	ProgressLog.Report(new LogMessage(string.Format(message, args)));
		//}

		public void Log(string message) {
			Log(message, LogType.Normal);
		}

		public void Log(string message, LogType lt) {
			if (message != null) {
				ProgressLog.Report(new LogMessage(message, lt));
			} else {
				ProgressLog.Report(new LogMessage("Interactor::Log(): null log message passed!"));
			}	
		}

		public void Log(LogMessage logMessage) {
			ProgressLog.Report(logMessage);
		}

		public void MoveMouseCursor(Point point, bool click) {
			string execString = string.Format("SendEvent {{Click {0}, {1}, {2}}}",
				point.X.ToString(), point.Y.ToString(), Convert.ToInt32(click).ToString());
			ExecuteStatement(execString);
		}

		public void KeyboardTypeKey(string key) {

		}

		public void KeyboardTypeKey(string key, KeyAction action) {

		}

		// SHOULD BE ASYNC BUT DEPRICATING EVENTUALLY ANYWAY
		public void InitOldScript() {
			VerifyRunning();
			string scriptRoot = Settings.Default["SettingsRootPath"].ToString();
			string gameExeRoot = Settings.Default["NeverwinterExePath"].ToString();
			string imagesFolder = Settings.Default["ImagesFolderPath"].ToString();

			if ((scriptRoot == "") || (gameExeRoot == "")) {
				Log(string.Format("Cannot load script file or paths: '{0}' & '{1}'.", scriptRoot, gameExeRoot));
				return;
			}

			try {
				NwCommonFileName = scriptRoot + "NW_Common.ahk";
				LoadFile(NwCommonFileName);

				AlibEng.Exec("SetWorkingDir %A_ScriptDir%");
				AlibEng.Exec("A_CommonDir = " + scriptRoot);
				AlibEng.Exec("A_ImagesDir = " + imagesFolder);
				AlibEng.Exec("NwFolder := \"" + gameExeRoot + "\"");

				//AlibEng.Exec("gcs_ini := A_CommonDir . \"\\nw_game_client_settings.ini\"");
				//AlibEng.Exec("as_ini := A_CommonDir . \"\\nw_account_settings.ini\"");
				//AlibEng.Exec("ai_log := A_CommonDir . \"\\NeverClicker_Log.txt\"");

				AlibEng.Exec("gcs_ini := \"" + Settings.Default["GameClientIniPath"].ToString() + "\"");
				AlibEng.Exec("as_ini := \"" + Settings.Default["GameAccountIniPath"].ToString() + "\"");
				AlibEng.Exec("ai_log := \"" + Settings.Default["LogFilePath"].ToString() + "\"");

				//AlibEng.Exec("^!=::Suspend");

				AlibEng.Exec("ToggleAfk := 0");
				AlibEng.Exec("ToggleMouseDragClick := 0");
				AlibEng.Exec("ToggleShit := 0");

				AlibEng.Exec("SendMode Input");
				AlibEng.Exec("CoordMode, Mouse, Screen");
				AlibEng.Exec("CoordMode, Pixel, Screen");
				AlibEng.Exec("SetMouseDelay, 55");
				AlibEng.Exec("SetKeyDelay, 55, 15");

				AlibEng.ExecFunction("init");
			} catch (Exception ex) {
				Log(ex.ToString());
			}

			Log("Old script initialized.");
		}

		public CancellationToken Run(IProgress<LogMessage> log) {
			if (State == AutomationState.Stopped) {
				ProgressLog = log;
				CancelSource = new CancellationTokenSource();
				State = AutomationState.Running;
			} else {
				throw new AlreadyRunningException();
			}

			return CancelSource.Token;
		}

		//public void Run(IProgress<string> log) {			
		//	if (State == AutomationState.Stopped) {
		//		ProgressLog = log;
		//		CancelSource = new CancellationTokenSource();
		//		State = AutomationState.Running;
		//	} else {
		//		throw new AlreadyRunningException();
		//	}
			
		//}

		public void Stop() {
			if (State != AutomationState.Stopped) {
				CancelSource.Cancel();
				State = AutomationState.Stopped;
				//Reload();
			}
		}


		// DEPRICATE?
		public AutomationState Pause() {
			if (State == AutomationState.Running) {
				Log("Interactor::Pause(): Temporarily disabled.");
				//State = AutomationState.Paused;
				// SORT OUT WHAT THE PROBLEM IS WITH SUSPEND AND ADD A TRY CATCH BLOCK
				//AlibEng.Suspend();
			}
			return State;
		}

		// DEPRICATE?
		public AutomationState Unpause() {
			if (State == AutomationState.Paused) {
				Log("Interactor::UnPause(): Temporarily disabled.");
				//State = AutomationState.Running;
				// SORT OUT WHAT THE PROBLEM IS WITH SUSPEND AND ADD A TRY CATCH BLOCK
				//AlibEng.UnSuspend();
			}
			return State;
		}

		// DEPRICATE?
		public void Reload() {
			VerifyStopped();
			//AlibEng.Suspend();
			Task.Delay(2000).Wait();
			AlibEng.Terminate();

			State = AutomationState.Running;
			InitAlibEng();
			State = AutomationState.Stopped;
		}

		public bool WaitUntil(int maxWait, Func<bool> condition) {
			int iters = 0;

			this.Log("Waiting maximum of " + maxWait + " seconds.");

			while (!condition()) {
				//this.Log("Waiting until: " + condition.ToString() + ".");
				this.Wait(1000);
				iters += 1;
				if (iters >= maxWait) { return false; }
				if (CancelSource.IsCancellationRequested) {	return false; }
			}

			return true;
		}

		public void Wait(int millisecondsDelay) {
			VerifyRunning();
			try {
				//Task.Delay(millisecondsDelay, CancelSource.Token).Wait();
				Task.Delay(millisecondsDelay).Wait();
			} catch (AggregateException ae) {
				ae.Handle((x) => {	
					if (x is TaskCanceledException) {						
						return true;
					} else {
						Log(x.ToString());
						System.Windows.Forms.MessageBox.Show(x.ToString());
						return false; // Let anything else stop the application.
					}
				});
			}
		}

		public void VerifyStopped() {
			if (State != AutomationState.Stopped) { throw new AlreadyRunningException(); }
		}

		public void VerifyRunning() {
			if ((CancelSource == null) || (State != AutomationState.Running && !(CancelSource.IsCancellationRequested))) {
				throw new NotRunningException();
			}		
		}


		// CONVERT TO ASYNC
		private void LoadFile(string fileName) {
			VerifyRunning();
			for (uint i = 0; i < MaxFileLoadAttempts; i++) {
				try {
					//log.Report(String.Format("Attempting to load '{0}'.", NwCommonFileName));
					AlibEng.AddFile(NwCommonFileName);
				} catch (Exception e) {
					Log(String.Format("Problem loading: '{0}': {1}", NwCommonFileName, e));
					continue;
				}

				Log(String.Format("'{0}' loaded.", NwCommonFileName));
				break;
			}
		}


		//	CORE INTERACTION PRIMITIVES
		//
		//
		//
		public string GetVar(string variableName) {
			return AlibInterface(new Func<string>(() => {
				return AlibEng.GetVar(variableName);
			}));
		}

		public void SetVar(string variableName, string value) {
			AlibInterface(new Func<string>(() => {
				AlibEng.SetVar(variableName, value);
				return null;
			}));
		}

		//public string EvaluateFunction(
		//			string functionName,
		//			string param1 = null,
		//			string param2 = null,
		//			string param3 = null,
		//			string param4 = null,
		//			string param5 = null,
		//			string param6 = null,
		//			string param7 = null,
		//			string param8 = null,
		//			string param9 = null,
		//			string param10 = null
		//) {
		//	try {
		//		var result = AlibEng.ExecFunction(functionName, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
		//		return result;
		//	} catch (Exception ex) {
		//		Log(ex.ToString());
		//		System.Windows.Forms.MessageBox.Show(ex.ToString());
		//		//return null;
		//		throw ex;
		//	}
		//}



		public string EvaluateFunction(string functionName, params string[] args) {
			//return AlibInterface(new Func<string>(() => {
			//	return AlibEng.ExecFunction(functionName, args);
			//}));

			////System.Windows.Forms.MessageBox.Show("test0");
			//VerifyRunning();
			////System.Windows.Forms.MessageBox.Show("test1");

			try {
				var result = AlibEng.ExecFunction(functionName, args);
				return result;
			} catch (Exception ex) {
				Log(ex.ToString());
				System.Windows.Forms.MessageBox.Show(ex.ToString());
				//return null;
				throw ex;
			}
		}

		public void ExecuteStatement(string statement) {
			AlibInterface(new Func<string>(() => {
				AlibEng.Exec(statement);
				return null;
			}));			
		}

		//public string EvaluateStatement(string statement) {
		//	return AlibEng.Eval(statement);
		//}

		private string AlibInterface(Func<string> alibAction) {
			VerifyRunning();

			try {
				var result = alibAction();
				return result;
			} catch (Exception ex) {
				Log(ex.ToString());
				throw ex;
				//return null;
			}
		}

	}

	public enum AutomationState {
		Stopped,
		Running,
		Paused
	}

	public enum KeyAction {
		Up,
		Down
	}

	class AlreadyRunningException : Exception {
		public AlreadyRunningException() : base("Interactor Already running!") {}
		public AlreadyRunningException(string message) : base(message) {}
		public AlreadyRunningException(string message, Exception inner) : base(message, inner) {}
	}

	class NotRunningException : Exception {
		public NotRunningException() : base("Interactor not running!") { }
		public NotRunningException(string message) : base(message) { }
		public NotRunningException(string message, Exception inner) : base(message, inner) { }
	}
}
