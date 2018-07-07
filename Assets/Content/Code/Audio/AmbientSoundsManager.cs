using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundsManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audioSource = new List<AudioSource>();
    [SerializeField] private List<AudioClip> _audioClipList = new List<AudioClip>();
    [SerializeField] private float _minInterval = 1f;
    [SerializeField] private float _maxInterval = 3f;

    private void Awake()
    {
        PlayAudio();
    }

    public void PlayAudio()
    {
        for (int i = 0; i < _audioSource.Count; i++)
        {
            if(!_audioSource[i].isPlaying)
            {
                _audioSource[i].clip = _audioClipList[Random.Range(0, _audioClipList.Count)];
                _audioSource[i].Play();
                break;
            }
        }

        Invoke("PlayAudio", Random.Range(_minInterval, _maxInterval));
    }
}
