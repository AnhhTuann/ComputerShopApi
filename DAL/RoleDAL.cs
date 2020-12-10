using System.Collections.Generic;
using System.Data.SQLite;
using System;
using DTO;

namespace DAL
{
	public static class RoleDAL
	{
		private static string table = "role";

		public static List<Role> GetAll()
		{
			DAL.ConnectDb();

			List<Role> data = new List<Role>();
			string query = $"SELECT * FROM {table}";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					Role role = new Role();
					role.Id = reader.GetInt32(0);
					role.Name = reader.GetString(1);

					data.Add(role);
				}

				reader.NextResult();
			}

			return data;
		}

		public static Role GetById(int id)
		{
			DAL.ConnectDb();

			Role role = new Role();
			string query = $"SELECT * FROM {table} WHERE id = @id";
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);

			command.Parameters.AddWithValue("@id", id);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				role.Id = reader.GetInt32(0);
				role.Name = reader.GetString(1);
			}

			return role;
		}
	}
}
