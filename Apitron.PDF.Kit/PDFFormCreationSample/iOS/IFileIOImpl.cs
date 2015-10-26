using System;
using PDFFormCreationSample;
using Xamarin.Forms;
using PDFFormCreationSample.iOS;

[assembly:Xamarin.Forms.Dependency(typeof(IFileIOImpl))]

namespace PDFFormCreationSample.iOS
{
	public class IFileIOImpl:IFileIO
	{
		public IFileIOImpl ()
		{
		}

		#region IFileIO implementation

		public System.IO.Stream CreateFile (string path)
		{
			return System.IO.File.Create (path);
		}

		public string GetMyDocumentsPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}

		#endregion
	}
}

