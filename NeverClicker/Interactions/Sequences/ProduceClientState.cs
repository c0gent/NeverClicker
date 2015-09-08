using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static bool ProduceClientState(Interactor intr, ClientState desiredState) {
			if (intr.CancelSource.Token.IsCancellationRequested) { return false; }

			intr.Log("Attempting to produce client state: " + desiredState.ToString(), LogEntryType.Info);

			if (Game.IsClientState(intr, desiredState)) {
				return true;
			} else if (desiredState == ClientState.Inactive) {
				if (Game.IsGameState(intr, GameState.ClientActive)) {
					intr.Log("Minimizing client...", LogEntryType.Normal);
					Screen.WindowMinimize(intr, Game.GAMECLIENTEXE);					
				} 
				return true;
			} else if (desiredState == ClientState.None) {
				intr.Log("Attempting to close game client...", LogEntryType.Normal);
				KillAll(intr);
				return true;
			} else if (desiredState == ClientState.CharSelect) {
				switch (Game.DetermineClientState(intr)) {
					case ClientState.None:
						intr.Log("Launching patcher...");
						return PatcherLogin(intr, desiredState);

					case ClientState.Inactive:						
						intr.Log("Game client currently inactive. Waiting 30 seconds before re-activating...", LogEntryType.Normal);

						const int waitIncr = 5000;

						for (int i = 0; i < 30000; i += waitIncr) {
							if (!Game.IsClientState(intr, ClientState.Inactive)) { break; }
							intr.Wait(waitIncr);
						}

						//intr.Wait(30000);
						intr.Log("Activating Client...", LogEntryType.Normal);
						ActivateClient(intr);
						return intr.WaitUntil(10, ClientState.CharSelect, Game.IsClientState, ProduceClientState);

					case ClientState.InWorld:
						intr.Log("Logging out...", LogEntryType.Normal);
						LogOut(intr);
						return intr.WaitUntil(45, ClientState.CharSelect, Game.IsClientState, ProduceClientState);

					case ClientState.LogIn:
						intr.Log("Client open, at login screen.", LogEntryType.Normal);
						ClientSignIn(intr);
						return intr.WaitUntil(30, ClientState.CharSelect, Game.IsClientState, ProduceClientState);

					default:				
						intr.Wait(1000);
						if (!intr.WaitUntil(60, ClientState.CharSelect, Game.IsClientState, null)) {
							intr.Log("Client state unknown. Attempting crash recovery...", LogEntryType.Info);
							CrashCheckRecovery(intr);
							return false;
						} else {
							return true;
						}
				}
			}

			return false;
		}
					
	}
}
