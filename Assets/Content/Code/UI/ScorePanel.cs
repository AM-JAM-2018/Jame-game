using NPCs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour, IResetable
{
    [SerializeField] private List<GameObject> _heartFullList = new List<GameObject>();
    [SerializeField] private Text _score = null;
    [SerializeField] private Text _name = null;


    private void Awake()
    {
        GameplayEvents.UpdateTotalScore += UpdateScore;
        GameplayEvents.OnIDDataEnterFail += LifeLost;
        GameplayEvents.OnCameraCatchFail += LifeLost;
        GameplayEvents.PlayerNameUpdateCallback += UpdateName;
            
        _score.text = "0";
        _name.text = string.Empty;
    }

    private void UpdateName(string name)
    {
        _name.text = name;
    }

    private void UpdateScore(int value)
    {
        _score.text = value.ToString();
    }

    public void ResetData()
    {
        foreach (var item in _heartFullList)
            item.gameObject.SetActive(true);
    }

    public void LifeLost(NPCId ID)
    {
        foreach (var item in _heartFullList)
        {
            if(item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(false);
                break;
            }
        }
    }
}
