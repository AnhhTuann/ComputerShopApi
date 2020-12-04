using System.Data.SQLite;

namespace DAL {
    public class DAL {
        public static SQLiteConnection Conn;
        public static void ConnectDb () {
            string connectionStr = "Data Source=../DAL/database.db;Version=3;FailIfMissing=True";
            Conn = new SQLiteConnection (connectionStr);
            Conn.Open ();
        }

        public static int GetLastRowIndex(string table)
		{
			DAL.ConnectDb();

			string query = "SELECT MAX(id) FROM " + table;
			SQLiteCommand command = new SQLiteCommand(query, DAL.Conn);
			SQLiteDataReader reader = command.ExecuteReader();
			int id = 0;

			while (reader.Read()) id = reader.GetInt32(0);

			return id;
		}
    }
}