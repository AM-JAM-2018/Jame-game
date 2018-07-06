using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerActions
{
	#region MEMBERS

	public static System.Action OnPlayerInteract = delegate{};
	public static System.Action<InputEnums.CodeInputButton> OnPlayerCodeInput = delegate{};

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	public static void NotifyOnPlayerInteract ()
	{
		OnPlayerInteract();
	}

	public static void NotifyOnPlayerCodeInput (InputEnums.CodeInputButton codeInput)
	{
		OnPlayerCodeInput(codeInput);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
