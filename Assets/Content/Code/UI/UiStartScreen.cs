using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStartScreen : MonoBehaviour {

	public static string playerName;
	public InputField playerNameInputfield;

	public void StartGame()
	{
		playerName = playerNameInputfield.text;
	}

	public void QuitGame()
	{
		Application.Quit();
	}


}
