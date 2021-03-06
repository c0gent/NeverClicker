﻿using NeverClicker.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NeverClicker {
	public partial class AutomationEngine {
		
		public void Timer() {
			Timer aTimer = new Timer();
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			aTimer.Interval = 5000;
			aTimer.Enabled = true;

			//Console.WriteLine("Press \'q\' to quit the sample.");
			//while (Console.Read() != 'q') ;
		}

		// Specify what you want to happen when the Elapsed event is raised.
		private void OnTimedEvent(object source, ElapsedEventArgs e) {
			LogProgress("Hello World!");
		}

		public async void SendKeys(string keys) {
			await Run(() => Keyboard.SendTest(Itr, keys));
		}
		

		// DISABLED UNTIL QUEUE POSITION IS SORTED:
		//public void AddGameTask(uint charIdx, int delaySec) {
		//	var dateTime = DateTime.Now.AddSeconds(delaySec);
		//	var taskKind = TaskKind.Invocation;

		//	GameTask gameTask = new GameTask(
		//		dateTime, charIdx, taskKind, 0
		//	);

		//	try {
		//		LogProgress(string.Format("Adding task with charIdx: {0}, dateTime: {1}, taskKind: {2}", charIdx, dateTime, taskKind));
		//		Queue.Add(gameTask);
		//	} catch (Exception exc) {
		//		LogProgress(exc.ToString());
		//	}
		//}


		public void ProcessNextGameTask() {
			//throw new NotImplementedException();

			LogProgress("Manually removing tasks has been disabled.");

			//GameTask nextTask;
		
			//if (!Queue.IsEmpty) {
			//	nextTask = Queue.NextTask;
			//	LogProgress("Processing next task for character " + nextTask.CharIdx.ToString()
			//		+ "; time: " + nextTask.MatureTime.ToShortTimeString()
			//		+ "; type: " + nextTask.Kind.ToString() + ".");
			//} else {
			//	LogProgress("Task queue is empty.");
			//}

			//EXECUTE TASK
			// SET TIMER FOR NEXT TASK
		}


		//
		//	INTERACTOR
		//


		public async void MouseMovementTest() {
			LogProgress("Mouse movement activated.");
			await Run(() => Sequences.MouseMoveTest(Itr));
			LogProgress("Mouse movement cancelled.");
		}


		public string GetVar(string variable) {
			return Itr.GetVar(variable);
		}


		public async void EvaluateFunction(string functionName, string param1 = null,
						string param2 = null, string param3 = null, string param4 = null
		) {
			var result = await Run(() => Itr.EvaluateFunction(functionName, param1, param2, param3, param4));
			LogProgress(string.Format("'{0}()' returns: '{1}'", functionName, result));
		}


		public async void ExecuteStatement(string statement) {
			LogProgress(string.Format("Evaluating '{0}'()...", statement));
			await Run(() => Itr.ExecuteStatement(statement));
			LogProgress(string.Format("'{0}' complete.", statement));
		}


		public async Task<bool> DetectWindow(string windowExe) {
			LogProgress(string.Format("Detecting: '{0}'...", windowExe));
			var result = await Run(() => Screen.WindowDetectExist(Itr, windowExe));
			return result;
		}

		public async void WindowMinimize(string windowExe) {
			LogProgress(string.Format("Minimizing: '{0}'...", windowExe));
			await Run(() => Screen.WindowMinimize(Itr, windowExe));
		}

		public async void WindowActivate(string windowExe) {
			LogProgress(string.Format("Activating: '{0}'...", windowExe));
			await Run(() => Screen.WindowActivate(Itr, windowExe));
		}

		public async void WindowKill(string windowExe) {
			LogProgress(string.Format("Activating: '{0}'...", windowExe));
			await Run(() => Screen.WindowKill(Itr, windowExe));
		}

		public async void ImageSearch(string imgCode) {
			ImageSearchResult searchResult = await Run(() => Screen.ImageSearch(Itr, imgCode));

			if (searchResult.Found) {
				LogProgress("Image found at: " + searchResult.Point.ToString());
			} else {
				LogProgress("Image not found.");
			}
		}

		public async void ImageClick(string imgCode) {
			var searchResult = await Run(() => Mouse.ClickImage(Itr, imgCode));

			if (searchResult) {
				LogProgress("Image clicked.");
			} else {
				LogProgress("Image not found.");
			}
		}

		public async void GotoCharSlot(uint charIdx) {
			await Run(() => {
				Sequences.ActivateClient(Itr);
				Sequences.SelectCharacter(Itr, charIdx, false);
			});
		}
	}
}
