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

	[SerializeField]
	private UiBaseWindow currentOpenWindow;

	#endregion

	#region PROPERTIES

	public UiBaseWindow CurrentOpenWindow
	{
		get {
			return currentOpenWindow;
		}
		private set {
			currentOpenWindow = value;
		}
	}

	public static UiWindowManager Instance;

	#endregion

	#region METHODS

	public UiBaseWindow ShowWindow(WindowType windowToShow)
	{
		HideWindow();
		UiBaseWindow windowToOpen = windows.Find(x => x.GetWindowType() == windowToShow);

		if(windowToOpen != null)
		{
			windowToOpen.Show();
		}

		return windowToOpen;
	}

	public void HideWindow()
	{
		if (CurrentOpenWindow != null)
		{
			CurrentOpenWindow.OnBeforeClose();
			CurrentOpenWindow.Hide();
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
