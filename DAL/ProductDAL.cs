using DTO;
using System.Data.SQLite;
using System.Collections.Generic;

namespace DAL
{
    public static class ProductDAL
    {
        public static int GetLastRowIndex () {
			DAL.ConnectDb ();

			string query = "SELECT MAX(id) FROM product";
			SQLiteCommand command = new SQLiteCommand (query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader ();
			int id = 0;

			while (reader.Read ()) id = reader.GetInt32 (0);

			return id;
		}

        public static List<Product> GetProducts() {
            DAL.ConnectDb ();

			List<Product> data = new List<Product> ();
			string query = "SELECT * FROM product";
			SQLiteCommand command = new SQLiteCommand (query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader ();

			while (reader.HasRows) {
				while (reader.Read ()) {
					Product product = new Product ();
					product.Id = reader.GetInt32 (0);
					product.Name = reader.GetString (1);
					product.Description = reader.GetString (2);
					product.Price = reader.GetInt32 (3);
					product.Amount = reader.GetInt32 (4);
					product.Category = CategoryDAL.GetCategoryById(reader.GetInt32(5));
					data.Add (product);
				}

				reader.NextResult ();
			}

			return data;
        }

        public static void Create(Product product) {
            DAL.ConnectDb();

            string query = "INSERT INTO product (name, description, price, amount, categoryId) VALUES (@name, @description, @price, @amount, @categoryId)";
            SQLiteCommand command = new SQLiteCommand (query, DAL.Conn);

			command.Parameters.AddWithValue ("@name", product.Name);
			command.Parameters.AddWithValue ("@description", product.Description);
			command.Parameters.AddWithValue ("@price", product.Price);
			command.Parameters.AddWithValue ("@amount", product.Amount);
			command.Parameters.AddWithValue ("@categoryId", product.Category.Id);
			command.ExecuteNonQuery ();
        }
    }
}