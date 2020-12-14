using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;
using System;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class LoginController : ControllerBase
	{
		private CustomerBLL customerService = new CustomerBLL();
		private StaffBLL staffService = new StaffBLL();

		[HttpPost]
		[Route("customer")]
		public ActionResult<Person> LoginCustomer(Person customer)
		{
			Person authorizedCustomer = customerService.Login(customer);

			if (authorizedCustomer != null)
			{
				Response.Cookies.Append("UserId", authorizedCustomer.Id.ToString());
				return authorizedCustomer;
			}

			return Unauthorized();
		}

		[HttpPost]
		[Route("staff")]
		public ActionResult<Staff> LoginStaff(Staff staff)
		{
			Staff authorizedStaff = staffService.Login(staff);

			if (authorizedStaff != null)
			{
				Response.Cookies.Append("StaffId", authorizedStaff.Id.ToString());
				return authorizedStaff;
			}

			return Unauthorized();
		}
	}
}