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
		public IEnumerable<Combo> GetAll([FromQuery] int contain)
		{
			return service.GetAll(contain);
		}

		[HttpGet("{id}")]
		public Combo GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public int Create(Combo combo)
		{
			return service.Create(combo);
		}

		[HttpPut]
		public void Update(Combo combo)
		{
			service.Update(combo);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			service.Delete(id);
		}
	}
}