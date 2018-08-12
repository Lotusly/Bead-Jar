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
	//[SerializeField]private List<Item> _highlightedItems;
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
		//_highlightedItems = new List<Item>();
		_carriedItems = new List<Item>();
	}

	/*public void AddHighlightedItem(Item highlighted)
	{
		_highlightedItems.Add(highlighted);
	}
	public void RemoveHighlightedItem(Item highlighted)
	{
		_highlightedItems.Remove(highlighted);
	}*/

	/*public void GrabHighlightedItem()
	{
		if (_highlightedItems.Count > 0)
		{
			GrabItem(_highlightedItems[0]);
		}
	}*/

	public void TryGrabItem(Item theItem)
	{
		if (Vector2.Distance(new Vector2(theItem.transform.position.x, theItem.transform.position.z),
			    new Vector2(CharacterGroup.Instance.transform.position.x, CharacterGroup.Instance.transform.position.z)) >
		    theItem.transform.lossyScale.x + 0.5)
		{
			CanvasManager.Instance.SetSystemMessage("Too far away to grab.");
		}
		if (_volume > _burden + theItem.Weight)
		{
			_carriedItems.Add(theItem);
			//_highlightedItems.Remove(theItem);
			_burden = _burden + theItem.Weight;
			CanvasManager.Instance.AddSlot(theItem);
			theItem.BeGrabbed(); 
		}
		else
		{
			CanvasManager.Instance.SetSystemMessage("No enough space in the backpack.");
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
