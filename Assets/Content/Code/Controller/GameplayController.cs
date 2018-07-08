using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayController : MonoBehaviour
{
	#region MEMBERS

	[SerializeField]
	private GameState currentGameState = GameState.MAIN_MENU;

	#endregion

	#region PROPERTIES

	public static GameplayController Instance { get; private set; }
	public GameState CurrentGameState {
		get {return currentGameState;}
		private set {currentGameState = value;}
	}

	#endregion

	#region FUNCTIONS

	public void SetGameState (GameState state)
	{
		CurrentGameState = state;
	}

	public void ResetGame ()
	{
		var resetables = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IResetable>();

		foreach (IResetable resetable in resetables)
		{
			resetable.ResetData();
		}
	}
	

	protected void Awake ()
	{
		Instance = this;
	}

	protected void OnDestroy ()
	{
		
	}

	#endregion

	#region CLASS_ENUMS

	public enum GameState
	{
		NONE,
		MAIN_MENU,
		GAME_START,
		GAME_RUNNING,
		GAME_END
	}

	#endregion
}
