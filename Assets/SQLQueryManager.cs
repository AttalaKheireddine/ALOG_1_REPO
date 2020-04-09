using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SQLQueryManager : Singleton<SQLQueryManager>
{
    IDbConnection dbcon;

    public string patientTableName
    {
        get
        {
            return "Patient";
        }
    }

    public string RDVTableName
    {
        get
        {
            return "RDV";
        }
    }

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

    public void ExecuteNonSelectQuery(string query,Dictionary<string,string> parameters)
    {
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = query;
        foreach(string param in parameters.Keys)
        {
            cmnd.Parameters.Add(new SqliteParameter(param, parameters[param]));
        }
        cmnd.Prepare();
        cmnd.ExecuteNonQuery();
    }
}
