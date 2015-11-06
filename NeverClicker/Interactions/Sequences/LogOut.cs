using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		//public static bool ActivateClient(Interactor intr) {
		//	//intr.ExecuteStatement("ActivateNeverwinter()");

		//	Screen.WindowActivate(intr, Game.GAMECLIENTEXE);

		//	intr.Wait(1000);

		//	return true;
		//}

		public static bool LogOut(Interactor intr) {
			//intr.ExecuteStatement("Logout()");
			intr.Log("Logging out...", LogEntryType.Info);

			MoveAround(intr);

			Keyboard.SendKey(intr, "Enter");
			intr.Wait(50);

			Keyboard.Send(intr, "/gotocharacterselect");
			intr.Wait(100);

			Keyboard.SendKey(intr, "Enter");
			intr.Wait(100);

			//intr.Wait(3000);

			return true;
		}
	}
}
