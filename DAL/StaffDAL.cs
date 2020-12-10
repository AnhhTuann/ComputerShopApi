using System.Collections.Generic;
using System.Data.SQLite;
using DTO;
using System;

namespace DAL
{
	public static class StaffDAL
	{
		private static string table = "staff";

		private static Staff extractData(SQLiteDataReader reader)
		{
			Staff staff = new Staff();
			staff.Id = reader.GetInt32(0);
			staff.Name = reader.GetString(1);
			staff.Email = reader.GetString(2);
			staff.Password = reader.GetString(3);
			staff.Address = reader.GetString(4);
			staff.Phone = reader.GetString(5);
			staff.Role = RoleDAL.GetById(reader.GetInt32(6));

			return staff;
		}

		public static List<Staff> GetAll()
		{
			DAL.ConnectDb();

			List<Staff> data = new List<Staff>();
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

		public static Staff GetById(int id)
		{
			DAL.ConnectDb();

			Staff staff = null;
			string query = $"SELECT * FROM {table} WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				staff = extractData(reader);
			}

			return staff;
		}

		public static Staff GetByEmail(string email)
		{
			DAL.ConnectDb();

			Staff staff = null;
			string query = $"SELECT * FROM {table} WHERE email = @email";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@email", email);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				staff = extractData(reader);
			}

			return staff;
		}

		public static int Create(Staff staff)
		{
			DAL.ConnectDb();

			string query =
					$"INSERT INTO {table} (name, email, password, address, phone) VALUES (@name, @email, @password, @address, @phone)";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@name", staff.Name);
			command.Parameters.AddWithValue("@email", staff.Email);
			command.Parameters.AddWithValue("@password", staff.Password);
			command.Parameters.AddWithValue("@address", staff.Address);
			command.Parameters.AddWithValue("@phone", staff.Phone);
			command.ExecuteNonQuery();

			return Convert.ToInt32(DAL.Conn.LastInsertRowId);
		}


		public static void Update(Staff staff)
		{
			DAL.ConnectDb();

			string query = $"UPDATE {table} SET name = @name, email = @email, password = @password, address = @address, phone = @phone WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", staff.Id);
			command.Parameters.AddWithValue("@name", staff.Name);
			command.Parameters.AddWithValue("@email", staff.Email);
			command.Parameters.AddWithValue("@password", staff.Password);
			command.Parameters.AddWithValue("@address", staff.Address);
			command.Parameters.AddWithValue("@phone", staff.Phone);
			command.ExecuteNonQuery();
		}
	}
}
