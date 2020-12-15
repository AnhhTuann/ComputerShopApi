using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class ExportBLL
	{
		private void calculate(Export ticket)
		{
			foreach (TicketDetails detail in ticket.Details)
			{
				ticket.TotalAmount += detail.Amount;
			}
		}

		public List<Export> GetAll(int productId)
		{
			List<Export> list = ExportDAL.GetAll(productId);
			ReceiptBLL receiptService = new ReceiptBLL();

			foreach (Export ticket in list)
			{
				calculate(ticket);
				receiptService.calculatePrice(ticket.Receipt);
			}

			return list;
		}

		public Export GetById(int id)
		{
			Export ticket = ExportDAL.GetById(id);
			calculate(ticket);
			return ticket;
		}

		public int Create(Export ticket)
		{
			return ExportDAL.Create(ticket);
		}
	}
}