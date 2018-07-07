using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManagent
{
    public class TimerMeshDisplay : MonoBehaviour
    {
        [SerializeField] Transform _minutes = null;
        [SerializeField] Transform _seconds = null;

        public void UpdateTimer(float time)
        {
            var ms = TimeConverter.TimeToAngle(time);
            _minutes.localRotation = Quaternion.Euler(ms[0]);
            _seconds.localRotation = Quaternion.Euler(ms[1]);
        }
    }
}
