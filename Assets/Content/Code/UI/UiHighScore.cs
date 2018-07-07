using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHighScore : UiBaseWindow {

	#region MEMBERS

	[SerializeField]
	private UiScoreEntry entryPrefab;

	[SerializeField]
	private Transform highscoreContent;

	private List<UiScoreEntry> entries = new List<UiScoreEntry>();

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	private void Awake()
	{
		List<ScoreManager.Score> score = ScoreManager.Instance.ScoreList;

		for (int i = 0; i < score.Count; i++)
		{
			UiScoreEntry entry = Instantiate(entryPrefab, highscoreContent) as UiScoreEntry;
			entry.SetScore(i, score[i].Name, score[i].Value);

			entries.Add(entry);
		}
	}

	public void RefreshHighScore()
	{
		List<ScoreManager.Score> score = ScoreManager.Instance.ScoreList;

		for (int i = 0; i < score.Count; i++)
		{
			if(i>= entries.Count)
			{
				entries.Add(Instantiate(entryPrefab, highscoreContent));
			}

			entries[i].SetScore(i, score[i].Name, score[i].Value);
		}
	}

	public override WindowType GetWindowType()
	{
		return WindowType.HIGH_SCORE;
	}

	#endregion

	#region ENUMS

	#endregion

}
