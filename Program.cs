/*
 * User: good
 * Date: 2/20/2018
 * Time: 7:10 PM
 */
using System;
using System.IO;
using System.Drawing;
// For specific image formats
using System.Drawing.Imaging;
using VNDSConverter;
using System.IO.Compression;
using System.Windows.Forms;

namespace VNDSConverter
{
	class Program
	{
	
		const bool autoDelete=true;
		
		static string changeDirectoryPath(string _newDirectory){
			string _returnString = _newDirectory.TrimEnd(Path.DirectorySeparatorChar);
			string _normalFolderName = Path.GetFileName(_returnString);
			_returnString = Path.GetDirectoryName(_returnString);
			_returnString = _returnString + Path.DirectorySeparatorChar +_normalFolderName+"-converted";
			return _returnString;
		}
		// Path the root directory of the new folder
		static string getNewCGDirectoryA(string _rootDirectory){
			return Path.Combine(_rootDirectory,"CG"+Path.DirectorySeparatorChar);
		}
		static string getNewCGDirectoryB(string _rootDirectory){
			return Path.Combine(_rootDirectory,"CGAlt"+Path.DirectorySeparatorChar);
		}
		static string getNewScriptDirectory(string _rootDirectory){
			return Path.Combine(_rootDirectory,"Scripts"+Path.DirectorySeparatorChar);
		}
		static string getNewAudioDirectory(string _rootDirectory){
			return Path.Combine(_rootDirectory,"SE"+Path.DirectorySeparatorChar);
		}
		
		// Pass the root directory of the old folder
		static string getOldCGDirectoryA(string _rootDirectory){
			return Path.Combine(_rootDirectory,"background"+Path.DirectorySeparatorChar);
		}
		static string getOldCGDirectoryB(string _rootDirectory){
			return Path.Combine(_rootDirectory,"foreground"+Path.DirectorySeparatorChar);
		}
		static string getOldScriptDirectory(string _rootDirectory){
			return Path.Combine(_rootDirectory,"script"+Path.DirectorySeparatorChar);
		}
		static string getOldAudioDirectory(string _rootDirectory){
			return Path.Combine(_rootDirectory,"sound"+Path.DirectorySeparatorChar);
		}
		
		static void createNewDirectories(string _newRootDirectory){
			Directory.CreateDirectory(_newRootDirectory);
			Directory.CreateDirectory(getNewAudioDirectory(_newRootDirectory));
			Directory.CreateDirectory(getNewCGDirectoryA(_newRootDirectory));
			Directory.CreateDirectory(getNewCGDirectoryB(_newRootDirectory));
			Directory.CreateDirectory(getNewScriptDirectory(_newRootDirectory));
		}
		
		static void copyIfExist(string _sourceFile, string _destFile){
			if (File.Exists(_sourceFile)){
				if (Options.detailedConsoleOutput){
					Console.Out.WriteLine("[COPY] {0} to {1}",_sourceFile,_destFile);
				}
				File.Copy(_sourceFile,_destFile);
			}
		}
		
		static void makeBitmap32Bit(ref Bitmap _toFix){
			Bitmap _newImage = new Bitmap(_toFix.Width,_toFix.Height,PixelFormat.Format32bppArgb);
			using (Graphics g = Graphics.FromImage(_newImage))
			{
				g.DrawImageUnscaledAndClipped(_toFix,new Rectangle(0,0,_newImage.Width,_newImage.Height));
			}
			_toFix.Dispose();
			_toFix = _newImage;
		}
		
		// After running this function, there will be copies of the images files with fixed bit depths in the PNG format
		static void processAndCopyImages(string _sourceDirectory, string _destDirectory){
			if (Options.simpleConsoleOutput){
				Console.Out.WriteLine("[IMAGE] {0} to {1}",_sourceDirectory,_destDirectory);
			}
			string[] _filesToProcess = Directory.GetFiles(_sourceDirectory,"*",SearchOption.AllDirectories);
			int _recordWidth=256;
			int _recordHeight=192;
			for (int i=0;i<_filesToProcess.Length;i++){
				string _cachedExtension = Path.GetExtension(_filesToProcess[i]);
				if (_cachedExtension==".png" || _cachedExtension==".jpg" || _cachedExtension==".jpeg"){
					if (Options.detailedConsoleOutput){
						Console.Out.WriteLine("[Image] {0}",_filesToProcess[i]);
					}
					Bitmap _tempLoadedBitmap;
					try{
						_tempLoadedBitmap = new Bitmap(_filesToProcess[i]);
						if (_tempLoadedBitmap.Width>_recordWidth){
							_recordWidth = _tempLoadedBitmap.Width;
						}
						if (_tempLoadedBitmap.Height>_recordHeight){
							_recordHeight = _tempLoadedBitmap.Height;
						}
						makeBitmap32Bit(ref _tempLoadedBitmap);
					}catch(Exception){
						if (Options.importantConsoleOutput){
							Console.Out.WriteLine("[BLACK] Force black image {0}",_filesToProcess[i]);
						}
						_tempLoadedBitmap = new Bitmap(256,192,PixelFormat.Format32bppArgb);
						using (Graphics g = Graphics.FromImage(_tempLoadedBitmap)){
							g.FillRectangle(Brushes.Black,0,0,_tempLoadedBitmap.Width,_tempLoadedBitmap.Height);
						}
					}
					_tempLoadedBitmap.Save(Path.Combine(_destDirectory,Path.GetFileName(_filesToProcess[i]).ToUpper()),ImageFormat.Png);
					/*if (_cachedExtension==".png"){
						_tempLoadedBitmap.Save(Path.Combine(_destDirectory,Path.GetFileName(_filesToProcess[i]).ToUpper()),ImageFormat.Png);
					}else if (_cachedExtension==".jpg" || _cachedExtension==".jpeg"){
						_tempLoadedBitmap.Save(Path.Combine(_destDirectory,Path.GetFileName(_filesToProcess[i]).ToUpper()),ImageFormat.Jpeg);
					}*/
					_tempLoadedBitmap.Dispose();
				}else{
					if (Options.errorConsoleOutput){
						Console.Out.WriteLine("[Skip] Non-image extension {0}.",Path.GetExtension(_filesToProcess[i]));
					}
				}
			}
		}
		
		static void maybeExtractZIPFile(string _zipPath, string _destPath){
			if (File.Exists(_zipPath)){
				using (ZipArchive myZipArchive = ZipFile.OpenRead(_zipPath)){
					if (Options.simpleConsoleOutput){
						Console.Out.WriteLine("[EXTRACT] {0} to {1}",_zipPath,_destPath);
					}
					try{
						myZipArchive.ExtractToDirectory(_destPath,true);
					}catch(Exception e){
						Console.Out.WriteLine("Error extracting ZIP file.");
						Console.Out.WriteLine(e.ToString());
						if (StolenCode.IsRunningOnMono()){
							Console.Out.WriteLine("=============");
							Console.Out.WriteLine("Please make sure Mono is updated if it's not already.");
						}
						printPressAnyKey();
						Console.ReadKey();
						Environment.Exit(1);
					}
				}
				
			}else{
				if (Options.simpleConsoleOutput){
					Console.Out.WriteLine("[NOT EXTRACT] {0} not exist.",_zipPath);
				}
			}
		}
		
		public static string doFunctionality(string _originalGameFolderName){
			string _newGameFolderPath = changeDirectoryPath(_originalGameFolderName);
			if (autoDelete){
				
				if (Directory.Exists(_newGameFolderPath)){
					try{
						if (Options.importantConsoleOutput){
							Console.Out.WriteLine("Attempting to delete old conversion directory...");
						}
						Directory.Delete(_newGameFolderPath,true);
					}catch(Exception e){
						if (Options.errorConsoleOutput){
							Console.Out.WriteLine("{0}\nFailed to delete the old conversion directory. This probably means the program can't write to at least one of the files in the directory, so conversion will probably fail when it tries to overwrite that file.",e.ToString());
						}
					}
				}
			}
			createNewDirectories(_newGameFolderPath);
			
			maybeExtractZIPFile(Path.Combine(_originalGameFolderName,"background.zip"),_originalGameFolderName);
			maybeExtractZIPFile(Path.Combine(_originalGameFolderName,"foreground.zip"),_originalGameFolderName);
			maybeExtractZIPFile(Path.Combine(_originalGameFolderName,"script.zip"),_originalGameFolderName);
			maybeExtractZIPFile(Path.Combine(_originalGameFolderName,"sound.zip"),_originalGameFolderName);
			
			StolenCode.copyDirectory(getOldAudioDirectory(_originalGameFolderName),getNewAudioDirectory(_newGameFolderPath));
			StolenCode.copyDirectory(getOldScriptDirectory(_originalGameFolderName),getNewScriptDirectory(_newGameFolderPath));
			processAndCopyImages(getOldCGDirectoryA(_originalGameFolderName),getNewCGDirectoryA(_newGameFolderPath));
			processAndCopyImages(getOldCGDirectoryB(_originalGameFolderName),getNewCGDirectoryB(_newGameFolderPath));
			if (Options.simpleConsoleOutput){
				Console.Out.WriteLine("[COPY] Assorted root game directory files");
			}
			copyIfExist(Path.Combine(_originalGameFolderName,"default.ttf"),Path.Combine(_newGameFolderPath,"default.ttf"));
			copyIfExist(Path.Combine(_originalGameFolderName,"icon.png"),Path.Combine(_newGameFolderPath,"icon.png"));
			copyIfExist(Path.Combine(_originalGameFolderName,"info.txt"),Path.Combine(_newGameFolderPath,"info.txt"));
			copyIfExist(Path.Combine(_originalGameFolderName,"thumbnail.png"),Path.Combine(_newGameFolderPath,"thumbnail.png"));
			copyIfExist(Path.Combine(_originalGameFolderName,"img.ini"),Path.Combine(_newGameFolderPath,"img.ini"));
			
			if (Options.simpleConsoleOutput){
				Console.Out.WriteLine("[CREATE] {0}",Path.Combine(_newGameFolderPath,"isvnds"));
			}
			File.Create(Path.Combine(_newGameFolderPath,"isvnds")).Dispose();
			
			return _newGameFolderPath;
		}
		
		static void toggleDependingOnArgs(string[] args, ref int i, ref bool _toToggle){
			i++;
			if (args[i]!="off"){
				_toToggle=true;
			}else{
				_toToggle=false;
			}
		}
		
		static void printPressAnyKey(){
			Console.Write("Press any key to continue . . . ");
		}
		
		[STAThread]
		static void Main(string[] args){
			string _sourceFile=null;
			if (args.Length==0){
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				MainForm myMainForm = new MainForm();
				Application.Run(myMainForm);
				_sourceFile = myMainForm.confirmedChosenDirectory;
				if (_sourceFile==null){
					Console.Out.WriteLine("_sourceFile==null");
					return;
				}
			}
			int i;
			for (i=0;i<args.Length;i++){
				if (args[i][0]!='-'){
					_sourceFile = args[i];
				}else{
					if (args[i]=="-simpleoutput"){
						toggleDependingOnArgs(args,ref i, ref Options.simpleConsoleOutput);
					}else if (args[i]=="-detailedoutput"){
						toggleDependingOnArgs(args,ref i, ref Options.detailedConsoleOutput);
					}else if (args[i]=="-erroroutput"){
						toggleDependingOnArgs(args,ref i, ref Options.errorConsoleOutput);
					}else if (args[i]=="-importantoutput"){
						toggleDependingOnArgs(args,ref i, ref Options.importantConsoleOutput);
					}
				}
			}
			if (_sourceFile==null){
				if (Options.importantConsoleOutput){
					Console.Out.WriteLine("No path found. Make sure your path doesn't start with a hyphen.");
					printPressAnyKey();
				}
				Console.ReadKey();
				return;
			}
			if (Options.importantConsoleOutput){
				Console.WriteLine("Hello World!");
			}
			// TODO: Implement Functionality Here
			if (Options.importantConsoleOutput){
				Console.Out.WriteLine("Done, you may close this window.\nThe converted game is at {0}",doFunctionality(_sourceFile));
				printPressAnyKey();
				Console.ReadKey(true);
			}
		}
	}
}