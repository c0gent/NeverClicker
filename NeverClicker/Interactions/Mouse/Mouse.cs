using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Mouse {

		public static void Click(Interactor intr, int xCoord, int yCoord) {
			intr.ExecuteStatement("SendEvent { Click " + xCoord + ", " + yCoord + ", 1 }");
		}

		public static void DoubleClick(Interactor intr, int xCoord, int yCoord) {
			intr.ExecuteStatement("SendEvent { Click " + xCoord + ", " + yCoord + ", 0 }");
			intr.Wait(10);
			intr.ExecuteStatement("SendEvent { Click 2 }");
		}

		public static void Move(Interactor intr, int xCoord, int yCoord) {
			intr.ExecuteStatement("SendEvent { Click " + xCoord + ", " + yCoord + ", 0 }");
		}

		public static void WheelUp(Interactor intr) {
			intr.ExecuteStatement("SendEvent { Click WheelUp }");
		}

		public static void WheelDown(Interactor intr) {
			intr.ExecuteStatement("SendEvent { Click WheelDown }");
		}

		//public static void SendInput(Interactor intr, string key) {
		//	//intr.Wait(200);
		//	intr.ExecuteStatement("SendInput " + key);
		//	intr.Wait(50);
		//}

		//public static void SendPlay(Interactor intr, string keys) {
		//	intr.ExecuteStatement("SendPlay " + keys);
		//	intr.Wait(50);
		//}

		//public static void SendEvent(Interactor intr, string keys) {
		//	intr.ExecuteStatement("SendEvent " + keys);
		//	intr.Wait(50);
		//}

		public static void SendTest(Interactor intr, string key) {
			intr.Wait(3000);
			intr.ExecuteStatement("SendEvent " + key);
			intr.Wait(50);
		}
	}
}
