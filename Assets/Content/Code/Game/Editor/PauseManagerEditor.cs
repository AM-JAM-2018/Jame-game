using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PauseManager))]
public class PauseManagerEditor : Editor
{
    private PauseManager manager = null;

    private void OnEnable()
    {
        manager = target as PauseManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Time.timeScale == 0f)
        {
            if (GUILayout.Button("Play"))
                manager.Play();
        }
        else
        {
            if (GUILayout.Button("Pause"))
                manager.Pouse();
        }
    }
}
