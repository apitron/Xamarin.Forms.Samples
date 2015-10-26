using System;
using QuickLook;
using Xamarin.Forms;
using PDFFormCreationSample;
using UIKit;
using PDFFormCreationSample.iOS;

[assembly:Dependency(typeof(PDFPreviewProvider))]

namespace PDFFormCreationSample.iOS
{
	public class PDFPreviewProvider:IPDFPreviewProvider
	{
		public PDFPreviewProvider ()
		{
		}

		#region IPDFPreviewProvider implementation

		public void TriggerPreview (string path)
		{
			QLPreviewController previewController = new QLPreviewController ();
			previewController.DataSource = new MyPreviewDataSource (path);

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(previewController,true, null);
		}

		#endregion
	}
}

