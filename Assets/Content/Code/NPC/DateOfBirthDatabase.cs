using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DateOfBirthDatabase.asset", menuName = "DateOfBirthDatabase")]
public class DateOfBirthDatabase : ScriptableObject
{
    private static DateOfBirthDatabase _instance = null;
    public static DateOfBirthDatabase Instance
    {
        get
        {
            if (_instance == null)
                _instance = (DateOfBirthDatabase)Resources.Load(typeof(DateOfBirthDatabase).Name);

            return _instance;
        }
    }

    [SerializeField] private string [] dataFormat;
    [SerializeField] private int _minDay = 1;
    [SerializeField] private int _maxDay = 31;
    [SerializeField] private int _minMontch = 1;
    [SerializeField] private int _maxMontach = 30;
    [SerializeField] private int _minYear = 0;
    [SerializeField] private int _maxYert = 6000;

    public string GetDate()
    {
        return string.Format(dataFormat[Random.Range(0, dataFormat.Length)], Random.Range(_minDay, _maxDay), Random.Range(_minMontch, _maxMontach), Random.Range(_minYear, _maxYert)); 
    }
}
