using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NeverClicker.Properties;
using System.IO;
//using AForge.Imaging;

namespace NeverClicker.Interactions {
	public static partial class Screen {
		public const string OUTPUT_VAR_X = "OutputVarX";
		public const string OUTPUT_VAR_Y = "OutputVarY";
		public const string ERROR_LEVEL = "ErrorLevel";


		/// <summary>
		/// Find an image and return it's upper left coordinate.
		/// </summary>
		/// This needs a crazy amount more error handling/reporting but it's just a huge pain. Most stuff is ignored.
		/// 
		/// ErrorLevel is set to 0 if the image was found in the specified region, 
		/// 1 if it was not found, or 2 if there was a problem that prevented the 
		/// command from conducting the search(such as failure to open the image file 
		/// or a badly formatted option).
		/// <param name="intr"></param>
		/// <param name="imgCode"></param>
		/// <param name="topLeft"></param>
		/// <param name="botRight"></param>
		/// <returns></returns>
		public static ImageSearchResult ImageSearch(Interactor intr, string imgCode, Point topLeft, Point botRight) {
			string imageFileName;

			if (!intr.ClientSettings.TryGetSetting(imgCode + "_ImageFile", "SearchRectanglesAnd_ImageFiles", out imageFileName)) {
				imageFileName = imgCode + ".png";
				intr.ClientSettings.SaveSetting(imageFileName, imgCode + "_ImageFile", "SearchRectanglesAnd_ImageFiles");
			}

			string imageFilePath;

			if (File.Exists(Settings.Default.ImagesFolderPath + "\\" + imageFileName)) {
				imageFilePath = Settings.Default.ImagesFolderPath + "\\" + imageFileName;
			} else {
				imageFilePath = SettingsForm.ProgramRootFolder + SettingsForm.BUILTIN_IMAGES_SUBPATH + "\\" + imageFileName;
			}

			intr.Log(new LogMessage(LogEntryType.Debug, "ImageSearch({0}): Searching for image: '{1}'"
				+ " [TopLeft:{2} BotRight:{3}]",
				imgCode,
				imageFilePath,
				topLeft,
				botRight
			));

			int outX = 0;
			int outY = 0;
			int errorLevel = 0;

			var imgSrcOptions = Settings.Default.ImageShadeVariation.ToString();

			intr.SetVar(OUTPUT_VAR_X, outX.ToString());
			intr.SetVar(OUTPUT_VAR_Y, outY.ToString());

			var statement = string.Format("ImageSearch, {0}, {1}, {2}, {3}, {4}, {5}, {6} {7}",
				 OUTPUT_VAR_X, OUTPUT_VAR_Y, topLeft.X.ToString(), topLeft.Y.ToString(), 
				 botRight.X.ToString(), botRight.Y.ToString(), "*" + imgSrcOptions, imageFilePath);

			intr.Wait(20);
			intr.ExecuteStatement(statement);

			int.TryParse(intr.GetVar(OUTPUT_VAR_X), out outX);
			int.TryParse(intr.GetVar(OUTPUT_VAR_Y), out outY);
			int.TryParse(intr.GetVar(ERROR_LEVEL), out errorLevel);			

			switch (errorLevel) {
				case 0:
					intr.Log(LogEntryType.Trace, "ImageSearch({0}): Found.", imgCode);
					return new ImageSearchResult(true, new Point(outX, outY));
				case 1:
					intr.Log(LogEntryType.Trace, "ImageSearch({0}): Not Found.", imgCode);
					return new ImageSearchResult(false, new Point(outX, outY));
				case 2:
					intr.Log(new LogMessage(
							"ImageSearch(" + imgCode + "): Results: "
							+ " OutputVarX:" + intr.GetVar(OUTPUT_VAR_X)
							+ " OutputVarY:" + intr.GetVar(OUTPUT_VAR_Y)
							+ " ErrorLevel:" + intr.GetVar(ERROR_LEVEL),
							LogEntryType.Debug					
					));
					intr.Log(LogEntryType.Fatal, "ImageSearch(" + imgCode + "): FATAL ERROR. INVALID IMAGE FILE OR OPTION FORMAT. " +
						"(Path: " + imageFilePath + ")");
					return new ImageSearchResult(false, new Point(outX, outY));
				default:
					intr.Log(LogEntryType.Fatal, "ImageSearch(" + imgCode + "): Not Found.");
					return new ImageSearchResult(false, new Point(outX, outY));
			}
		}

		public static ImageSearchResult ImageSearch(Interactor intr, List<string> imgCodes, Point topLeft, Point botRight) {
			foreach (var imgCode in imgCodes) {
				var res = ImageSearch(intr, imgCode, topLeft, botRight);

				if (res.Found) {
					return res;
				}
			}
			return new ImageSearchResult();
		}

		public static ImageSearchResult ImageSearch(Interactor intr, string imgCode) {
			int scrWidth;
			int scrHeight;
			bool success = int.TryParse(intr.GetVar("A_ScreenWidth"), out scrWidth);
			success &= int.TryParse(intr.GetVar("A_ScreenHeight"), out scrHeight);

			if (success) {
				return ImageSearch(intr, imgCode, new Point(0, 0), new Point(scrWidth, scrHeight));
			} else {
				return new ImageSearchResult();
			}
		}

		public static ImageSearchResult ImageSearch(Interactor intr, List<string> imgCodes) {
			foreach (var imgCode in imgCodes) {
				var res = ImageSearch(intr, imgCode);
				if (res.Found) {
					return res;
				}
			}
			return new ImageSearchResult();
		}
	}

	public class ImageSearchResult {
		public Point Point;
		public bool Found;

		public ImageSearchResult(bool found, Point point) {
			Found = found;
			Point = point;
		}

		public ImageSearchResult() {
			Found = false;
			Point = new Point(0, 0);
		}
	}

	class ProblemConductingImageSearchException : Exception {
		public ProblemConductingImageSearchException() : base("There was a problem that prevented ImageSearch"  
			+ " from conducting the search(such as failure to open the image file or a badly formatted option)") { }
		public ProblemConductingImageSearchException(string message) : base(message) { }
		public ProblemConductingImageSearchException(string message, Exception inner) : base(message, inner) { }
	}
}
