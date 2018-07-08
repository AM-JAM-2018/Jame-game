using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCs;

using CodeInputButton = InputEnums.CodeInputButton;
using RaceEnum = NPCs.NPC.RaceEnum;

using System;

public class QTEController : InteractableObject {

	public const int QTE_LENGTH = 10;

	#region MEMBERS

	[SerializeField]
	private RaceEnum qteControllerRaceType;
	[SerializeField]
	private bool isIllegalQTEController = false;
	 
	private Queue<CodeInputButton> qteInputs;
	private CodeInputButton currentTargetButton;

	private Action<int> onCorrectButtonPress = delegate { };
	private Action<CodeInputButton> onNewTargetButtonPicked = delegate { };
	private Action onQteFinished = delegate { };

	#endregion

	#region PROPERTIES

	public Action<int> OnCorrectButtonPress
	{
		get {
			return onCorrectButtonPress;
		}
		set {
			onCorrectButtonPress = value;
		}
	}

	public Action OnQteFinished
	{
		get {
			return onQteFinished;
		}
		set {
			onQteFinished = value;
		}
	}

	public Action<CodeInputButton> OnNewTargetButtonPicked
	{
		get {
			return onNewTargetButtonPicked;
		}
		set {
			onNewTargetButtonPicked = value;
		}
	}

	public int QteInputsLeft
	{
		get {
			return qteInputs.Count;
		}
	}

	private bool IsIllegalQTEController {
		get {return isIllegalQTEController;}
		set {isIllegalQTEController = value;}
	}

	private bool IsCurrentlyInQTE {get; set;}
	private int CurrentWrongInputCount {get; set;}

	#endregion

	#region METHODS

	public void SetQteInputs(Queue<CodeInputButton> qteInputs)
	{
		this.qteInputs = qteInputs;
	}

	public override void EnableInteraction()
	{
		if(IsCurrentlyInQTE == true || QTEManager.CurrentlyHeldId == null || (QTEManager.CurrentlyHeldId.Race != qteControllerRaceType && IsIllegalQTEController == false))
		{
			return;
		}

		IsCurrentlyInQTE = true;
		CurrentWrongInputCount = 0;

		UiQteWindow qteWindow = UiWindowManager.Instance.ShowWindow(UiBaseWindow.WindowType.QTE) as UiQteWindow;
		PlayerActions.OnPlayerCodeInput += HandleInput;
		SetNewId(QTEManager.CurrentlyHeldId);
		
		RODOCamera.Instance.CameraActivated.AddListener(HandleCameraActivation);

		qteWindow.OnWindowClose = HandleQteInterruption;
		qteWindow.SetQteController(this);

		PickNewTargetButton();
		GameplayEvents.NotifyOnLockPlayerInput();
		GameplayEvents.NotifyOnStartEnteringIDData(QTEManager.CurrentlyHeldId);
	}

	private void HandleCameraActivation()
	{
		if (IsIllegalQTEController == false)
		{
			return;
		}
		
		MainGameController.Instance.AddFailToCounter();
		MainGameController.Instance.AddScore(ScoreData.ScoreType.CAMERA_CAUGHT);
	}

	public void SetNewId(NPCId npcId)
	{
		SetQteInputs(npcId.NPCPesel);
	}

	private void HandleInput(CodeInputButton buttonPressed)
	{
		if(IsCorrectButtonPressed(buttonPressed, currentTargetButton))
		{
			HandleCorrectButtonPressed();
		}
		else
		{
			HandleWrongButtonPressed();
		}
	}

	private bool IsCorrectButtonPressed(CodeInputButton buttonPressed, CodeInputButton targetButton)
	{
		return buttonPressed.Equals(targetButton);
	}

	private void HandleCorrectButtonPressed()
	{
		OnCorrectButtonPress(qteInputs.Count);

		if (qteInputs.Count > 0)
		{
			PickNewTargetButton();
		}
		else
		{
			HandleQteFinished();
		}

		MainGameController.Instance.AddScore(ScoreData.ScoreType.CODE_INPUT);
	}

	private void HandleWrongButtonPressed()
	{
		CurrentWrongInputCount++;
		
		MainGameController.Instance.AddScore(ScoreData.ScoreType.CODE_INPUT_FAIL);
	}

	private void HandleQteFinished()
	{
		IsCurrentlyInQTE = false;
		
		currentTargetButton = CodeInputButton.NONE;
		OnQteFinished();
		GameplayEvents.NotifyOnUnlockPlayerInput();
		GameplayEvents.NotifyOnEndEnteringIDData(QTEManager.CurrentlyHeldId);
		RODOCamera.Instance.CameraActivated.RemoveListener(HandleCameraActivation);

		if (CurrentWrongInputCount == 0)
		{
			MainGameController.Instance.AddScore(ScoreData.ScoreType.CODE_INPUT_PERFECT);
		}
	}

	private void HandleQteInterruption()
	{
		ClearInputs();
		GameplayEvents.NotifyOnUnlockPlayerInput();
	}

	private void PickNewTargetButton()
	{
		currentTargetButton = qteInputs.Dequeue();

		if(currentTargetButton == CodeInputButton.NONE || currentTargetButton == CodeInputButton.COUNT)
		{
			HandleCorrectButtonPressed();
			return;
		}

		OnNewTargetButtonPicked(currentTargetButton);
	}

	private void ClearInputs()
	{
		qteInputs.Clear();
	}

	#endregion

	#region ENUMS_CLASSES

	#endregion

}
