﻿using System.Collections;
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
		if (gameObject.activeInHierarchy == false)
		{
			return;
		}
		
		if(NpcWaiting != null && QTEManager.CurrentlyHeldId == null)
		{
			QTEManager.CurrentlyHeldId = NpcWaiting.ID;
			NPCs.NPCIdGUI.Instance.SetID(NpcWaiting.ID);
			GameplayEvents.NotifyOnTakeCustomerID(NpcWaiting.ID);

			Debug.Log("NotifyOnTakeCustomerID");
		}
		else if(NpcWaiting != null && QTEManager.CurrentlyHeldId.Equals(NpcWaiting.ID))
		{
			QTEManager.CurrentlyHeldId = null;
			QTEManager.NotifyTaskFinished();
			GameplayEvents.NotifyOnReturnCustomerID(NpcWaiting.ID);
			NPCs.NPCIdGUI.Instance.gameObject.SetActive(false);

			Debug.Log("NotifyOnReturnCustomerID");
		}
	}


	#endregion

	#region ENUMS

	#endregion


}