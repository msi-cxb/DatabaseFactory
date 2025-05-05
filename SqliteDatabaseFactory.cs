using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractFactoryPattern.DatabaseInterface;

namespace AbstractFactoryPattern
{
    public class SqliteDatabaseFactory : IDatabaseFactory
    {
        public IDatabaseService CreateDatabaseService()
        {
            return new SqliteDataBaseService();
        }
    }
    public class SqliteDataBaseService : IDatabaseService
    {
        private string _databaseType = "SQLITE";
        private string _dataSource = string.Empty;
        private SQLiteConnection? conn = null;

        public SqliteDataBaseService()
        {
            Console.WriteLine($"[{_databaseType}][SqliteDataBaseService] **********************");
        }

        public string GetDatabaseType()
        {
            return _databaseType;
        }

        public void GetConnection(string dataSource)
        {
            // todo check the path of dataSource
            conn = new SQLiteConnection($"Data Source={dataSource};Version=3;");
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_databaseType}][GetConnection] {ex.Message}");
            }

            Console.WriteLine($"[{_databaseType}][GetConnection] {dataSource}");
        }

        public void ExecuteNonQuery(string sql)
        {
            using SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var rtn = cmd.ExecuteNonQuery();

            Console.WriteLine($"[{_databaseType}][ExecuteNonQuery] {sql} --> {rtn}");
        }

        public Int32 ExecuteScalar(string sql)
        {
            using SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var rtn = cmd.ExecuteScalar();

            Console.WriteLine($"[{_databaseType}][ExecuteScalar] {sql} --> {rtn}");

            return Convert.ToInt32(rtn);
        }

        public void ExecuteReader(string sql)
        {
            using SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            SQLiteDataReader reader = cmd.ExecuteReader();

            Console.WriteLine($"[{_databaseType}][ExecuteReader] {sql} --> {reader.FieldCount} {reader.HasRows}");
        }
        public void Close()
        {
            Console.WriteLine($"[{_databaseType}][Close] **********************");
            conn.Close();

        }


    }
}
