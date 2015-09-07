using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool ActivateClient(Interactor intr) {
			//var desiredState = ClientState.CharSelect;
			//intr.ExecuteStatement("ActivateNeverwinter()");

			Screen.WindowActivate(intr, Game.GAMECLIENTEXE);

			intr.Wait(1000);

			return true;

			//if (intr.WaitUntil(5, () => { return Game.GetClientState(intr) == desiredState; })) {
			//	LogSuccess(intr, ClientState.Inactive, desiredState);
			//	return true;
			//} else {
			//	LogFailure(intr, ClientState.Inactive, desiredState);
			//	return ProduceClientState(intr, desiredState);
			//}
		}

		public static bool LogOut(Interactor intr) {
			//var desiredState = ClientState.CharSelect;
			intr.ExecuteStatement("Logout()");
			//intr.ExecuteStatement("ActivateNeverwinter()");

			intr.Wait(3000);

			return true;

			//if (intr.WaitUntil(10, () => { return Game.GetClientState(intr) == desiredState; })) {
			//	LogSuccess(intr, ClientState.InWorld, desiredState);
			//	return true;
			//} else {
			//	LogFailure(intr, ClientState.InWorld, desiredState);
			//	return ProduceClientState(intr, desiredState);
			//}
		}

		public static bool ClientSignIn(Interactor intr) {
			//var desiredState = ClientState.CharSelect;
			intr.Log("Signing in Client...", LogEntryType.Info);
			intr.ExecuteStatement("ClientLogin()");

			intr.Wait(3000);

			return true;

			//if (intr.WaitUntil(60, () => { return Game.GetClientState(intr) == desiredState; })) {
			//	LogSuccess(intr, ClientState.LogIn, desiredState);
			//	return true;
			//} else {
			//	LogFailure(intr, ClientState.LogIn, desiredState);
			//	return ProduceClientState(intr, desiredState);
			//}
		}


		public static void KillAll(Interactor intr) {
			intr.Log("Closing game client and/or patcher...", LogEntryType.Info);
			intr.ExecuteStatement("VigilantlyCloseClientAndExit()");
		}
	}
}
