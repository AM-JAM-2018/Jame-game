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
	[SerializeField]
	private Animator targetAnimator;

	[Header("[ Animation ]")]
	[SerializeField]
	private float idleAnimationTriggerTimeOut = 5;

	[Space(5)]
	[SerializeField]
	private string horizontalAxisAnimatorParam = "HorizontalAxis";
	[SerializeField]
	private string verticalAxisAnimatorParam = "VerticalAxis";
	[SerializeField]
	private string isWalkingAnimatorParam = "IsWalking";
	[SerializeField]
	private string isIdleTriggerAnimatorParam = "IsIdleTrigger";
	
	[Header("[ Settings ]")]
	[SerializeField]
	private float playerSpeed = 1000;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private Rigidbody TargetRigidbody {
		get {return targetRigidbody;}
	}
	private Animator TargetAnimator {
		get {return targetAnimator;}
	}	

	// ANIMATION
	private float IdleAnimationTriggerTimeOut {
		get {return idleAnimationTriggerTimeOut;}
	}
	
	private string HorizontalAxisAnimatorParam {
		get {return horizontalAxisAnimatorParam;}
	}
	private string VerticalAxisAnimatorParam {
		get {return verticalAxisAnimatorParam;}
	}
	private string IsWalkingAnimatorParam {
		get {return isWalkingAnimatorParam;}
	}
	private string IsIdleTriggerAnimatorParam {
		get {return isIdleTriggerAnimatorParam;}
	}

	// SETTINGS
	private float PlayerSpeed {
		get {return playerSpeed;}
	}

	// VARIABLES
	private bool CanMove {get; set;}
	private Vector2 CurrentWalkingDirection {get; set;}
	private float NextIdleAnimationTriggerTime {get; set;}

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

	protected void Update ()
	{
		HandleAnimation();
	}

	protected void FixedUpdate ()
	{
		if (CanMove == true)
		{
			// RotateCharacterToWalkDirection();
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

	private void HandleAnimation()
	{
		bool isWalking = (CurrentWalkingDirection != Vector2.zero) && (CanMove == true);
		Vector2 walkDirection = CurrentWalkingDirection;

		// override for side walk to prevent side animation while walking forward
		if (Mathf.Round(walkDirection.y) != 0)
		{
			walkDirection.x = 0;
		}

		// handle idle to walking animation switch
		TargetAnimator.SetBool(IsWalkingAnimatorParam, isWalking);

		// handle directional walking animations
		TargetAnimator.SetFloat(HorizontalAxisAnimatorParam, Mathf.Round(walkDirection.x), 0, 100);
		TargetAnimator.SetFloat(VerticalAxisAnimatorParam, Mathf.Round(walkDirection.y), 0, 100);

		// handle idle animation trigger
		if (isWalking == true)
		{
			TargetAnimator.ResetTrigger(IsIdleTriggerAnimatorParam);
			NextIdleAnimationTriggerTime = Time.time + IdleAnimationTriggerTimeOut;
		}
		else if (Time.time > NextIdleAnimationTriggerTime)
		{
			NextIdleAnimationTriggerTime = Time.time + IdleAnimationTriggerTimeOut * 2;
			TargetAnimator.SetTrigger(IsIdleTriggerAnimatorParam);
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
