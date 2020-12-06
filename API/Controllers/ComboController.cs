using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class ComboController : ControllerBase
	{
		private ComboBLL service = new ComboBLL();

		[HttpGet]
		public IEnumerable<Combo> GetAll()
		{
			return service.GetAll();
		}

		[HttpGet]
		[Route("{id}")]
		public Combo GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public int Create(Combo combo)
		{
			return service.Create(combo);
		}
	}
}