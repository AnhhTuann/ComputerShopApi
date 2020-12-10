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

		public static List<Receipt> GetAll()
		{
			DAL.ConnectDb();

			List<Receipt> data = new List<Receipt>();
			string query = $"SELECT * FROM {table}";
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

		public static int Create(Receipt receipt)
		{
			DAL.ConnectDb();
			SQLiteCommand command = new SQLiteCommand(DAL.Conn);
			SQLiteTransaction transaction = DAL.Conn.BeginTransaction();
			command.Transaction = transaction;

			try
			{
				string query =
								$"INSERT INTO {table} (recipient, address, phone, status, date, customerId) VALUES (@recipient, @address, @phone, @status, @date, @customerId)";
				command.CommandText = query;

				command.Parameters.AddWithValue("@recipient", receipt.Recipient);
				command.Parameters.AddWithValue("@address", receipt.Address);
				command.Parameters.AddWithValue("@phone", receipt.Phone);
				command.Parameters.AddWithValue("@status", receipt.Status);
				command.Parameters.AddWithValue("@date", receipt.Date);
				command.Parameters.AddWithValue("@customerId", receipt.Customer.Id);

				command.ExecuteNonQuery();

				receipt.Id = Convert.ToInt32(DAL.Conn.LastInsertRowId);

				foreach (ReceiptDetails detail in receipt.Details)
				{
					query = $"INSERT INTO {detailsTable} (receiptId, productId, amount) VALUES (@receiptId, @productId, @amount)";
					command.CommandText = query;

					command.Parameters.AddWithValue("@receiptId", receipt.Id);
					command.Parameters.AddWithValue("@productId", detail.Product.Id);
					command.Parameters.AddWithValue("@amount", detail.Amount);
					command.ExecuteNonQuery();
				}

				foreach (ReceiptCombos detail in receipt.Combos)
				{
					query = $"INSERT INTO {combosTable} (receiptId, comboId, amount) VALUES (@receiptId, @comboId, @amount)";
					command.CommandText = query;

					command.Parameters.AddWithValue("@receiptId", receipt.Id);
					command.Parameters.AddWithValue("@comboId", detail.Combo.Id);
					command.Parameters.AddWithValue("@amount", detail.Amount);
					command.ExecuteNonQuery();
				}

				transaction.Commit();
				return receipt.Id;
			}
			catch (Exception e)
			{
				transaction.Rollback();
				throw e;
			}
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
	}
}