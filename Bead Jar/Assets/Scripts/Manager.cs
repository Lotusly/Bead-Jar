using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

	public static Manager Instance;
	// TMP serialize
	[SerializeField]private float _remainingTime;

	[SerializeField] private Text _timeDisplay;
	public float RemainingTime
	{
		get { return this._remainingTime; }
		set { this._remainingTime = value; }
	}
	
	void Awake()
	{
		if (Instance == null) Instance = this;
	}
	
	// CLEAN use switch and Time.time
	public void UpdateTime(float change)
	{
		_remainingTime += change;
		FlushTime();
	}

	private void FlushTime()
	{
		_timeDisplay.text = string.Format("{0:F1}",_remainingTime);
	}
}
