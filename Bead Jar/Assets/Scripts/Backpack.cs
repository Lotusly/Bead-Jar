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
		get { return this._volume; }
		set { this._volume = value; }
	}
	// TMP serialize
	[SerializeField] private float _burden = 0;
	[SerializeField]private List<Item> _highlightedItems;
	// TMP serialize
	[SerializeField] private List<Item> _carriedItems;
	
	public float Burden
	{
		get { return this._burden;}
		set { this._burden = value; }
	}

	

	
	void Awake()
	{
		if (Instance == null) Instance = this;
		_highlightedItems = new List<Item>();
		_carriedItems = new List<Item>();
	}

	public void AddHighlightedItem(Item highlighted)
	{
		_highlightedItems.Add(highlighted);
	}
	public void RemoveHighlightedItem(Item highlighted)
	{
		_highlightedItems.Remove(highlighted);
	}

	public void GrabHighlightedItem()
	{
		if (_highlightedItems.Count > 0)
		{
			GrabItem(_highlightedItems[0]);
		}
	}

	private void GrabItem(Item theItem)
	{
		if (_volume > _burden + theItem.Weight)
		{
			_carriedItems.Add(theItem);
			_highlightedItems.Remove(theItem);
			_burden = _burden + theItem.Weight;
			CanvasManager.Instance.AddSlot(theItem);
			theItem.BeGrabbed(); 
		}
		else
		{
			
		}
		
	}

	public void DropItem(Item theItem)
	{
		if (_carriedItems.Contains(theItem))
		{
			_carriedItems.Remove(theItem);
			_burden -= theItem.Weight;
			theItem.BeDropped();
		}
	}

	public float GetTotalResourceMul()
	{
		float result = 0;
		foreach (Item i in _carriedItems)
		{
			result += i.GetResourceMul();
		}
		return result;
	}
	
	public float GetTotalContentmentMul()
	{
		float result = 0;
		foreach (Item i in _carriedItems)
		{
			result += i.GetContentmentMul();
		}
		return result;
	}
}
