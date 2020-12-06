using DTO;
using System.Data.SQLite;
using System.Collections.Generic;
using System;

namespace DAL
{
	public static class StorageDAL
	{
		public static List<Storage> GetAll()
		{
			DAL.ConnectDb();

			List<Storage> data = new List<Storage>();
			string query = "SELECT * FROM storage";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					Storage storage = new Storage();
					storage.Id = reader.GetInt32(0);
					storage.Product = ProductDAL.GetById(reader.GetInt32(1));
					storage.Import = reader.GetInt32(2);
					storage.Export = reader.GetInt32(3);
					storage.Date = reader.GetString(4);
					data.Add(storage);
				}

				reader.NextResult();
			}

			return data;
		}
		public static int Create(Storage storage)
		{
			DAL.ConnectDb();

			string query =
					"INSERT INTO storage (productId, import, export, date) VALUES (@productId, @import, @export, @date)";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@productId", storage.Product.Id);
			command
					.Parameters
					.AddWithValue("@import", storage.Import);
			command.Parameters.AddWithValue("@export", storage.Export);
			command.Parameters.AddWithValue("@date", storage.Date);
			command.ExecuteNonQuery();

			return Convert.ToInt32(DAL.Conn.LastInsertRowId);
		}
	}
}