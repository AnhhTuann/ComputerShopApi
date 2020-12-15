using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;
using System;
using API.Components;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ReceiptController : ControllerBase
	{
		private ReceiptBLL receiptService = new ReceiptBLL();
		private CustomerBLL customerService = new CustomerBLL();

		[HttpGet]
		public IEnumerable<Receipt> GetAll()
		{
			return receiptService.GetAll();
		}

		[HttpGet]
		[Route("{id}")]
		public ActionResult<Receipt> GetById(int id)
		{
			Receipt receipt = receiptService.GetById(id);

			if (receipt == null)
			{
				return NotFound();
			}

			return receipt;
		}

		[HttpPost]
		public ActionResult<int> Create(Receipt receipt)
		{
			string userId = Request.Cookies["UserId"];

			if (userId == null || !ActiveCustomer.contain(Int32.Parse(userId))) return Unauthorized();

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

			if (staffId == null)
			{
				return Unauthorized();
			}

			if (receiptService.Update(receipt, Int32.Parse(staffId)))
			{
				return Ok();
			}

			return BadRequest();
		}
	}
}