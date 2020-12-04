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
	public class StorageController : ControllerBase
	{
		private ProductBLL productService = new ProductBLL();
		private StorageBLL storageService = new StorageBLL();

		[HttpGet]
		public IEnumerable<Storage> GetAll()
		{
			return storageService.GetAll();
		}

		[HttpPost]
		public int Create(Storage storage)
		{
			Product product = productService.GetById(storage.Product.Id);
			product.Amount = product.Amount + storage.Import - storage.Export;

			productService.Update(product);
			return storageService.Create(storage); ;
		}
	}
}