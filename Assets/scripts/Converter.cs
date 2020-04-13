using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;


public class Converter : Singleton<Converter>
{
    public void MakeNewPatient(string name, string surname, string condition)
    {
        Patient patient = new Patient(null, name, surname, condition);
        patient.AddToDatabaseAsNew();
    }

    public void MakeNewRDV(string dateTime, int patientId, string description)
    {
        RDV rdv = new RDV(null,  patientId, dateTime, description);
        rdv.AddToDatabaseAsNew();
    }

    public List<Patient> GetPatientsFromQuery(string query,Dictionary<string,string> parameters)
    {
        List<Patient> result = new List<Patient>();
        IDataReader reader = SQLQueryManager.Instance.GetSelectQueryResult(query, parameters);
        while (reader.Read())
        {
            result.Add(new Patient(reader));
        }
        return result;
    }

    public List<RDV> GetRDVsFromQuery(string query, Dictionary<string, string> parameters)
    {
        List<RDV> result = new List<RDV>();
        IDataReader reader = SQLQueryManager.Instance.GetSelectQueryResult(query, parameters);
        while (reader.Read())
        {
            result.Add(new RDV(reader));
        }
        return result;
    }
}
