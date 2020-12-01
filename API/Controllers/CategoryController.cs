using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private CategoryBLL service = new CategoryBLL();

		[HttpGet]
		public IEnumerable<Category> GetAll()
		{
			return service.GetAll();
		}

		[HttpGet("{id}")]
		public Category GetById(int id)
		{
			return service.GetById(id);
		}

		[HttpPost]
		public void Create(Category category)
		{
			service.Create(category);
		}
	}
}