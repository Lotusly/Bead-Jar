using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

	public static Manager Instance;
	// TMP serialize
	[SerializeField]private float _remainingTime;

	
	private bool _countTime=false;
	private bool _inGame = true;
	
	// TMP serialize
	[SerializeField] private float _resource=0;

	// TMP serialize
	[SerializeField] private float _score=0;
	
	public float RemainingTime
	{
		get { return this._remainingTime; }
		set { this._remainingTime = value; }
	}
	
	void Awake()
	{
		if (Instance == null) Instance = this;
	}

	void Update()
	{
		if (_countTime)
		{
			_remainingTime -= Time.deltaTime;
			UpdateTime();
			_resource += Backpack.Instance.GetTotalResourceMul() * Time.deltaTime;
			UpdateResource();
			_score += Backpack.Instance.GetTotalContentmentMul() + Time.deltaTime;
			UpdateScore();
			if (_remainingTime <= 0)
			{
				EndGame();
			}
		}
	}
	
	public void SetCountTime(bool newCT)
	{
		_countTime = newCT;
	}

	private void UpdateTime()
	{
		CanvasManager.Instance.SetTime(_remainingTime);
	}

	private void UpdateResource()
	{
		CanvasManager.Instance.SetResource(_resource);
	}
	
	private void UpdateScore()
	{
		CanvasManager.Instance.SetScore(_score);
	}

	public void EndGame()
	{
		CharacterGroup.Instance.SetControllable(false);
		CanvasManager.Instance.ShowEndGame();
	}
}
