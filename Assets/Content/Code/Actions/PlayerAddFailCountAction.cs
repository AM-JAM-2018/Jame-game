using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddFailCountAction : PlayerAction
{
	public override void Execute()
	{
		MainGameController.Instance.AddFailToCounter();
	}
}
