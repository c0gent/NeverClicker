using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void MoveAround(Interactor intr) {
			string moveLeftKey = intr.GameClient.GetSetting("NwMoveLeftKey", "GameHotkeys");
			string moveRightKey = intr.GameClient.GetSetting("NwMoveRightKey", "GameHotkeys");

			intr.WaitRand(150, 350);
			Keyboard.KeyPress(intr, moveLeftKey, 90);
			intr.WaitRand(50, 250);
			Keyboard.KeyPress(intr, moveRightKey, 90);
			intr.WaitRand(50, 250);
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