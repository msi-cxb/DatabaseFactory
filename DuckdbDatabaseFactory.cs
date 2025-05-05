using DuckDB.NET.Data;
using DuckDB.NET.Native;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractFactoryPattern.DatabaseInterface;

namespace AbstractFactoryPattern
{
    public class DuckdbDatabaseFactory : IDatabaseFactory
    {
        public IDatabaseService CreateDatabaseService()
        {
            return new DuckdbDataBaseService();
        }
    }

    public class DuckdbDataBaseService : IDatabaseService
    {
        private string _databaseType = "DUCKDB";
        private string _dataSource = string.Empty;
        private DuckDBConnection? conn = null;

        public DuckdbDataBaseService()
        {
            Console.WriteLine($"[{_databaseType}][DuckdbDataBaseService] **********************");
        }

        public string GetDatabaseType()
        {
            return _databaseType;
        }

        public void GetConnection(string dataSource)
        {
            conn = new DuckDBConnection($"Data Source={dataSource}");
            conn.Open();
            Console.WriteLine($"[{_databaseType}][GetConnection] {dataSource}");
        }

        public void ExecuteNonQuery(string sql)
        {
            using DuckDBCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var rtn = cmd.ExecuteNonQuery();

            Console.WriteLine($"[{_databaseType}][ExecuteNonQuery] {sql} --> {rtn}");
        }

        public Int32 ExecuteScalar(string sql)
        {
            using DuckDBCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var rtn = cmd.ExecuteScalar();

            Console.WriteLine($"[{_databaseType}][ExecuteScalar] {sql} --> {rtn}");

            return Convert.ToInt32(rtn);
        }

        public void ExecuteReader(string sql)
        {
            using DuckDBCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            DuckDBDataReader reader = cmd.ExecuteReader();

            Console.WriteLine($"[{_databaseType}][ExecuteReader] {sql} --> {reader.FieldCount} {reader.HasRows}");
        }

        public void Close()
        {
            Console.WriteLine($"[{_databaseType}][Close] **********************");
            conn.Close();
        }
    }
}
