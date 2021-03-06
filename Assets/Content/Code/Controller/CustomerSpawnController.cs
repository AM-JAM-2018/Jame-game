﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnController : MonoBehaviour, IResetable
{
    public static CustomerSpawnController Instance { get; private set; }
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
	
	// VARIABLES
	private float NextSpawnTime {get; set;}

    public Vector2 SpawnIntervalRange
    {
        get { return spawnIntervalRange; }
        set { spawnIntervalRange = value; }
    }

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
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

		GameplayEvents.OnTakeCustomerID += HandleOnTakeCustomerID;
		GameplayEvents.OnReturnCustomerID += HandleOnReturnCustomerID;
		GameplayEvents.OnEndEnteringIDData += HandleOnEndEnteringIDData;

		GameplayEvents.OnGameStart += HandleOnGameStart;
	}

	protected void Start ()
	{
		ActivateRandomSlotsByCount(3);
		MainGameController.Instance.SetComputersTriggers(false);
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

		GameplayEvents.OnGameStart -= HandleOnGameStart;
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
		
		if (activeSlots.Length == 0)
		{
			return;
		}

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

	private void HandleOnGameStart ()
	{
		ActivateRandomSlotsByCount(3);
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
