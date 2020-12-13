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
		public IEnumerable<Import> GetAll([FromQuery] int contain)
		{
			return service.GetAll(contain);
		}

		[HttpGet]
		[Route("{id}")]
		public Import GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public int Create(Import ticket)
		{
			return service.Create(ticket);
		}
	}
}