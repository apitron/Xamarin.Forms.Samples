using System;
using Xamarin.Forms;
using XamarinFormsSample;
using Apitron.PDF.Rasterizer;
using Android.Graphics;
using System.IO;
using XamarinFormsSample.Droid;
using Java.Nio;

[assembly: Dependency(typeof(Renderer))]

namespace XamarinFormsSample.Droid
{
	/// <summary>
	/// Android specific implementation of <see cref="XamarinFormsSample.IRenderer"/> interface.
	/// </summary>
	public class Renderer:IRenderer
	{
		public Renderer ()
		{
		}

		#region IRenderer implementation
		public System.IO.Stream RenderToStream (byte[] documentData, int pageIndex)
		{
			using (MemoryStream ms = new MemoryStream (documentData))
			{
				// open document
				using (Document doc = new Document (ms)) 
				{
					// prepare for rendering
					int width = (int)doc.Pages [pageIndex].Width;
					int height = (int)doc.Pages [pageIndex].Height;
					// render as ints array
					int[] renderedPage = doc.Pages [pageIndex].RenderAsInts (width, height, new Apitron.PDF.Rasterizer.Configuration.RenderingSettings ());

					// create bitmap and save it to stream
					Bitmap bm = Bitmap.CreateBitmap (renderedPage, width, height, Bitmap.Config.Argb8888);

					MemoryStream outputStream = new MemoryStream ();
					bm.Compress (Bitmap.CompressFormat.Png, 100, outputStream);

					outputStream.Position = 0;

					return outputStream;
				}
			}
		}
		#endregion
	}
}

