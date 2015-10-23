using System;
using System.IO;

namespace XamarinFormsSample
{
	/// <summary>
	/// Renderer interface to be implemented in platform specific implementations.
	/// </summary>
	public interface IRenderer
	{
		/// <summary>
		/// Renders PDF data to an an image stream.
		/// </summary>
		/// <returns>Stream that contains image data.</returns>
		/// <param name="documentData">PDF document.</param>
		/// <param name="page">Page index</param>
		Stream RenderToStream(byte[] documentData, int pageIndex);
	}
}

