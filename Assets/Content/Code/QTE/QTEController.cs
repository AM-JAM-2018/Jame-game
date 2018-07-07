using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCs;

using CodeInputButton = InputEnums.CodeInputButton;
using System;

public class QTEController : InteractableObject {

	public const int QTE_LENGTH = 10;

	#region MEMBERS

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

	#endregion

	#region METHODS

	public void SetQteInputs(Queue<CodeInputButton> qteInputs)
	{
		this.qteInputs = qteInputs;
	}

	public override void EnableInteraction()
	{
		if(QTEManager.CurrentlyHeldId == null)
		{
			return;
		}

		UiQteWindow qteWindow = UiWindowManager.Instance.ShowWindow(UiBaseWindow.WindowType.QTE) as UiQteWindow;
		PlayerActions.OnPlayerCodeInput += HandleInput;
		SetNewId(QTEManager.CurrentlyHeldId);

		qteWindow.OnWindowClose = HandleQteInterruption;
		qteWindow.SetQteController(this);

		PickNewTargetButton();

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
	}

	private void HandleWrongButtonPressed()
	{

	}

	private void HandleQteFinished()
	{
		currentTargetButton = CodeInputButton.NONE;
		OnQteFinished();
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
