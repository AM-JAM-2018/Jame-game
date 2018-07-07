using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "NameDatabase.asset", menuName = "NameDatabase")]
    public class NameDatabase : ScriptableObject
    {
        private static NameDatabase _instance = null;
        public static NameDatabase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (NameDatabase)Resources.Load(typeof(NameDatabase).Name);

                return _instance;
            }
        }

        [SerializeField] List<string> _name = new List<string>();
        [SerializeField] List<string> _surname = new List<string>();

        [SerializeField] List<string> _sex = new List<string>();

        public string[] GetName()
        {
            return new string[] { Select(_name), Select(_surname) };
        }

        public string GetSex()
        {
            return Select(_sex);
        }

        private string Select(List<string> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}