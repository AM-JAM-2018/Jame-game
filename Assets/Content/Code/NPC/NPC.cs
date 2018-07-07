using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NPCs
{
    [RequireComponent(typeof(NPCId))]
    public class NPC : MonoBehaviour
    {
        public enum RaceEnum
        {
            Alien,
            Dog,
            Reptile
        }

        [SerializeField] private RaceEnum _race = RaceEnum.Alien;
        [SerializeField] private NPCId _id = null;
        [SerializeField] private NPCWalkingController _walkingController = null;
        public RaceEnum Race { get { return _race; } }
        public NPCId ID
        {
            get
            {
                if(_id == null)
                    _id = GetComponent<NPCId>();

                return _id;
            }
        }
        public NPCWalkingController WalkingController {
            get {
                _walkingController = GetComponent<NPCWalkingController>();

                return _walkingController;
            }
        }


        [SerializeField, Range(0f, 1f)] private float _patience = 1f;
        public float Patience { get { return _patience; } }
        public float PatienceLossRate = 0.1f;

        [SerializeField] private bool _isWaiting = false;
        public bool IsWaiting { get { return _isWaiting; } }

        public UnityEvent WaitingStarted = new UnityEvent();
        public UpdatePatience UpdatePatience = new UpdatePatience();

        public UnityEvent PatianceLost = new UnityEvent();

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

        private void Update()
        {
            if(IsWaiting)
            {
                if ((_patience -= PatienceLossRate * Time.deltaTime) <= 0f)
                {
                    PatianceLost.Invoke();
                    _isWaiting = false;
                }
                UpdatePatience.Invoke(_patience);
            }
        }

        public void Wait()
        {
            WaitingStarted.Invoke();
            _isWaiting = true;
        }
    }

    [Serializable] public class UpdatePatience : UnityEvent<float> {}
}
