using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventsRouter : MonoBehaviour
{
	#region MEMBERS

	public System.Action<Collider> OnTriggerEnterEvent = delegate{};
	public System.Action<Collider> OnTriggerExitEvent = delegate{};
	public System.Action<Collider> OnTriggerStayEvent = delegate{};

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	protected void OnTriggerEnter (Collider hit)
	{
		OnTriggerEnterEvent(hit);
	}

	protected void OnTriggerExit (Collider hit)
	{
		OnTriggerExitEvent(hit);
	}

	protected void OnTriggerStay (Collider hit)
	{
		OnTriggerStayEvent(hit);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
