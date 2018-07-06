using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCGenerator
{

    public class GameObjectDresser : MonoBehaviour, INPCDresser
    {
        [SerializeField] Vector3 _localPosition = Vector3.zero;

        [SerializeField] private List<GameObject> prefabList = new List<GameObject>();

        public void Dress(GameObject npc)
        {
            GameObject @object = Instantiate(prefabList[Random.Range(0, prefabList.Count)], npc.transform);
            @object.transform.localPosition = _localPosition;
        }
    }
}