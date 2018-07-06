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
        public RaceEnum Race { get { return _race; } }

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
