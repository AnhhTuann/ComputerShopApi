using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ReceiptController : ControllerBase
	{
		private ReceiptBLL service = new ReceiptBLL();

		[HttpGet]
		public IEnumerable<Receipt> GetAll()
		{
			return service.GetAll();
		}

		[HttpGet]
		[Route("{id}")]
		public Receipt GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public int Create(Receipt receipt)
		{
			return service.Create(receipt);
		}
	}
}