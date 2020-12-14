using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;
using System;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ReceiptController : ControllerBase
	{
		private ReceiptBLL receiptService = new ReceiptBLL();
		private CustomerBLL customerService = new CustomerBLL();

		[HttpGet]
		public IEnumerable<Receipt> GetAll([FromQuery] int customerId)
		{
			return receiptService.GetAll(customerId);
		}

		[HttpGet]
		[Route("{id}")]
		public Receipt GetById(int id)
		{
			return receiptService.GetById(id);
		}

		[HttpPost]
		public ActionResult<int> Create(Receipt receipt)
		{
			string userId = Request.Cookies["UserId"];

			if (userId == null) return Unauthorized();

			Person customer = customerService.GetById(Int32.Parse(userId));
			receipt.Customer = customer;
			return receiptService.Create(receipt);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			receiptService.Delete(id);
		}

		[HttpPut]
		public ActionResult Update(Receipt receipt)
		{
			string staffId = Request.Cookies["StaffId"];
			string customerId = Request.Cookies["UserId"];

			if (staffId == null && customerId == null)
			{
				return Unauthorized();
			}

			if ((staffId != null && receiptService.Update(receipt, Int32.Parse(staffId))) || (customerId != null && receiptService.Update(receipt)))
			{
				return Ok();
			}

			return BadRequest();
		}
	}
}