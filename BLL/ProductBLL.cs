using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class ProductBLL
	{
		public List<Product> GetAll()
		{
			return ProductDAL.GetAll();
		}

		public Product GetById(int id)
		{
			return ProductDAL.GetById(id);
		}

		public int Create(Product product)
		{
			return ProductDAL.Create(product);
		}

		public void Update(Product product)
		{
			ProductDAL.Update(product);
		}
	}
}