using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    private float _timeScale = 0;
    private float _timeStemp = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        _timeScale = Time.timeScale;
        _timeStemp = Time.maximumDeltaTime;
    }

    public void Pouse()
    {
        Time.timeScale= 0;
        Time.maximumDeltaTime = 0;
        GameplayEvents.NotifyOnLockPlayerInput();
    }

    public void Play()
    {
        Time.timeScale = _timeScale;
        Time.maximumDeltaTime = _timeStemp;
        GameplayEvents.NotifyOnUnlockPlayerInput();
    }
}
