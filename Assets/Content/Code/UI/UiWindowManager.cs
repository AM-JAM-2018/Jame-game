using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WindowType = UiBaseWindow.WindowType;

public class UiWindowManager : MonoBehaviour {

	#region MEMBERS
	[SerializeField]
	private Transform windowRoot;

	[SerializeField]
	private List<UiBaseWindow> windows;

	#endregion

	#region PROPERTIES

	public UiBaseWindow CurrentOpenWindow
	{
		get; set;
	}

	public static UiWindowManager Instance;

	#endregion

	#region METHODS

	public UiBaseWindow ShowWindow(WindowType windowToShow)
	{
		HideWindow();
		UiBaseWindow windowToSpawn = windows.Find(x => x.GetWindowType() == windowToShow);

		if(windowToSpawn != null)
		{
			CurrentOpenWindow = Instantiate(windowToSpawn, windowRoot);
		}

		return windowToSpawn;
	}

	public void HideWindow()
	{
		if (CurrentOpenWindow != null)
		{
			Destroy(CurrentOpenWindow.gameObject);
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
	#endregion

	#region ENUMS_CLASSES

	#endregion




}
