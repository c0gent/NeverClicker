using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool CrashCheckRecovery(Interactor intr) {
			var desiredState = ClientState.CharSelect;

			Screen.Wake(intr);

			Mouse.Move(intr, 1, 1);

			intr.Wait(5000);

			switch (Game.DetermineGameState(intr)) {
				case GameState.Closed:
					return ProduceClientState(intr, desiredState);

				case GameState.Patcher:
					return ProduceClientState(intr, desiredState);

				case GameState.Unknown:
				case GameState.ClientActive:
					if (!ProduceClientState(intr, desiredState)) {
						// CHECK FOR POPUP WINDOWS ETC.
						// CHECK FOR CRASH
						KillAll(intr);						
					}
					return ProduceClientState(intr, desiredState);
				default:
					intr.Log("ProduceClientState(): Unable to produce desired client state.");
					//throw new NotImplementedException("ProduceClientState(): Unable to produce desired client state.");
					return false;
			}
		}
	}
}
