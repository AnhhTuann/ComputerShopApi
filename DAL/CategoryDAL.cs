using DTO;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
namespace DAL {
    public static class CategoryDAL {
        public static Category GetCategoryById (int id) {
			DAL.ConnectDb ();

			Category category = new Category ();
			string query = "SELECT * FROM category WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand (query, DAL.Conn);

			command.Parameters.AddWithValue ("@id", id);

			SQLiteDataReader reader = command.ExecuteReader ();

			while (reader.Read ()) {
				category.Id = Int32.Parse (reader["id"].ToString ());
				category.Name = reader["name"].ToString ();
			}

			return category;
		}

		public static void Create(Category category) {
            DAL.ConnectDb();

            string query = "INSERT INTO category (name) VALUES (@name)";
            SQLiteCommand command = new SQLiteCommand (query, DAL.Conn);

			command.Parameters.AddWithValue ("@name", category.Name);
			command.ExecuteNonQuery ();
        }
    }
}