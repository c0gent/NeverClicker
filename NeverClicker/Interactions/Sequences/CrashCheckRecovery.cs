using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool CrashCheckRecovery(Interactor itr) {
			var desiredState = ClientState.CharSelect;

			switch (Game.GetGameState(itr)) {
				case GameState.Closed:
					return ProduceClientState(itr, desiredState);

				case GameState.Patcher:
					return ProduceClientState(itr, desiredState);

				case GameState.Unknown:
				case GameState.ClientActive:
					// CHECK FOR POPUP WINDOWS ETC.
					// CHECK FOR CRASH
					KillAll(itr);
					return ProduceClientState(itr, desiredState);

				default:
					itr.Log("ProduceClientState(): Unable to produce desired client state.");
					//throw new NotImplementedException("ProduceClientState(): Unable to produce desired client state.");
					return false;
			}
		}
	}
}
