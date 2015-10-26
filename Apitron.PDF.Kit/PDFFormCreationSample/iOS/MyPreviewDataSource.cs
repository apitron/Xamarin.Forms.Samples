using System;
using System.IO;
using QuickLook;
using Foundation;
using UIKit;

namespace PDFFormCreationSample.iOS
{
	class MyPreviewDataSource:QLPreviewControllerDataSource
	{
		FilePreviewItem targetItem;

		public MyPreviewDataSource (string filePath)
		{
			if (string.IsNullOrEmpty (filePath))
			{
				throw new ArgumentException ("Target path can't be null or empty");
			}

			targetItem = new FilePreviewItem(Path.GetFileName(filePath),NSUrl.FromFilename (filePath));
		}

		public override nint PreviewItemCount (QLPreviewController controller)
		{
			return 1;
		}

		public override IQLPreviewItem GetPreviewItem (QLPreviewController controller, nint index)
		{
			return targetItem;
		}
	}
}


