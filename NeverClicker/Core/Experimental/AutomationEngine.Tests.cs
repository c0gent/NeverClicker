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
			Log("Hello World!");
		}

		public async void SendKeys(string keys) {
			await Run(() => Keyboard.SendTest(Itr, keys));
		}
		

		public void AddGameTask(uint charIdx, int delaySec) {
			var dateTime = DateTime.Now.AddSeconds(delaySec);
			var taskKind = TaskKind.Invocation;

			GameTask gameTask = new GameTask(
				dateTime, charIdx, taskKind, 0
			);

			try {
				Log(string.Format("Adding task with charIdx: {0}, dateTime: {1}, taskKind: {2}", charIdx, dateTime, taskKind));
				Queue.Add(gameTask);
			} catch (Exception exc) {
				Log(exc.ToString());
			}
		}


		public void ProcessNextGameTask() {
			//throw new NotImplementedException();

			Log("Manually removing tasks has been disabled.");

			//GameTask nextTask;
		
			//if (!Queue.IsEmpty) {
			//	nextTask = Queue.NextTask;
			//	Log("Processing next task for character " + nextTask.CharIdx.ToString()
			//		+ "; time: " + nextTask.MatureTime.ToShortTimeString()
			//		+ "; type: " + nextTask.Kind.ToString() + ".");
			//} else {
			//	Log("Task queue is empty.");
			//}

			//EXECUTE TASK
			// SET TIMER FOR NEXT TASK
		}


		//
		//	INTERACTOR
		//


		public async void MouseMovementTest() {
			Log("Mouse movement activated.");
			await Run(() => Sequences.MouseMoveTest(Itr));
			Log("Mouse movement cancelled.");
		}


		public string GetVar(string variable) {
			return Itr.GetVar(variable);
		}


		public async void EvaluateFunction(string functionName, string param1 = null,
						string param2 = null, string param3 = null, string param4 = null
		) {
			var result = await Run(() => Itr.EvaluateFunction(functionName, param1, param2, param3, param4));
			Log(string.Format("'{0}()' returns: '{1}'", functionName, result));
		}


		public async void ExecuteStatement(string statement) {
			Log(string.Format("Evaluating '{0}'()...", statement));
			await Run(() => Itr.ExecuteStatement(statement));
			Log(string.Format("'{0}' complete.", statement));
		}


		public async Task<bool> DetectWindow(string windowExe) {
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
			ImageSearchResult searchResult = await Run(() => Screen.ImageSearch(Itr, imgCode));

			if (searchResult.Found) {
				Log("Image found at: " + searchResult.Point.ToString());
			} else {
				Log("Image not found.");
			}
		}

		public async void ImageClick(string imgCode) {
			var searchResult = await Run(() => Mouse.ClickImage(Itr, imgCode));

			if (searchResult) {
				Log("Image clicked.");
			} else {
				Log("Image not found.");
			}
		}
	}
}