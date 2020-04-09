﻿using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SqliteTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        // Create database
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";

        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
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

        // Insert values in table
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO patient (name,surname,condition) VALUES ('a','b','c')";
        cmnd.ExecuteNonQuery();

        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM patient";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("name: " + reader[1].ToString());
            Debug.Log("surname: " + reader[2].ToString());
            Debug.Log("condition: " + reader[3].ToString());
        }

        // Close connection
        dbcon.Close();

    }

    // Update is called once per frame
    void Update()
    {

    }
}