using NPCGenerator;
using NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceLossRateDresser : MonoBehaviour, INPCDresser
{
    public static PatienceLossRateDresser Instance { get; private set; }

    [SerializeField] private Vector2 _patienceLossRate = new Vector2(0.001f, 0.01f);

    public Vector2 PatienceLossRate
    {
        get { return _patienceLossRate; }
        set { _patienceLossRate = value; }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void Dress(GameObject npc)
    {
        npc.GetComponent<NPC>().PatienceLossRate = Random.Range(_patienceLossRate.x, _patienceLossRate.y);
    }
}
