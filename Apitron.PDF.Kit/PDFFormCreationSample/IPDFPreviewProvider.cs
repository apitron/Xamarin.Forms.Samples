using System;

namespace PDFFormCreationSample
{
	public interface IPDFPreviewProvider
	{
		void TriggerPreview(string path);
	}
}

