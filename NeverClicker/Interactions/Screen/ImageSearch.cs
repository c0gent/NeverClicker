using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NeverClicker.Properties;

namespace NeverClicker.Interactions {
	public static partial class Screen {
		public const string OUTPUT_VAR_X = "OutputVarX";
		public const string OUTPUT_VAR_Y = "OutputVarY";
		public const string ERROR_LEVEL = "ErrorLevel";
		public const string OPTIONS = "*40";

		public static Point ImageSearchAndClick(Interactor itr, string imgCode) {
			return new Point(0, 0);
		}

		public static ImageSearchResult ImageSearch(Interactor itr, string imgCode) {
			//ImageSearch, ImgX, ImgY, 1, 1, 1920, 1080, *40 % image_file %
			var imageFileName = itr.GameClient.GetSetting(imgCode + "_ImageFile", "SearchRectanglesAnd_ImageFiles");
			if (string.IsNullOrWhiteSpace(imageFileName)) {
				itr.Log("Image code prefix '" + imgCode + "' not found in settings ini file.");
				return new ImageSearchResult() { Found = false, Point = new Point(0, 0) };
			}

			var imageFilePath = Settings.Default.ImagesFolderPath.ToString() + imageFileName;

			itr.Log(new LogMessage("Searching for image: '" + imageFilePath
				+ "' [ScreenWidth:" + itr.GetVar("A_ScreenWidth")
				+ " ScreenHeight:" + itr.GetVar("A_ScreenHeight") + "]",
				LogType.Detail			
			));

			int outX = 0;
			int outY = 0;
			int errorLevel = 0;

			itr.SetVar(OUTPUT_VAR_X, outX.ToString());
			itr.SetVar(OUTPUT_VAR_Y, outY.ToString());

			var statement = string.Format("ImageSearch, {0}, {1}, {2}, {3}, {4}, {5}, {6} {7}",
				 OUTPUT_VAR_X, OUTPUT_VAR_Y, "0", "0", "A_ScreenWidth", "A_ScreenHeight", OPTIONS, imageFilePath);

			itr.Log(new LogMessage("Executing: '" + statement + "'", LogType.Detail));

			itr.ExecuteStatement(statement);

			int.TryParse(itr.GetVar(OUTPUT_VAR_X), out outX);
			int.TryParse(itr.GetVar(OUTPUT_VAR_Y), out outY);
			int.TryParse(itr.GetVar(ERROR_LEVEL), out errorLevel);

			itr.Log(new LogMessage(
					"ImageSearch Results: "
					+ " OutputVarX:" + itr.GetVar(OUTPUT_VAR_X)
					+ " OutputVarY:" + itr.GetVar(OUTPUT_VAR_Y)
					+ " ErrorLevel:" + itr.GetVar(ERROR_LEVEL),
					LogType.Detail					
			));

			//try {

			//	outX = int.Parse(itr.GetVar(OUTPUT_VAR_X));
			//	outY = int.Parse(itr.GetVar(OUTPUT_VAR_Y));
			//	errorLevel = int.Parse(itr.GetVar(ERROR_LEVEL));
			//} catch (Exception){
			//	throw new ProblemConductingImageSearchException("ImageSearch Results: "
			//		+ " OutputVarX:" + itr.GetVar(OUTPUT_VAR_X)
			//		+ " OutPutVarY:" + itr.GetVar(OUTPUT_VAR_Y)
			//		+ " ErrorLevel:" + itr.GetVar(ERROR_LEVEL)
			//	);
			//	//return new FindResult() { Found = false, At = new Point(0, 0) };
			//}

			switch (errorLevel) {
				case 0:
					return new ImageSearchResult() { Found = true, Point = new Point(outX, outY) };
				case 1:
					return new ImageSearchResult() { Found = false, Point = new Point(outX, outY) };				
				case 2:
					throw new ProblemConductingImageSearchException();
				default:
					throw new ProblemConductingImageSearchException();
			}

		}
	}

	public class ImageSearchResult {
		public Point Point;
		public bool Found;		
	}

	class ProblemConductingImageSearchException : Exception {
		public ProblemConductingImageSearchException() : base("There was a problem that prevented ImageSearch"  
			+ " from conducting the search(such as failure to open the image file or a badly formatted option)") { }
		public ProblemConductingImageSearchException(string message) : base(message) { }
		public ProblemConductingImageSearchException(string message, Exception inner) : base(message, inner) { }
	}
}


// ErrorLevel is set to 0 if the image was found in the specified region, 
// 1 if it was not found, or 2 if there was a problem that prevented the 
// command from conducting the search(such as failure to open the image file 
// or a badly formatted option).