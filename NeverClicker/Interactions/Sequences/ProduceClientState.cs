using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static bool ProduceClientState(Interactor itr, ClientState desiredState) {
			if (itr.CancelSource.Token.IsCancellationRequested) {
				return false;
			}

			itr.Log("Attempting to produce client state: " + desiredState.ToString());

			var currentState = Game.GetClientState(itr);

			if (desiredState == currentState) {
				return true;
			} else if (desiredState == ClientState.Inactive) {
				Screen.WindowMinimize(itr, Game.GAMECLIENTEXE);
			} else if (desiredState == ClientState.None) {
				Sequences.KillAll(itr);
			} else if (desiredState == ClientState.CharSelect) {
				switch (currentState) {
					case ClientState.None:
						itr.Log("Client not found, launching patcher.");
						return Sequences.PatcherLogin(itr);

					case ClientState.Inactive:
						itr.Log("Client inactive, activating.");
						return Sequences.ActivateClient(itr);

					case ClientState.InWorld:
						itr.Log("Client in world, logging out.");
						return Sequences.LogOut(itr);

					case ClientState.LogIn:
						itr.Log("Client open, at login screen.");
						return Sequences.ClientSignIn(itr);

					default:
						itr.Log("Client state unknown. Attempting to retry...");
						break;
				}
			}

			return false;
		}
					
	}
}
