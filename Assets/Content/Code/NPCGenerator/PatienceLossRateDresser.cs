using NPCGenerator;
using NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceLossRateDresser : MonoBehaviour, INPCDresser
{
    [SerializeField] private float _minPatienceLossRate = .05f;
    [SerializeField] private float _maxPatienceLossRate = .01f;

    public void Dress(GameObject npc)
    {
        npc.GetComponent<NPC>().PatienceLossRate = Random.Range(_minPatienceLossRate, _maxPatienceLossRate);
    }
}
