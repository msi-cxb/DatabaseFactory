using AbstractFactoryPattern;
using System.Collections;

class Program
{
    static void Main()
    {
        var factoryArrayList = new ArrayList();

        factoryArrayList.Add(new TestDatabaseFactory());
        factoryArrayList.Add(new SqliteDatabaseFactory());
        factoryArrayList.Add(new PostgreSQLDatabaseFactory());
        factoryArrayList.Add(new SqliteODBCDatabaseFactory());
        factoryArrayList.Add(new DuckdbODBCDatabaseFactory());
        factoryArrayList.Add(new PostgreSQLDatabaseFactory());

        foreach (IDatabaseFactory f in factoryArrayList) 
        {
            // use factory to create new database interface
            // using will call Dispose to close the db
            using var db = new DatabaseManager(f);

            // get the database type
            var type = db.GetDatabaseType();
            Console.WriteLine($"******************************\nGetDatabaseType: {type}");

            // create/open database file using DatabaseType as file name
            switch( type )
            {
                case "PostgreSQL":
                    // this needs to be a valid PostgreSQL connection string
                    db.GetDatabaseConnection($"Host=charlies-MacBook-Pro.local;Username=postgres;Password=postgres;Database=postgres");
                    break;
                default:
                    // for file based databases, this is just the name of the database file
                    db.GetDatabaseConnection($"__{type}.db");
                    break;
            }
            
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

//var connString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";

//await using var conn = new NpgsqlConnection(connString);
//await conn.OpenAsync();

//// Insert some data
//await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
//{
//    cmd.Parameters.AddWithValue("p", "Hello world");
//    await cmd.ExecuteNonQueryAsync();
//}

//// Retrieve all rows
//await using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
//await using (var reader = await cmd.ExecuteReaderAsync())
//{
//    while (await reader.ReadAsync())
//        Console.WriteLine(reader.GetString(0));
//}