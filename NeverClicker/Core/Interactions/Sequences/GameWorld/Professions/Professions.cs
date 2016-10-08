﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		private static bool CollectCompleted(Interactor intr) {
			var resultAvailable = Screen.ImageSearch(intr, "ProfessionsCollectResult");

			//if (Mouse.ClickImage(intr, "ProfessionsCollectResult")) {
			if (resultAvailable.Found) {
				Mouse.Click(intr, resultAvailable.Point);
				intr.Wait(500);
				Mouse.ClickImage(intr, "ProfessionsTakeRewardsButton");
				intr.Wait(1500);
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
				
			Keyboard.SendKey(intr, "Shift", "Home");
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

		private static string[] LeadershipIconsSmall = {
			"ProfessionsLeadershipMercenaryIcon", 
			"ProfessionsLeadershipGuardIcon",
			"ProfessionsLeadershipFootmanIcon",
			"ProfessionsLeadershipManAtArmsIcon", 
			"ProfessionsLeadershipAdventurerIcon", 
			"ProfessionsLeadershipHeroIcon"};

		private static string[] LeadershipTilesLarge = {
			"ProfessionsLeadershipMercenaryTileLarge", 
			"ProfessionsLeadershipGuardTileLarge",
			"ProfessionsLeadershipFootmanTileLarge",
			"ProfessionsLeadershipManAtArmsTileLarge", 
			"ProfessionsLeadershipAdventurerTileLarge", 
			"ProfessionsLeadershipHeroTileLarge"};

		// Actually queues a profession task:
		private static void ContinueTask(Interactor intr, Point continueButton) {
			Mouse.Click(intr, continueButton);
			intr.Wait(100);

			Mouse.ClickImage(intr, "ProfessionsAssetButton");
			intr.Wait(50);

			//// <<<<< TODO: ADD DETECTION FOR OTHER SECONDARY ASSETS >>>>>
			//if (!Mouse.ClickImage(intr, "ProfessionsMercenaryIcon")) {
			//	if (!Mouse.ClickImage(intr, "ProfessionsManAtArmsIcon")) {
			//		if (!Mouse.ClickImage(intr, "ProfessionsGuardIcon")) {
			//			if (!Mouse.ClickImage(intr, "ProfessionsLeadershipAdventurerIcon")) {
			//				if (!Mouse.ClickImage(intr, "ProfessionsLeadershipHeroIcon")) {
			//					intr.Log(LogEntryType.Debug, "No optional professions assets found.");
			//				}
			//			}
			//		}
			//	}
			//}		

			// Attempt to add optional assets:
			string assetLabel = null;

			foreach (var iconLabel in LeadershipIconsSmall) {
				if (Mouse.ClickImage(intr, iconLabel)) {
					assetLabel = iconLabel;
					break;
				}
			}

			if (string.IsNullOrWhiteSpace(assetLabel)) {
				intr.Log(LogEntryType.Debug, "No optional professions assets found.");
			} else {
				intr.Log(LogEntryType.Debug, "Using optional profession asset with label: {0}", assetLabel);
			}			

			intr.Wait(50);

			Mouse.ClickImage(intr, "ProfessionsStartTaskButton");
			intr.Wait(500);

			//Mouse.ClickImage(intr, "ProfessionsWindowTitle");
		}

		public static CompletionStatus MaintainProfs (Interactor intr, string charZeroIdxLabel, List<int> completionList) {
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }	

			string profsWinKey = intr.AccountSettings.GetSettingValOr("Professions", "GameHotkeys", Global.Default.ProfessionsWindowKey);

			intr.Log(LogEntryType.Debug, "Opening professions window for character " + charZeroIdxLabel + ".");

			Keyboard.SendKey(intr, profsWinKey);
			intr.Wait(1000);
			
			if (!Screen.ImageSearch(intr, "ProfessionsWindowTitle").Found) {
				MoveAround(intr);
				Keyboard.SendKey(intr, profsWinKey);
				intr.Wait(200);

				if (!Screen.ImageSearch(intr, "ProfessionsWindowTitle").Found) {
					intr.Log(LogEntryType.FatalWithScreenshot, "Unable to open professions window");
					return CompletionStatus.Failed;
				}
			}

			if (Mouse.ClickImage(intr, "ProfessionsOverviewInactiveTile")) {
				intr.Wait(500);
			}

			int profResultsCollected = 0;

			if (Screen.ImageSearch(intr, "ProfessionsCollectResult").Found) {
				while (profResultsCollected < 9) {
					if (!CollectCompleted(intr)) {
						break;
					} else {
						profResultsCollected += 1;
					}
				}

				intr.Log(LogEntryType.Debug, "Collected " + profResultsCollected + " profession results for character + " 
					+ charZeroIdxLabel + ".");
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

				var EmptySlotResult = Screen.ImageSearch(intr, "ProfessionsEmptySlot");

				if (EmptySlotResult.Found) {
					intr.Log(LogEntryType.Info, "Empty professions slot found at: " + EmptySlotResult.Point.ToString() + ".");
				} else {
					intr.Log(LogEntryType.Info, "All professions slots busy.");
					break;
				}

				// Click the "Choose Task" button below the empty slot:
				Mouse.Click(intr, EmptySlotResult.Point, 30, 90);
				intr.Wait(400);

				// Click the "Leadership" category tile if it is not selected (determined by color):
				Mouse.ClickImage(intr, "ProfessionsLeadershipTileUnselected");
				intr.Wait(200);
				
				var taskContinueResult = Screen.ImageSearch(intr, "ProfessionsTaskContinueButton");

				if (i > 0 && taskContinueResult.Found) {
					ContinueTask(intr, taskContinueResult.Point);
					success = true;
				} else {
					while(true) {
						if (currentTask < ProfessionTasksRef.ProfessionTaskNames.Length) {
							if (SelectProfTask(intr, ProfessionTasksRef.ProfessionTaskNames[currentTask])) {
								success = true;
								break;								
							} else {
								currentTask += 1;
								continue;
							}
						} else {
							intr.Log(LogEntryType.Normal, "Could not find valid professions task.");
							CollectCompleted(intr);
							Mouse.ClickImage(intr, "ProfessionsWindowTitle");
							break;
						}
					}
				}

				if (success && currentTask < ProfessionTasksRef.ProfessionTaskNames.Length) {
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