using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour {

	#region MEMBERS

	[SerializeField]
	private List<UiIdDefinition> idDefinitions;

	public event Action onIdGrab;
	public event Action onIdTaskFinished;

	#endregion

	#region PROPERTIES
	public static QTEManager Instance { get; set; }

	public static NPCs.NPCId CurrentlyHeldId {
		get; set;
	}

	public List<UiIdDefinition> IdDefinitions
	{
		get {
			return idDefinitions;
		}
	}


	#endregion

	#region METHODS

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
	}

	public static void NotifyTaskFinished()
	{

	}

	#endregion

	#region ENUMS

	#endregion
}
