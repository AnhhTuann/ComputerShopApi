using DAL;

namespace BLL
{
	public class ImageBLL
	{
		public void UploadImage(byte[] file, string name)
		{
			ImageDAL.UploadImage(file, name);
		}
	}
}