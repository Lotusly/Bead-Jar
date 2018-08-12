using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroup : MonoBehaviour
{
	public static CharacterGroup Instance;
	public float _speed;

	public Vector3 _speedVector;

	private bool _controllable = true;
	// Use this for initialization
	void Awake ()
	{
		if (Instance == null) Instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_speed = calSpeed(Backpack.Instance.Burden / Backpack.Instance.Volume);

		if (_controllable)
		{
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W))
			{
				_speedVector = ((Input.GetKey(KeyCode.D) ? 1 : 0) * Vector3.right +
				                (Input.GetKey(KeyCode.S) ? 1 : 0) * Vector3.back +
				                (Input.GetKey(KeyCode.A) ? 1 : 0) * Vector3.left +
				                (Input.GetKey(KeyCode.W) ? 1 : 0) * Vector3.forward).normalized * _speed;
				transform.position += _speedVector * Time.deltaTime;
				Manager.Instance.SetCountTime(true);
			}
			else
			{
				_speedVector = Vector3.zero;
				Manager.Instance.SetCountTime(false);
			}
			/*if (Input.GetKeyDown(KeyCode.Space))
			{
				Backpack.Instance.GrabHighlightedItem();
			}*/
		}

	}

	// Calculate the speed.
	// TMP mapping
	private float calSpeed(float burdenRatio)
	{
		// The speed when free from burden
		float speedBase = 3;
		if (burdenRatio < 0 || burdenRatio > 1)
		{
			Debug.LogError("Character.calSpeed: burdenRatio out of range = " + burdenRatio.ToString());
		}
		return speedBase *  (1 - burdenRatio);
	}

	public void SetControllable(bool newState)
	{
		_controllable = newState;
	}
	
	
}
