using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : Item
{

	private bool _achieved;

	void Update () {
		if (carried && Manager.Instance.CountTime)
		{
			_timePoint += Time.deltaTime;
			theSlot.UpdateData(GetResourceMul(),GetContentmentMul());
			if (!_achieved && _timePoint > 500)
			{
				name = "Parent";
				_achieved = true;
				Manager.Instance.GetAchievement(1);
			}
		}
	}
}
