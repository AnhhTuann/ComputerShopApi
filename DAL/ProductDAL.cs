using System.Collections.Generic;
using System.Data.SQLite;
using System;
using DTO;

namespace DAL
{
	public static class ProductDAL
	{
		private static string table = "product";
		private static string subTable = "category";

		private static Product extractData(SQLiteDataReader reader)
		{
			Product product = new Product();
			product.Id = reader.GetInt32(0);
			product.Name = reader.GetString(1);
			product.Description = reader.GetString(2);
			product.Price = reader.GetDouble(3);
			product.Amount = reader.GetInt32(4);
			product.Category = CategoryDAL.GetById(reader.GetInt32(5));
			product.Image = reader.GetString(6);

			return product;
		}

		private static void populateData(SQLiteCommand command, Product product)
		{
			command.Parameters.AddWithValue("@name", product.Name);
			command.Parameters.AddWithValue("@description", product.Description);
			command.Parameters.AddWithValue("@price", product.Price);
			command.Parameters.AddWithValue("@categoryId", product.Category.Id);
			command.Parameters.AddWithValue("@amount", product.Amount);
			command.Parameters.AddWithValue("@image", product.Image);
		}

		public static List<Product> GetAll(string search = "", int categoryId = 0, int minPrice = 0, int maxPrice = 0)
		{
			string categoryIdCondition = "";
			string maxPriceCondition = "";

			if (categoryId != 0)
			{
				categoryIdCondition = "AND categoryId = " + categoryId;
			}

			if (maxPrice > 0 && maxPrice >= minPrice)
			{
				maxPriceCondition = "AND price <= " + maxPrice;
			}

			DAL.ConnectDb();

			List<Product> data = new List<Product>();
			string query = $"SELECT * FROM {table} JOIN {subTable} ON {subTable}.id = {table}.categoryId WHERE ({table}.name LIKE '%{search}%' OR description LIKE '%{search}%' OR {subTable}.name LIKE '%{search}%') {categoryIdCondition} AND (price >= {minPrice} {maxPriceCondition})";
			Console.WriteLine(query);
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

		public static Product GetById(int id)
		{
			DAL.ConnectDb();

			Product product = null;
			string query = $"SELECT * FROM {table} WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				product = extractData(reader);
			}

			return product;
		}

		public static int Create(Product product)
		{
			DAL.ConnectDb();

			string query =
					$"INSERT INTO {table} (name, description, price, amount, categoryId, image) VALUES (@name, @description, @price, @amount, @categoryId, @image)";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			populateData(command, product);
			command.ExecuteNonQuery();

			return Convert.ToInt32(DAL.Conn.LastInsertRowId);
		}


		public static void Update(Product product)
		{
			DAL.ConnectDb();

			string query = $"UPDATE {table} SET name = @name, description = @description, price = @price, categoryId = @categoryId, amount = @amount, image = @image WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", product.Id);
			populateData(command, product);
			command.ExecuteNonQuery();
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
