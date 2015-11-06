using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void MoveAround(Interactor intr) {
			string moveLeftKey = intr.GameAccount.GetSettingOrEmpty("NwMoveLeftKey", "GameHotkeys");
			string moveRightKey = intr.GameAccount.GetSettingOrEmpty("NwMoveRightKey", "GameHotkeys");
			string moveForeKey = intr.GameAccount.GetSettingOrEmpty("NwMoveForeKey", "GameHotkeys");
			string moveBackKey = intr.GameAccount.GetSettingOrEmpty("NwMoveBackKey", "GameHotkeys");

			intr.WaitRand(40, 120);

			int dirRand = intr.Rand(0, 6);
			
			int keyDelay = 40;

			if (dirRand == 0 || dirRand == 1) {
				Keyboard.KeyPress(intr, moveLeftKey, keyDelay);
			} else if (dirRand == 2 || dirRand == 3) {
				Keyboard.KeyPress(intr, moveRightKey, keyDelay);
			} else if (dirRand == 4) {
				Keyboard.KeyPress(intr, moveForeKey, keyDelay);
			} else if (dirRand == 5) {
				Keyboard.KeyPress(intr, moveBackKey, keyDelay);
			}

			intr.WaitRand(120, 220);
		}
	}
}

//[GameHotkeys]
//NwMoveLeftKey=;
//NwMoveRightKey=a

//MoveAround() {
//	global
//	Sleep 135 + Ran(70)
	
//	Send {%NwMoveLeftKey% down}
//	Sleep 180
//	Send {%NwMoveLeftKey% up}
//	Sleep 180
	
//	Send {%NwMoveRightKey% down}
//	Sleep 180
//	Send {%NwMoveRightKey% up}
//	Sleep 180
//}