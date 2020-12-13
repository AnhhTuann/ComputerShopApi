using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class CategoryBLL
	{
		public List<Category> GetAll(string search)
		{
			return CategoryDAL.GetAll(search);
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

		public void Delete(int id)
		{
			CategoryDAL.Delete(id);
		}
	}
}