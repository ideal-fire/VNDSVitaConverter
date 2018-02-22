/*
 * User: good
 * Date: 2/21/2018
 * Time: 5:48 PM
 */
using System;
using System.IO;
using System.IO.Compression;

namespace VNDSConverter
{
	public static class StolenCode
	{
		// https://stackoverflow.com/a/8022011
		public static void copyDirectory(string source_dir, string destination_dir){
			if (Options.simpleConsoleOutput){
				Console.Out.WriteLine("[COPY] {0} to {1}",source_dir,destination_dir);
			}
			if (!System.IO.Directory.Exists(source_dir)){
				return;
			}
			foreach (string dir in System.IO.Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories)){
				System.IO.Directory.CreateDirectory(System.IO.Path.Combine(destination_dir, dir.Substring(source_dir.Length)));
				// Example:
				//     > C:\sources (and not C:\E:\sources)
			}
			foreach (string file_name in System.IO.Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories)){
				if (Options.detailedConsoleOutput){
					Console.Out.WriteLine("[Copy] {0} to {1}",file_name,System.IO.Path.Combine(destination_dir, file_name.Substring(source_dir.Length)));
				}
    			System.IO.File.Copy(file_name, System.IO.Path.Combine(destination_dir, file_name.Substring(source_dir.Length)));
    		}
		}
		
		public static bool IsRunningOnMono(){
			return Type.GetType ("Mono.Runtime") != null;
		}
		
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
