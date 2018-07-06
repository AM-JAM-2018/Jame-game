using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CodeInputButtonSetup
{
	#region MEMBERS

	[SerializeField]
	private string buttonName;
	[SerializeField]
	private InputEnums.CodeInputButton buttonInput;

	#endregion

	#region PROPERTIES

	public string ButtonName {
		get { return buttonName; }
	}
	public InputEnums.CodeInputButton ButtonInput {
		get { return buttonInput; }
	}

	#endregion

	#region FUNCTIONS

	public bool CheckInput()
	{
		return Input.GetButtonDown(ButtonName);
	}

	#endregion
}

