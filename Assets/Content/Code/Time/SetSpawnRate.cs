using System.Collections;
using System.Collections.Generic;
using TimeManagent;
using UnityEngine;

public class SetSpawnRate : BaseTimerAction
{
    [SerializeField] private Vector2 _spawnIntervalRange = Vector2.zero;

    public override void Perform(params object[] data)
    {
        CustomerSpawnController.Instance.SpawnIntervalRange = _spawnIntervalRange;
    }
}
