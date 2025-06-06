using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractFactoryPattern.DatabaseInterface;

namespace AbstractFactoryPattern
{
    public class DatabaseInterface
    {
        public interface IDatabaseService
        {
            public string GetDatabaseType();
            public void GetConnection(string filename);
            public void ExecuteNonQuery(string sql);
            public int ExecuteScalar(string sql);
            public void ExecuteReader(string sql);
            public void Close();
            public Boolean IsOpen();
        }
    }

    public interface IDatabaseFactory
    {
        public IDatabaseService CreateDatabaseService();
    }
}
