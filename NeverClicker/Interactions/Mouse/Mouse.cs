using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Mouse {

		public static void ClickRepeat(Interactor intr, int xCoord, int yCoord, int repeats) {
			for (int c = 0; c < repeats; c++) {
				Click(intr, xCoord, yCoord);
				intr.Wait(25);
			}
		}

		public static void Click(Interactor intr, Point point, int xOfs, int yOfs) {
			Click(intr, point.X + xOfs, point.Y + yOfs);
		}

		public static void Click(Interactor intr, int xCoord, int yCoord, int xOfs, int yOfs) {
			Click(intr, xCoord + xOfs, yCoord + yOfs);
		}

		public static void Click(Interactor intr, Point point) {
			Click(intr, point.X, point.Y);
		}

		public static void Click(Interactor intr, int xCoord, int yCoord) {
			intr.ExecuteStatement("SendEvent { Click " + xCoord + ", " + yCoord + ", 1 }");
		}


		public static void DoubleClick(Interactor intr, Point point) {
			DoubleClick(intr, point.X, point.Y);
		}

		public static void DoubleClick(Interactor intr, int xCoord, int yCoord) {
			intr.ExecuteStatement("SendEvent { Click " + xCoord + ", " + yCoord + ", 0 }");
			intr.Wait(10);
			intr.ExecuteStatement("SendEvent { Click 2 }");
		}


		public static void Move(Interactor intr, Point point) {
			Move(intr, point.X, point.Y);
		}

		public static void Move(Interactor intr, int xCoord, int yCoord) {
			intr.ExecuteStatement("SendEvent { Click " + xCoord + ", " + yCoord + ", 0 }");
		}


		public static void WheelUp(Interactor intr, int repeats) {			
			for (int c = 0; c < repeats; c++) {
				intr.ExecuteStatement("SendEvent { Click WheelUp }");
				intr.Wait(5);
			}
		}

		public static void WheelDown(Interactor intr, int repeats) {
			for (int c = 0; c < repeats; c++) {
				intr.ExecuteStatement("SendEvent { Click WheelDown }");
				intr.Wait(5);
			}
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
