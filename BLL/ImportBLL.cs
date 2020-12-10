using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class ImportBLL
	{
		private void calculate(Ticket ticket)
		{
			foreach (TicketDetails detail in ticket.Details)
			{
				ticket.TotalAmount += detail.Amount;
				ticket.TotalCost += detail.Amount * detail.Product.Price;
			}
		}

		public List<Ticket> GetAll(int productId)
		{
			List<Ticket> list = ImportDAL.GetAll(productId);

			foreach (Ticket ticket in list)
			{
				calculate(ticket);
			}

			return list;
		}

		public Ticket GetById(int id)
		{
			Ticket ticket = ImportDAL.GetById(id);
			calculate(ticket);
			return ticket;
		}

		public int Create(Ticket ticket)
		{
			return ImportDAL.Create(ticket);
		}
	}
}