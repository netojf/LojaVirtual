using System;

namespace LojaVirtual.Tools
{
	public static class FileTools
	{
		public static string GetExtensionByName(string name)
		{
			int extensionIndex = name.IndexOf('.') + 1;
			return name.Remove(0, extensionIndex);
		}

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
