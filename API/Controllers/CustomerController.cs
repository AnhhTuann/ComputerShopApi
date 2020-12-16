using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

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
		public ActionResult ChangePassword([FromBody] int customerId, [FromBody] string oldPassword, [FromBody] string newPassword)
		{
			if (customerService.ChagePassword(customerId, oldPassword, newPassword))
			{
				return Ok();
			}

			return Unauthorized();
		}
	}
}