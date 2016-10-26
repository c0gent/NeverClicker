using NeverClicker.Globals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Globals {
	public enum Profession {
		Leadership,
		Jewelcrafting,
		Alchemy,
		Other,
	}

	public enum ProfessionAssetId {
		None,
		Mercenary,
		Guard,
		Footman,
		ManAtArms,
		Adventurer,
		Hero,		
	}

	public struct LeadershipAsset {
		public readonly ProfessionAssetId AssetId;
		public readonly string Label;
		public readonly float BonusFactor;

		public LeadershipAsset(ProfessionAssetId assetId, float bonusFactor) {
			AssetId = assetId;
			Label = assetId.ToString("G");
			BonusFactor = bonusFactor;
		}

		public string SmallIconImageLabel { get {
			return "ProfessionsLeadership" + Label + "Icon";
		} }

		public string LargeTileImageLabel { get {
			return "ProfessionsLeadership" + Label + "TileLarge";
		} }
	}

	public struct ProfessionTaskResult {
		public int TaskId;
		public float BonusFactor;

		public ProfessionTaskResult(int taskId, float bonusFactor) {
			TaskId = taskId;
			BonusFactor = bonusFactor;
		}
	}
}

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static LeadershipAsset[] leadershipAssets = {
			new LeadershipAsset(ProfessionAssetId.Mercenary, 0.05f),
			new LeadershipAsset(ProfessionAssetId.Guard, 0.05f),
			new LeadershipAsset(ProfessionAssetId.Footman, 0.05f),
			new LeadershipAsset(ProfessionAssetId.ManAtArms, 0.10f),
			new LeadershipAsset(ProfessionAssetId.Adventurer, 0.25f),
			new LeadershipAsset(ProfessionAssetId.Hero, 0.50f),
		};
		

		private static bool CollectCompleted(Interactor intr) {
			var resultAvailable = Screen.ImageSearch(intr, "ProfessionsCollectResult");

			if (resultAvailable.Found) {
				Mouse.Click(intr, resultAvailable.Point, 15, 5);
				intr.Wait(900);
				Mouse.ClickImage(intr, "ProfessionsTakeRewardsButton", 21, 5);
				intr.Wait(1500);
				return true;
			} else {
				return false;
			}
		}


		private static void SelectProfTask(Interactor intr, string taskName) {
			intr.Log(LogEntryType.Debug, "Attempting to select profession task: '{0}'.", taskName);
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
		}


		// Actually queues a profession task:
		private static bool ContinueTask(Interactor intr, Point continueButton, out float bonusFactor) {
			Mouse.Click(intr, continueButton);
			intr.Wait(100);

			// Detect currently selected asset:
			LeadershipAsset primaryAsset = new LeadershipAsset(ProfessionAssetId.None, 1.00f);

			foreach (var asset in leadershipAssets) {
				if (Screen.ImageSearch(intr, asset.LargeTileImageLabel).Found) {
					primaryAsset = asset;
					break;
				}
			}

			if (primaryAsset.AssetId == ProfessionAssetId.None) {
				intr.Log(LogEntryType.Error, "Primary profession asset not detected.");
				bonusFactor = 0.0f;
				return false;
			} else {
				intr.Log(LogEntryType.Debug, "Using primary profession asset: '{0}'.", primaryAsset.Label);
			}

			// Attempt to add optional assets:
			Mouse.ClickImage(intr, "ProfessionsAssetButton");
			intr.Wait(50);
			
			LeadershipAsset optionalAsset = new LeadershipAsset(ProfessionAssetId.None, 1.00f);

			foreach (var asset in leadershipAssets) {
				if (Mouse.ClickImage(intr, asset.SmallIconImageLabel)) {
					optionalAsset = asset;
					break;
				}
			}

			if (optionalAsset.AssetId == ProfessionAssetId.None) {
				intr.Log(LogEntryType.Debug, "No optional profession assets found.");
			} else {
				intr.Log(LogEntryType.Debug, "Using optional profession asset: '{0}'.", optionalAsset.Label);
			}			

			intr.Wait(50);

			// Enqueue the profession task:
			Mouse.ClickImage(intr, "ProfessionsStartTaskButton");
			intr.Wait(500);

			if (primaryAsset.AssetId == ProfessionAssetId.Mercenary || 
						primaryAsset.AssetId == ProfessionAssetId.Guard ||
						primaryAsset.AssetId == ProfessionAssetId.Footman) {
				bonusFactor = optionalAsset.BonusFactor;
			} else {
				bonusFactor = primaryAsset.BonusFactor + optionalAsset.BonusFactor;
			}
			
			return true;
		}


		// Combines tasks with the same id into one task, preserving the worst (lowest) bonus factor.
		public static void CondenseTasks(Interactor intr, List<ProfessionTaskResult> completionList) {
			var buckets = new Dictionary<int, ProfessionTaskResult>(9);

			foreach (ProfessionTaskResult taskResult in completionList) {
				//intr.Log(LogEntryType.Debug, "Professions::CondenseTasks(): Attempting to condense list task (id: {0}, bf: {1})...", 
				//	taskResult.TaskId, taskResult.BonusFactor);
				int bucket_id = taskResult.TaskId;
				ProfessionTaskResult peerTaskResult;
				bool peerExists = buckets.TryGetValue(bucket_id, out peerTaskResult);
				
				if (peerExists) {
					//intr.Log(LogEntryType.Debug, "Professions::CondenseTasks(): Pre-existing peer found (id: {0}, bf: {1}).", 
					//	peerTaskResult.TaskId, peerTaskResult.BonusFactor);
					if (taskResult.BonusFactor < peerTaskResult.BonusFactor) {
						buckets[bucket_id] = taskResult;
					}
				} else {
					//intr.Log(LogEntryType.Debug, "Professions::CondenseTasks(): No peer found, adding task (id: {0}, bf: {1}).", 
					//	taskResult.TaskId, taskResult.BonusFactor);
					buckets.Add(bucket_id, taskResult);
				}
			}

			completionList.Clear();

			foreach (ProfessionTaskResult taskResult in buckets.Values) {
				intr.Log(LogEntryType.Debug, "Professions::CondenseTasks(): Profession completion list task: '{0}' has been " +
					"condensed with a bonus factor of: '{1}'.", taskResult.TaskId, taskResult.BonusFactor);
				completionList.Add(taskResult);
			}
		}


		public static CompletionStatus MaintainProfs (Interactor intr, uint charIdx, 
						List<ProfessionTaskResult> completionList) {
			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }

			string charLabel = intr.AccountSettings.CharNames[(int)charIdx];
			string profsWinKey = intr.AccountSettings.GetSettingValOr("professions", "gameHotkeys", Global.Default.ProfessionsWindowKey);

			intr.Log(LogEntryType.Debug, "Opening professions window for " + charLabel + ".");

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

				intr.Log(LogEntryType.Debug, "Collected " + profResultsCollected + " profession results for " +
					charLabel + ".");
			}

			int noValidTaskId = 0;
			int noValidTaskCounter = 0;
			int currentTaskId = 0;
			var anySuccess = false;

			for (int slotId = 0; slotId < 9; slotId++) {
				if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; };
				
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
				
				if (slotId == 0 || !taskContinueResult.Found) {
					while(true) {
						if (currentTaskId < ProfessionTasksRef.ProfessionTaskNames.Length) {
							SelectProfTask(intr, ProfessionTasksRef.ProfessionTaskNames[currentTaskId]);
							taskContinueResult = Screen.ImageSearch(intr, "ProfessionsTaskContinueButton");

							if (taskContinueResult.Found) {
								intr.Log(LogEntryType.Debug, "Profession task: '{0}' has been selected.", 
									ProfessionTasksRef.ProfessionTaskNames[currentTaskId]);
								break;								
							} else {
								currentTaskId += 1;
								continue;
							}
						} else {
							// We've been stuck //
							if (noValidTaskId == currentTaskId) {
								// We have already set `noValidTaskId` for this task //
								if (noValidTaskCounter >= 2) {
									// This is the 3rd time we've been here //
									intr.Log(LogEntryType.Error, "Error starting profession task on " + charLabel + ":");
									intr.Log(LogEntryType.Error, "- Ensure that profession assets are sorted correctly in inventory.");
									return CompletionStatus.Stuck;
								} else {
									noValidTaskCounter += 1;
								}
							} else {
								noValidTaskId = currentTaskId;
								noValidTaskCounter = 1;
							}
							
							intr.Log(LogEntryType.Normal, "Could not find valid professions task.");
							CollectCompleted(intr);
							Mouse.ClickImage(intr, "ProfessionsWindowTitle");
							Mouse.Move(intr, Screen.ImageSearch(intr, "ProfessionsWindowTitle").Point);
						}
					}
				}

				if (taskContinueResult.Found && currentTaskId < ProfessionTasksRef.ProfessionTaskNames.Length) {
					float bonusFactor = 0.0f;

					// Start the in-game task:
					if (ContinueTask(intr, taskContinueResult.Point, out bonusFactor)) {
						intr.Log(LogEntryType.Debug, "Profession task '{0}' (id: {1}, bf: {2}) started.", 
							ProfessionTasksRef.ProfessionTaskNames[currentTaskId], currentTaskId, bonusFactor);		
								
						completionList.Add(new ProfessionTaskResult(currentTaskId, bonusFactor));
						anySuccess = true;
					}
				} 
			}

			// Condense tasks into groups (of 3 generally):
			CondenseTasks(intr, completionList);

			if (intr.CancelSource.IsCancellationRequested) { return CompletionStatus.Cancelled; }

			if (anySuccess) {
				return CompletionStatus.Complete;
			} else {
				return CompletionStatus.Immature;
			}
		}
	}
}
