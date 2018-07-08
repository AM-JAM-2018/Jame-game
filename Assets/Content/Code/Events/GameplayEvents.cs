using NPCs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayEvents
{
	#region MEMBERS

	public static Action OnGameStart = delegate{};
	public static Action OnGameOver = delegate{};
	public static Action OnGameFinish = delegate{};

	public static Action<IInteractable> OnPlayerEnterInteractableTrigger = delegate{};
	public static Action<IInteractable> OnPlayerExitInteractableTrigger = delegate{};
	
	public static Action OnLockPlayerInput = delegate{};
	public static Action OnUnlockPlayerInput = delegate{};

	public static Action<NPCs.NPCId> OnTakeCustomerID = delegate{};
	public static Action<NPCs.NPCId> OnReturnCustomerID = delegate{};

	public static Action<NPCs.NPCId> OnStartEnteringIDData = delegate{};
	public static Action<NPCs.NPCId> OnEndEnteringIDData = delegate{};

	public static Action<NPCs.NPCId> OnIDDataEnterFail = delegate{};
	public static Action<NPCs.NPCId> OnCameraCatchFail = delegate{};

    public static Action<int> UpdateTotalScore = delegate {};
    public static Action<int> UpdatePartialScore = delegate {};

    public static Action<GameplayController.GameState> OnFailCallback = delegate {};

    public static Action<string> PlayerNameUpdateCallback = delegate {};


    #endregion

    #region PROPERTIES

    #endregion

    #region FUNCTIONS

    public static void NotifyOnPlayerEnterInteractableTrigger (IInteractable trigger)
	{
		OnPlayerEnterInteractableTrigger(trigger);
	}

	public static void NotifyOnPlayerExitInteractableTrigger (IInteractable trigger)
	{
		OnPlayerExitInteractableTrigger(trigger);
	}

	public static void NotifyOnGameStart ()
	{
		OnGameStart();
	}

	public static void NotifyOnGameOver ()
	{
		OnGameOver();
	}

	public static void NotifyOnGameFinish ()
	{
		OnGameFinish();
	}

	public static void NotifyOnLockPlayerInput ()
	{
		OnLockPlayerInput();
	}

	public static void NotifyOnUnlockPlayerInput ()
	{
		OnUnlockPlayerInput();
	}

	public static void NotifyOnTakeCustomerID (NPCs.NPCId customerID)
	{
		OnTakeCustomerID(customerID);
	}
	
	public static void NotifyOnReturnCustomerID (NPCs.NPCId customerID)
	{
		OnReturnCustomerID(customerID);
	}

	public static void NotifyOnStartEnteringIDData (NPCs.NPCId customerID)
	{
		OnStartEnteringIDData(customerID);
	}
	
	public static void NotifyOnEndEnteringIDData (NPCs.NPCId customerID)
	{
		OnEndEnteringIDData(customerID);
	}

	public static void NotifyOnIDDataEnterFail (NPCs.NPCId customerID)
	{
		OnIDDataEnterFail(customerID);
	}
	
	public static void NotifyOnCameraCatchFail (NPCs.NPCId customerID)
	{
		OnCameraCatchFail(customerID);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
