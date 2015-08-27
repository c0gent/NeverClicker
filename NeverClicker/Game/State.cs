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

		public const string GAMEPATCHEREXE = "Neverwinter.exe";
		public const string GAMECLIENTEXE = "GameClient.exe";

		public static GameState GetGameState(Interactor interactor) {
			if (interactor.WindowDetectExists(GAMEPATCHEREXE)) {
				return GameState.Patcher;
			} else if (interactor.WindowDetectExists(GAMECLIENTEXE)) {
				if (interactor.WindowDetectActive(GAMECLIENTEXE)) {
					return GameState.ClientActive;
				} else {
					return GameState.ClientInactive;
                }				
			} else {
				return GameState.Closed;
			}
			//return GameState.Unknown;
		}

		public static ClientState GetClientState(Interactor interactor) {
			if (interactor.WindowDetectExists(GAMECLIENTEXE)) {
				//interactor.IniParser
				return ClientState.Unknown;
			} else {
				return ClientState.None;
			}

		}

	}

	public enum GameState {
		Closed,
		Patcher,
		ClientActive,
		ClientInactive,
		Unknown
	}

	public enum ClientState {
		None,
		SignIn,
		CharSelect,
		World,
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

