using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Mouse {

		// Clicks an image.
		//
		// [TODO]: Randomize click location within image area.
		//
		public static bool ClickImage(Interactor intr, string imgCode) {
			return ClickImage(intr, imgCode, 0, 0);
		}

		public static bool ClickImage(Interactor intr, string imgCode, int xOfs, int yOfs, Point topLeft, Point botRight) {
			var result = Screen.ImageSearch(intr, imgCode, topLeft, botRight);
			if (result.Found) {
				Click(intr, result.Point.X + topLeft.X + 5, result.Point.Y + topLeft.Y + 5);
				return true;
			} else {
				return false;
			}
		}

		public static bool ClickImage(Interactor intr, List<string> imgCodes, int xOfs, int yOfs, Point topLeft, Point botRight) {
			var result = Screen.ImageSearch(intr, imgCodes, topLeft, botRight);
			if (result.Found) {
				Click(intr, result.Point.X + topLeft.X + 5, result.Point.Y + topLeft.Y + 5);
				return true;
			} else {
				return false;
			}
		}

		public static bool ClickImage(Interactor intr, string imgCode, int xOfs, int yOfs) {
			var result = Screen.ImageSearch(intr, imgCode);
			if (result.Found) {
				Click(intr, result.Point.X + 5, result.Point.Y + 5);
				return true;
			} else {
				return false;
			}
		}
	}
}



//FindAndClick(image_file, clk := 1, log := 1) 
//{
//	global
	
//	; LogAppend("[***** TRACE: FINDANDCLICK(): " . image_file . " (log:" . log . ")]")
	
//	ImageSearch, ImgX, ImgY, 1, 1, 1920, 1080, *40 %image_file%
//	;Sleep 100
//	ImgX += 5
//	ImgY += 5
//	if (!ErrorLevel) {
//		if (clk == 1) {
//			SendEvent {Click %ImgX%, %ImgY%, 1}
//		}
		
//		return 1
//	} else {
//		if (log) {
//			el_string := ""
//			if (ErrorLevel == 1) {
//				el_string = "1 - Image not found on screen"
//			} else if (ErrorLevel == 2) {
//				el_string = "2 - Image file or ImageSearch option error"
//			}
//			LogAppend("[FindAndClick - _ImageFile: " . image_file . " - ErrorLevel: " . el_string . "]")
//		}
//		return 0
//	}
//}