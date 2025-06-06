using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractFactoryPattern.DatabaseInterface;

namespace AbstractFactoryPattern
{
    public class TestDatabaseFactory : IDatabaseFactory
    {
        public IDatabaseService CreateDatabaseService()
        {
            return new FakeDatabaseService();
        }
    }

    public class FakeDatabaseService : IDatabaseService
    {
        private string _databaseType = "TEST";
        private string _dataSource = string.Empty;

        public FakeDatabaseService()
        {
            Console.WriteLine($"[{_databaseType}][Constructor] **********************");
        }

        public string GetDatabaseType()
        {
            return _databaseType;
        }
        
        public void GetConnection(string dataSource)
        {
            Console.WriteLine($"[{_databaseType}][GetConnection] {dataSource}");
        }
        
        public void ExecuteNonQuery(string sql)
        {
            Console.WriteLine($"[{_databaseType}][ExecuteNonQuery] {sql}");
        }
        
        public int ExecuteScalar(string sql)
        {
            Console.WriteLine($"[{_databaseType}][ExecuteScalar] {sql}");
            return 0;
        }
        
        public void ExecuteReader(string sql)
        {
            Console.WriteLine($"[{_databaseType}][ExecuteReader] {sql}");
        }
        
        public void Close()
        {
            Console.WriteLine($"[{_databaseType}][Close] **********************");

        }
        
        public Boolean IsOpen()
        {
            return true;
        }
    }
}
