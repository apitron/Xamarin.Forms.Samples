using System;
using System.IO;

namespace PDFFormCreationSample
{
	public interface IFileIO
	{
		Stream CreateFile(string path);
		string GetMyDocumentsPath();
	}
}

