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
		private ProductBLL productService = new ProductBLL();
		private ImageBLL imageService = new ImageBLL();

		[HttpGet]
		public IEnumerable<Product> GetAll([FromQuery] string search, [FromQuery] int category, [FromQuery] int minPrice, [FromQuery] int maxPrice)
		{
			return productService.GetAll(search, category, minPrice, maxPrice);
		}

		[HttpGet]
		[Route("{id}")]
		public Product GetById(int id)
		{
			return productService.GetById(id);
		}

		[HttpPost]
		public int Create([FromForm] Product product, [FromForm] IFormFile productImage)
		{
			byte[] file;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				productImage.CopyTo(memoryStream);
				file = memoryStream.ToArray();
			}

			product.Image = System.Guid.NewGuid().ToString();
			imageService.Upload(file, product.Image);

			return productService.Create(product);
		}

		[HttpPut]
		public void Update(Product product)
		{
			productService.Update(product);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			productService.Delete(id);
		}
	}
}