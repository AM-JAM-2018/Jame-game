using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private CollisionEventsRouter rootCollisionsRouter;

	#endregion

	#region PROPERTIES

	// REFERENCES
	private CollisionEventsRouter RootCollisionsRouter {
		get {return rootCollisionsRouter;}
	}

	private IInteractable LastDetectedIInteractable {get; set;}

	#endregion

	#region FUNCTIONS

	public void SetInteractionInput(bool interaction)
	{
		if (interaction == false || LastDetectedIInteractable == null)
		{
			return;
		}

		LastDetectedIInteractable.EnableInteraction();
	}

	public void SetCodeInputValue(CodeInputButtonSetup codeInput)
	{
		if (codeInput == null || LastDetectedIInteractable == null)
		{
			return;
		}

		PlayerActions.NotifyOnPlayerCodeInput(codeInput.ButtonInput);
	}

	protected void Awake ()
	{
		RootCollisionsRouter.OnTriggerEnterEvent += HandleOnTriggerEnterEvent;
		RootCollisionsRouter.OnTriggerExitEvent += HandleOnTriggerExitEvent;
	}

	protected void OnDestroy ()
	{
		RootCollisionsRouter.OnTriggerEnterEvent -= HandleOnTriggerEnterEvent;
		RootCollisionsRouter.OnTriggerExitEvent -= HandleOnTriggerExitEvent;
	}

	private void HandleOnTriggerEnterEvent (Collider collider)
	{
		IInteractable interactable = collider.gameObject.GetComponentInParent<IInteractable>();
		
		if (interactable == null)
		{
			return;
		}

		LastDetectedIInteractable = interactable;
	}

	private void HandleOnTriggerExitEvent (Collider collider)
	{
		IInteractable interactable = collider.gameObject.GetComponentInParent<IInteractable>();
		
		if (interactable == null)
		{
			return;
		}

		LastDetectedIInteractable = null;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
