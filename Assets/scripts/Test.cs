using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IDataReader reader = SQLQueryManager.Instance.GetSelectQueryResult("SELECT * FROM Patient");

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("name: " + reader[1].ToString());
            Debug.Log("surname: " + reader[2].ToString());
            Debug.Log("condition: " + reader[3].ToString());
        }
    }
}
