using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
	public class ReceiptBLL
	{
		private void calculatePrice(Receipt receipt)
		{
			ComboBLL comboService = new ComboBLL();
			foreach (ReceiptDetails detail in receipt.Details)
			{
				receipt.TotalCost += detail.Product.Price * detail.Amount;
			}

			foreach (ReceiptCombos combo in receipt.Combos)
			{
				Combo c = combo.Combo;
				comboService.calculatePrice(c);
				receipt.TotalCost += c.Price;
			}
		}

		public List<Receipt> GetAll(int customerId)
		{
			List<Receipt> list = ReceiptDAL.GetAll(customerId);

			foreach (Receipt receipt in list)
			{
				calculatePrice(receipt);
			}

			return list;
		}

		public Receipt GetById(int id)
		{
			Receipt receipt = ReceiptDAL.GetById(id);
			calculatePrice(receipt);
			return receipt;
		}

		public int Create(Receipt receipt)
		{
			return ReceiptDAL.Create(receipt);
		}

		public bool Update(Receipt receipt, int staffId)
		{
			if (ReceiptDAL.Update(receipt) && receipt.Status == 1)
			{
				ReceiptDAL.CreateExportTicket(receipt, staffId);
				return true;
			}

			return false;
		}

		public bool Update(Receipt receipt)
		{
			if (receipt.Status == 1 || !ReceiptDAL.Update(receipt))
			{
				return false;
			}

			return true;
		}

		public void Delete(int id)
		{
			ReceiptDAL.Delete(id);
		}
	}
}