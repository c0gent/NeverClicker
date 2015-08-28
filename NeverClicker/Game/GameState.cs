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

		public static bool ProduceClientState(Interactor itr, ClientState desiredState) {
			itr.Log("Attempting to produce client state: " + desiredState.ToString());

			var currentState = GetClientState(itr);

			if (desiredState == currentState) {
				return true;
			} else if (desiredState == ClientState.CharSelect) {
				switch (currentState) {
					case ClientState.Inactive:
						itr.ExecuteStatement("ActivateNeverwinter()");
						if (itr.WaitUntil(300, () => { return GetClientState(itr) == ClientState.CharSelect; })) {
							return true;
						} else {
							//throw new NotImplementedException("ProduceClientState(): Waited too long to become active.");
							itr.Log("ProduceClientState(): Task cancelled or waited too long to become active.");
							break;
						}						
                    case ClientState.InWorld:
						LogOut(itr);
						if (itr.WaitUntil(30, () => { return GetClientState(itr) == ClientState.CharSelect; })) {
							return true;
						} else {
							//throw new NotImplementedException("ProduceClientState(): Waited too long to log out.");
							itr.Log("ProduceClientState(): Task cancelled or waited too long to log out.");
							break;
						}						
					case ClientState.SignIn:
						ClientSignIn(itr);
						//throw new NotImplementedException("ProduceClientState(): ClientSignIn.");
						itr.Log("ProduceClientState(): ClientSignIn (IMPLEMENT ME :).");				
						break;
					default:
						switch (GetGameState(itr)) {
							case GameState.Closed:
								itr.ExecuteStatement("ActivateNeverwinter()");
								return true;
							case GameState.Patcher:
								itr.ExecuteStatement("ActivateNeverwinter()");
								return true;
							default:
								//throw new NotImplementedException("ProduceClientState(): Unable to produce desired client state.");
								itr.Log("ProduceClientState(): Unable to produce desired client state.");
								break;
						}
						break;
				}
			}

			return false;
		}

		public static void LogOut(Interactor itr) {
			itr.Log("Logging out. ***NOT YET IMPLEMENTED***");
		}

		public static void ClientSignIn(Interactor itr) {
			itr.Log("Signing in Client. ***NOT YET IMPLEMENTED***");
		}


		public static GameState GetGameState(Interactor itr) {
			if (Screen.WindowDetectExists(itr, GAMEPATCHEREXE)) {
				return GameState.Patcher;
			} else if (Screen.WindowDetectExists(itr, GAMECLIENTEXE)) {
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
			if (Screen.WindowDetectExists(itr, GAMECLIENTEXE)) {
				if (Screen.WindowDetectActive(itr, GAMECLIENTEXE)) {
					if (Screen.ImageSearch(itr, "EnterWorldButton").Found) {
						return ClientState.CharSelect;
					} else if (Screen.ImageSearch(itr, "AbilityPanelSerpent").Found) {
						return ClientState.InWorld;
					} else if (Screen.ImageSearch(itr, "LoginButton").Found) {
						return ClientState.SignIn;
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
		SignIn,
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

