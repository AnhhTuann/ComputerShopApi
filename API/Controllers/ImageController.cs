using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BLL;
using System.IO;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ImageController : ControllerBase
	{
		private ImageBLL service = new ImageBLL();

		[HttpPost]
		public void UploadImage([FromForm] IFormFile image)
		{
			byte[] file;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.CopyTo(memoryStream);
				file = memoryStream.ToArray();
			}

			service.UploadImage(file, "test");
		}
	}
}