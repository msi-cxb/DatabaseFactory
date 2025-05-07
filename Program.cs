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
            // use factory to create new database interface
            // using will call Dispose to close the db
            using var db = new DatabaseManager(f);

            // get the database type
            var type = db.GetDatabaseType();

            // create/open database file
            db.GetDatabaseConnection($"{type}.db");

            // execute some sql
            db.ExecuteNonQuery("DROP TABLE IF EXISTS sampleOne;");
            db.ExecuteNonQuery("CREATE TABLE if not exists sampleOne(i INTEGER, r REAL, t TEXT);");
            db.ExecuteNonQuery("INSERT INTO sampleOne VALUES (1,1.1,'hello'),(2,2.2,'world')");
            var rtn = db.ExecuteScalar("Select count(*) from sampleOne");
            db.ExecuteReader("SELECT i,r,t FROM sampleOne;");

            db.ExecuteNonQuery("DROP TABLE IF EXISTS sampleTwo;");
            db.ExecuteNonQuery("CREATE TABLE if not exists sampleTwo(i INTEGER, r FLOAT, t VARCHAR);");
            db.ExecuteNonQuery("INSERT INTO sampleTwo VALUES (1,1.1,'hello'),(2,2.2,'world')");
            rtn = db.ExecuteScalar("Select count(*) from sampleTwo");
            db.ExecuteReader("SELECT i,r,t FROM sampleTwo;");
        }
    }
}