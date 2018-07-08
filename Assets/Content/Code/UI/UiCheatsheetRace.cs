using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UiCheatsheetRace : MonoBehaviour
{

	public NPCs.NPC.RaceEnum raceType;
	public Animator raceAnimator;

	public void NotifyThatSelected()
	{
		CheatsheetController.Instance.SetSelectedRace(raceType);
	}

}
