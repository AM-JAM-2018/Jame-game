﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TimeManagent
{
    public class GameTimer : MonoBehaviour
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
            if (timeStemps[_index].Activate(_counter += Time.deltaTime))
                if (++_index == timeStemps.Length)
                    _index = timeStemps.Length - 1;

            if (_counter - _oldCounter >= 1f)
            {
                _oldCounter = _counter;
                updateTime.Invoke(_counter);
            }
        }

        [Serializable] public class UpdateTime : UnityEvent<float> { }
    }
}