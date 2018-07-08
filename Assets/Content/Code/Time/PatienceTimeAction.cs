using System.Collections;
using System.Collections.Generic;
using TimeManagent;
using UnityEngine;

public class PatienceTimeAction : BaseTimerAction
{
    [SerializeField] private Vector2 _patienceRateRange = Vector2.zero;

    public override void Perform(params object[] data)
    {
        PatienceLossRateDresser.Instance.PatienceLossRate = _patienceRateRange;
    }
    
}
