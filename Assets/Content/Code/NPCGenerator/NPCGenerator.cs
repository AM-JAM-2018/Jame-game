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
        private List<INPCDresser> _dressers = new List<INPCDresser>();
        [SerializeField] private List<Object> _generatorObjects = new List<Object>();

        private void Awake()
        {
            for (int i = 0; i < _generatorObjects.Count; i++)
                if (_generatorObjects[i] is INPCDresser)
                    _dressers.Add(_generatorObjects[i] as INPCDresser);
        }

        public GameObject Generate()
        {
            GameObject npc = new GameObject();
            npc.name = "Npc";

            for (int i = 0; i < _dressers.Count; i++)
                _dressers[i].Dress(npc);

            return npc;
        }
    }
}