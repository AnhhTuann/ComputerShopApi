using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class StaffController : ControllerBase
	{
		private StaffBLL service = new StaffBLL();

		[HttpGet]
		public IEnumerable<Staff> GetAll()
		{
			return service.GetAll();
		}

		[HttpGet]
		[Route("{id}")]
		public Staff GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public ActionResult<int> Create(Staff staff)
		{
			int id = service.Create(staff);

			if (id == -1)
			{
				return Conflict();
			}

			return id;
		}
	}
}