using System;
using System.Drawing;
using System.IO;
using AForge.Imaging;

namespace NeverClicker.ImageCompare {
	/// <summary>
	/// Image comparison class to match and rate if bitmapped images are similar.
	/// </summary>
	public static class ImageComparer {
		// The file extension for the generated Bitmap files
		private const string BitMapExtension = ".bmp";

		/// <summary>
		/// Compares the images.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="targetImage">The target image.</param>
		/// <param name="compareLevel">The compare level.</param>
		/// <param name="filepath">The filepath.</param>
		/// <param name="similarityThreshold">The similarity threshold.</param>
		/// <returns>Boolean result</returns>
		public static Boolean CompareImages(string image, string targetImage, double compareLevel, string filepath, float similarityThreshold) {
			// Load images into bitmaps
			var imageOne = new Bitmap(image);
			var imageTwo = new Bitmap(targetImage);

			var newBitmap1 = ChangePixelFormat(new Bitmap(imageOne), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			var newBitmap2 = ChangePixelFormat(new Bitmap(imageTwo), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			newBitmap1 = SaveBitmapToFile(newBitmap1, filepath, image, BitMapExtension);
			newBitmap2 = SaveBitmapToFile(newBitmap2, filepath, targetImage, BitMapExtension);

			// Setup the AForge library
			var tm = new ExhaustiveTemplateMatching(similarityThreshold);

			// Process the images
			var results = tm.ProcessImage(newBitmap1, newBitmap2);

			// Compare the results, 0 indicates no match so return false
			if (results.Length <= 0) {
				return false;
			}

			// Return true if similarity score is equal or greater than the comparison level
			var match = results[0].Similarity >= compareLevel;

			return match;
		}

		/// <summary>
		/// Saves the bitmap automatic file.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="filepath">The filepath.</param>
		/// <param name="name">The name.</param>
		/// <param name="extension">The extension.</param>
		/// <returns>Bitmap image</returns>
		private static Bitmap SaveBitmapToFile(Bitmap image, string filepath, string name, string extension) {
			var savePath = string.Concat(filepath, "\\", Path.GetFileNameWithoutExtension(name), extension);

			image.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);

			return image;
		}

		/// <summary>
		/// Change the pixel format of the bitmap image
		/// </summary>
		/// <param name="inputImage">Bitmapped image</param>
		/// <param name="newFormat">Bitmap format - 24bpp</param>
		/// <returns>Bitmap image</returns>
		private static Bitmap ChangePixelFormat(Bitmap inputImage, System.Drawing.Imaging.PixelFormat newFormat) {
			return (inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), newFormat));
		}
	}


	//static class ImageComparerTester {
	//	static void Main() {
	//		// Filepath to the image directory
	//		const string dirpath = @"C:\Temp\";

	//		// The threshold is the minimal acceptable similarity between template candidate. 
	//		// Min (loose) is 0.0 Max (strict) is 1.0
	//		const float similarityThreshold = 0.50f;

	//		const string testImageOne = @"C:\Temp\ChartImg.png";
	//		const string testImageTwo = @"C:\Temp\ChartPic_000001.png";
	//		const string testImageThree = @"C:\Temp\ChartPic_000002.png";

	//		// Comparison level is initially set to 0.95
	//		// Increment loop in steps of .01
	//		for (var compareLevel = 0.95; compareLevel <= 1.00; compareLevel += 0.01) {
	//			// Run the tests
	//			var testOne = ImageComparer.CompareImages(testImageOne, testImageOne, compareLevel, dirpath, similarityThreshold);
	//			var testTwo = ImageComparer.CompareImages(testImageOne, testImageTwo, compareLevel, dirpath, similarityThreshold);
	//			var testThree = ImageComparer.CompareImages(testImageOne, testImageThree, compareLevel, dirpath, similarityThreshold);
	//			var testFour = ImageComparer.CompareImages(testImageTwo, testImageThree, compareLevel, dirpath, similarityThreshold);

	//			// Output the results
	//			Console.WriteLine("Test images for similarities at compareLevel: {0}", compareLevel);
	//			Console.WriteLine("Results for Image 1 compared to Image 1 - Expected: True : Actual {0}", testOne);
	//			Console.WriteLine("Results for Image 1 compared to Image 2 - Expected: True : Actual {0}", testTwo);
	//			Console.WriteLine("Results for Image 1 compared to Image 3 - Expected: False : Actual {0}", testThree);
	//			Console.WriteLine("Results for Image 2 compared to Image 3 - Expected: False : Actual {0}", testFour);
	//		}

	//		Console.WriteLine("End of comparison.");
	//		Console.ReadKey();
	//	}
	//}
}
