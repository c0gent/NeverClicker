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
		public static bool ProduceClientState(Interactor itr, ClientState desiredState) {
			if (itr.CancelSource.Token.IsCancellationRequested) {
				return false;
			}

			itr.Log("Attempting to produce client state: " + desiredState.ToString());
			
			var currentState = GetClientState(itr);

			if (desiredState == currentState) {
				return true;
			} else if (desiredState == ClientState.Inactive) {
				Screen.WindowMinimize(itr, GAMECLIENTEXE);
			} else if (desiredState == ClientState.None) {
				CloseAll(itr);
			} else if (desiredState == ClientState.CharSelect) {
				switch (currentState) {
					case ClientState.None:
						itr.Log("Client not found, launching patcher.");
						itr.ExecuteStatement("ActivateNeverwinter()");

						if (itr.WaitUntil(1500, () => { return GetClientState(itr) == desiredState; })) {
							LogSuccess(itr, ClientState.None, desiredState);
							return true;
						} else {
							LogFailure(itr, ClientState.None, desiredState);
							itr.Wait(10000);
							return ProduceClientState(itr, desiredState);
						}

					case ClientState.Inactive:						
						itr.Log("Client inactive, activating.");
						itr.ExecuteStatement("ActivateNeverwinter()");

						if (itr.WaitUntil(150, () => { return GetClientState(itr) == desiredState; })) {
							LogSuccess(itr, ClientState.Inactive, desiredState);
							return true;
						} else {
							LogFailure(itr, ClientState.Inactive, desiredState);
							return ProduceClientState(itr, desiredState);
						}
											
                    case ClientState.InWorld:
						itr.Log("Client in world, logging out.");
						LogOut(itr);
											
						if (itr.WaitUntil(600, () => { return GetClientState(itr) == desiredState; })) {
							LogSuccess(itr, ClientState.InWorld, desiredState);
							return true;
						} else {
							LogFailure(itr, ClientState.InWorld, desiredState);
							return ProduceClientState(itr, desiredState);
						}	
											
					case ClientState.LogIn:
						itr.Log("Client open, at login screen.");
						ClientSignIn(itr);

						if (itr.WaitUntil(600, () => { return GetClientState(itr) == desiredState; })) {
							LogSuccess(itr, ClientState.LogIn, desiredState);
							return true;
						} else {
							LogFailure(itr, ClientState.LogIn, desiredState);
							return ProduceClientState(itr, desiredState);
						}

					default:
						itr.Log("Client state unknown. Attempting to retry...");
						switch (GetGameState(itr)) {
							case GameState.Closed:
								return ProduceClientState(itr, desiredState);

							case GameState.Patcher:
								return ProduceClientState(itr, desiredState);

							default:
								itr.Log("ProduceClientState(): Unable to produce desired client state.");
								//throw new NotImplementedException("ProduceClientState(): Unable to produce desired client state.");
								break;
						}
						break;
				}
			}

			return false;
		}

		public static void LogOut(Interactor itr) {			
			itr.Log("Logging out.");
			itr.ExecuteStatement("ActivateNeverwinter()");
		}

		public static void ClientSignIn(Interactor itr) {
			itr.Log("Signing in Client.");
			itr.ExecuteStatement("ActivateNeverwinter()");
		}

		public static void PatcherLogin(Interactor itr) {

		}

		public static void CloseAll(Interactor itr) {
			itr.Log("Closing Neverwinter.");
			itr.ExecuteStatement("VigilantlyCloseClientAndExit()");
		}


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
					} else if (Screen.ImageSearch(itr, "LoginButton").Found) {
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

		public static void LogSuccess<T, U>(Interactor itr, T start, U end) {
			itr.Log("ProduceClientState(): " + start.ToString() + " -> " + end.ToString() + "success.");
		}

		public static void LogFailure<T, U>(Interactor itr, T start, U end) {
			itr.Log("ProduceClientState(): " + start.ToString() + " -> " + end.ToString() + "failure. Retrying...");
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

