using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerTopDownCamera : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private Transform target;
	[SerializeField]
	private PostProcessingBehaviour postProcessing;

	[Header("[ Settings ]")]
	[SerializeField]
	private Vector3 cameraOffset = new Vector3(0, 10, -10);
	[SerializeField]
	private float cameraCatchUpTime = 2;
	[SerializeField]
	private Vector2 followBounds = new Vector2(5, 3);
	[SerializeField]
	private float stopFollowingThreshold = 1;

	// NOT A PROPERTY DUE TO REF
	private Vector3 currentFollowSpeed;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private Transform Target {
		get {return target;}
	}

	// SETTINGS
	private Vector3 CameraOffset {
		get {return cameraOffset;}
	}
	private float CameraCatchUpTime {
		get {return cameraCatchUpTime;}
	}
	private Vector2 FollowBounds {
		get {return followBounds;}
	}
	private float StopFollowingThreshold {
		get {return stopFollowingThreshold;}
	}

	// VARIABLES
	private bool IsFollowingTarget {get; set;}

	#endregion

	#region FUNCTIONS

	public void SetEffectsState (bool state)
	{
		postProcessing.enabled = state;
	}

	protected virtual void Awake ()
	{

	}

	protected virtual void FixedUpdate ()
	{
		UpdateCamera();
	}
	
	protected virtual void OnDestroy ()
	{

	}

	private void UpdateCamera ()
	{
		if (Target == null)
		{
			return;
		}

		// check if the camera is within defined bounds
		// if not, set the flag for the camera to start moving
		IsFollowingTarget |= IsTargetOutOfBounds();

		// if the flag is set, follow the target until it reaches set threashold
		if (IsFollowingTarget == true)
		{
			MoveCameraTowardsTarget();

			// if the camera is centered, set the flag back to false
			IsFollowingTarget = !IsCameraCenteredOnPlayer();
		}
	}

	private void MoveCameraTowardsTarget ()
	{
		// move camera to position of referenced Target transform in time set in CameraCatchUpTime variable
		Vector3 movePosition = Vector3.SmoothDamp(transform.position, Target.position + CameraOffset, ref currentFollowSpeed, CameraCatchUpTime);

		// set new position to our transform
		transform.position = movePosition;
	}

	private bool IsTargetOutOfBounds ()
	{
		// check camera offset relative to Target position
		Vector3 offset = Target.position - (transform.position - CameraOffset);

		offset.y = 0;

		// return boolean result
		return (Mathf.Abs(offset.x) > FollowBounds.x || Mathf.Abs(offset.z) > FollowBounds.y);
	}

	private bool IsCameraCenteredOnPlayer ()
	{
		float distance = Vector3.Distance(Target.position, transform.position - CameraOffset);

		return (distance < StopFollowingThreshold);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
