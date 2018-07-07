using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCs
{
    [CustomEditor(typeof(NPC))]
    public class NPCInspector : Editor
    {
        private NPC _npc = null;

        private void OnEnable()
        {
            _npc = target as NPC;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Wait"))
                _npc.Wait();
        }
    }
}