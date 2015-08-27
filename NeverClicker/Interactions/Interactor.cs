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
		public IProgress<string> ProgressLog { get; private set; }
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
		}

		public void Log(string message, params string[] args) {
			ProgressLog.Report(string.Format(message, args));
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

		public bool WindowDetectExists(string windowExe) {
			string detectionParam = String.Format("ahk_exe {0}", windowExe);
			var result = EvaluateFunction("WinExist", detectionParam);
			return result.Trim() != "0x0";
		}

		public bool WindowDetectActive(string windowExe) {
			string detectionParam = String.Format("ahk_exe {0}", windowExe);
			var result = EvaluateFunction("WinActive", detectionParam);
			return result.Trim() != "0x0";
		}


		// SHOULD BE ASYNC BUT DEPRICATING EVENTUALLY ANYWAY
		public void InitOldScript() {
			VerifyRunning();
			string scriptRoot = Settings.Default["SettingsRootPath"].ToString();
			string gameExeRoot = Settings.Default["NeverwinterExePath"].ToString();
			string imagesFolder = Settings.Default["ImagesFolderPath"].ToString();

			if ((scriptRoot == "") || (gameExeRoot == "")) {
				ProgressLog.Report(string.Format("Cannot load script file or paths: '{0}' & '{1}'.", scriptRoot, gameExeRoot));
				return;
			}

			try {
				NwCommonFileName = scriptRoot + "NW_Common.ahk";
				LoadFile(ProgressLog, NwCommonFileName);

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
		
		public void Run(IProgress<string> log) {			
			if (State == AutomationState.Stopped) {
				ProgressLog = log;
				CancelSource = new CancellationTokenSource();
				State = AutomationState.Running;
			} else {
				throw new AlreadyRunningException();
			}
			
		}

		public void Stop() {
			if (State != AutomationState.Stopped) {
				CancelSource.Cancel();
				State = AutomationState.Stopped;
				Reload();
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

		public void Wait(int millisecondsDelay) {
			VerifyRunning();
			try {
				Task.Delay(millisecondsDelay, CancelSource.Token).Wait();
			} catch (AggregateException ae) {
				ae.Handle((x) => {	
					if (x is TaskCanceledException) {						
						return true;
					} else {
						Log(x.ToString());
						return false; // Let anything else stop the application.
					}
				});
			}
		}

		public void VerifyStopped() {
			if (State != AutomationState.Stopped) { throw new AlreadyRunningException(); }
		}

		public void VerifyRunning() {
			if (State != AutomationState.Running && !CancelSource.IsCancellationRequested) { throw new NotRunningException(); }
		}


		// CONVERT TO ASYNC
		private void LoadFile(IProgress<string> log, string fileName) {
			VerifyRunning();
			for (uint i = 0; i < MaxFileLoadAttempts; i++) {
				try {
					//log.Report(String.Format("Attempting to load '{0}'.", NwCommonFileName));
					AlibEng.AddFile(NwCommonFileName);
				} catch (Exception e) {
					log.Report(String.Format("Problem loading: '{0}': {1}", NwCommonFileName, e));
					continue;
				}

				log.Report(String.Format("'{0}' loaded.", NwCommonFileName));
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

		public string EvaluateFunction(string functionName, params string[] args) {
			return AlibInterface(new Func<string>(() => {
				return AlibEng.ExecFunction(functionName, args);
			}));
		}

		public void ExecuteStatement(string statement) {
			AlibInterface(new Func<string>(() => {
				AlibEng.Exec(statement);
				return null;
			}));			
		}

		private string AlibInterface(Func<string> alibAction) {
			VerifyRunning();

			try {
				var result = alibAction();
				return result;
			} catch (Exception exc) {
				ProgressLog.Report(exc.ToString());
				return null;
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
