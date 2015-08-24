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

namespace NeverClicker.Interactions {
	// INTERACTOR: MANAGES ALIBENGINE
	class Interactor {
		private AlibEngine AlibEng;
		String NwCommonFileName;
		static private uint MaxFileLoadAttempts = 5;
		public AutomationState State = AutomationState.Stopped;

		public Interactor(MainForm mainForm) {
			InitAlibEng();
		}

		private void InitAlibEng() {
			AlibEng = new AlibEngine();
		}

		// SHOULD BE ASYNC BUT DEPRICATING EVENTUALLY ANYWAY
		public void InitOldAutoCyclerScript(IProgress<string> progress) {
			string scriptRoot = Settings.Default["ScriptRootPath"].ToString();
			string gameRoot = Settings.Default["NeverwinterExeLocation"].ToString();

			if ((scriptRoot == "") || (gameRoot == "")) {
				progress.Report(String.Format("Cannot load script file or paths: '{0}' & '{1}'.", scriptRoot, gameRoot));
				return;
			}

			NwCommonFileName = scriptRoot + "NW_Common.ahk";
			LoadFile(progress, NwCommonFileName);

			AlibEng.Exec("SetWorkingDir %A_ScriptDir%");
			AlibEng.Exec("A_CommonDir = " + scriptRoot);
			AlibEng.Exec("A_ImagesDir = " + scriptRoot + "StandardImages");
			AlibEng.Exec("NwFolder := \"" + gameRoot + "\"");

			AlibEng.Exec("gcs_ini := A_CommonDir . \"\\nw_game_client_settings.ini\"");
			AlibEng.Exec("as_ini := A_CommonDir . \"\\nw_account_settings.ini\"");
			AlibEng.Exec("ai_log := A_CommonDir . \"\\nw_autoinvoke.log\"");

			AlibEng.Exec("^!=::Suspend");

			AlibEng.Exec("ToggleAfk := 0");
			AlibEng.Exec("ToggleMouseDragClick := 0");
			AlibEng.Exec("ToggleShit := 0");

			AlibEng.Exec("SendMode Input");
			AlibEng.Exec("CoordMode, Mouse, Screen");
			AlibEng.Exec("CoordMode, Pixel, Screen");
			AlibEng.Exec("SetMouseDelay, 55");
			AlibEng.Exec("SetKeyDelay, 55, 15");

			AlibEng.ExecFunction("init");

			progress.Report("Old script loaded.");
		}

		// CONVERT TO ASYNC
		private void LoadFile(IProgress<string> progress, string fileName) {
			for (uint i = 0; i < MaxFileLoadAttempts; i++) {
				try {
					progress.Report(String.Format("Attempting to load '{0}'.", NwCommonFileName));
					AlibEng.AddFile(NwCommonFileName);
				} catch (Exception e) {
					progress.Report(String.Format("Problem loading: '{0}': {1}", NwCommonFileName, e));
					continue;
				}

				progress.Report(String.Format("'{0}' loaded.", NwCommonFileName));
				break;
			}
		}

		public AutomationState Pause() {
			if (State == AutomationState.Running) {
				State = AutomationState.Paused;
				AlibEng.Suspend();
			}

			return State;
		}

		public AutomationState Unpause() {
			if (State == AutomationState.Paused) {
				State = AutomationState.Running;
				AlibEng.UnSuspend();
			}

			return State;
		}

		public void Reload() {
			AlibEng.Suspend();
			//Task.Delay(100).Wait();
			AlibEng.Terminate();

			InitAlibEng();
		}

		public void MoveMouseCursor(Point point, bool click) {
			String execString = String.Format("SendEvent {{Click {0}, {1}, {2}}}",
				point.X.ToString(), point.Y.ToString(), Convert.ToInt32(click).ToString());

			AlibEng.Exec(execString);
		}

		public string GetVar(string variableName) {
			return AlibEng.GetVar(variableName);
		}

		public string EvaluateFunction(Interactor interactor, IProgress<string> progress, CancellationToken cancelToken, string functionName, params string[] args) {
			return AlibEng.ExecFunction(functionName, args);
		}

		public void EvaluateStatement(Interactor interactor, IProgress<string> progress, CancellationToken cancelToken, string statement) {
			AlibEng.Exec(statement);
		}
	}

}
