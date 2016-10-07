using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static bool ProduceClientState(Interactor intr, ClientState desiredState, int attemptCount) {
			if (intr.CancelSource.Token.IsCancellationRequested) { return false; }

			attemptCount += 1;

			intr.Log(LogEntryType.Info, "Attempting to produce client state: " + desiredState.ToString());

			if (States.IsClientState(intr, desiredState)) {
				intr.Log(LogEntryType.Info, "Client state is already: " + desiredState.ToString());
				return true;
			} else if (desiredState == ClientState.Inactive) {
				if (States.IsGameState(intr, GameState.ClientActive)) {
					intr.Log( "Minimizing client...");
					Screen.WindowMinimize(intr, States.GAMECLIENTEXE);					
				} 
				return true;
			} else if (desiredState == ClientState.None) {
				intr.Log("Attempting to close game client...");
				KillAll(intr);
				return true;
			} else if (desiredState == ClientState.CharSelect) {
				var currentClientState = States.DetermineClientState(intr);
				intr.Log(LogEntryType.Debug, "Interactions::ProduceClientState(): Current client state is " + 
					currentClientState.ToString());
				switch (currentClientState) {
					case ClientState.None:
						intr.Log("Launching patcher...");
						return PatcherLogin(intr, desiredState);
					case ClientState.Inactive:						
						intr.Log("Game client is currently in the background. Waiting 30 seconds " +
							"or until client is brought to foreground before continuing...");

						const int waitIncr = 5000;

						for (int i = 0; i < 30000; i += waitIncr) {
							if (!States.IsClientState(intr, ClientState.Inactive)) { break; }
							intr.Wait(waitIncr);
						}

						//intr.Wait(30000);
						intr.Log("Activating Client...");
						ActivateClient(intr);
						return intr.WaitUntil(10, ClientState.CharSelect, States.IsClientState, ProduceClientState, attemptCount);
					case ClientState.InWorld:
						if (attemptCount >= 10) {
							intr.Log(LogEntryType.FatalWithScreenshot, "Stuck at in world. Killing all and restarting.");
							KillAll(intr);
							intr.Wait(5000);
							return ProduceClientState(intr, ClientState.CharSelect, 0);
						} else {
							intr.Log("Logging out...");
							LogOut(intr);
							return intr.WaitUntil(45, ClientState.CharSelect, States.IsClientState, ProduceClientState, attemptCount);
						}
					case ClientState.LogIn:
						if (attemptCount >= 10) {
							intr.Log(LogEntryType.FatalWithScreenshot, "Stuck at client login screen. Killing all and restarting.");
							KillAll(intr);
							intr.Wait(5000);
							return ProduceClientState(intr, ClientState.CharSelect, 0);
						} else {
							intr.Log("Client open, at login screen.");
							ClientSignIn(intr);
							return intr.WaitUntil(30, ClientState.CharSelect, States.IsClientState, ProduceClientState, attemptCount);
						}
					case ClientState.Unknown:
					default:						
						ClearDialogues(intr);

						if (!intr.WaitUntil(30, ClientState.CharSelect, States.IsClientState, null, attemptCount)) {
							intr.Log(LogEntryType.Info, "Client state unknown. Attempting crash recovery...");

							CrashCheckRecovery(intr, 0);
							return ProduceClientState(intr, desiredState, attemptCount);

							//var curState = Game.DetermineClientState(intr);

							//if (curState == ClientState.Unknown) {
							//	//CrashCheckRecovery(intr);
							//	return 
							//} else {
							//	return 
							//}					
						} else {
							return true;
						}
				}
			}

			return false;
		}
					
	}
}
