﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {	

		public static CompletionStatus MaintainProfs (Interactor intr, string charZeroIdxLabel, List<int> completionList) {			
			string profsWinKey = intr.GameAccount.GetSettingOrEmpty("NwProfessionsWindowKey", "GameHotkeys");

			Keyboard.SendKey(intr, profsWinKey);
			intr.Wait(1000);
			
			if (!Screen.ImageSearch(intr, "ProfessionsWindowTitle").Found) {
				MoveAround(intr);
				Keyboard.SendKey(intr, profsWinKey);
				intr.Wait(200);

				if (!Screen.ImageSearch(intr, "ProfessionsWindowTitle").Found) {
					intr.Log("Unable to open professions window", LogEntryType.FatalWithScreenshot);
					return CompletionStatus.Failed ;
				}
			}

			if (Mouse.ClickImage(intr, "ProfessionsOverviewInactiveTile")) {
				intr.Wait(1000);
			}

			for (int i = 0; i < 9; i++) {
				if (!CollectCompleted(intr)) {
					break;
				}
			}

			int currentTask = 0;
			var success = false;
			var anySuccess = false;

			for (int i = 0; i < 9; i++) {
				if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; };

				success = false;
				
				if (Mouse.ClickImage(intr, "ProfessionsOverviewInactiveTile")) {
					intr.Wait(400);
				}

				var result = Screen.ImageSearch(intr, "ProfessionsEmptySlot");

				if (result.Found) {
					intr.Log("Empty professions slot found at: " + result.Point.ToString() + ".", LogEntryType.Info);
				} else {
					intr.Log("Empty professions slot not found.", LogEntryType.Info);
					break;
				}

				Mouse.Click(intr, result.Point, 30, 90);
				intr.Wait(200);

				Mouse.ClickImage(intr, "ProfessionsLeadershipTileUnselected");
				intr.Wait(200);
				
				var taskContinueResult = Screen.ImageSearch(intr, "ProfessionsTaskContinueButton");

				if (i > 0 && taskContinueResult.Found) {
					ContinueTask(intr, taskContinueResult.Point);
					success = true;
				} else {
					while(true) {
						if (currentTask < TaskQueue.ProfessionTaskNames.Length) {
							if (SelectProfTask(intr, TaskQueue.ProfessionTaskNames[currentTask])) {
								success = true;
								break;								
							} else {
								currentTask += 1;
								continue;
							}
						} else {
							intr.Log("Could not find valid professions task.", LogEntryType.Normal);
							CollectCompleted(intr);
							Mouse.ClickImage(intr, "ProfessionsWindowTitle");
							break;
						}
					}
				}

				if (success && currentTask < TaskQueue.ProfessionTaskNames.Length) {
					completionList.Add(currentTask);
					anySuccess = true;
				} 
			}

			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }

			if (anySuccess) {
				return CompletionStatus.Complete;
			} else {
				return CompletionStatus.Immature;
			}
		}


		private static bool CollectCompleted(Interactor intr) {
			if (Mouse.ClickImage(intr, "ProfessionsCollectResult")) {
				intr.Wait(2500);
				Mouse.ClickImage(intr, "ProfessionsTakeRewardsButton");
				intr.Wait(2000);
				return true;
			} else {
				return false;
			}
		}


		private static bool SelectProfTask(Interactor intr, string taskName) {
			var searchButton = Screen.ImageSearch(intr, "ProfessionsSearchButton");
			intr.Wait(50);

			Mouse.Click(intr, searchButton.Point, -100, 0);
			intr.Wait(50);
				
			Keyboard.SendKeyWithMod(intr, "Shift", "Home");
			intr.Wait(50);

			Keyboard.Send(intr, taskName);
			intr.Wait(50);

			Keyboard.SendKey(intr, "Enter");
			intr.Wait(100);

			var taskContinueResult = Screen.ImageSearch(intr, "ProfessionsTaskContinueButton");

			if (taskContinueResult.Found) {
				ContinueTask(intr, taskContinueResult.Point);
				return true;
			} else {
				return false;
			}
		}

		private static void ContinueTask(Interactor intr, Point continueButton) {
			Mouse.Click(intr, continueButton);
			intr.Wait(100);

			Mouse.ClickImage(intr, "ProfessionsAssetButton");
			intr.Wait(50);

			// <<<<< TODO: ADD DETECTION FOR OTHER SECONDARY ASSETS >>>>>
			if (!Mouse.ClickImage(intr, "ProfessionsMercenaryIcon")) {
				if (!Mouse.ClickImage(intr, "ProfessionsManAtArmsIcon")) {
					Mouse.ClickImage(intr, "ProfessionsGuardIcon");
				}
			}

			intr.Wait(50);

			Mouse.ClickImage(intr, "ProfessionsStartTaskButton");
			intr.Wait(200);

			//Mouse.ClickImage(intr, "ProfessionsWindowTitle");
		}
	}
}



				// SEARCH BUTTON: ProfessionsSearchButton
				// ProfessionsTaskContinueButton

				//// SCROLL DOWN
				//Mouse.ClickRepeat(intr, 1387, 860, 6);
				//intr.Wait(1000);
				//Mouse.WheelUp(intr, 6);
				//intr.Wait(500);

				//// SCROLL UP A BIT
				//Mouse.Move(intr, 1200, 800);
				//intr.Wait(500);
				//Mouse.WheelUp(intr, 6);
				//intr.Wait(500);

				//// GET MOUSE OUT OF THE WAY
				//Mouse.Move(intr, 1400, 900);
				//intr.Wait(1000);								

				//// SCAN FOR VALID TASKS
				//var protectMarketResult = Screen.ImageSearch(intr, "ProfessionsProtectMarketRewardIcon");
				//// PRIORITY 1
				//if (protectMarketResult.Found) {
				//	Mouse.Click(intr, protectMarketResult.Point, 228, 25);				
				//} else {
				//	var destroyCampResult = Screen.ImageSearch(intr, "ProfessionsDestroyCampRewardIcon");
				//	// PRIORITY 2					
				//	if (destroyCampResult.Found) {
				//		Mouse.Click(intr, destroyCampResult.Point, 228, 25);					
				//	} else { // PRIORITY 3
				//		// SCROLL BACK DOWN TO BOTTOM
				//		Mouse.Move(intr, 1200, 800);
				//		intr.Wait(100);
				//		Mouse.WheelDown(intr, 6);
				//		intr.Wait(100);
				//		// CLICK WHERE BATTLE ELEMENTAL CULTISTS 'CONTINUE' BUTTON SHOULD BE
				//		Mouse.Click(intr, 1330, 847);
				//	}
				//} 