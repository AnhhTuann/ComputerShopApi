using Microsoft.AspNetCore.Mvc;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ImageController : ControllerBase
	{
		private ImageBLL service = new ImageBLL();

		[HttpGet]
		[Route("{name}")]
		public FileContentResult Get(string name)
		{
			return File(service.Get(name), "image/png");
		}
	}
}