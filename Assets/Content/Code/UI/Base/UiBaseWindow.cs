﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class UiBaseWindow : MonoBehaviour {

	#region MEMBERS

	[SerializeField]
	private GameObject rootPanel;

	private Action onWindowClose = delegate { };
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

	

	#endregion

	#region ENUMS
	public enum WindowType
	{
		NOT_SPECIFIED,
		QTE
	}
	#endregion
}
