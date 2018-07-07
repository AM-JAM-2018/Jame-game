using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScoreEntry : MonoBehaviour {


	#region MEMBERS

	[SerializeField]
	private Text entryIndexText;

	[SerializeField]
	private Text nameText;

	[SerializeField]
	private Text scoreText;


	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	public void SetScore(int index, string playerName, int score)
	{
		entryIndexText.text = string.Format("{0}.", index.ToString());
		nameText.text = playerName;
		scoreText.text = string.Format("{0} pts.", score.ToString());
	}

	#endregion

	#region ENUMS

	#endregion

}
