using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddScoreAction : PlayerAction
{
	[SerializeField]
	private ScoreData.ScoreType scoreType;

	private ScoreData.ScoreType ScoreType {
		get {return scoreType;}
	}
	
	public override void Execute()
	{
		MainGameController.Instance.AddScore(ScoreType);
	}
}
