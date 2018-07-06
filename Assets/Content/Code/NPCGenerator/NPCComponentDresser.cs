using NPCGenerator;
using NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComponentDresser : MonoBehaviour, INPCDresser
{
    public void Dress(GameObject npc)
    {
        npc.AddComponent<NPC>();
    }
}
