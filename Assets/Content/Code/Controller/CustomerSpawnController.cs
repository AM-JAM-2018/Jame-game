using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnController : MonoBehaviour, IResetable
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private CustomerSpawnSlot[] spawnSlots;
	
	[Header("[ Settings ]")]
	[SerializeField]
	private Vector2 spawnIntervalRange = new Vector2(5, 20);

	#endregion

	#region PROPERTIES

	// REFERENCES
	private CustomerSpawnSlot[] SpawnSlots {
		get {return spawnSlots;}
	}
	
	// SETTINGS
	private Vector2 SpawnIntervalRange {
		get {return spawnIntervalRange;}
	}

	// VARIABLES
	private float NextSpawnTime {get; set;}

	#endregion

	#region FUNCTIONS

	public void ResetData ()
	{
		SetSlotsState(false);

		for (int i = 0; i < SpawnSlots.Length; i++)
		{
			SpawnSlots[i].DisposeOfCustomer();
		}
	}

	public void ActivateRandomSlotsByCount (int count)
	{
		SetSlotsState(false);
		
		List<int> activatedIndexes = new List<int>();
		
		count = Mathf.Clamp(count, 0, SpawnSlots.Length);

		while (activatedIndexes.Count < count)
		{
			int randomIndex = Random.Range(0, SpawnSlots.Length);

			if (activatedIndexes.Contains(randomIndex) == false)
			{
				SpawnSlots[randomIndex].SetActiveState(true);
				
				activatedIndexes.Add(randomIndex);
			}
		}
	}

	public void SetInteractionTriggersStates (bool state)
	{
		for (int i = 0; i < SpawnSlots.Length; i++)
		{
			SpawnSlots[i].SetTriggerState(state);
		}
	}

	public void SetSlotsState(bool state)
	{
		for (int i = 0; i < SpawnSlots.Length; i++)
		{
			SpawnSlots[i].SetActiveState(state);
		}
	}

	protected void Awake ()
	{
		GameplayEvents.OnTakeCustomerID += HandleOnTakeCustomerID;
		GameplayEvents.OnReturnCustomerID += HandleOnReturnCustomerID;
		GameplayEvents.OnEndEnteringIDData += HandleOnEndEnteringIDData;
	}

	protected void Start ()
	{
		ActivateRandomSlotsByCount(3);
	}
	
	protected void Update ()
	{
		if (GameplayController.Instance.CurrentGameState != GameplayController.GameState.GAME_RUNNING)
		{
			return;
		}
		
		HandleSpawning();
	}

	protected void OnDestroy ()
	{
		GameplayEvents.OnTakeCustomerID -= HandleOnTakeCustomerID;
		GameplayEvents.OnReturnCustomerID -= HandleOnReturnCustomerID;
		GameplayEvents.OnEndEnteringIDData -= HandleOnEndEnteringIDData;
	}

	private void HandleSpawning ()
	{
		if (Time.time < NextSpawnTime)
		{
			return;
		}

		SpawnNPCAtRandomSlot();

		NextSpawnTime = Time.time + Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
	}

	private void SpawnNPCAtRandomSlot ()
	{
		CustomerSpawnSlot[] activeSlots = GetActiveSpawnSlots();
		int randomSlot = Random.Range(0, activeSlots.Length);

		activeSlots[randomSlot].TryToSpawnCustomer();
	}

	private CustomerSpawnSlot[] GetActiveSpawnSlots ()
	{
		List<CustomerSpawnSlot> output = new List<CustomerSpawnSlot>();

		for (int i = 0; i < SpawnSlots.Length; i++)
		{
			if (SpawnSlots[i].IsEnabled == true)
			{
				output.Add(SpawnSlots[i]);
			}
		}

		return output.ToArray();
	}

	private void HandleOnTakeCustomerID (NPCs.NPCId customerID)
	{
		SetInteractionTriggersStates(false);
	}

	private void HandleOnReturnCustomerID (NPCs.NPCId customerID)
	{
		
	}

	private void HandleOnEndEnteringIDData (NPCs.NPCId customerID)
	{
		SetInteractionTriggersStates(true);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
