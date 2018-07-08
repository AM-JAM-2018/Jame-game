using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour, IResetable
{
	#region MEMBERS

	[Header("[ Settings ]")]
	[SerializeField]
	private int playerMaxFailCount = 3;
	[SerializeField]
	private ScoreData[] playerScoreValues = new ScoreData[]{
		new ScoreData(ScoreData.ScoreType.CODE_INPUT),
		new ScoreData(ScoreData.ScoreType.CODE_INPUT_FAIL),
		new ScoreData(ScoreData.ScoreType.CODE_INPUT_PERFECT),
		new ScoreData(ScoreData.ScoreType.CUSTOMER_QUIT),
		new ScoreData(ScoreData.ScoreType.WRONG_CUSTOMER),
		new ScoreData(ScoreData.ScoreType.CAMERA_CAUGHT)};
	[SerializeField]
	private GameObject[] playerInteractionComputers;

	#endregion

	#region PROPERTIES

	public static MainGameController Instance {get; private set;}

	// SETTINGS
	private int PlayerMaxFailCount {
		get {return playerMaxFailCount;}
	}
	private ScoreData[] PlayerScoreValues {
		get {return playerScoreValues;}
	}
	private GameObject[] PlayerInteractionComputers {
		get {return playerInteractionComputers;}
	}

	// VARIABLES
	public int CurrentFailCount { get; private set; }
	public int CurrentScore { get; private set; }
	public string PlayerName { get; private set; }

	#endregion

	#region FUNCTIONS

	public void ResetData()
	{
		CurrentScore = 0;
		CurrentFailCount = 0;
	}

	public void SetComputersTriggers (bool state)
	{
		for (int i = 0; i < PlayerInteractionComputers.Length; i++)
		{
			PlayerInteractionComputers[i].SetActive(state);
		}
	}

	public void SaveScore()
	{

	}

	public void SetPlayerName(string name)
	{
		PlayerName = name;
        GameplayEvents.PlayerNameUpdateCallback(name);
    }

    public void AddScore(ScoreData.ScoreType type)
	{
		for (int i = 0; i < PlayerScoreValues.Length; i++)
		{
			if (PlayerScoreValues[i].ScoreValueType == type)
			{
				CurrentScore += PlayerScoreValues[i].ScoreValue;
                GameplayEvents.UpdatePartialScore(PlayerScoreValues[i].ScoreValue);
                GameplayEvents.UpdateTotalScore(CurrentScore);
                break;
			}
		}
	}

	public void AddFailToCounter ()
	{
		CurrentFailCount++;
        var gamestate = GameplayController.GameState.GAME_RUNNING;

        if (CurrentFailCount >= PlayerMaxFailCount)
		{
            gamestate = GameplayController.GameState.GAME_END;
            GameplayController.Instance.SetGameState(gamestate);
        }

        GameplayEvents.OnFailCallback(gamestate);
    }

	protected void Awake ()
	{
		Instance = this;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
