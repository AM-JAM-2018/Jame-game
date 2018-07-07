using UnityEngine;
using UnityEngine.Events;

public class RODOCamera : MonoBehaviour
{
    public bool Status = false;

    [SerializeField] private float _minDeactivatedInterval = 0f;
    [SerializeField] private float _maxDeactivatedInterval = 1f;

    [SerializeField] private float _minActivatedInterval = 0f;
    [SerializeField] private float _maxActivatedInterval = 1f;

    [SerializeField] public UnityEvent CameraActivated = new UnityEvent();
    [SerializeField] public UnityEvent CameraDeactivated = new UnityEvent();

    private void Awake()
    {
        Invoke("Activate", GetInterval(_minActivatedInterval, _maxActivatedInterval));
    }

    private float GetInterval(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void Activate()
    {
        Status = true;
        CameraActivated.Invoke();
        Invoke("Deactivate", GetInterval(_minDeactivatedInterval, _maxDeactivatedInterval));
    }

    public void Deactivate()
    {
        Status = false;
        CameraDeactivated.Invoke();
        Invoke("Activate", GetInterval(_minActivatedInterval, _maxActivatedInterval));
    }
}
