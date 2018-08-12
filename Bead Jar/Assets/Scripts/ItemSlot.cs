using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

	public Item theItem;

	private Text _text;

	void Awake()
	{
		_text = GetComponentInChildren<Text>();
	}
	// Use this for initialization
	void Start () {
		
	}

	public void OnPointerEnter()
	{
		_text.color = Color.black;
		_text.fontStyle = FontStyle.Bold;
	}

	public void OnPointerExit()
	{
		_text.color = Color.white;
		_text.fontStyle = FontStyle.Normal;
	}

	public void OnPointerDown()
	{
		Backpack.Instance.DropItem(theItem);
		CanvasManager.Instance.RemoveSlot(this);
		Destroy(gameObject);
	}


}
