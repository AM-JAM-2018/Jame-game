using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCs;

using CodeInputButton = InputEnums.CodeInputButton;

public class QTEController : MonoBehaviour, IInteractable{

	public const int QTE_LENGTH = 10;

	#region MEMBERS

	private Queue<CodeInputButton> qteInputs;
	private CodeInputButton currentTargetButton;

	[SerializeField]
	private NPCId npcId;

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	public void SetQteInputs(Queue<CodeInputButton> qteInputs)
	{
		this.qteInputs = qteInputs;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			EnableInteraction();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			HandleInput(CodeInputButton.RIGHT_SIDE_UP);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			HandleInput(CodeInputButton.RIGHT_SIDE_RIGHT);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			HandleInput(CodeInputButton.RIGHT_SIDE_DOWN);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			HandleInput(CodeInputButton.RIGHT_SIDE_LEFT);
		}
	}
	
	public void EnableInteraction()
	{
		UiQteWindow qteWindow = UiWindowManager.Instance.ShowWindow(UiBaseWindow.WindowType.QTE) as UiQteWindow;

		qteWindow.OnWindowClose = HandleQteInterruption;

		SetNewId(npcId);
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
		if(qteInputs.Count > 0)
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
		Debug.LogWarning("Wrooong mudafuckaa");
	}

	private void HandleQteFinished()
	{
		currentTargetButton = CodeInputButton.NONE;
	}

	private void HandleQteInterruption()
	{
		ClearInputs();
	}


	private void PickNewTargetButton()
	{
		currentTargetButton = qteInputs.Dequeue();

		if(currentTargetButton == CodeInputButton.NONE || currentTargetButton == CodeInputButton.COUNT)
		{
			HandleCorrectButtonPressed();
			return;
		}

		Debug.LogFormat("next button is: {0}", currentTargetButton.ToString());
	}

	private void RandomizeQte()
	{
		ClearInputs();
		for (int i = 0; i < QTE_LENGTH; i++)
		{
			qteInputs.Enqueue(RandomizeInputButton());
		}
	}

	private void ClearInputs()
	{
		qteInputs.Clear();
	}

	public static CodeInputButton RandomizeInputButton()
	{
		return (CodeInputButton) UnityEngine.Random.Range((int)CodeInputButton.RIGHT_SIDE_UP, (int)CodeInputButton.COUNT);
	}

	

	#endregion

	#region ENUMS

	#endregion

}
