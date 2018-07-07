using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnSlot : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private GameObject targetTrigger;
	[SerializeField]
	private Transform customerTargetPoint;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private GameObject TargetTrigger {
		get {return targetTrigger;}
	}
	private Transform CustomerTargetPoint {
		get {return customerTargetPoint;}
	}

	// VARIABLES
	public bool IsEnabled {get; private set;}
	public NPCs.NPC CurrentSpawnedNPC {get; private set;}

	#endregion

	#region FUNCTIONS

	public void SetActiveState (bool state)
	{
		IsEnabled = state;
	}

	public void SetTriggerState (bool state)
	{
		TargetTrigger.SetActive(state);
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
