using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractFactoryPattern.DatabaseInterface;

namespace AbstractFactoryPattern
{
    public class PostgreSQLDatabaseFactory : IDatabaseFactory
    {
        public IDatabaseService CreateDatabaseService()
        {
            return new PostgreSQLDataBaseService();
        }
    }

    public class PostgreSQLDataBaseService : IDatabaseService
    {
        private string _databaseType = "PostgreSQL";
        private string _dataSource = string.Empty;
        private string _connString = null;

        public PostgreSQLDataBaseService()
        {
            Console.WriteLine($"[{_databaseType}][Constructor] **********************");
        }

        public string GetDatabaseType()
        {
            return _databaseType;
        }

        public void GetConnection(string connString)
        {
            _connString = connString;
            Console.WriteLine($"[{_databaseType}][GetConnection] {_connString}");
        }

        public void ExecuteNonQuery(string sql)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(_connString);
            conn.Open();
            using NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var rtn = cmd.ExecuteNonQuery();
            Console.WriteLine($"[{_databaseType}][ExecuteNonQuery] {sql} --> {rtn}");
        }

        public Int32 ExecuteScalar(string sql)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(_connString);
            conn.Open();
            using NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var rtn = cmd.ExecuteScalar();

            Console.WriteLine($"[{_databaseType}][ExecuteScalar] {sql} --> {rtn}");

            return Convert.ToInt32(rtn);
        }

        public void ExecuteReader(string sql)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(_connString);
            conn.Open();
            using NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            NpgsqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine($"[{_databaseType}][ExecuteReader] {sql} --> {reader.FieldCount} {reader.HasRows}");
            Console.WriteLine();

            for (var index = 0; index < reader.FieldCount; index++)
            {
                var column = reader.GetName(index);
                Console.Write($"{index}:{column} ");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                {
                    if (reader.IsDBNull(ordinal))
                    {
                        Console.Write($"{ordinal}:type NULL:value NULL");
                        continue;
                    }
                    switch (reader.GetFieldType(ordinal).Name)
                    {
                        case "Int32":
                            Console.Write($"[{ordinal}:{reader.GetFieldType(ordinal).Name}:{reader.GetFieldValue<Int32>(ordinal)}] ");
                            break;
                        case "Int64":
                            Console.Write($"[{ordinal}:{reader.GetFieldType(ordinal).Name}:{reader.GetFieldValue<Int64>(ordinal)}] ");
                            break;
                        case "Double":
                            Console.Write($"[{ordinal}:{reader.GetFieldType(ordinal).Name}:{reader.GetFieldValue<Double>(ordinal)}] ");
                            break;
                        case "String":
                            Console.Write($"[{ordinal}:{reader.GetFieldType(ordinal).Name}:{reader.GetFieldValue<String>(ordinal)}] ");
                            break;
                        default:
                            Console.Write($"[{ordinal}:unknown type {reader.GetFieldType(ordinal).Name}] ");
                            break;

                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        
        public void Close()
        {
            if ( IsOpen() )
            {
                Console.WriteLine($"[{_databaseType}][Close] not implemented **********************");
            }
        }
        
        public Boolean IsOpen()
        {
            Console.WriteLine($"[{_databaseType}][IsOpen] not implemented **********************");
            return false;
        }
    }
}
