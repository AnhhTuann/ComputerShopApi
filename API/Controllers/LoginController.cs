using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;
using API.Components;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class LoginController : ControllerBase
	{
		private CustomerBLL customerService = new CustomerBLL();

		[HttpPost]
		[Route("/customer")]
		public ActionResult<Person> LoginCustomer(Person customer)
		{
			Person authorizedCustomer = customerService.Login(customer);

			if (authorizedCustomer != null)
			{
				ActiveCustomer.trackCustomer(authorizedCustomer);
				return authorizedCustomer;
			}

			return Unauthorized();
		}
	}
}