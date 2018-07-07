using System;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class RODOCamera : MonoBehaviour
{
    public static RODOCamera Instance { get; private set; }
    public bool Status = false;

    [Serializable]
    public class RODOCameraConfig
    {
        [SerializeField] private float _minDeactivatedInterval = 0f;
        public float MinDeactivatedInterval { get { return _minDeactivatedInterval; } }
        [SerializeField] private float _maxDeactivatedInterval = 1f;
        public float MaxDeactivatedInterval { get { return _maxDeactivatedInterval; } }

        [SerializeField] private float _minActivatedInterval = 0f;
        public float MinActivatedInterval { get { return _minActivatedInterval; } }
        [SerializeField] private float _maxActivatedInterval = 1f;
        public float MaxActivatedInterval { get { return _maxActivatedInterval; } }
    }

    public RODOCameraConfig Config = new RODOCameraConfig(); 


    [SerializeField] public UnityEvent CameraActivated = new UnityEvent();
    [SerializeField] public UnityEvent CameraDeactivated = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        Invoke("Activate", GetInterval(Config.MinActivatedInterval, Config.MaxActivatedInterval));
    }

    private float GetInterval(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void Activate()
    {
        Status = true;
        CameraActivated.Invoke();
        Invoke("Deactivate", GetInterval(Config.MinDeactivatedInterval, Config.MaxDeactivatedInterval));
    }

    public void Deactivate()
    {
        Status = false;
        CameraDeactivated.Invoke();
        Invoke("Activate", GetInterval(Config.MinActivatedInterval, Config.MaxActivatedInterval));
    }
}
