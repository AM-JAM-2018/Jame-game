using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : LookAtTarget
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	protected void Start ()
	{
		Target = Camera.main.transform;
	}
	
	#endregion

	#region CLASS_ENUMS

	#endregion
}
