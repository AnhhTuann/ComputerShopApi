using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class ComboBLL
	{
		private void calculatePrice(Combo combo)
		{
			foreach (ComboDetails detail in combo.Details)
			{
				combo.OriginPrice += detail.Product.Price;
			}

			combo.Price = combo.OriginPrice - (combo.OriginPrice * combo.Discount / 100);
		}

		public List<Combo> GetAll(int productId)
		{
			List<Combo> list = ComboDAL.GetAll(productId);

			foreach (Combo combo in list)
			{
				calculatePrice(combo);
			}

			return list;
		}

		public Combo GetById(int id)
		{
			Combo combo = ComboDAL.GetById(id);
			calculatePrice(combo);
			return combo;
		}

		public int Create(Combo combo)
		{
			return ComboDAL.Create(combo);
		}
	}
}