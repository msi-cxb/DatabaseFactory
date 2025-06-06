using AbstractFactoryPattern;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        var factoryArrayList = new ArrayList();

        factoryArrayList.Add(new TestDatabaseFactory());
        factoryArrayList.Add(new SqliteDatabaseFactory());
        factoryArrayList.Add(new DuckdbDatabaseFactory());
        factoryArrayList.Add(new SqliteODBCDatabaseFactory());
        factoryArrayList.Add(new DuckdbODBCDatabaseFactory());

        foreach (IDatabaseFactory f in factoryArrayList) 
        {
            // use factory to create new database interface
            // using will call Dispose to close the db
            using var db = new DatabaseManager(f);

            // get the database type
            var type = db.GetDatabaseType();
            Console.WriteLine($"******************************\nGetDatabaseType: {type}");

            // create/open database file using DatabaseType as file name
            db.GetDatabaseConnection($"__{type}.db");

            // execute some sql, making sure to use SQL that works in both SQLite3 and DuckDB

            // this uses SQLite3 types but should be compatible with DuckDB
            db.ExecuteNonQuery("DROP TABLE IF EXISTS sampleOne;");
            db.ExecuteNonQuery("CREATE TABLE if not exists sampleOne(i INTEGER, r REAL, t TEXT);");
            db.ExecuteNonQuery("INSERT INTO sampleOne VALUES (1,1.1,'hello'),(2,2.2,'world')");
            var rtn = db.ExecuteScalar("Select count(*) from sampleOne");
            Console.WriteLine($"ExecuteScalar return {rtn}");
            db.ExecuteReader("SELECT i,r,t FROM sampleOne;");

            // this uses DuckDB types that should be compatible with SQLite3
            db.ExecuteNonQuery("DROP TABLE IF EXISTS sampleTwo;");
            db.ExecuteNonQuery("CREATE TABLE if not exists sampleTwo(i INTEGER, r FLOAT, t VARCHAR);");
            db.ExecuteNonQuery("INSERT INTO sampleTwo VALUES (1,1.1,'hello'),(2,2.2,'world')");
            rtn = db.ExecuteScalar("Select count(*) from sampleTwo");
            Console.WriteLine($"ExecuteScalar return {rtn}");
            db.ExecuteReader("SELECT i,r,t FROM sampleTwo;");
        }
    }
}