using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    [RequireComponent(typeof(NPCId))]
    public class NPC : MonoBehaviour
    {
        public enum RaceEnum
        {
            Man,
            Alien,
            Dog,
            Cat
        }

        [SerializeField] private RaceEnum _race = RaceEnum.Man;
        [SerializeField] private NPCId _id = null;
        public RaceEnum Race { get { return _race; } }
        public NPCId ID {
            get {
                _id = GetComponent<NPCId>();

                return _id;
            }
        }

        private void Awake()
        {
            SetRace();
        }

        private void Reset()
        {
            SetRace();
        }

        private void SetRace()
        {
            int lenght = Enum.GetNames(typeof(RaceEnum)).Length;
            _race = (RaceEnum)UnityEngine.Random.Range(0, lenght);
        }
    }
}
