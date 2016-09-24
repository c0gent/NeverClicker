﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static bool ProduceClientState(Interactor intr, ClientState desiredState, int attemptCount) {
			if (intr.CancelSource.Token.IsCancellationRequested) { return false; }

			attemptCount += 1;

			intr.Log("Attempting to produce client state: " + desiredState.ToString(), LogEntryType.Info);

			if (Game.IsClientState(intr, desiredState)) {
				intr.Log("Client state is already: " + desiredState.ToString(), LogEntryType.Info);
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
				var currentClientState = Game.DetermineClientState(intr);
				intr.Log("Interactions::ProduceClientState(): Current client state is " + 
					currentClientState.ToString(), LogEntryType.Debug);
				switch (currentClientState) {
					case ClientState.None:
						intr.Log("Launching patcher...");
						return PatcherLogin(intr, desiredState);
					case ClientState.Inactive:						
						intr.Log("Game client is currently in the background. Waiting 30 seconds " +
							"or until client is brought to foreground before continuing...", LogEntryType.Normal);

						const int waitIncr = 5000;

						for (int i = 0; i < 30000; i += waitIncr) {
							if (!Game.IsClientState(intr, ClientState.Inactive)) { break; }
							intr.Wait(waitIncr);
						}

						//intr.Wait(30000);
						intr.Log("Activating Client...", LogEntryType.Normal);
						ActivateClient(intr);
						return intr.WaitUntil(10, ClientState.CharSelect, Game.IsClientState, ProduceClientState, attemptCount);
					case ClientState.InWorld:
						if (attemptCount >= 10) {
							intr.Log("Stuck at in world. Killing all and restarting.", LogEntryType.FatalWithScreenshot);
							KillAll(intr);
							intr.Wait(5000);
							return ProduceClientState(intr, ClientState.CharSelect, 0);
						} else {
							intr.Log("Logging out...", LogEntryType.Normal);
							LogOut(intr);
							return intr.WaitUntil(45, ClientState.CharSelect, Game.IsClientState, ProduceClientState, attemptCount);
						}
					case ClientState.LogIn:
						if (attemptCount >= 10) {
							intr.Log("Stuck at client login screen. Killing all and restarting.", LogEntryType.FatalWithScreenshot);
							KillAll(intr);
							intr.Wait(5000);
							return ProduceClientState(intr, ClientState.CharSelect, 0);
						} else {
							intr.Log("Client open, at login screen.", LogEntryType.Normal);
							ClientSignIn(intr);
							return intr.WaitUntil(30, ClientState.CharSelect, Game.IsClientState, ProduceClientState, attemptCount);
						}
					case ClientState.Unknown:
					default:						
						ClearDialogues(intr);

						if (!intr.WaitUntil(30, ClientState.CharSelect, Game.IsClientState, null, attemptCount)) {
							intr.Log("Client state unknown. Attempting crash recovery...", LogEntryType.Info);

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