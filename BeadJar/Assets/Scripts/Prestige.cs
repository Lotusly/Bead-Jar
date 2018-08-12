﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prestige : Item {

	public void BeGrabbed()
	{
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		carried = true;
		Manager.Instance.SetFixResource(true);
	}

	public void BeDropped()
	{
		transform.position = CharacterGroup.Instance.transform.position;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
		carried = false;
		Manager.Instance.SetFixResource(false);
	}
}
