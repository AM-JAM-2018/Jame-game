using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private Transform target;

	[Header("[ Settings ]")]
	[SerializeField]
	private Vector3 upAxis = Vector3.up;

	#endregion

	#region PROPERTIES

	// REFERENCES
	protected Transform Target {
		get {return target;}
		set {target = value;}
	}
	
	// SETTINGS
	protected Vector3 UpAxis {
		get {return upAxis;}
	}

	#endregion

	#region FUNCTIONS

	protected void Update ()
	{
		LookAt();
	}

	private void LookAt ()
	{
		Vector3 lookDirection = Target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(lookDirection, UpAxis);

		transform.rotation = lookRotation;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
