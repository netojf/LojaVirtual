using System;

namespace LojaVirtual.Tools
{
	public static class FileTools
	{
		/// <summary>
		/// Get extension of a file
		/// *The name of file must contain the extension
		/// </summary>
		/// <param name="name">File Full Name With Extension</param>
		/// <returns>The extension in provided <paramref name="name"/></returns>
		public static string GetExtensionByName(string name)
		{
			int extensionIndex = name.IndexOf('.') + 1;
			return name.Remove(0, extensionIndex);
		}

		/// <summary>
		/// Get a String64 of a image for src attribute in tag
		/// <paramref name="extension"/> Must have the extension, Ex: .jpg
		/// </summary>
		/// <param name="data">Image Data in Bytes</param>
		/// <param name="extension">Extension of image</param>
		/// <returns>A full src attribute content of a image in string64</returns>
		public static string GetImageSrc64String(byte[] data, string extension)
		{
			try
			{
				return new string("data:image/" + extension + ";base64," + Convert.ToBase64String(data));

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
