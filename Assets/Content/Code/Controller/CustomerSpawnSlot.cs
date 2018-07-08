using System;
using System.Collections;
using System.Collections.Generic;
using NPCs;
using UnityEngine;

public class CustomerSpawnSlot : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private GameObject targetTrigger;
	[SerializeField]
	private Transform customerTargetPoint;
	[SerializeField]
	private WindowController customerWindow;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private GameObject TargetTrigger {
		get {return targetTrigger;}
	}
	private Transform CustomerTargetPoint {
		get {return customerTargetPoint;}
	}
	private WindowController CustomerWindow {
		get {return customerWindow;}
	}

	// VARIABLES
	public bool IsEnabled {get; private set;}
	public NPCs.NPC CurrentSpawnedNPC {get; private set;}

	#endregion

	#region FUNCTIONS

	public void SetActiveState (bool state)
	{
		IsEnabled = state;

		SetTriggerState(state);
	}

	public void SetTriggerState (bool state)
	{
		TargetTrigger.SetActive(state);
	}

	public void DisposeOfCustomer ()
	{
		if (CurrentSpawnedNPC != null)
		{
			// CurrentSpawnedNPC.??? cancel his waiting?
			
			Destroy(CurrentSpawnedNPC.gameObject);
		}
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

		CurrentSpawnedNPC.PatianceLost.AddListener(FailCurrentCustomer);

		CurrentSpawnedNPC.WalkingController.SetStartPoint(transform);
		CurrentSpawnedNPC.WalkingController.SetEndPoint(CustomerTargetPoint);

		CurrentSpawnedNPC.WalkingController.GoTowardsEndPoint(AssignCurrentNPCToCustomerWindow, CurrentSpawnedNPC.Wait, ActivateWindowTrigger);
	}

	public void AssignCurrentNPCToCustomerWindow ()
	{
		CustomerWindow.NpcWaiting = CurrentSpawnedNPC;
	}

	public void ActivateWindowTrigger ()
	{
		if (QTEManager.CurrentlyHeldId != null)
		{
			return;
		}
		
		SetTriggerState(true);
	}

	protected void Awake ()
	{
		GameplayEvents.OnReturnCustomerID += HandleOnReturnCustomerID;
		GameplayEvents.OnEndEnteringIDData += HandleOnEndEnteringIDData;

	}

	protected void OnDestroy ()
	{
		GameplayEvents.OnReturnCustomerID -= HandleOnReturnCustomerID;
		GameplayEvents.OnEndEnteringIDData -= HandleOnEndEnteringIDData;
	}

	private void FailCurrentCustomer()
	{
		if (QTEManager.CurrentlyHeldId == CurrentSpawnedNPC.ID)
		{
			return;
		}

		MainGameController.Instance.AddFailToCounter();
		GameplayEvents.NotifyOnIDDataEnterFail(CurrentSpawnedNPC.ID);
		CurrentSpawnedNPC.WalkingController.GoTowardsStartPoint();
		SetTriggerState(false);
	}

	private void HandleOnReturnCustomerID(NPCId customerID)
	{
		if (IsCorrectID(customerID) == false)
		{
			return;
		}

		CurrentSpawnedNPC.WalkingController.GoTowardsStartPoint(DisposeOfCustomer);
	}

	private void HandleOnEndEnteringIDData(NPCId customerID)
	{
		SetTriggerState(IsEnabled);
	}

	private bool IsCorrectID (NPCId customerID)
	{
		return CurrentSpawnedNPC != null && customerID != null && customerID == CurrentSpawnedNPC.ID;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
