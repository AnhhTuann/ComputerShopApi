using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ExportController : ControllerBase
	{
		private ExportBLL service = new ExportBLL();

		[HttpGet]
		public IEnumerable<Export> GetAll([FromQuery] int contain)
		{
			return service.GetAll(contain);
		}

		[HttpGet]
		[Route("{id}")]
		public Export GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public int Create(Export ticket)
		{
			return service.Create(ticket);
		}
	}
}