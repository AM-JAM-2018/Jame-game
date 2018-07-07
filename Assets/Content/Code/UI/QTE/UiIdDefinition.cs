using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class UiIdDefinition {

	#region MEMBERS

	[SerializeField]
	private Font idFont;

	[SerializeField]
	private NPCs.NPC.RaceEnum idRaceType;

	#endregion

	#region PROPERTIES

	public Font IdFont
	{
		get {
			return idFont;
		}
	}

	public NPCs.NPC.RaceEnum IdRaceType
	{
		get {
			return idRaceType;
		}
	}

	#endregion

	#region METHODS

	#endregion

	#region ENUMS


	#endregion


}
