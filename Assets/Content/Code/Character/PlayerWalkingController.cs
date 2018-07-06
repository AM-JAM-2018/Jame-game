using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingController : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private Rigidbody targetRigidbody;

	[Header("[ Settings ]")]
	[SerializeField]
	private float playerSpeed = 1000;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private Rigidbody TargetRigidbody {
		get {return targetRigidbody;}
	}

	// SETTINGS
	private float PlayerSpeed {
		get {return playerSpeed;}
	}

	// VARIABLES
	private bool CanMove {get; set;}
	private Vector2 CurrentWalkingDirection {get; set;}

	#endregion

	#region FUNCTIONS
	
	public void SetInput(Vector2 direction)
	{
		CurrentWalkingDirection = direction;
	}

	public void SetInputLockState (bool state)
	{
		CanMove = !state;
	}

	protected void Awake ()
	{
		CanMove = true;
	}

	protected void FixedUpdate ()
	{
		if (CanMove == true)
		{
			RotateCharacterToWalkDirection();
			HandleWalking();
		}
	}

	private void RotateCharacterToWalkDirection()
	{
		// do nothing if analog stick is centered
		if (CurrentWalkingDirection == Vector2.zero)
		{
			return;
		}

		// calculate analog's inclination angle
		var angle = Mathf.Atan2(CurrentWalkingDirection.y, -CurrentWalkingDirection.x) * Mathf.Rad2Deg;

		// and set character's rotation to this angle
		TargetRigidbody.rotation = Quaternion.Euler(0, angle, 0);
	}

	private void HandleWalking()
	{
		Vector3 forceDirection = new Vector3(CurrentWalkingDirection.x, 0, CurrentWalkingDirection.y);

		TargetRigidbody.AddForce(forceDirection * PlayerSpeed * Time.deltaTime);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
