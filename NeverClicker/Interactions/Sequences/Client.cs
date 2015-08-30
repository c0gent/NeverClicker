using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool ActivateClient(Interactor itr) {
			var desiredState = ClientState.CharSelect;
			itr.ExecuteStatement("ActivateNeverwinter()");

			if (itr.WaitUntil(5, () => { return Game.GetClientState(itr) == desiredState; })) {
				LogSuccess(itr, ClientState.Inactive, desiredState);
				return true;
			} else {
				LogFailure(itr, ClientState.Inactive, desiredState);
				return ProduceClientState(itr, desiredState);
			}
		}

		public static bool LogOut(Interactor itr) {
			var desiredState = ClientState.CharSelect;
			itr.Log("Logging out.");
			itr.ExecuteStatement("ActivateNeverwinter()");

			if (itr.WaitUntil(10, () => { return Game.GetClientState(itr) == desiredState; })) {
				LogSuccess(itr, ClientState.InWorld, desiredState);
				return true;
			} else {
				LogFailure(itr, ClientState.InWorld, desiredState);
				return ProduceClientState(itr, desiredState);
			}
		}

		public static bool ClientSignIn(Interactor itr) {
			var desiredState = ClientState.CharSelect;
			itr.Log("Signing in Client.");
			itr.ExecuteStatement("ClientLogin()");

			if (itr.WaitUntil(60, () => { return Game.GetClientState(itr) == desiredState; })) {
				LogSuccess(itr, ClientState.LogIn, desiredState);
				return true;
			} else {
				LogFailure(itr, ClientState.LogIn, desiredState);
				return ProduceClientState(itr, desiredState);
			}
		}

		public static bool PatcherLogin(Interactor itr) {
			var desiredState = ClientState.CharSelect;
			itr.ExecuteStatement("ActivateNeverwinter()");

			if (itr.WaitUntil(500, () => { return Game.GetClientState(itr) == desiredState; })) {
				LogSuccess(itr, ClientState.None, desiredState);
				return true;
			} else {
				LogFailure(itr, ClientState.None, desiredState);
				itr.Wait(10000);
				return ProduceClientState(itr, desiredState);
			}
		}

		public static void KillAll(Interactor itr) {
			itr.Log("Closing Neverwinter.");
			itr.ExecuteStatement("VigilantlyCloseClientAndExit()");
		}
	}
}
