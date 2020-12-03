using System.IO;

namespace DAL
{
	public static class ImageDAL
	{
		public static void UploadImage(byte[] file, string name)
		{
			string path = $"../DAL/Media/{name}";
			using (FileStream fs = File.Create(path))
			{
				fs.Write(file, 0, file.Length);
			}
		}
	}
}
