using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private PlayerWalkingController walkingController;
	[SerializeField]
	private PlayerInteractionController interactionController;
	
	[Header("[ Settings ]")]
	[SerializeField]
	private string verticalAxis = "Vertical";
	[SerializeField]
	private string horizontalAxis = "Horizontal";
	[SerializeField]
	private string interactionButton = "PlayerInteraction";
	[SerializeField]
	private CodeInputButtonSetup[] codeInputButtons;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private PlayerWalkingController WalkingController {
		get {return walkingController;}
	}
	private PlayerInteractionController InteractionController {
		get {return interactionController;}
	}

	// SETTINGS
	private string VerticalAxis {
		get {return verticalAxis;}
	}
	private string HorizontalAxis {
		get {return horizontalAxis;}
	}
	private string InteractionButton {
		get {return interactionButton;}
	}
	private CodeInputButtonSetup[] CodeInputButtons {
		get {return codeInputButtons;}
	}

	#endregion

	#region FUNCTIONS

	public void Update ()
	{
		HandleWalking();
		HandleInteraction();
	}

	private void HandleWalking ()
	{
		if (WalkingController == null)
		{
			return;
		}

		float horizontal = Input.GetAxisRaw(HorizontalAxis);
		float vertical = Input.GetAxisRaw(VerticalAxis);
		
		Vector2 direction = new Vector2(horizontal, vertical);

		WalkingController.SetInput(direction);
	}

	private void HandleInteraction ()
	{
		if (InteractionController == null)
		{
			return;
		}
		
		bool interaction = Input.GetButtonDown(InteractionButton);
		CodeInputButtonSetup codeInput = GetCurrentCodeInput();

		InteractionController.SetInteractionInput(interaction);
		InteractionController.SetCodeInputValue(codeInput);
	}

	private CodeInputButtonSetup GetCurrentCodeInput ()
	{
		for (int i = 0; i < CodeInputButtons.Length; i++)
		{
			if (CodeInputButtons[i].CheckInput() == true)
			{
				return CodeInputButtons[i];
			}
		}

		return null;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
