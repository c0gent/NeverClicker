using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void MoveAround(Interactor intr) {
			string moveLeftKey = intr.GameAccount.GetSetting("NwMoveLeftKey", "GameHotkeys");
			string moveRightKey = intr.GameAccount.GetSetting("NwMoveRightKey", "GameHotkeys");

			intr.WaitRand(150, 350);
			Keyboard.KeyPress(intr, moveLeftKey, 40);
			intr.WaitRand(50, 350);
			Keyboard.KeyPress(intr, moveRightKey, 40);
			intr.WaitRand(50, 350);
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