using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ImportController : ControllerBase
	{
		private ImportBLL service = new ImportBLL();

		[HttpGet]
		public IEnumerable<Ticket> GetAll([FromQuery] int contain)
		{
			return service.GetAll(contain);
		}

		[HttpGet]
		[Route("{id}")]
		public Ticket GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public int Create(Ticket ticket)
		{
			return service.Create(ticket);
		}
	}
}