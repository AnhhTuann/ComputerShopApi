using DAL;

namespace BLL
{
	public class ImageBLL
	{
		public byte[] Get(string name)
		{
			return ImageDAL.Get(name);
		}

		public void Upload(byte[] file, string name)
		{
			ImageDAL.Upload(file, name);
		}
	}
}