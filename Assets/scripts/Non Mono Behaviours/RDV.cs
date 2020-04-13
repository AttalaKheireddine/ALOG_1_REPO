using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class RDV : DatabaseObject
{
    public int? id;   //becasue sometimes the object is new so its id is not mattering
    public string dateTime;
    public int patientId;
    public string description;

    const string dateTimeParameter = "@dateTime";
    const string patientIdParameter = "@patientId";
    const string idParameter = "@id";
    const string descriptionPrameter = "@description";

    public RDV(int? id,  int patientId, string dateTime, string description)
    {
        this.id = id;
        this.dateTime = dateTime;
        this.patientId = patientId;
        this.description = description;
    }

    public RDV(IDataReader reader)
    {
        this.id = int.Parse(reader[0].ToString());
        this.dateTime = reader[2].ToString();
        this.patientId = int.Parse(reader[1].ToString());
        this.description = reader[3].ToString();
    }

    public void AddToDatabaseAsNew()
    {
        string tableName = SQLQueryManager.Instance.RDVTableName;
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add(dateTimeParameter,dateTime);
        parameters.Add(patientIdParameter,patientId.ToString());
        parameters.Add(descriptionPrameter,description);

        string queryText = string.Format("INSERT INTO {0} (patientId,time, description) values ({1},'{2}','{3}')"
            , tableName,patientId,dateTime,description);
        Debug.Log(queryText);
        SQLQueryManager.Instance.ExecuteNonSelectQuery(queryText, parameters);
    }

    public void UpdateIntoDatabase()
    {
        string tableName = SQLQueryManager.Instance.RDVTableName;
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add(descriptionPrameter,description);
        parameters.Add(idParameter, id.ToString());
        parameters.Add(dateTimeParameter, dateTime);
        parameters.Add(patientIdParameter, patientId.ToString());

        string queryText = string.Format("UPDATE {0} time = '{1}', patientId = {2}, description = '{3}'  WHERE id ={4} ",
            tableName, dateTimeParameter, patientIdParameter, descriptionPrameter, idParameter);
        SQLQueryManager.Instance.ExecuteNonSelectQuery(queryText, parameters);
    }
}
