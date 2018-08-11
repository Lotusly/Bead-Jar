using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Backpack : MonoBehaviour
{

	public static Backpack Instance;
	[SerializeField] private float _volume;
	public float Volume
	{
		get { return this._volume;}
		set { this._volume = value; }
	}
	/*// TMP serialize
	[SerializeField] private float _space;
	public float Space
	{
		get { return this._space; }
		set { this._space = value; }
	}*/
	// TMP serialize
	[SerializeField] private float _burden=0;
	public float Burden
	{
		get { return this._burden;}
		set { this._burden = value; }
	}

	

	
	void Awake()
	{
		if (Instance == null) Instance = this;
	}
	
	
}
