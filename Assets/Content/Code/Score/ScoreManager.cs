using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreManager.asset", menuName = "ScoreManager")]
public class ScoreManager : ScriptableObject
{
    [Serializable]
    public class Score : IComparable
    {
        public string Name = string.Empty;
        public int Value = 0;

        public Score(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public int CompareTo(object obj)
        {
            Score score = obj as Score;
            if (Value > score.Value)
                return -1;
            else if (Value < score.Value)
                return 1;

            return 0;
        }
    }

    private static ScoreManager _instance = null;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = (ScoreManager)Resources.Load(typeof(ScoreManager).Name);

            return _instance;
        }
    }

    [SerializeField] private List<Score> _scoreList = new List<Score>();
    public List<Score> ScoreList { get { return _scoreList; } }

    public void AddScore(string name, int value)
    {
        var currentScore = _scoreList.Count == 0? null : _scoreList.FirstOrDefault(s => s.Name == name);
        if (currentScore == null)
            _scoreList.Add(new Score(name, value));
        else
            currentScore.Value = value > currentScore.Value ? value : currentScore.Value;

        _scoreList.Sort();
    }

    public void Reset()
    {
        _scoreList.Clear();
    }
}
