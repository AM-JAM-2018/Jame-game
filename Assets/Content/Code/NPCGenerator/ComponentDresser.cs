using NPCGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentDresser<T> : MonoBehaviour, INPCDresser where T : Component
{
    public void Dress(GameObject npc)
    {
        npc.AddComponent<T>();
    }
}
