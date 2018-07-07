using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
	#region MEMBERS

	[SerializeField]
	private List<ScoreData> scoresCollection = new List<ScoreData>();

	#endregion

	#region PROPERTIES

	public List<ScoreData> ScoresCollection {
		get {return scoresCollection;}
		set {scoresCollection = value;}
	}

	#endregion

	#region FUNCTIONS

	public void TryToAddScore (string playerName, int score)
	{
		
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
