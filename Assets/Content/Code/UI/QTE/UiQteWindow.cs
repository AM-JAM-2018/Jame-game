using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiQteWindow : UiBaseWindow {

	#region MEMBERS

	[SerializeField]
	private List<InputSprite> inputSprites;

	[SerializeField]
	private Sprite successSprite;

	[SerializeField]
	private Image targetButtonImage;

	[SerializeField]
	private Text npcFirstnameText;

	[SerializeField]
	private Text npcSurnameText;

	[SerializeField]
	private Image informationIndicator;

	#endregion

	#region PROPERTIES

	public QTEController qteController
	{
		get;
		private set;
	}

	private int FullNameCharactersCount
	{
		get;
		set;
	}

	private int OriginalPeselLength
	{
		get;
		set;
	}

	private List<InputSprite> InputSprites
	{
		get {
			return inputSprites;
		}
	}

	#endregion

	#region METHODS

	public override WindowType GetWindowType()
	{
		return WindowType.QTE;
	}

	public override void OnBeforeClose()
	{
		base.OnBeforeClose();

		OnWindowClose();
		ClearDelegates();
	}

	public void HandleCorrectButtonClick(int inputsLeft)
	{
		int howManyCharacterToWrite = GetHowManyCharactersToWrite(inputsLeft);

		string firstName = string.Empty; 
		string surname = string.Empty;

		for (int i = 0; i < howManyCharacterToWrite; i++)
		{
			if (i < QTEManager.CurrentlyHeldId.Name.Length)
			{
				firstName += QTEManager.CurrentlyHeldId.Name[i];
			}
			else if(i - QTEManager.CurrentlyHeldId.Name.Length < QTEManager.CurrentlyHeldId.Surname.Length)
			{
				surname += QTEManager.CurrentlyHeldId.Surname[i - QTEManager.CurrentlyHeldId.Name.Length];
			}
			else
			{
				break;
			}
		}

		npcFirstnameText.text = string.Format("<b>{0}</b>",firstName);
		npcSurnameText.text = string.Format("<b>{0}</b>", surname);

	}

	private void HandleWrongButtonClick()
	{

	}

	public void HandleFinishedQte()
	{
		targetButtonImage.gameObject.SetActive(false);

		Hide();
	}

	public override void Show()
	{
		base.Show();
		targetButtonImage.gameObject.SetActive(true);

		informationIndicator.gameObject.SetActive(false);
	}

	public void SetNewTargetInput(InputEnums.CodeInputButton targetButtonType)
	{
		InputSprite inputSprite = inputSprites.Find(x => x.ButtonType == targetButtonType);

		if (inputSprite != null)
		{
			this.targetButtonImage.sprite = inputSprite.Sprite;
		}
	}

	public void SetQteController(QTEController qteController)
	{
		this.qteController = qteController;
		FullNameCharactersCount = QTEManager.CurrentlyHeldId.Name.Length + QTEManager.CurrentlyHeldId.Surname.Length;
		OriginalPeselLength = qteController.QteInputsLeft;

		SetQteControllerDelegates();
		ClearText();
	}

	private void SetQteControllerDelegates()
	{
		qteController.OnCorrectButtonPress = HandleCorrectButtonClick;
		qteController.OnNewTargetButtonPicked = SetNewTargetInput;
		qteController.OnQteFinished = HandleFinishedQte;
	}

	private void SetTextFonts()
	{
		npcFirstnameText.font = null;
		npcSurnameText.font = null;
	}

	private int GetHowManyCharactersToWrite(int inputsLeft)
	{
		float percentage = 1.0f - ((float)inputsLeft / OriginalPeselLength);
		return (int)Mathf.Floor(percentage * FullNameCharactersCount);
	}

	private void Clear()
	{
		ClearDelegates();
	}

	private void ClearText()
	{
		npcFirstnameText.text = string.Empty;
		npcSurnameText.text = string.Empty;
		return;
	}

	private void ClearDelegates()
	{
		qteController.OnCorrectButtonPress = null;
		qteController.OnQteFinished-= null;
		qteController.OnNewTargetButtonPicked = null;
		OnWindowClose = null;
	}

	#endregion

	#region ENUMS_CLASS
	[Serializable]
	public class InputSprite
	{
		[SerializeField]
		private Sprite sprite;

		[SerializeField]
		private InputEnums.CodeInputButton buttonType;

		public Sprite Sprite
		{
			get {
				return sprite;
			}
		}

		public InputEnums.CodeInputButton ButtonType
		{
			get {
				return buttonType;
			}
		}

	}

	#endregion

}
