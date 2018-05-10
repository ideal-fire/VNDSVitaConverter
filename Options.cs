/*
 * User: good
 * Date: 2/21/2018
 * Time: 5:57 PM
 */
using System;

namespace VNDSConverter
{
	public static class Options
	{
		public static bool detailedConsoleOutput=false;
		public static bool simpleConsoleOutput=true;
		public static bool errorConsoleOutput=true;
		public static bool importantConsoleOutput=true;
		
		public static bool canConvertAudio=false;

		public static bool canUseFFmpeg=false;
		public static bool canInfiniteProcess=false;

		public static byte writtenVersionNumber=2;
		
		// Image dimensions will be rounded up to the nearest multiple of this number
		public static int imageRoundUpWidth=100;
		public static int imageRoundUpHeight=100;

		//public const string[] possibleFreacLocations = new string[]{"./putwindowexehere","./freac-1.1-alpha-20180306-linux-64/freac","./freac-1.1-alpha-20180306-linux/freac"}
		//public static string actualFreacLocation;

		// Should the old conversion folder be deleted before trying to convert the game?
		public static bool autoDelete=true;
	}
}
