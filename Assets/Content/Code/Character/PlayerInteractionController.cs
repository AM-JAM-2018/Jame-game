using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	public void SetInteractionInput(bool interaction)
	{
		if (interaction == false)
		{
			return;
		}
	}

	public void SetCodeInputValue(CodeInputButtonSetup codeInput)
	{
		if (codeInput == null)
		{
			return;
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
