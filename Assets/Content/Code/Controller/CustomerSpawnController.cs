using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnController : MonoBehaviour
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

	protected void Update ()
	{
		HandleSpawning();
	}

	private void HandleSpawning ()
	{
		if (Time.time < NextSpawnTime)
		{
			return;
		}

		NextSpawnTime = Time.time + Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
	}

	private void SetSlotsState (bool state)
	{
		for (int i = 0; i < SpawnSlots.Length; i++)
		{
			SpawnSlots[i].SetActiveState(state);
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
