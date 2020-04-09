using System.Data;
using Mono.Data.Sqlite;
public interface DatabaseObject
{
    void AddToDatabaseAsNew();
    void UpdateIntoDatabase();
}
