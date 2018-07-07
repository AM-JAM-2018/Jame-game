using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TimeManagent
{
    public class GeneralUnityEventTimerAction : BaseTimerAction
    {
        [SerializeField] private UnityEvent @event = new UnityEvent();

        public override void Perform(params object[] data)
        {
            @event.Invoke();
        }
    }
}
