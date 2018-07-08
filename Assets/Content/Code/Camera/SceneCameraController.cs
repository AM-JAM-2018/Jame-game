using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneCameraController : MonoBehaviour
{
	#region MEMBERS

	[SerializeField]
	private PlayerTopDownCamera targetCamera;

	[Space]
	[SerializeField]
	private Transform mainMenuAnchorPoint;
	[SerializeField]
	private Transform gameAnchorPoint;

	[Space]
	[SerializeField]
	private float targetReachThreshold;
	[SerializeField]
	private float cameraLerpSpeed;
	[SerializeField]
	private float cameraRotationLerpSpeed;

	[SerializeField]
	private UnityEvent OnCameraReachMenu;
	[SerializeField]
	private UnityEvent OnCameraReachGame;

	#endregion

	#region PROPERTIES

	private PlayerTopDownCamera TargetCamera {
		get {return targetCamera;}
	}
	private Transform MainMenuAnchorPoint {
		get {return mainMenuAnchorPoint;}
	}
	private Transform GameAnchorPoint {
		get {return gameAnchorPoint;}
	}

	private float TargetReachThreshold {
		get {return targetReachThreshold;}
	}
	private float CameraLerpSpeed {
		get {return cameraLerpSpeed;}
	}
	private float CameraRotationLerpSpeed {
		get {return cameraRotationLerpSpeed;}
	}

	#endregion

	#region FUNCTIONS

	protected void Awake ()
	{
		GameplayEvents.OnGameStart += HandleOnGameStart;
		GameplayEvents.OnGameFinish += HandleOnGameFinish;

		TargetCamera.enabled = false;
	}

	protected void Start ()
	{
		TargetCamera.enabled = false;

		GameplayEvents.NotifyOnLockPlayerInput();
		StartCoroutine(MoveCameraTowardsPoint(MainMenuAnchorPoint, ResetGame));
	}

	protected void OnDestroy ()
	{
		GameplayEvents.OnGameStart -= HandleOnGameStart;
		GameplayEvents.OnGameFinish -= HandleOnGameFinish;
	}

	private void HandleOnGameStart ()
	{
		TargetCamera.SetEffectsState(true);
		StartCoroutine(MoveCameraTowardsPoint(GameAnchorPoint, StartGame));
	}
	
	private void HandleOnGameFinish ()
	{
		TargetCamera.enabled = false;

		GameplayEvents.NotifyOnLockPlayerInput();
		StartCoroutine(MoveCameraTowardsPoint(MainMenuAnchorPoint, ResetGame));
	}

	private IEnumerator MoveCameraTowardsPoint (Transform target, System.Action finishAction)
	{
		float distance = Vector3.Distance(target.position, TargetCamera.transform.position);

		while (distance > TargetReachThreshold)
		{
			Vector3 newPosition = Vector3.MoveTowards(TargetCamera.transform.position, target.position, Time.unscaledDeltaTime * CameraLerpSpeed);
			Vector3 newRotation = new Vector3(
				Mathf.MoveTowardsAngle(TargetCamera.transform.localEulerAngles.x, target.localEulerAngles.x, Time.unscaledDeltaTime * CameraRotationLerpSpeed),
				Mathf.MoveTowardsAngle(TargetCamera.transform.localEulerAngles.y, target.localEulerAngles.y, Time.unscaledDeltaTime * CameraRotationLerpSpeed),
				Mathf.MoveTowardsAngle(TargetCamera.transform.localEulerAngles.z, target.localEulerAngles.z, Time.unscaledDeltaTime * CameraRotationLerpSpeed));

			TargetCamera.transform.position = newPosition;
			TargetCamera.transform.localEulerAngles = newRotation;
			
			distance = Vector3.Distance(target.position, TargetCamera.transform.position);

			yield return null;
		}

		finishAction();
	}

	private void StartGame ()
	{
		TargetCamera.enabled = true;

		GameplayController.Instance.SetGameState(GameplayController.GameState.GAME_RUNNING);
		GameplayEvents.NotifyOnUnlockPlayerInput();
		OnCameraReachGame.Invoke();
	}

	private void ResetGame ()
	{
		TargetCamera.SetEffectsState(false);
		GameplayController.Instance.ResetGame();
		OnCameraReachMenu.Invoke();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
