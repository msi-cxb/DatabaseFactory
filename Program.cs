using AbstractFactoryPattern;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        var factoryArrayList = new ArrayList();

        factoryArrayList.Add(new SqliteDatabaseFactory());
        factoryArrayList.Add(new DuckdbDatabaseFactory());

        foreach(IDatabaseFactory f in factoryArrayList) 
        {
            var db = new DatabaseManager(f);
            var type = db.GetDatabaseType();
            db.GetDatabaseConnection($"{type}.db");
            db.ExecuteNonQuery("DROP TABLE IF EXISTS integers;");
            db.ExecuteNonQuery("CREATE TABLE if not exists integers(foo INTEGER, bar INTEGER);");
            db.ExecuteNonQuery("INSERT INTO integers VALUES (3, 4), (5, 6), (7, NULL);");
            var rtn = db.ExecuteScalar("Select count(*) from integers");
            db.Close();
        }
    }
}