using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

	public static CanvasManager Instance;

	public Text TimeDisplay;
	public Text ResourceDisplay;
	public Text ScoreDisplay;
	
	public Text EndGameDisplay;
	public GameObject SlotPrefab;
	public RectTransform HeadLine;

	public Vector2 SlotInterval;
	
	// TMP serialize
	private List<ItemSlot> _slots;
	
	
	void Awake()
	{
		if (Instance == null) Instance = this;
		_slots = new List<ItemSlot>();
	}

	public void ShowEndGame()
	{
		EndGameDisplay.text = "Game Over!";
	}

	public void AddSlot(Item theItem)
	{
		GameObject newSlotObject = Instantiate(SlotPrefab,transform);
		newSlotObject.GetComponent<RectTransform>().anchoredPosition =
			HeadLine.anchoredPosition + SlotInterval;
		Text newText = newSlotObject.GetComponentInChildren<Text>();
		ItemSlot newSlot = newSlotObject.GetComponent<ItemSlot>();
		newSlot.theItem = theItem;
		newText.text = theItem.name + "\t\t" + theItem.Weight;
		_slots.Add(newSlot);
	}

	public void RemoveSlot(ItemSlot theSlot)
	{
		_slots.Remove(theSlot);
		PlaceSlots();
	}

	private void PlaceSlots()
	{
		for (int i = 0; i < _slots.Count; i++)
		{
			_slots[i].GetComponent<RectTransform>().anchoredPosition =
				HeadLine.anchoredPosition + (i + 1) * SlotInterval;
		}
	}

	public void SetTime(float newTime)
	{
		TimeDisplay.text = string.Format("{0:F1}",newTime);
	}

	public void SetResource(float newResource)
	{
		ResourceDisplay.text = string.Format("{0:F1}",newResource);
	}

	public void SetScore(float newScore)
	{
		ScoreDisplay.text = string.Format("{0:F1}",newScore);
	}
}
