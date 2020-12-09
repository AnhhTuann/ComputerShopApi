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
				return BadRequest();
			}

			return id;
		}
	}
}