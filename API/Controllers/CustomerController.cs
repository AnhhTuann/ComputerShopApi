using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;
using API.Models;
using System;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private CustomerBLL customerService = new CustomerBLL();

		[HttpGet]
		public IEnumerable<Person> GetAll()
		{
			return customerService.GetAll();
		}

		[HttpGet]
		[Route("{id}")]
		public Person GetById(int id)
		{
			return customerService.GetById(id);
		}

		[HttpPost]
		public ActionResult<int> Create(Person customer)
		{
			int id = customerService.Create(customer);

			if (id == -1)
			{
				return Conflict();
			}

			return id;
		}

		[HttpPut]
		public void Update(Person customer)
		{
			customerService.Update(customer);
		}

		[HttpPut]
		[Route("password")]
		public ActionResult ChangePassword(ChangePasswordInfo info)
		{
			string customerId = Request.Cookies["UserId"];
			if (customerService.ChagePassword(Int32.Parse(customerId), info.OldPassword, info.NewPassword))
			{
				return Ok();
			}

			return Unauthorized();
		}
	}
}