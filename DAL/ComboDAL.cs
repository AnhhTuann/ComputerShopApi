using DTO;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace DAL
{
	public class ComboDAL
	{
		private static string table = "combo";
		private static string subTable = "comboDetails";

		private static Combo extractData(SQLiteDataReader reader)
		{
			Combo combo = new Combo();
			combo.Id = reader.GetInt32(0);
			combo.Name = reader.GetString(1);
			combo.Discount = reader.GetInt32(2);

			string detailsQuery = $"SELECT * FROM {subTable} WHERE comboId = @comboId";
			SQLiteCommand detailsCommand = new SQLiteCommand(detailsQuery, DAL.Conn);

			detailsCommand.Parameters.AddWithValue("@comboId", combo.Id);

			SQLiteDataReader detailsReader = detailsCommand.ExecuteReader();

			while (detailsReader.HasRows)
			{
				while (detailsReader.Read())
				{
					ComboDetails details = new ComboDetails();
					details.Combo = combo;
					details.Product = ProductDAL.GetById(detailsReader.GetInt32(1));
					details.Amount = detailsReader.GetInt32(2);

					combo.Details.Add(details);
				}

				detailsReader.NextResult();
			}

			return combo;
		}

		public static List<Combo> GetAll(int productId = 0)
		{
			string productQuery = "";

			if (productId != 0)
			{
				productQuery = $"WHERE {subTable}.productId = {productId}";
			}

			DAL.ConnectDb();

			List<Combo> data = new List<Combo>();
			string query = $"SELECT * FROM {table} JOIN {subTable} ON {table}.id = {subTable}.comboId {productQuery} GROUP BY {subTable}.comboId";
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

		private static void InserDetails(Combo combo)
		{
			DAL.ConnectDb();

			foreach (ComboDetails detail in combo.Details)
			{
				string query = $"INSERT INTO {subTable} (comboId, productId, amount) VALUES (@comboId, @productId, @amount)";
				SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

				command.Parameters.AddWithValue("@comboId", combo.Id);
				command.Parameters.AddWithValue("@productId", detail.Product.Id);
				command.Parameters.AddWithValue("@amount", detail.Amount);
				command.ExecuteNonQuery();
			}
		}

		public static int Create(Combo combo)
		{
			DAL.ConnectDb();

			string query =
							$"INSERT INTO {table} (name, discount) VALUES (@name, @discount)";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@name", combo.Name);
			command.Parameters.AddWithValue("@discount", combo.Discount);
			command.ExecuteNonQuery();

			combo.Id = Convert.ToInt32(DAL.Conn.LastInsertRowId);

			InserDetails(combo);

			return combo.Id;
		}

		public static Combo GetById(int id)
		{
			DAL.ConnectDb();

			Combo combo = null;
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

		public static void Update(Combo combo)
		{
			DAL.ConnectDb();
			string query =
							$"UPDATE {table} SET name = @name, discount = @discount WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@name", combo.Name);
			command.Parameters.AddWithValue("@discount", combo.Discount);
			command.Parameters.AddWithValue("@id", combo.Id);
			command.ExecuteNonQuery();

			string deleteQuery = $"DELETE FROM {subTable} WHERE comboId = @id";
			SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, DAL.Conn);
			deleteCommand.Parameters.AddWithValue("@id", combo.Id);
			deleteCommand.ExecuteNonQuery();

			InserDetails(combo);
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