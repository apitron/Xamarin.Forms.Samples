using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.IO;
using System.Reflection;

namespace XamarinFormsSample
{
	public partial class MyPage : ContentPage
	{
		public MyPage ()
		{
			InitializeComponent ();
		}

		void OnRenderPdfClicked(object sender, EventArgs args)
		{
			Assembly currentAssembly = typeof(MyPage).GetTypeInfo ().Assembly;

			using (Stream resourceStream = currentAssembly.GetManifestResourceStream ("XamarinFormsSample.Data.testfile.pdf")) 
			{				
				byte[] buffer = new byte[resourceStream.Length];
				resourceStream.Read (buffer, 0, buffer.Length);

				var renderer = DependencyService.Get<IRenderer>();
				myImage.Source = ImageSource.FromStream (()=>
					{						
						return renderer.RenderToStream(buffer,0);
					});
			}				
		}
	}
}

