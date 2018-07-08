using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
	#region MEMBERS

	[SerializeField]
	private ScoreType scoreValueType;
	[SerializeField]
	private int scoreValue;

	#endregion

	#region PROPERTIES

	public ScoreType ScoreValueType {
		get {return scoreValueType;}
	}
	public int ScoreValue {
		get {return scoreValue;}
	}

	#endregion

	#region FUNCTIONS

	public ScoreData (ScoreType type)
	{
		scoreValueType = type;
	}

	#endregion

	#region CLASS_ENUMS
	
	public enum ScoreType
	{
		CODE_INPUT,
		CODE_INPUT_FAIL,
		CODE_INPUT_PERFECT,
		CUSTOMER_QUIT,
		WRONG_CUSTOMER,
		CAMERA_CAUGHT,
		ILLEGAL
	}

	#endregion


}
