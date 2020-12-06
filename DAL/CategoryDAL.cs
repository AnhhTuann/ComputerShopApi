using System.Collections.Generic;
using System.Data.SQLite;
using System;
using DTO;

namespace DAL
{
	public static class CategoryDAL
	{
		private static string tableName = "category";

		public static List<Category> GetAll(string search = "")
		{
			DAL.ConnectDb();

			List<Category> data = new List<Category>();
			string query = $"SELECT * FROM {tableName} WHERE name LIKE '%{search}%'";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					Category category = new Category();
					category.Id = reader.GetInt32(0);
					category.Name = reader.GetString(1);

					data.Add(category);
				}

				reader.NextResult();
			}

			return data;
		}

		public static Category GetById(int id)
		{
			DAL.ConnectDb();

			Category category = new Category();
			string query = $"SELECT * FROM {tableName} WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				category.Id = reader.GetInt32(0);
				category.Name = reader.GetString(1);
			}

			return category;
		}

		public static int Create(Category category)
		{
			DAL.ConnectDb();

			string query = $"INSERT INTO {tableName} (name) VALUES (@name)";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@name", category.Name);
			command.ExecuteNonQuery();

			return DAL.GetLastRowIndex("category");
		}

		public static void Update(Category category)
		{
			DAL.ConnectDb();

			string query = $"UPDATE {tableName} SET name = @name WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@name", category.Name);
			command.ExecuteNonQuery();
		}
	}
}
