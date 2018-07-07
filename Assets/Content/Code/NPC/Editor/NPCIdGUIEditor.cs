using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCs
{

    [CustomEditor(typeof(NPCIdGUI))]
    public class NPCIdGUIEditor : Editor
    {
        [SerializeField] private NPCIdGUI gui = null;

        private void OnEnable()
        {
            gui = target as NPCIdGUI;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Show"))
                gui.Show();

            if(GUILayout.Button("Hide"))
                gui.Hide();
        }
    }
}