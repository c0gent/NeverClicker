using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static bool PatcherLogin(Interactor itr) {
			//var desiredState = ClientState.CharSelect;
			itr.ExecuteStatement("ActivateNeverwinter()");

			return itr.WaitUntil(1200, ClientState.CharSelect, Game.GetClientState, ProduceClientState);

		}

	}
}
