using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class ImportBLL
	{
		private void calculate(Import ticket)
		{
			foreach (TicketDetails detail in ticket.Details)
			{
				ticket.TotalAmount += detail.Amount;
				ticket.TotalCost += detail.Amount * detail.Product.Price;
			}
		}

		public List<Import> GetAll(int productId)
		{
			List<Import> list = ImportDAL.GetAll(productId);

			foreach (Import ticket in list)
			{
				calculate(ticket);
			}

			return list;
		}

		public Import GetById(int id)
		{
			Import ticket = ImportDAL.GetById(id);
			calculate(ticket);
			return ticket;
		}

		public int Create(Import ticket)
		{
			return ImportDAL.Create(ticket);
		}
	}
}