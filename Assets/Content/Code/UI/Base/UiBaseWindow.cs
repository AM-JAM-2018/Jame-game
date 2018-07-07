using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class UiBaseWindow : MonoBehaviour {

	#region MEMBERS

	[SerializeField]
	private GameObject rootPanel;

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	public virtual void Hide()
	{
		UiWindowManager.Instance.HideWindow();
	}

	public virtual WindowType GetWindowType()
	{
		return WindowType.NOT_SPECIFIED;
	}

	#endregion

	#region ENUMS
	public enum WindowType
	{
		NOT_SPECIFIED,
		QTE
	}
	#endregion
}
