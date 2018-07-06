using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private InteractableObject rootInteractableObject;
	
	[Header("[ Events ]")]
	[SerializeField]
	private UnityEvent onTriggerEnterEvent;
	[SerializeField]
	private UnityEvent onTriggerExitEvent;

	#endregion

	#region PROPERTIES

	// REFERENCES
	public InteractableObject RootInteractableObject {
		get {return rootInteractableObject;}
	}

	// EVENTS
	public UnityEvent OnTriggerEnterEvent {
		get {return onTriggerEnterEvent;}
	}
	public UnityEvent OnTriggerExitEvent {
		get {return onTriggerExitEvent;}
	}

	#endregion

	#region FUNCTIONS

	protected void OnTriggerEnter (Collider collider)
	{
		OnTriggerEnterEvent.Invoke();
		GameplayEvents.NotifyOnPlayerEnterInteractableTrigger(RootInteractableObject);
	}

	protected void OnTriggerExit (Collider collider)
	{
		OnTriggerExitEvent.Invoke();
		GameplayEvents.NotifyOnPlayerExitInteractableTrigger(RootInteractableObject);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
