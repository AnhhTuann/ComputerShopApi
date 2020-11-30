using DTO;
using System.Data.SQLite;
using System;

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
    }
}