using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : InteractableObject {

	#region MEMBERS

	[SerializeField]
	private NPCs.NPC npcWaiting;

	#endregion

	#region PROPERTIES

	public NPCs.NPC NpcWaiting
	{
		get {
			return npcWaiting;
		}
		set {
			npcWaiting = value;
		}
	}

	#endregion

	#region METHODS

	public override void EnableInteraction()
	{
		if(NpcWaiting != null && QTEManager.CurrentlyHeldId == null)
		{
			QTEManager.CurrentlyHeldId = NpcWaiting.ID;
			GameplayEvents.NotifyOnTakeCustomerID(NpcWaiting.ID);
		}
		else if(NpcWaiting != null && QTEManager.CurrentlyHeldId.Equals(NpcWaiting.ID))
		{
			QTEManager.CurrentlyHeldId = null;
			QTEManager.NotifyTaskFinished();
			GameplayEvents.NotifyOnReturnCustomerID(NpcWaiting.ID);
		}
	}


	#endregion

	#region ENUMS

	#endregion


}
