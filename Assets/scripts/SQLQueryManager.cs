using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SQLQueryManager : Singleton<SQLQueryManager>
{
    IDbConnection dbcon;
    // Start is called before the first frame update
    void Awake()
    {
        // Create database
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";

        // Open connection
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Create table
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS Patient (id INTEGER PRIMARY KEY, " +
            "name VARACHAR(100), " +
            "surname VARCHAR(100), " +
            "condition TEXT )";

        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();

        q_createTable = "CREATE TABLE IF NOT EXISTS RDV (id INTEGER PRIMARY KEY," +
            " patientId INTEGER, " +
            "time DATETIME, " +
            "description TEXT," +
            " FOREIGN KEY (PatientId) REFERENCES Patient(Id) ) ";

        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();
    }

    public IDataReader GetSelectQueryResult(string query)
    {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        return cmnd_read.ExecuteReader();
    }
}
