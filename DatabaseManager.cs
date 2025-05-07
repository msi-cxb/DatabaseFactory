using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractFactoryPattern.DatabaseInterface;

namespace AbstractFactoryPattern
{
    public class DatabaseManager : IDisposable
    {
        private readonly IDatabaseService _database;

        public void Dispose()
        {
            Console.WriteLine($"[DatabaseManager] ***** Dispose *****");
            _database.Close();
        }

        public DatabaseManager(IDatabaseFactory factory)
        {
            _database = factory.CreateDatabaseService();
        }

        public string GetDatabaseType()
        {
            return _database.GetDatabaseType();
        }
        public void GetDatabaseConnection(string filename)
        {
            _database.GetConnection(filename);
        }

        public void ExecuteNonQuery(string sql)
        {
            _database.ExecuteNonQuery(sql);
        }

        public int ExecuteScalar(string sql)
        {
            return _database.ExecuteScalar(sql);
        }

        public void ExecuteReader(string sql)
        {
            _database.ExecuteReader(sql);
        }
        public void Close()
        {
            _database.Close();
        }
        public Boolean IsOpen()
        {
            return _database.IsOpen();
        }

    }
}
