using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayEvents
{
	#region MEMBERS

	public static Action<IInteractable> OnPlayerEnterInteractableTrigger = delegate{};
	public static Action<IInteractable> OnPlayerExitInteractableTrigger = delegate{};
	
	public static Action OnLockPlayerInput = delegate{};
	public static Action OnUnlockPlayerInput = delegate{};

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

	public static void NotifyOnLockPlayerInput ()
	{
		OnLockPlayerInput();
	}

	public static void NotifyOnUnlockPlayerInput ()
	{
		OnUnlockPlayerInput();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
