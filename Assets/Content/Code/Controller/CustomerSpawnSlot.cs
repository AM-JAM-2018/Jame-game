using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnSlot : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private Transform customerTargetPoint;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private Transform CustomerTargetPoint {
		get {return customerTargetPoint;}
	}

	// VARIABLES
	public bool IsEnabled {get; private set;}
	private NPCs.NPC CurrentSpawnedNPC {get; set;}

	#endregion

	#region FUNCTIONS

	public void SetActiveState (bool state)
	{
		IsEnabled = state;
	}

	public void TryToSpawnCustomer ()
	{
		if (CurrentSpawnedNPC != null || IsEnabled == false)
		{
			return;
		}

		CurrentSpawnedNPC = NPCGenerator.NPCGenerator.instance.Generate();

		CurrentSpawnedNPC.transform.position = transform.position;
		CurrentSpawnedNPC.transform.rotation = transform.rotation;

		NPCWalkingController npcWalker = CurrentSpawnedNPC.GetComponent<NPCWalkingController>();

		npcWalker.SetStartPoint(transform);
		npcWalker.SetEndPoint(CustomerTargetPoint);

		npcWalker.GoTowardsEndPoint(CurrentSpawnedNPC.Wait);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
