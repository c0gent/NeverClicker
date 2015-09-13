using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void ClearOkButtons(Interactor intr) {
			Mouse.ClickImage(intr, "CharSelectOkayButton");
			Mouse.ClickImage(intr, "CharSelectOkButton");
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