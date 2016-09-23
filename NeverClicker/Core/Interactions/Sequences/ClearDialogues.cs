using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void ClearDialogues(Interactor intr) {
			intr.Log("Attempting to clear dialogues...", LogEntryType.Debug);

			Point topLeft = new Point(680, 400);
			Point botRight = new Point(1240, 680);

			Keyboard.SendKey(intr, Keyboard.KeyMod.Ctrl, "2");

			// These probably don't work:
			Mouse.ClickImage(intr, "CharSelectOkayButton", 0, 0, topLeft, botRight);
			Mouse.ClickImage(intr, "CharSelectOkButton", 0, 0, topLeft, botRight);
			Mouse.ClickImage(intr, "CharSelectOkButton_3", 0, 0, topLeft, botRight);
			//Mouse.ClickImage(intr, "DeclineButton", 0, 0, topLeft, botRight);
			//Mouse.ClickImage(intr, "DeclineButton_2", 0, 0, topLeft, botRight);
		}

		public static void ClearWindowsWithX(Interactor intr) {
			Mouse.ClickImage(intr, "WindowXButton");
		}
	}		
}

//ClearOkPopupBullshit() {
//	global
//	if (FindAndClick(OkayUcs_ImageFile, 1, 1) || FindAndClick(OkUcs_ImageFile, 1, 1)) {
//		return 1
//	}
//	return 0
//}