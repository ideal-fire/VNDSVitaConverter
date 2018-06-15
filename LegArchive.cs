using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
namespace VNDSConverter{
	class LegArchive{
		BinaryWriter myBinaryWriter;
		const byte ARCHIVEVERSION=1;
		readonly byte[] MAGICNUMBERS = {76,69,71,65,82,67,72}; // LEGARCH
		readonly byte[] ENDTABLEIDENTIFICATION = {76,69,71,65,82,67,72,84,66,76}; // LEGARCHTBL
		int filenamePrefixLength;
		List<string> filenameList;
		List<long> filePositionList;
		public LegArchive(string _filename, string _prefix){
			filePositionList = new List<long>();
			filenameList = new List<string>();
			myBinaryWriter = new BinaryWriter(new FileStream(_filename,FileMode.Create)); 
			myBinaryWriter.Write(MAGICNUMBERS);
			myBinaryWriter.Write(ARCHIVEVERSION);
			filenamePrefixLength = _prefix.Length;
		}
		// Pass absolute filenames
		public void addFile(string _filename){
			myBinaryWriter.BaseStream.Flush();
			//Console.Out.WriteLine("Positin is "+myBinaryWriter.BaseStream.Position);
			filePositionList.Add(myBinaryWriter.BaseStream.Position);
			filenameList.Add(_filename.Substring(filenamePrefixLength));
			//Console.Out.WriteLine(""+File.ReadAllBytes(_filename).Length);
			myBinaryWriter.Write(File.ReadAllBytes(_filename));
		}
	
		private void writeCString(BinaryWriter _passedWriter,string _stringToWrite){
			_passedWriter.Write(ASCIIEncoding.ASCII.GetBytes(_stringToWrite));
			_passedWriter.Write((byte)0);
		}
	
		// special/title.mp3 is what it says
		public void finish(){
			myBinaryWriter.Write(ENDTABLEIDENTIFICATION);
			myBinaryWriter.Write((int)filenameList.Count);
			for (int i=0;i<filenameList.Count;++i){
				writeCString(myBinaryWriter,filenameList[i]);
				myBinaryWriter.Write(filePositionList[i]); // long
			}
			myBinaryWriter.Close();
			myBinaryWriter.Dispose();
		}
	}
}