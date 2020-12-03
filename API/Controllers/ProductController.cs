using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
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
		public IEnumerable<Product> GetAll()
		{
			return productService.GetAll();
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
			imageService.UploadImage(file, product.Image);

			return productService.Create(product);;
		}
	}
}