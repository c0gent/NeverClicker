using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool ActivateClient(Interactor intr) {
			//intr.ExecuteStatement("ActivateNeverwinter()");

			Screen.WindowActivate(intr, States.GAMECLIENTEXE);

			intr.Wait(1500);

			return true;
		}
	}
}
