/*
 * User: good
 * Date: 2/21/2018
 * Time: 5:57 PM
 */
using System;

namespace VNDSConverter{
	public static class Options{
		// 2 is ???
		// 3 is after added sound archive
		// 4 is beta 3ds support
		public static byte writtenVersionNumber=4;

		public static bool detailedConsoleOutput=false;
		public static bool simpleConsoleOutput=true;
		public static bool errorConsoleOutput=true;
		public static bool importantConsoleOutput=true;
		
		public static bool canConvertAudio=false;

		public static bool canUseFFmpeg=false;
		public static bool canInfiniteProcess=false;

		// Should the old conversion folder be deleted before trying to convert the game?
		public static bool autoDelete=true;

		/////////////////////////////////////////////////////////////////////////////////////

		public static string platformName=null;

		// Image dimensions will be rounded up to the nearest multiple of this number
		public static int imageRoundUpWidth=100;
		public static int imageRoundUpHeight=100;
		public static bool doImageRounding=true;

		public static bool resizeImages=false;
		public static int targetWidth;
		public static int targetHeight;
		// Usage with resizeImages on is not supported for bustshots
		public static double resizeRatio=1.0;

		/////////////////////////////////////////////////////////////////////////////////////
		public static string[] possiblePlatforms={"PSVITA","3DS"};
		public static void applyPlatformPresent(string _platformName){
			platformName = _platformName;
			if (_platformName=="PSVITA"){
				doImageRounding=true;
				resizeImages=false;
			}else if (_platformName=="3DS"){
				doImageRounding=false;
				resizeImages=true;
				targetWidth=400;
				targetHeight=240;
			}else{
				throw new ArgumentException("Bad platform string: "+_platformName);
			}
		}
	}
}
