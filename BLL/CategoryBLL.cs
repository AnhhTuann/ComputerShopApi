using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class CategoryBLL
	{
		public List<Category> GetAll()
		{
			return CategoryDAL.GetAll();
		}

		public Category GetById(int id)
		{
			return CategoryDAL.GetById(id);
		}

		public int Create(Category category)
		{
			return CategoryDAL.Create(category);
		}

		public void Update(Category category)
		{
			CategoryDAL.Update(category);
		}
	}
}