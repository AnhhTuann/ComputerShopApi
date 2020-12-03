using System.IO;

namespace DAL
{
	public static class ImageDAL
	{
		private static string basePath = "../DAL/Media/";

		public static byte[] Get(string name)
		{
			string path = $"{basePath}{name}";
			return File.ReadAllBytes(path);
		}
		public static void Upload(byte[] file, string name)
		{
			string path = $"{basePath}{name}";
			using (FileStream fs = File.Create(path))
			{
				fs.Write(file, 0, file.Length);
			}
		}
	}
}
