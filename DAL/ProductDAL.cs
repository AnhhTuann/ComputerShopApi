using System.Collections.Generic;
using System.Data.SQLite;
using System;
using DTO;

namespace DAL
{
	public static class ProductDAL
	{
		public static List<Product> GetAll()
		{
			DAL.ConnectDb();

			List<Product> data = new List<Product>();
			string query = "SELECT * FROM product";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					Product product = new Product();
					product.Id = reader.GetInt32(0);
					product.Name = reader.GetString(1);
					product.Description = reader.GetString(2);
					product.Price = reader.GetInt32(3);
					product.Amount = reader.GetInt32(4);
					product.Category =
							CategoryDAL.GetById(reader.GetInt32(5));
					product.Image = reader.GetString(6);
					data.Add(product);
				}

				reader.NextResult();
			}

			return data;
		}

		public static Product GetById(int id)
		{
			DAL.ConnectDb();

			Product product = new Product();
			string query = "SELECT * FROM category WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				product.Id = reader.GetInt32(0);
				product.Name = reader.GetString(1);
				product.Description = reader.GetString(2);
				product.Price = reader.GetInt32(3);
				product.Amount = reader.GetInt32(4);
				product.Category =
						CategoryDAL.GetById(reader.GetInt32(5));
				product.Image = reader.GetString(6);
			}

			return product;
		}

		public static int Create(Product product)
		{
			DAL.ConnectDb();

			string query =
					"INSERT INTO product (name, description, price, amount, categoryId, image) VALUES (@name, @description, @price, @amount, @categoryId, @image)";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@name", product.Name);
			command
					.Parameters
					.AddWithValue("@description", product.Description);
			command.Parameters.AddWithValue("@price", product.Price);
			command.Parameters.AddWithValue("@amount", product.Amount);
			command.Parameters.AddWithValue("@categoryId", product.Category.Id);
			command.Parameters.AddWithValue("@image", product.Image);
			command.ExecuteNonQuery();

			return DAL.GetLastRowIndex("product");
		}


		public static void Update(Product product)
		{
			DAL.ConnectDb();

			string query = "UPDATE product SET name = @name, description = @description, @price = price, categoryId = @categoryId, amount = @amount, image = @image WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", product.Id);
			command.Parameters.AddWithValue("@name", product.Name);
			command.Parameters.AddWithValue("@description", product.Description);
			command.Parameters.AddWithValue("@price", product.Price);
			command.Parameters.AddWithValue("@categoryId", product.Category.Id);
			command.Parameters.AddWithValue("@amount", product.Amount);
			command.Parameters.AddWithValue("@image", product.Image);
			command.ExecuteNonQuery();
		}
	}
}
