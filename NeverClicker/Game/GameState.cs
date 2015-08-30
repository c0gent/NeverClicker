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
				
		public static GameState GetGameState(Interactor itr) {
			if (Screen.WindowDetectExist(itr, GAMEPATCHEREXE)) {
				return GameState.Patcher;
			} else if (Screen.WindowDetectExist(itr, GAMECLIENTEXE)) {
				if (Screen.WindowDetectActive(itr, GAMECLIENTEXE)) {
					return GameState.ClientActive;
				} else {
					return GameState.ClientInactive;
                }				
			} else {
				return GameState.Closed;
			}
			//return GameState.Unknown;
		}

		public static ClientState GetClientState(Interactor itr) {
			if (Screen.WindowDetectExist(itr, GAMECLIENTEXE)) {
				if (Screen.WindowDetectActive(itr, GAMECLIENTEXE)) {
					if (Screen.ImageSearch(itr, "EnterWorldButton").Found) {
						return ClientState.CharSelect;
					} else if (Screen.ImageSearch(itr, "AbilityPanelSerpent").Found) {
						return ClientState.InWorld;
					} else if (Screen.ImageSearch(itr, "ClientLoginButton").Found) {
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
		Signin,
		Patching,
		Ready,
		Unknown
	}

	public enum WorldState {
		None,
		InvokeReady,
		BagOpen,
		VaultOpen,
		DialogueBox,
		Unknown
	}

	public enum DialogueBox {
		None,
		MaxBlessings,
		ResetFeats,
		Unknown
	}

}

