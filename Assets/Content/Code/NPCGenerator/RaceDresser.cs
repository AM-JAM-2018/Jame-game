using NPCGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCs;

public class RaceDresser : MonoBehaviour, INPCDresser
{
    public void Dress(GameObject npc)
    {
        NPC npcComponent = npc.GetComponentInChildren<NPC>();
        GameObject @object = null;
        if(_raceObjectDictionary.TryGetValue(npcComponent.Race, out @object))
        {
            Instantiate(@object, npc.transform).transform.localPosition = _localPosition;
        }
    }

    [Serializable]
    public class RaceObjectPair
    {
        public NPC.RaceEnum Race = NPC.RaceEnum.Alien;
        public GameObject RaceObject = null;
    }

    [SerializeField] private List<RaceObjectPair> _raceObjectList = new List<RaceObjectPair>();
    Dictionary<NPC.RaceEnum, GameObject> _raceObjectDictionary = new Dictionary<NPC.RaceEnum, GameObject>();
    [SerializeField] private Vector3 _localPosition = Vector3.zero;

    private void Awake()
    {
        for (int i = 0; i < _raceObjectList.Count; i++)
        {
            _raceObjectDictionary.Add(_raceObjectList[i].Race, _raceObjectList[i].RaceObject);
        }
    }

}
