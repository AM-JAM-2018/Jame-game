using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

namespace TimeManagent
{
    public class GameTimer : MonoBehaviour, IResetable
    {
        [Serializable]
        public class TimeStemp
        {
            [SerializeField] private bool _activated = false;
            [SerializeField] private float _minutes = 0;
            [SerializeField, Range(0, 59)] private float _seconds = 0;

            [SerializeField] List<BaseTimerAction> actions = new List<BaseTimerAction>();

            public float GetTime()
            {
                return (60 * _minutes) + (_seconds * 1);
            }

            public bool Activate(float timer, params object[] data)
            {
                if (GetTime() < timer && !_activated)
                {
                    for (int i = 0; i < actions.Count; i++)
                        actions[i].Perform(data);

                    _activated = true;
                }

                return _activated;
            }
        }

        [SerializeField] private float _counter = 0;
        [SerializeField] private float _oldCounter = 0;
        [SerializeField] private int _index = 0;
        [SerializeField] private TimeStemp[] timeStemps = null;

        [SerializeField] private UpdateTime updateTime = new UpdateTime();

        private void Update()
        {
            _counter += Time.deltaTime;

            if (timeStemps.Length > 0)
            {
                if (timeStemps[_index].Activate(_counter))
                    if (++_index == timeStemps.Length)
                        _index = timeStemps.Length - 1;
            }

            if (_counter - _oldCounter >= 1f)
            {
                _oldCounter = _counter;
                updateTime.Invoke(_counter);
            }
        }

        public void ResetData()
        {
            _index = 0;
            _counter = _oldCounter = 0;
        }

        [Serializable] public class UpdateTime : UnityEvent<float> { }
    }
}