using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class Patient : DatabaseObject
{
    public int? id;   //becasue sometimes the object is new so its id is not mattering
    public string name;
    public string surname;
    public string medicalCondition;

    const string nameParameter = "@name";
    const string surnameParameter = "@surname";
    const string idParameter = "@id";
    const string medicalConditionParameter = "@med_cond"; 

    public Patient(int? id, string name, string surname, string medicalCondition)
    {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.medicalCondition = medicalCondition;
    }

    public Patient(IDataReader reader)
    {
        this.id = int.Parse(reader[0].ToString());
        this.name = reader[1].ToString();
        this.surname = reader[2].ToString();
        this.medicalCondition = reader[3].ToString();
    }

    public void AddToDatabaseAsNew()
    {
        string tableName = SQLQueryManager.Instance.patientTableName;
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add(nameParameter, name);
        parameters.Add(surnameParameter, surname);
        parameters.Add(medicalConditionParameter, medicalCondition);

        string queryText = string.Format("INSERT INTO {0} (name,surname,condition) values ('{1}','{2}','{3}')"
            , tableName, nameParameter, surnameParameter, medicalConditionParameter);
        SQLQueryManager.Instance.ExecuteNonSelectQuery(queryText, parameters);
    }

    public void UpdateIntoDatabase()
    {

        string tableName = SQLQueryManager.Instance.patientTableName;
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add(nameParameter, name);
        parameters.Add(idParameter, id.ToString());
        parameters.Add(surnameParameter, surname);
        parameters.Add(medicalConditionParameter, medicalCondition);

        string queryText = string.Format("UPDATE {0} name = '{1}', surname = '{2}', condition = '{3}'  WHERE id ={4} ",
            tableName, nameParameter, surnameParameter, medicalConditionParameter, idParameter);
        SQLQueryManager.Instance.ExecuteNonSelectQuery(queryText, parameters);
    }
}
