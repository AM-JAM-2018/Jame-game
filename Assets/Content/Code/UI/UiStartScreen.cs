using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStartScreen : MonoBehaviour {

	public static string playerName;
	public InputField playerNameInputfield;

	public GameObject leaderboards;
	public GameObject mainMenu;
	public GameObject startGame;

	protected void Awake ()
	{
		GameplayEvents.OnGameOver += HandleOnGameOver;
	}

	protected void OnDestroy ()
	{
		GameplayEvents.OnGameOver -= HandleOnGameOver;
	}

	public void HandleOnGameOver ()
	{
		ShowLeaderboards();
	}

	public void StartGame()
	{
		MainGameController.Instance.SetPlayerName(playerNameInputfield.text);

		GameplayEvents.NotifyOnGameStart();
		gameObject.SetActive(false);
	}

	public void ShowLeaderboards ()
	{
		leaderboards.SetActive(true);
		mainMenu.SetActive(false);
		startGame.SetActive(false);
	}

	public void ShowMainMenu ()
	{
		leaderboards.SetActive(false);
		startGame.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void ShowStartGame ()
	{
		leaderboards.SetActive(false);
		mainMenu.SetActive(false);
		startGame.SetActive(true);
	}

	public void QuitGame()
	{
		Application.Quit();
	}


}
