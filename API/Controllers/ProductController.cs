using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private ProductBLL service = new ProductBLL();

		[HttpGet]
		public IEnumerable<Product> GetAll()
		{
			return service.GetAll();
		}

		[HttpPost]
		public int Create(Product product)
		{
			return service.Create(product);
		}
	}
}