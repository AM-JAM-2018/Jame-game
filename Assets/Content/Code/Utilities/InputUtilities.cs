using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputUtilities {

	public static InputEnums.CodeInputButton RandomizeInputButton()
	{
		return (InputEnums.CodeInputButton)Random.Range((int)InputEnums.CodeInputButton.RIGHT_SIDE_UP, (int)InputEnums.CodeInputButton.COUNT);
	}

}
