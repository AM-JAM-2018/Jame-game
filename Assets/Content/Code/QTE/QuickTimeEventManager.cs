using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeInputButton = InputEnums.CodeInputButton;


public class QuickTimeEventManager : MonoBehaviour {

	public const int QTE_LENGTH = 10;
	public const int BUTTON_COUNT = 4;
	
	#region MEMBERS

	private Queue<CodeInputButton> qteInputs;

	private CodeInputButton currentTargetButton;

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	public void SetQteInputs(Queue<InputEnums.CodeInputButton> qteInputs)
	{
		this.qteInputs = qteInputs;
	}

	private void Update()
	{
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
			currentTargetButton = qteInputs.Dequeue();
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
		Debug.LogError("QTE FINISHED! WOoohooo");
	}

	private void Start()
	{
		RandomizeQte();
	}

	private void PickNewTargetButton()
	{

	}

	private void RandomizeQte()
	{
		qteInputs.Clear();

		for (int i = 0; i < QTE_LENGTH; i++)
		{
			qteInputs.Enqueue(RandomizeInputButton());
		}
	}


	public static InputEnums.CodeInputButton RandomizeInputButton()
	{
		return (InputEnums.CodeInputButton) Random.Range((int)InputEnums.CodeInputButton.RIGHT_SIDE_UP, (int)InputEnums.CodeInputButton.COUNT);
	}

	#endregion

	#region ENUMS

	#endregion

}
