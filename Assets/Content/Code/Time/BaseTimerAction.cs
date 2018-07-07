using UnityEngine;

namespace TimeManagent
{
    public abstract class BaseTimerAction : MonoBehaviour
    {
        public abstract void Perform(params object[] data);
    }
}