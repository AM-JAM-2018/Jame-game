using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalkingController : MonoBehaviour
{
	#region MEMBERS

	[Header("[ Settings ]")]
	[SerializeField]
	private float walkSpeed = 5;
	[SerializeField]
	private float pointReachDistanceThreshold = 0.1f;

	#endregion

	#region PROPERTIES

	// SETTINGS
	private float WalkSpeed {
		get {return walkSpeed;}
	}
	private float PointReachDistanceThreshold {
		get {return pointReachDistanceThreshold;}
	}

	// VARIABLES
	private Transform StartPoint {get; set;}
	private Transform EndPoint {get; set;}

	#endregion

	#region FUNCTIONS

	public void SetStartPoint (Transform point)
	{
		StartPoint = point;	
	}

	public void SetEndPoint (Transform point)
	{
		EndPoint = point;	
	}

	public void GoTowardsStartPoint (System.Action goalReachAction)
	{
		StartCoroutine(WalkTowardsPoint(StartPoint, goalReachAction));
	}

	public void GoTowardsEndPoint(System.Action goalReachAction)
	{
		StartCoroutine(WalkTowardsPoint(EndPoint, goalReachAction));
	}

	private IEnumerator WalkTowardsPoint (Transform target, System.Action walkEndAction)
	{
		float remainingDistance = Vector3.Distance(target.position, transform.position);
		
		while (remainingDistance > PointReachDistanceThreshold)
		{
			Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, WalkSpeed * Time.deltaTime);

			newPosition.y = transform.position.y;

			transform.position = newPosition;

			remainingDistance = Vector3.Distance(target.position, transform.position);

			yield return null;
		}

		walkEndAction();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
