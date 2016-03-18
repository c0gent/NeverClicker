using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NeverClicker.Interactions;

namespace NeverClicker {
	public static partial class Game {
		//Interactions.Interactor Interactor;
		//private GameClientState State;

		// STORE THESE IN A FILE
		public const string GAMEPATCHEREXE = "Neverwinter.exe";
		public const string GAMECLIENTEXE = "GameClient.exe";

		// <<<<< TODO: MAKE RECURSIVE CALLS >>>>>

		public static bool IsGameState(Interactor intr, GameState desiredState) {
			switch (desiredState) {
				case GameState.Closed:
					return DetermineGameState(intr) == GameState.Closed;
				case GameState.ClientActive:
					return Screen.WindowDetectActive(intr, GAMECLIENTEXE);
				case GameState.ClientInactive:
					return IsClientState(intr, ClientState.Inactive);
				case GameState.Patcher:
					return Screen.WindowDetectExist(intr, GAMEPATCHEREXE);
			}
			return false;
		}
		
		public static GameState DetermineGameState(Interactor intr) {
			if (Screen.WindowDetectExist(intr, GAMEPATCHEREXE)) {
				return GameState.Patcher;
			} else if (Screen.WindowDetectExist(intr, GAMECLIENTEXE)) {
				if (Screen.WindowDetectActive(intr, GAMECLIENTEXE)) {
					return GameState.ClientActive;
				} else {
					return GameState.ClientInactive;
				}
			} else {
				return GameState.Closed;
			}
		}


		public static bool IsClientState(Interactor intr, ClientState desiredState) {
			switch (desiredState) {
				case ClientState.None:
					return !Screen.WindowDetectExist(intr, GAMECLIENTEXE);
				case ClientState.Inactive:
					return (!Screen.WindowDetectActive(intr, GAMECLIENTEXE) && Screen.WindowDetectExist(intr, GAMECLIENTEXE));
				case ClientState.CharSelect:
					return Screen.ImageSearch(intr, "EnterWorldButton").Found;
				case ClientState.InWorld:
					return Screen.ImageSearch(intr, "AbilityPanelSerpent").Found;
				case ClientState.LogIn:
					return Screen.ImageSearch(intr, "ClientLoginButton").Found;
			}
			return false;
		}

		public static ClientState DetermineClientState(Interactor intr) {
			if (Screen.WindowDetectExist(intr, GAMECLIENTEXE)) {
				if (Screen.WindowDetectActive(intr, GAMECLIENTEXE)) {
					if (Screen.ImageSearch(intr, "EnterWorldButton").Found) {
						return ClientState.CharSelect;
					} else if (Screen.ImageSearch(intr, "AbilityPanelSerpent").Found) {
						return ClientState.InWorld;
					} else if (Screen.ImageSearch(intr, "ClientLoginButton").Found) {
						return ClientState.LogIn;
					} else {
						return ClientState.Unknown;
					}
				} else {
					return ClientState.Inactive;
				}			
			} else {
				return ClientState.None;
			}
		}


		public static bool IsDialogueBoxState(Interactor intr, DialogueBoxState desiredState) {
			switch (desiredState) {
				case DialogueBoxState.InvocationSuccess:
					return Screen.ImageSearch(intr, "InvocationSuccessWindowTitle").Found;
			}
			return false;
		}

		public static DialogueBoxState DetermineDialogueBoxState(Interactor intr) {
			if (IsClientState(intr, ClientState.InWorld)) {
				if (Screen.ImageSearch(intr, "InvocationSuccessWindowTitle").Found) {
					return DialogueBoxState.InvocationSuccess;
				} else {
					return DialogueBoxState.Unknown;
				}
			} else {
				return DialogueBoxState.None;
			}
		}


		//public static WorldInvocationState DetermineInvocationState(Interactor intr) {
		//	if (IsClientState(intr, ClientState.InWorld)) {
		//		if (Screen.ImageSearch(intr, "InvocationReady").Found) {
		//			return WorldInvocationState.Ready;
		//		} else if (Screen.ImageSearch(intr, "InvocationNotReady").Found) {
		//			return WorldInvocationState.NotReady;
		//		} else {
		//			return WorldInvocationState.Unknown;
		//		}
  //          } else {
		//		return WorldInvocationState.None;
		//	}
		//}


		//play button 
		//login
		//none

		
		public static bool IsPatcherState(Interactor intr, PatcherState desiredState) {
			switch (desiredState) {
				case PatcherState.PlayButton:
					return Screen.ImageSearch(intr, "PatcherPlayButton").Found;
				case PatcherState.LogIn:
					return Screen.ImageSearch(intr, "PatcherLoginButtonPart").Found;
				case PatcherState.None:
					return DeterminePatcherState(intr) == PatcherState.None;
			}			
			return false;
		}


		public static PatcherState DeterminePatcherState(Interactor intr) {
			if (Screen.WindowDetectExist(intr, GAMEPATCHEREXE)) {
				if (Screen.WindowDetectActive(intr, GAMEPATCHEREXE)) {
					if (Screen.ImageSearch(intr, "PatcherLoginButtonPart").Found) {
						return PatcherState.LogIn;
					} else if (Screen.ImageSearch(intr, "PatcherPlayButton").Found) {
						return PatcherState.PlayButton;
					} else {
						return PatcherState.Unknown;
					}
				} else {
					return PatcherState.Inactive;
				}
			} else {
				return PatcherState.None;
			}
		}

		public static bool IsServerState(Interactor intr, ServerState desiredState) {
			switch (desiredState) {
				case ServerState.Up:
					return Screen.ImageSearch(intr, "PatcherServerUpIndicator").Found;
				case ServerState.Down:
					return Screen.ImageSearch(intr, "PatcherServerDownIndicator").Found;
				default:
					return false;
			}
		}
	}


	public enum GameState {
		Closed,
		Patcher,
		ClientActive,
		ClientInactive, // DUPLICATE
		Unknown
	}

	public enum ClientState {
		None,
		LogIn,
		CharSelect,
		InWorld,
		Inactive,
		Unknown
	}

	public enum PatcherState {
		None,
		LogIn,
		Patching,	// CancelButton
		PlayButton,
		Inactive,
		Unknown
	}

	public enum ServerState {
		Down,
		Up
	}

	public enum WorldInvocationState {
		None,		
		Ready,
		NotReady,
		Unknown,
	}

	//	WorldWindowState: Windows which have interactable elements 
	//		not dialogue boxes like invocation success, etc., which go under their own category
	public enum WorldWindowState { 
		None,
		Inventory,
		Professions,
		VaultOfPiety,
		DialogueBox,
		Unknown
	}

	public enum DialogueBoxState {
		None,
		MaxBlessings,
		ResetFeats,
		RewardsOfDevotion,
		InvocationSuccess,
		Unknown
	}
}

