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

		public int Create(Product product)
		{
			ProductDAL.Create(product);
			return ProductDAL.GetLastRowIndex();
		}
	}
}