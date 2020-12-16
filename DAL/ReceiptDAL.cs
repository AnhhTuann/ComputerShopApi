using DTO;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace DAL
{
	public class ReceiptDAL
	{
		private static string table = "receipt";
		private static string detailsTable = "receiptDetails";
		private static string combosTable = "receiptCombos";

		private static Receipt extractData(SQLiteDataReader reader)
		{
			Receipt receipt = new Receipt();
			receipt.Id = reader.GetInt32(0);
			receipt.Recipient = reader.GetString(1);
			receipt.Address = reader.GetString(2);
			receipt.Phone = reader.GetString(3);
			receipt.Status = reader.GetInt32(4);
			receipt.Date = reader.GetString(5);
			receipt.Customer = CustomerDAL.GetById(reader.GetInt32(6));

			string detailsQuery = $"SELECT * FROM {detailsTable} WHERE receiptId = @receiptId";
			SQLiteCommand detailsCommand = new SQLiteCommand(detailsQuery, DAL.Conn);

			detailsCommand.Parameters.AddWithValue("@receiptId", receipt.Id);

			SQLiteDataReader detailsReader = detailsCommand.ExecuteReader();

			while (detailsReader.HasRows)
			{
				while (detailsReader.Read())
				{
					ReceiptDetails details = new ReceiptDetails();
					details.Receipt = receipt;
					details.Product = ProductDAL.GetById(detailsReader.GetInt32(1));
					details.Amount = detailsReader.GetInt32(2);

					receipt.Details.Add(details);
				}

				detailsReader.NextResult();
			}

			string combosQuery = $"SELECT * FROM {combosTable} WHERE receiptId = @receiptId";
			SQLiteCommand combosCommand = new SQLiteCommand(combosQuery, DAL.Conn);

			combosCommand.Parameters.AddWithValue("@receiptId", receipt.Id);

			SQLiteDataReader combosReader = combosCommand.ExecuteReader();

			while (combosReader.HasRows)
			{
				while (combosReader.Read())
				{
					ReceiptCombos combos = new ReceiptCombos();
					combos.Receipt = receipt;
					combos.Combo = ComboDAL.GetById(combosReader.GetInt32(1));
					combos.Amount = combosReader.GetInt32(2);

					receipt.Combos.Add(combos);
				}

				combosReader.NextResult();
			}

			return receipt;
		}

		public static List<Receipt> GetAll(int customerId = 0)
		{
			DAL.ConnectDb();
			string byCustomer = "";

			if (customerId != 0)
			{
				byCustomer = " WHERE customerId = " + customerId;
			}

			List<Receipt> data = new List<Receipt>();
			string query = $"SELECT * FROM {table} {byCustomer}";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					data.Add(extractData(reader));
				}

				reader.NextResult();
			}


			return data;
		}

		private static bool CheckAmount(Receipt receipt)
		{
			foreach (ReceiptDetails detail in receipt.Details)
			{
				Product p = ProductDAL.GetById(detail.Product.Id);

				if (p == null)
				{
					return false;
				}

				if (p.Amount < detail.Amount)
				{
					return false;
				}
			}

			foreach (ReceiptCombos detail in receipt.Combos)
			{
				foreach (ComboDetails comboDetails in detail.Combo.Details)
				{
					if (comboDetails.Product.Amount < comboDetails.Amount * detail.Amount)
					{
						return false;
					}
				}
			}

			return true;
		}

		private static void InsertDetails(Receipt receipt)
		{
			DAL.ConnectDb();

			foreach (ReceiptDetails detail in receipt.Details)
			{
				string query = $"INSERT INTO {detailsTable} (receiptId, productId, amount) VALUES (@receiptId, @productId, @amount)";
				SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

				command.Parameters.AddWithValue("@receiptId", receipt.Id);
				command.Parameters.AddWithValue("@productId", detail.Product.Id);
				command.Parameters.AddWithValue("@amount", detail.Amount);
				command.ExecuteNonQuery();
			}

			foreach (ReceiptCombos detail in receipt.Combos)
			{
				string query = $"INSERT INTO {combosTable} (receiptId, comboId, amount) VALUES (@receiptId, @comboId, @amount)";
				SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

				command.Parameters.AddWithValue("@receiptId", receipt.Id);
				command.Parameters.AddWithValue("@comboId", detail.Combo.Id);
				command.Parameters.AddWithValue("@amount", detail.Amount);
				command.ExecuteNonQuery();
			}
		}

		public static int Create(Receipt receipt)
		{
			DAL.ConnectDb();

			if (!CheckAmount(receipt))
			{
				return -1;
			}

			string query =
							$"INSERT INTO {table} (recipient, address, phone, status, date, customerId) VALUES (@recipient, @address, @phone, @status, @date, @customerId)";

			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@recipient", receipt.Recipient);
			command.Parameters.AddWithValue("@address", receipt.Address);
			command.Parameters.AddWithValue("@phone", receipt.Phone);
			command.Parameters.AddWithValue("@status", receipt.Status);
			command.Parameters.AddWithValue("@date", receipt.Date);
			command.Parameters.AddWithValue("@customerId", receipt.Customer.Id);
			command.ExecuteNonQuery();

			receipt.Id = Convert.ToInt32(DAL.Conn.LastInsertRowId);

			InsertDetails(receipt);

			return receipt.Id;
		}

		public static Receipt GetById(int id)
		{
			DAL.ConnectDb();

			Receipt combo = null;
			string query = $"SELECT * FROM {table} WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				combo = extractData(reader);
			}


			return combo;
		}

		public static void CreateExportTicket(Receipt receipt, int staffId)
		{
			Export ticket = new Export();
			ticket.Receipt = receipt;
			ticket.Staff = StaffDAL.GetById(staffId);

			foreach (ReceiptDetails detail in receipt.Details)
			{
				TicketDetails ticketDetails = new TicketDetails()
				{
					Ticket = ticket,
					Product = detail.Product,
					Amount = detail.Amount
				};

				ticket.Details.Add(ticketDetails);
			}

			foreach (ReceiptCombos detail in receipt.Combos)
			{
				foreach (ComboDetails comboDetails in detail.Combo.Details)
				{
					TicketDetails ticketDetails = new TicketDetails()
					{
						Ticket = ticket,
						Product = comboDetails.Product,
						Amount = comboDetails.Amount
					};

					ticket.Details.Add(ticketDetails);
				}
			}

			ExportDAL.Create(ticket);
		}

		public static void Update(Receipt receipt)
		{
			DAL.ConnectDb();

			string query =
							$"UPDATE {table} SET recipient = @recipient, address = @address, phone = @phone, status = @status, date = @date, customerId = @customerId WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@recipient", receipt.Recipient);
			command.Parameters.AddWithValue("@address", receipt.Address);
			command.Parameters.AddWithValue("@phone", receipt.Phone);
			command.Parameters.AddWithValue("@status", receipt.Status);
			command.Parameters.AddWithValue("@date", receipt.Date);
			command.Parameters.AddWithValue("@customerId", receipt.Customer.Id);
			command.Parameters.AddWithValue("@id", receipt.Id);
			command.ExecuteNonQuery();

			String deleteDetailsQuery = $"DELETE FROM {detailsTable} WHERE receiptId = @id";
			SQLiteCommand deleteDetailsCommand = new SQLiteCommand(deleteDetailsQuery, DAL.Conn);
			deleteDetailsCommand.Parameters.AddWithValue("@id", receipt.Id);
			deleteDetailsCommand.ExecuteNonQuery();

			String deleteCombosQuery = $"DELETE FROM {combosTable} WHERE receiptId = @id";
			SQLiteCommand deleteCombosCommand = new SQLiteCommand(deleteCombosQuery, DAL.Conn);
			deleteCombosCommand.Parameters.AddWithValue("@id", receipt.Id);
			deleteCombosCommand.ExecuteNonQuery();

			InsertDetails(receipt);
		}

		public static void Delete(int id)
		{
			DAL.ConnectDb();

			string query = $"DELETE FROM {table} WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
		}
	}
}