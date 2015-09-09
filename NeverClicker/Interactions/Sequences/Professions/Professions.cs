using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static int[] ProfessionTaskDurationMinutes = { 720, 960, 1440 };
		public static string[] ProfessionTaskNames = { "Protect Magical", "Destroy Enemy", "Battle Elemental" };

		public static CompletionStatus MaintainProfs (Interactor intr, string charZeroIdxLabel) {			
			intr.Wait(1000);
			ClearOkButtons(intr);
			intr.Wait(200);	
			MoveAround(intr);

			string profsWinKey = intr.GameAccount.GetSetting("NwProfessionsWindowKey", "GameHotkeys");

			Keyboard.SendKey(intr, profsWinKey);
			intr.Wait(1000);
			
			if (!Screen.ImageSearch(intr, "ProfessionsWindowTitle").Found) {
				MoveAround(intr);
				Keyboard.SendKey(intr, profsWinKey);
				intr.Wait(200);

				if (!Screen.ImageSearch(intr, "ProfessionsWindowTitle").Found) {
					intr.Log("Unable to open professions window", LogEntryType.Error);
					return CompletionStatus.Failed ;
				}
			}


			//ProfessionsLeadershipTileUnselected



			Mouse.ClickImage(intr, "ProfessionsOverviewInactiveTile");
			intr.Wait(200);

			for (int i = 0; i < 9; i++) {
				intr.Wait(500);

				if (Mouse.ClickImage(intr, "ProfessionsCollectResult")) {
					intr.Wait(1000);
					Mouse.ClickImage(intr, "ProfessionsTakeRewardsButton");
					intr.Wait(2000);
				} else {
					break;
				}				
			}

			int currentPriority = 0;
			var success = false;

			for (int i = 0; i < 9; i++) {

				success = false;
				
				Mouse.ClickImage(intr, "ProfessionsOverviewInactiveTile");

				intr.Wait(400);				

				var result = Screen.ImageSearch(intr, "ProfessionsEmptySlot");

				if (result.Found) {
					intr.Log("Empty professions slot found at: " + result.Point.ToString() + ".", LogEntryType.Debug);
				} else {
					intr.Log("Empty professions slot not found.", LogEntryType.Debug);
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
					switch (currentPriority) {
						case 0:
							if (!SelectProfTask(intr, ProfessionTaskNames[currentPriority])) {
								currentPriority += 1;
								goto case 1;
							} else {
								success = true;
								break;
							}
						case 1:
							if (!SelectProfTask(intr, ProfessionTaskNames[currentPriority])) {
								currentPriority += 1;
								goto case 2;
							} else {
								success = true;
								break;
							}
						case 2:
							if (!SelectProfTask(intr, ProfessionTaskNames[currentPriority])) {
								intr.Log("Could not find valid professions task.", LogEntryType.Normal);
								Mouse.ClickImage(intr, "ProfessionsWindowTitle");
							} else {
								success = true;
							}
							break;
					}
				}

				if (success) {
					intr.GameAccount.SaveSetting(DateTime.Now.ToString(), "MostRecentProfTime_" + currentPriority, charZeroIdxLabel);
				}

				//intr.GameAccount.SaveSetting(DateTime.Now.ToString(), "MostRecentProfTime", charZeroIdxLabel);

				//if (!SelectProfTask(intr, "Protect Magical")) {
				//	if (!SelectProfTask(intr, "Destroy Enemy")) {
				//		if (!SelectProfTask(intr, "Battle Elemental")) {
				//			intr.Log("Could not find valid professions task.", LogEntryType.Normal);
				//			break;
				//		}
				//	}
				//}
			}
			
			intr.Wait(1000);

			//Keyboard.SendKey(intr, profsWinKey);

			//Keyboard.SendKey(intr, "Escape");

			// DETECT COMPLETE SLOT
			// COLLECT REWARDS
			// REPEAT

			// DETECT EMPTY SLOT
			// SEARCH FOR HIGHEST PRIORITY TASK
			// IF FOUND -> QUEUE
			// IF NOT, SEARCH FOR NEXT HIGHEST PRIORITY

			// STORE A LIST OF WHAT WAS QUEUED


			return CompletionStatus.Complete; // ***** TEMP
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

			Mouse.ClickImage(intr, "ProfessionsMercenaryIcon");
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