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
        Converter.Instance.MakeNewRDV("2020-12-11 23:56:38", 2, "something stupid");
        List<RDV> rdvs = Converter.Instance.GetRDVsFromQuery("SELECT * FROM RDV;", new Dictionary<string, string>());
        foreach(RDV p in rdvs)
        {
            Debug.Log(string.Format("patient id = {0} name = {1} surname = {2}", p.id,p.description,p.patientId));
        }
    }
}
