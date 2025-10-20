# Database

[toc]

## About

A C# factory that talks to PostgreSQL, DuckDB and SQLite3 databases. In the later two cases, both .NET and ODBC are implemented. 

Based on https://www.c-sharpcorner.com/article/abstract-factory-pattern-for-notification-services-in-c-sharp-14/

## Dependencies

### .NET

Uses .NET Core 9

### ODBC Drivers

- SQLite ODBC driver (latest)
- DuckDB ODBC driver (latest)

### NuGet packages

- DuckDB.NET.Data.Full 1.4.1
- System.Data.SQLite 1.0.119
- PostgreSQL Npgsql 9.0.4

## TODO

- [ ] add threading support (async/await/etc.)
- [ ] 



## References

* [Interlocked Class (System.Threading) | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-9.0)
