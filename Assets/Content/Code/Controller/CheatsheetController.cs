using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatsheetController : MonoBehaviour {

	public static CheatsheetController Instance { get; set; }
	public Animator rootAnimator;
	public NPCs.NPC.RaceEnum currentRaceSelected;

	public List<UiCheatsheetRace> cheatsheetRaces;
	public List<KeyCheatsheetDefinition> cheatsheetKeys;

	private const string leftTrigger = "leftTrigger";
	private const string rightTrigger = "rightTrigger";

	private bool IsVisible;

	private bool WasL2Pressed {get; set;}
	private bool WasR2Pressed {get; set;}


	private float timeOnTrigger;

	private void Update()
	{
		if (CheckL2AxisButton() == true)
		{
			TriggerAnimators(leftTrigger);
		}
		else if (CheckR2AxisButton() == true)
		{
			TriggerAnimators(rightTrigger);
		}
	}

	private bool CheckL2AxisButton ()
	{
		if (Input.GetAxisRaw("L2") > 0.5f && WasL2Pressed == false)
		{
			WasL2Pressed = true;
			
			return true;
		}

		if (Input.GetAxisRaw("L2") < 0.5f)
		{
			WasL2Pressed = false;
		}

		return false;
	}
	private bool CheckR2AxisButton ()
	{
		if (Input.GetAxisRaw("R2") > 0.5f && WasR2Pressed == false)
		{
			WasR2Pressed = true;
			
			return true;
		}

		if (Input.GetAxisRaw("R2") < 0.5f)
		{
			WasR2Pressed = false;
		}

		return false;
	}
	
	public void SetSelectedRace(NPCs.NPC.RaceEnum race)
	{
		if(race == currentRaceSelected)
		{
			return;
		}

		currentRaceSelected = race;

		SetKeysToCurrentRace();
	}

	public void Show()
	{
		rootAnimator.SetTrigger("show");
		IsVisible = true;
	}

	public void Hide()
	{
		rootAnimator.SetTrigger("hide");
		IsVisible = false;
	}

	private void SetKeysToCurrentRace()
	{
		foreach (KeyCheatsheetDefinition key in cheatsheetKeys)
		{
			key.inputText.text = AlienLanguageManager.Instance.GetValue(currentRaceSelected, key.inputButton);
		}
	}

	private void TriggerAnimators(string trigger)
	{
		if(IsVisible == false)
		{
			return;
		}

		for (int i = 0; i < cheatsheetRaces.Count; i++)
		{
			cheatsheetRaces[i].raceAnimator.SetTrigger(trigger);
		}
	}

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	private void Start()
	{
		SetKeysToCurrentRace();
	}

	[System.Serializable]
	public class KeyCheatsheetDefinition
	{
		public InputEnums.CodeInputButton inputButton;
		public Text inputText;
	}

}
