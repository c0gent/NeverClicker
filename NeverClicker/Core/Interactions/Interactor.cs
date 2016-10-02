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
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Immutable;
using NeverClicker.Core;

namespace NeverClicker.Interactions {
	// INTERACTOR: MANAGES ALIBENGINE
	public class Interactor {
		private AlibEngine AlibEng;
		//string NwCommonFileName;
		static private uint MaxFileLoadAttempts = 5;
		public Random Rng = new Random();
		public AutomationState State { get; private set; } = AutomationState.Stopped;
		public IProgress<LogMessage> ProgressLog { get; private set; }
		public IProgress<LogMessage> ErrorLog { get; private set; }
		public IProgress<ImmutableSortedDictionary<long, GameTask>> QueueList { get; private set; }
		public CancellationTokenSource CancelSource { get; private set; }
		//public IniFile GameAccount; // = new IniFile(Settings.Default["GameAccountIniPath"].ToString());
		//public IniFile GameClient; // = new IniFile(Settings.Default["GameClientIniPath"].ToString());
		public AccountSettings AccountSettings;
		public ClientSettings ClientSettings;
		public AccountStates AccountStates;
		LogFile LogFile;

		public Interactor() {
			//InitAlibEng(); // LOADED BELOW (LoadSettings())
			LogFile = new LogFile();

			LoadSettings();			
		}

		public bool LoadSettings() {
			////this.GameAccount = new IniFile(Settings.Default.SettingsFolderPath + SettingsForm.GAME_ACCOUNT_INI_FILE_NAME);
			//this.GameClient = new IniFile(Settings.Default.SettingsFolderPath + SettingsForm.GAME_CLIENT_INI_FILE_NAME);	
			this.AccountSettings = new AccountSettings();
			this.ClientSettings = new ClientSettings();
			this.AccountStates = new AccountStates();

			BackwardCompatability();

			InitAlibEng();
			return true;
		}

		// Migrate any settings which may have been moved
		private void BackwardCompatability() {
			// Move `CharacterSelectScrollBarTopX` & `CharacterSelectScrollBarTopY` from `KeyBindAndUi` to `ClickLocations`:
			int characterSelectScrollBarTopX;

			if (!ClientSettings.TryGetSetting("CharacterSelectScrollBarTopX", "ClickLocations", out characterSelectScrollBarTopX)) {
				if (ClientSettings.TryGetSetting("CharacterSelectScrollBarTopX", "KeyBindAndUi", out characterSelectScrollBarTopX)) {
					ClientSettings.SaveSetting(characterSelectScrollBarTopX.ToString(), "CharacterSelectScrollBarTopX", "ClickLocations");
				} else {
					Log("Unable to load 'CharacterSelectScrollBarTopX' setting.", LogEntryType.Fatal);
				}				
			}

			int characterSelectScrollBarTopY;

			if (!ClientSettings.TryGetSetting("CharacterSelectScrollBarTopY", "ClickLocations", out characterSelectScrollBarTopY)) {
				if (ClientSettings.TryGetSetting("CharacterSelectScrollBarTopY", "KeyBindAndUi", out characterSelectScrollBarTopY)) {
					ClientSettings.SaveSetting(characterSelectScrollBarTopY.ToString(), "CharacterSelectScrollBarTopY", "ClickLocations");
				} else {
					Log("Unable to load 'CharacterSelectScrollBarTopY' setting.", LogEntryType.Fatal);
				}
			}
		}


		private void InitAlibEng() {
			AlibEng = new AlibEngine();

			AlibEng.Exec("SendMode Input");
			AlibEng.Exec("CoordMode, Mouse, Screen");
			AlibEng.Exec("CoordMode, Pixel, Screen");
			AlibEng.Exec("SetMouseDelay, 55");
			AlibEng.Exec("SetKeyDelay, 55, 15");
		}

		// UpdateQueueList(): MAKE THIS ASYNC
		public void UpdateQueueList(ImmutableSortedDictionary<long, GameTask> taskListCopy) {
			QueueList.Report(taskListCopy);
		}

		//public void Log(string message) {
		//	Log(message, LogEntryType.Normal);
		//}

		public void Log(string message, LogEntryType lt) {
			//if (message != null) {
			//	ProgressLog.Report(new LogMessage(message, lt));
			//} else {
			//	ProgressLog.Report(new LogMessage("Interactor::Log(): null log message passed!", LogEntryType.Warning));
			//}
			Log(new LogMessage(message, lt));
		}

		//public void Log(LogMessage logMessage) {
		//	ProgressLog.Report(logMessage);
		//}

		public void Log(string message) {
			Log(new LogMessage(message));
		}
		
		public void Log(LogMessage logMessage) {			
			switch (logMessage.Type) {
				case LogEntryType.FatalWithScreenshot:					
					LogFile.AppendMessage(logMessage);
					//MainForm.WriteLine(logMessage.Text);
					ProgressLog?.Report(logMessage);
					//MessageBox.Show(MainForm, logMessage.Text + " -- " 
					//	+ SaveErrorScreenshot(), "NeverClicker - " + logMessage.Text);
					//MainForm.AppendError(logMessage.Text + " -- " + SaveErrorScreenshot());
					ErrorLog?.Report(new LogMessage(logMessage.Text + " -- " + SaveErrorScreenshot()));
					break;
				case LogEntryType.Fatal:					
					LogFile.AppendMessage(logMessage);
					ProgressLog?.Report(logMessage);
					//MessageBox.Show(MainForm, logMessage.Text, "NeverClicker - " + logMessage.Text);
					ErrorLog?.Report(logMessage);
					break;
				case LogEntryType.Error:
					LogFile.AppendMessage(logMessage);
					ErrorLog?.Report(logMessage);
					break;
				case LogEntryType.Warning:
				case LogEntryType.Normal:
					LogFile.AppendMessage(logMessage);
					ProgressLog?.Report(logMessage);
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
			var errMsg = "Image file: " + errorImageFileName;
			ScreenCapture sc = new ScreenCapture();
			Image img = sc.CaptureScreen();
			img.Save(errorImageFileName, ImageFormat.Png);
			return errMsg;
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

		// CONVERT TO ASYNC
		private bool LoadScript(string fileName) {
			if (!File.Exists(fileName)) {
				Log(fileName + " does not exist.", LogEntryType.Fatal);
				return false;
			} 

			VerifyRunning();
			for (uint i = 0; i < MaxFileLoadAttempts; i++) {
				try {
					//log.Report(String.Format("Attempting to load '{0}'.", NwCommonFileName));
					AlibEng.AddFile(fileName);
				} catch (Exception e) {
					Log(string.Format("Problem loading: '{0}': {1}", fileName, e), LogEntryType.Error);
					continue;
				}

				Log(string.Format("'{0}' loaded.", fileName), LogEntryType.Debug);
				break;
			}

			return true;
		}

		public CancellationToken Start(IProgress<LogMessage> progressLog, IProgress<LogMessage> errorLog,
					IProgress<ImmutableSortedDictionary<long, GameTask>> queueList) {
		//public CancellationToken Start(IProgress<LogMessage> log) {
			if (State == AutomationState.Stopped) {
				ProgressLog = progressLog;
				ErrorLog = errorLog;
				QueueList = queueList;
				CancelSource = new CancellationTokenSource();
				State = AutomationState.Running;
			} else {
				Log("Interactor::Start(): Automation already running", LogEntryType.Fatal);
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

		public void Reload() {
			VerifyStopped();
			Task.Delay(2000).Wait();
			AlibEng.Terminate();

			State = AutomationState.Running;
			InitAlibEng();
			State = AutomationState.Stopped;
		}

		/// <summary>
		/// Waits until a particular state is reached.
		/// </summary>
		/// <typeparam name="TState">Type of objective state</typeparam>
		/// <param name="maxWaitSeconds">Maximum amount of time to wait before returning</param>
		/// <param name="endState">Objective state</param>
		/// <param name="isState">Function to call to query the current state</param>
		/// <param name="doFailure">Function to call in the event of a failure (this can loop back recursively)</param>
		/// <param name="attemptCount">Number of recursions to attempt (simply passed through, must be incremented by 'doFailure')</param>
		/// <returns>Signal determining success or failure</returns>
		public bool WaitUntil<TState>(int maxWaitSeconds, TState endState, Func<Interactor, TState, bool> isState,  
						Func<Interactor, TState, int, bool> doFailure, int attemptCount) where TState : struct 
		{
			const int secondsPerIter = 1;
			return WaitUntil(maxWaitSeconds, secondsPerIter, endState, isState, doFailure, attemptCount);
		}

        public bool WaitUntil<TState>(int maxWaitSeconds, int secondsPerIter, TState endState, Func<Interactor, TState, bool> isState,  
						Func<Interactor, TState, int, bool> doFailure, int attemptCount) where TState : struct 
		{
			//const int secondsPerIter = 1;
			int maxIters = maxWaitSeconds / secondsPerIter;
			int iters = 0;

			this.Log("Waiting for " + endState.ToString() + " a maximum of " + maxWaitSeconds.ToString("F0") + " seconds.", LogEntryType.Debug);

			while (!(isState(this, endState))) {
				if (CancelSource.IsCancellationRequested) { return false; }
				this.Log("Waiting until state: " + endState.ToString() + ".", LogEntryType.Debug);
				this.Wait(1000 * secondsPerIter);
				iters += 1;
				if (iters >= maxIters) {					
					LogWaitStatus(this, endState, false);
					if (doFailure != null) {
						return doFailure(this, endState, attemptCount);
					} else {
						return false;
					}
				}				
			}

			LogWaitStatus(this, endState, true);
			return true;
		}

		public int Rand(int min, int max) {
			return Rng.Next(min, max);
		}

		public TimeSpan AddRandomDelay(TimeSpan original) {
			var min = (int)original.TotalSeconds;
			// MAX_DELAY = ORIG + (IF ORIG < ONE HOUR --> ORIG / 2) ELSE IF (ORIG IS >= ONE HOUR --> ONE HOUR)
			var max = min + ((min < 3600) ? (min / 2) : 3600);
			int newDelay = Rng.Next(min, max);
			return new TimeSpan(0, 0, 0, newDelay);
		}

		public TimeSpan RandomDelay(int minutesMin, int minutesMax) {
			int rnd = Rng.Next(minutesMin, minutesMax);
			return new TimeSpan(0, rnd, 1);
		}

		public int WaitRand(int minMs, int maxMs) {
			//uint delayRange = (maxMs > minMs) ? (maxMs - minMs) : (minMs - maxMs);
			var waitTime = Rng.Next(minMs, maxMs);
			this.Wait(waitTime);
			return waitTime;
		}

		public void Wait(TimeSpan timeSpanDelay) {			
			this.Wait((int)timeSpanDelay.TotalMilliseconds);
		}

		public void Wait(int millisecondsDelay) {
			VerifyRunning();
			Log("Waiting for " + millisecondsDelay.ToString("F0") + "ms.", LogEntryType.Debug);
			try {
				Task.Delay(millisecondsDelay, CancelSource.Token).Wait();
			} catch (AggregateException ae) {
				ae.Handle((x) => {	
					if (x is TaskCanceledException) {						
						return true;
					} else {
						Log("Interactor::Wait(): Error: " + x.ToString(), LogEntryType.Fatal);
						//MessageBox.Show(x.ToString());
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

	
		/////////	CORE INTERACTION PRIMITIVES:   //////////

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
			return AlibInterface(new Func<string>(() => {
				return AlibEng.ExecFunction(functionName, args);
			}));

			////System.Windows.Forms.MessageBox.Show("test0");
			//VerifyRunning();
			////System.Windows.Forms.MessageBox.Show("test1");
			//if (CancelSource.Token.IsCancellationRequested) { return ""; }

			//try {
			//	var result = AlibEng.ExecFunction(functionName, args);
			//	return result;
			//} catch (Exception ex) {
			//	Log(ex.ToString());
			//	System.Windows.Forms.MessageBox.Show(ex.ToString());
			//	//return null;
			//	throw ex;
			//}
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
			if (CancelSource.Token.IsCancellationRequested) { return ""; }

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


		public static void LogWaitStatus<TState>(Interactor intr, TState end, bool success) {
			if (success) {
				intr.Log("WaitUntil(): Found client state: "
					+ " -> " + end.ToString() + ".", LogEntryType.Debug);
			} else {
				intr.Log("WaitUntil(): Failure to find client state: "
					+ " -> " + end.ToString() + ". Re-evaluating...", LogEntryType.Debug);
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



// SHOULD BE ASYNC BUT DEPRICATING EVENTUALLY ANYWAY
		//public bool InitOldScript_DEPRICATING() {
		//	VerifyRunning();
		//	string scriptRoot = Settings.Default.UserRootFolderPath + "\\Assets";

		//	if (Directory.Exists(Settings.Default.AssetsFolderPath)) {
		//		scriptRoot = Settings.Default.AssetsFolderPath;
		//	} else {
		//		Log(Settings.Default.AssetsFolderPath + " does not exist. Using: " + Settings.Default.UserRootFolderPath + "\\Assets");
		//	}

		//	string gameExeRoot = Settings.Default.NeverwinterExePath;
		//	string settingsFolder = Settings.Default.SettingsFolderPath;
  //          string imagesFolder = Settings.Default.ImagesFolderPath;
		//	string logsFolder = Settings.Default.LogsFolderPath;
  //          string scriptFileName = "\\NW_Common.ahk";

		//	if ((scriptRoot == "") || (gameExeRoot == "")) {
		//		Log(string.Format("Cannot load script file or paths: '{0}' & '{1}'.", scriptRoot, gameExeRoot), LogEntryType.Fatal);
		//		return false;
		//	}

		//	try {
		//		var nwCommonFileName = scriptRoot + scriptFileName;

		//		if (!File.Exists(nwCommonFileName)) {
		//			Log("Error: " + nwCommonFileName + " does not exist.", LogEntryType.Fatal);
		//			return false;
		//		}

		//		if (!LoadScript(nwCommonFileName)) {
		//			return false;
		//		}

		//		AlibEng.Exec("SetWorkingDir %A_ScriptDir%");
		//		//AlibEng.Exec("A_CommonDir = " + scriptRoot);
		//		AlibEng.Exec("A_SettingsDir = " + settingsFolder);
		//		//Log("A_ImagesDir = " + imagesFolder);
  //              AlibEng.Exec("A_ImagesDir = " + imagesFolder);
		//		AlibEng.Exec("A_LogsDir = " + logsFolder);
		//		AlibEng.Exec("NwFolder := \"" + Path.GetDirectoryName(gameExeRoot) + "\"");
		//		//AlibEng.Exec("NwExe := " + gameExeRoot);

					

		//		this.AlibEng.Exec("gcs_ini := \"" + Settings.Default.SettingsFolderPath.ToString() + "\"");
		//		this.AlibEng.Exec("as_ini := \"" + Settings.Default.SettingsFolderPath.ToString() + "\"");
		//		this.AlibEng.Exec("ai_log := \"" + Settings.Default.LogsFolderPath + "\"");

		//		AlibEng.Exec("ToggleAfk := 0");
		//		AlibEng.Exec("ToggleMouseDragClick := 0");
		//		AlibEng.Exec("ToggleShit := 0");

		//		AlibEng.ExecFunction("Init");
		//	} catch (Exception ex) {
		//		Log(ex.ToString(), LogEntryType.Error);
		//		return false;
		//	}

		//	Log("Old script initialized.", LogEntryType.Debug);
		//	return true;
		//}