using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienLanguageManager : MonoBehaviour {

	#region MEMBERS
	[SerializeField]
	private List<AlienLanguage> languages;
	#endregion

	#region PROPERTIES

	public static AlienLanguageManager Instance { get; set; }

	#endregion

	#region METHODS

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	public string GetValue(NPCs.NPC.RaceEnum race, InputEnums.CodeInputButton input)
	{
		AlienLanguage targetLanguage = languages.Find(x => x.raceType == race);
		return targetLanguage.sentences.Find(x => x.input == input).sentence;
	}

	#endregion

	#region ENUMS_CLASS

	[System.Serializable]
	public class AlienLanguage
	{
		public NPCs.NPC.RaceEnum raceType;
		public List<AlienLanguageSentence> sentences;
	}

	[System.Serializable]
	public class AlienLanguageSentence
	{
		public string sentence;
		public InputEnums.CodeInputButton input;
	}

	#endregion
}
