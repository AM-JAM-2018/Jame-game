using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour {

	#region MEMBERS

	[SerializeField]
	private List<UiIdDefinition> idDefinitions;

	#endregion

	#region PROPERTIES
	public static QTEManager Instance { get; set; }

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
	#endregion

	#region ENUMS

	#endregion
}
