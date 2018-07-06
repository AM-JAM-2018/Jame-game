using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCGenerator
{
    [CustomEditor(typeof(NPCGenerator))]
    public class NPCGeneratorEditor : Editor
    {
        private NPCGenerator Generator = null;

        private void OnEnable()
        {
            Generator = target as NPCGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Generate"))
                Generator.Generate();
        }
    }
}

