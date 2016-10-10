using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static bool CrashCheckRecovery(Interactor intr, int prev_tries_unimplemented) {
			intr.Log(LogEntryType.Info, "CrashCheckRecovery(): Initiating...");

			Screen.Wake(intr);

			Mouse.Move(intr, 1, 1);

			intr.Wait(5000);

			ClearDialogues(intr);

			intr.Wait(2000);

			Screen.WindowKillTitle(intr, "Verify?");

			KillAll(intr);

			return true;

			//switch (Game.DetermineGameState(intr)) {
			//	case GameState.Closed:
			//		intr.Log("CrashCheckRecovery(): GameState == Closed. Calling ProduceClientState().", LogEntryType.Info);
			//		return ProduceClientState(intr, ClientState.CharSelect);
			//	case GameState.Patcher:
			//		intr.Log("CrashCheckRecovery(): GameState == Closed. Calling ProduceClientState().", LogEntryType.Info);
			//		return ProduceClientState(intr, ClientState.CharSelect);
			//	case GameState.ClientActive:
			//		intr.Log("CrashCheckRecovery(): GameState == Closed. Killing all then calling ProduceClientState().", LogEntryType.Info);
			//		KillAll(intr);
			//		return ProduceClientState(intr, ClientState.CharSelect);
			//	case GameState.Unknown:
			//	default:
			//		intr.Log("CrashCheckRecovery(): GameState unknown. Killing all.");
			//		KillAll(intr);
			//		return false;
			//}
		}
	}
}
