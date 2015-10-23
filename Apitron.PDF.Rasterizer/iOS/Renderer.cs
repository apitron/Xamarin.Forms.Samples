using System;
using Xamarin.Forms;
using XamarinFormsSample;
using Apitron.PDF.Rasterizer;
using System.IO;
using XamarinFormsSample.iOS;
using Apitron.PDF.Rasterizer.Configuration;
using CoreGraphics;
using UIKit;

[assembly: Dependency(typeof(Renderer))]

namespace XamarinFormsSample.iOS
{
	/// <summary>
	/// iOS specific implementation of <see cref="XamarinFormsSample.IRenderer"/> interface.
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
					// render the page to a raw bitmap data represented by byte array
					byte[] imageData = ConvertBGRAtoRGBA(doc.Pages [pageIndex].RenderAsBytes (width,height, new RenderingSettings (), null));

					// create CGDataProvider which will serve CGImage creation
					CGDataProvider dataProvider = new CGDataProvider (imageData, 0, imageData.Length);

					// create core graphics image using data provider created above, note that
					// we use CGImageAlphaInfo.Last(ARGB) pixel format
					CGImage cgImage = new CGImage(width,height,8,32,width*4,CGColorSpace.CreateDeviceRGB(),CGImageAlphaInfo.Last,dataProvider,null,false, CGColorRenderingIntent.Default);

					// create UIImage and save it to gallery
					UIImage finalImage = new UIImage (cgImage);
								
					return finalImage.AsPNG ().AsStream();
				}
			}						
		}

		/// <summary>
		/// Converts the BGRA data to RGBA.
		/// </summary>
		/// <returns>Same byte array but with RGBA color dara.</returns>
		/// <param name="bgraData">Raw bitmap data in BGRA8888 format .</param>
		byte[] ConvertBGRAtoRGBA(byte[] bgraData)
		{
			// implemented simple conversion, swap 2 bytes.
			byte tmp;

			for(int i=0,k=2;i<bgraData.Length;i+=4,k+=4)
			{
				tmp = bgraData [i];
				bgraData [i] = bgraData [k];
				bgraData [k] = tmp;
			}

			return bgraData;
		}

		#endregion
	}
}

