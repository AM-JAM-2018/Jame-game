using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class UiBaseWindow : MonoBehaviour {

	#region MEMBERS
	
	private Action onWindowClose = delegate { };

	[SerializeField]
	private GameObject root;

	#endregion

	#region PROPERTIES

	public Action OnWindowClose
	{
		get {
			return onWindowClose;
		}
		set {
			onWindowClose = value;
		}
	} 

	#endregion

	#region METHODS

	
	public virtual void OnBeforeClose()
	{
		onWindowClose();
	}

	public virtual WindowType GetWindowType()
	{
		return WindowType.NOT_SPECIFIED;
	}

	public virtual void Show()
	{
		root.SetActive(true);
	}

	public virtual void Hide()
	{
		root.SetActive(false);
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
