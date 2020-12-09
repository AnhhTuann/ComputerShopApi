using System.Collections.Generic;
using System.Data.SQLite;
using DTO;
using System;

namespace DAL
{
	public static class CustomerDAL
	{
		private static Person extractData(SQLiteDataReader reader)
		{
			Person customer = new Person();
			customer.Id = reader.GetInt32(0);
			customer.Name = reader.GetString(1);
			customer.Email = reader.GetString(2);
			customer.Password = reader.GetString(3);
			customer.Address = reader.GetString(4);
			customer.Phone = reader.GetString(5);

			return customer;
		}

		public static List<Person> GetAll()
		{
			DAL.ConnectDb();

			List<Person> data = new List<Person>();
			string query = "SELECT * FROM customer";
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

		public static Person GetById(int id)
		{
			DAL.ConnectDb();

			Person customer = null;
			string query = "SELECT * FROM customer WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				customer = extractData(reader);
			}

			return customer;
		}

		public static Person GetByEmail(string email)
		{
			DAL.ConnectDb();

			Person customer = null;
			string query = "SELECT * FROM customer WHERE email = @email";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@email", email);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				customer = extractData(reader);
			}

			return customer;
		}

		public static int Create(Person customer)
		{
			DAL.ConnectDb();

			string query =
					"INSERT INTO customer (name, email, password, address, phone) VALUES (@name, @email, @password, @address, @phone)";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@name", customer.Name);
			command.Parameters.AddWithValue("@email", customer.Email);
			command.Parameters.AddWithValue("@password", customer.Password);
			command.Parameters.AddWithValue("@address", customer.Address);
			command.Parameters.AddWithValue("@phone", customer.Phone);
			command.ExecuteNonQuery();

			return Convert.ToInt32(DAL.Conn.LastInsertRowId);
		}


		public static void Update(Person customer)
		{
			DAL.ConnectDb();

			string query = "UPDATE customer SET name = @name, email = @email, password = @password, address = @address, phone = @phone WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", customer.Id);
			command.Parameters.AddWithValue("@name", customer.Name);
			command.Parameters.AddWithValue("@email", customer.Email);
			command.Parameters.AddWithValue("@password", customer.Password);
			command.Parameters.AddWithValue("@address", customer.Address);
			command.Parameters.AddWithValue("@phone", customer.Phone);
			command.ExecuteNonQuery();
		}
	}
}
