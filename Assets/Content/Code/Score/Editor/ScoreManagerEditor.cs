using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScoreManager))]
public class ScoreManagerEditor : Editor
{
    private ScoreManager _scoreManager = null;

    private string _name = string.Empty;
    private int _value = 0;

    private void OnEnable()
    {
        _scoreManager = target as ScoreManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _name = EditorGUILayout.TextField("Name: ", _name, GUILayout.Width(200));
        _value = EditorGUILayout.IntField("Value", _value, GUILayout.Width(200));
        if (GUILayout.Button("Add"))
            _scoreManager.AddScore(_name, _value);

        if (GUILayout.Button("Reset"))
            _scoreManager.Reset();
    }
}
