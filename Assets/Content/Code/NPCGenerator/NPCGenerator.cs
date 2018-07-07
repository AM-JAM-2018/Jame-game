using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCGenerator
{
    using Random = UnityEngine.Random;
    using Object = UnityEngine.Object;

    public class NPCGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject npcPrefab = null;
        private List<INPCDresser> _dressers = new List<INPCDresser>();
        [SerializeField] private List<Object> _generatorObjects = new List<Object>();

        public static NPCGenerator instance = null;

        private void Awake()
        {
			instance = this;
            
            for (int i = 0; i < _generatorObjects.Count; i++)
                if (_generatorObjects[i] is INPCDresser)
                    _dressers.Add(_generatorObjects[i] as INPCDresser);
        }

        public void AddDresser(INPCDresser dresser)
        {
            _dressers.Add(dresser);
        }

        public GameObject Generate()
        {
            GameObject npc = npcPrefab == null ? new GameObject() : Instantiate(npcPrefab);
            npc.name = "Npc";

            for (int i = 0; i < _dressers.Count; i++)
                _dressers[i].Dress(npc);

            return npc;
        }
    }
}