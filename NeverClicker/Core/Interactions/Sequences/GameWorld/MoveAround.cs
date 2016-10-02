using NeverClicker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void MoveAround(Interactor intr) {
			string moveLeftKey = intr.AccountSettings.GetSettingValOr("MoveLeft", "GameHotkeys", Global.Default.MoveLeftKey);
			string moveRightKey = intr.AccountSettings.GetSettingValOr("MoveRight", "GameHotkeys", Global.Default.MoveRightKey);
			string moveForeKey = intr.AccountSettings.GetSettingValOr("MoveForward", "GameHotkeys", Global.Default.MoveForwardKey);
			string moveBackKey = intr.AccountSettings.GetSettingValOr("MoveBackward", "GameHotkeys", Global.Default.MoveBackwardKey);

			intr.WaitRand(40, 120);

			int dirRand = intr.Rand(0, 6);
			
			int keyDelay = 20;

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