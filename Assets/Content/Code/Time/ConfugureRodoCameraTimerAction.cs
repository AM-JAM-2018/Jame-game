using System.Collections;
using System.Collections.Generic;
using TimeManagent;
using UnityEngine;

using Config = RODOCamera.RODOCameraConfig;

public class ConfugureRodoCameraTimerAction : BaseTimerAction
{
    [SerializeField] private Config _config = new Config();

    public override void Perform(params object[] data)
    {
        RODOCamera.Instance.Config = _config;
    }
}
