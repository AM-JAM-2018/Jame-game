using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameDatabase.asset", menuName = "NameDatabase")]
public class NameDatabase : ScriptableObject
{
    private static NameDatabase _instance = null;
    public static NameDatabase Instance
    {
        get
        {
            if (_instance == null)
                _instance = (NameDatabase)Resources.Load("NameDatabase");

            return _instance;
        }
    }

    [SerializeField] List<string> _name = new List<string>();
    [SerializeField] List<string> _surname = new List<string>();

    public string[] GetName()
    {
        return new string[] { _name[Random.Range(0, _name.Count)], _surname[Random.Range(0, _surname.Count)] };
    }
}
