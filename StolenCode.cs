/*
 * User: good
 * Date: 2/21/2018
 * Time: 5:48 PM
 */
using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace VNDSConverter
{
	public static class StolenCode
	{
		static byte cachedIsRunningMono = 3;
		public static bool IsRunningOnMono(){
			if (cachedIsRunningMono==3){
				cachedIsRunningMono = (byte)((Type.GetType ("Mono.Runtime") != null)==true ? 1 : 0);
			}
			if (cachedIsRunningMono==1){
				return true;
			}else{
				return false;
			}
		}
		// https://stackoverflow.com/a/14795752
		public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite){
			if (!overwrite)
			{
				archive.ExtractToDirectory(destinationDirectoryName);
				return;
			}
			foreach (ZipArchiveEntry file in archive.Entries)
			{
				string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
				string directory = Path.GetDirectoryName(completeFileName);
	
				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);
	
				if (file.Name != "")
					file.ExtractToFile(completeFileName, true);
			}
		}
		
	}
}
