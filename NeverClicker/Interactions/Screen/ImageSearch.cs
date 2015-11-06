using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NeverClicker.Properties;
//using AForge.Imaging;

namespace NeverClicker.Interactions {
	public static partial class Screen {
		public const string OUTPUT_VAR_X = "OutputVarX";
		public const string OUTPUT_VAR_Y = "OutputVarY";
		public const string ERROR_LEVEL = "ErrorLevel";
		//public const string OPTIONS = "*40";

		//public static Point ImageSearchAndClick(Interactor intr, string imgCode) {
		//	return new Point(0, 0);
		//}

		public static ImageSearchResult ImageSearch(Interactor intr, string imgCode) {
			int scrWidth;
			int scrHeight;
			bool success;
			success = int.TryParse(intr.GetVar("A_ScreenWidth"), out scrWidth);
			success &= int.TryParse(intr.GetVar("A_ScreenHeight"), out scrHeight);

			if (success) {
				return ImageSearch(intr, imgCode, new Point(0, 0), new Point(scrWidth, scrHeight));
			} else {
				return new ImageSearchResult() { Found = false, Point = new Point(0, 0) };
			}
		}

		public static ImageSearchResult ImageSearch(Interactor intr, string imgCode, Point topLeft, Point botRight) {
			//ImageSearch, ImgX, ImgY, 1, 1, 1920, 1080, *40 % image_file %
			string imageFileName;
			//var imageFileName = intr.GameClient.GetSettingOrEmpty(imgCode + "_ImageFile", "SearchRectanglesAnd_ImageFiles");

			if (!intr.GameClient.TryGetSetting(imgCode + "_ImageFile", "SearchRectanglesAnd_ImageFiles", out imageFileName)) {
				//intr.Log("Image code prefix '" + imgCode + "' not found in settings ini file. Creating.", LogEntryType.Debug);
				imageFileName = imgCode + ".png";
				intr.GameClient.SaveSetting(imageFileName, imgCode + "_ImageFile", "SearchRectanglesAnd_ImageFiles");
			}

			var imageFilePath = Settings.Default.ImagesFolderPath + "\\" + imageFileName;

			intr.Log(new LogMessage("ImageSearch(" + imgCode + "): Searching for image: '" + imageFilePath + "'"
				+ " [TopLeft:" + topLeft.ToString()
				+ " BotRight:" + botRight.ToString() + "]",
				LogEntryType.Debug			
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

			//intr.Log(new LogMessage(""ImageSearch(" + imgCode + "): Executing: '" + statement + "'", LogEntryType.Detail));

			intr.Wait(20);
			intr.ExecuteStatement(statement);

			int.TryParse(intr.GetVar(OUTPUT_VAR_X), out outX);
			int.TryParse(intr.GetVar(OUTPUT_VAR_Y), out outY);
			int.TryParse(intr.GetVar(ERROR_LEVEL), out errorLevel);

			intr.Log(new LogMessage(
					"ImageSearch(" + imgCode + "): Results: "
					+ " OutputVarX:" + intr.GetVar(OUTPUT_VAR_X)
					+ " OutputVarY:" + intr.GetVar(OUTPUT_VAR_Y)
					+ " ErrorLevel:" + intr.GetVar(ERROR_LEVEL),
					LogEntryType.Debug					
			));

			//try {

			//	outX = int.Parse(intr.GetVar(OUTPUT_VAR_X));
			//	outY = int.Parse(intr.GetVar(OUTPUT_VAR_Y));
			//	errorLevel = int.Parse(intr.GetVar(ERROR_LEVEL));
			//} catch (Exception){
			//	throw new ProblemConductingImageSearchException("ImageSearch Results: "
			//		+ " OutputVarX:" + intr.GetVar(OUTPUT_VAR_X)
			//		+ " OutPutVarY:" + intr.GetVar(OUTPUT_VAR_Y)
			//		+ " ErrorLevel:" + intr.GetVar(ERROR_LEVEL)
			//	);
			//	//return new FindResult() { Found = false, At = new Point(0, 0) };
			//}

			switch (errorLevel) {
				case 0:
					intr.Log("ImageSearch(" + imgCode + "): Found.", LogEntryType.Debug);
					return new ImageSearchResult() { Found = true, Point = new Point(outX, outY) };
				case 1:
					intr.Log("ImageSearch(" + imgCode + "): Not Found.", LogEntryType.Debug);
					return new ImageSearchResult() { Found = false, Point = new Point(outX, outY) };				
				case 2:
					intr.Log("ImageSearch(" + imgCode + "): FATAL ERROR. UNABLE TO FIND IMAGE OR BAD OPTION FORMAT.", LogEntryType.Fatal);
					return new ImageSearchResult() { Found = false, Point = new Point(outX, outY) };
				default:
					intr.Log("ImageSearch(" + imgCode + "): Not Found.", LogEntryType.Fatal);
					return new ImageSearchResult() { Found = false, Point = new Point(outX, outY) };
					//throw new ProblemConductingImageSearchException();
			}

		}




	//	public static ImageSearchResult ImageSearchNew(Interactor intr, string imgCode) {
	//		var imageFileName = intr.GameClient.GetSetting(imgCode + "_ImageFile", "SearchRectanglesAnd_ImageFiles");

	//		if (string.IsNullOrWhiteSpace(imageFileName)) {
	//			intr.Log("Image code prefix '" + imgCode + "' not found in settings ini file.", LogEntryType.Error);
	//			return new ImageSearchResult() { Found = false, Point = new Point(0, 0) };
	//		}

	//		var imageFilePath = Settings.Default.ImagesFolderPath + "\\" + imageFileName;

	//		intr.Log(new LogMessage("ImageSearchNew(" + imgCode + "): Searching for image: '" + imageFilePath,
	//			LogEntryType.Info		
	//		));		


	//		ScreenCapture sc = new ScreenCapture();

	//		var image1 = (Bitmap)sc.CaptureScreen();
	//		var image2 = new Bitmap(imageFilePath);
						
	//		var newBitmap1 = ImageCompare.ImageComparer.ChangePixelFormat(new Bitmap(image1), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
	//		var newBitmap2 = ImageCompare.ImageComparer.ChangePixelFormat(new Bitmap(image2), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

	//		var similarityThreshold = 0.950f;
	//		var compareLevel = 0.950f;

	//		var tm = new ExhaustiveTemplateMatching(similarityThreshold);
	//		//var tm = new ExhaustiveBlockMatching(8, 12);

	//		// Process the images
	//		var results = tm.ProcessImage(newBitmap1, newBitmap2);

	//		// Compare the results, 0 indicates no match so return false
	//		if (results.Length <= 0) {
	//			return new ImageSearchResult() { Found = false, Point = new Point(0, 0) };
	//		}

	//		// Return true if similarity score is equal or greater than the comparison level
	//		var match = results[0].Similarity >= compareLevel;

	//		return new ImageSearchResult() { Found = true, Point = new Point(0, 0) };
	//	}



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