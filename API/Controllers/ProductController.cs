using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DTO;
using BLL;
using System.IO;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ProductController : ControllerBase
	{
		private ProductBLL service = new ProductBLL();
		private ImageBLL imageService = new ImageBLL();

		[HttpGet]
		public IEnumerable<Product> GetAll()
		{
			return service.GetAll();
		}

		[HttpPost]
		public int Create([FromForm] Product product, [FromForm] IFormFile image)
		{
			int productId = service.Create(product);
			byte[] file;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.CopyTo(memoryStream);
				file = memoryStream.ToArray();
			}

			product.Image = $"img_{productId}_{product.Name}";
			imageService.UploadImage(file, product.Image);

			return productId;
		}
	}
}