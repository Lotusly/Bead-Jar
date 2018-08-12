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
	public Text SystemMessage;
	public Image[] Achievements;
	private Coroutine _runningCoroutine;
	
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
			HeadLine.anchoredPosition + SlotInterval * (_slots.Count+1);
		ItemSlot newSlot = newSlotObject.GetComponent<ItemSlot>();
		newSlot.theItem = theItem;
		theItem.theSlot = newSlot;
		newSlot.UpdateData(theItem.GetResourceMul(),theItem.GetContentmentMul());
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

	public void ShowAchievement(int index)
	{
		Achievements[index].enabled = true;
	}

	public void SetTime(float newTime)
	{
		TimeDisplay.text = "Time: "+string.Format("{0:F2}",newTime);
	}

	public void SetResource(float newResource)
	{
		if(newResource<0)ResourceDisplay.text = "Resource: N/A";
		else ResourceDisplay.text = "Resource: "+string.Format("{0:F2}",newResource);
	}

	public void SetScore(float newScore)
	{
		ScoreDisplay.text = "Score: "+string.Format("{0:F2}",newScore);
	}

	public void SetSystemMessage(string message)
	{
		SystemMessage.text = message;
		if(_runningCoroutine!=null) StopCoroutine(_runningCoroutine);
		_runningCoroutine = StartCoroutine(delayClear());

	}

	IEnumerator delayClear()
	{
		float delay = 1;
		float t = 0;
		while (t < delay)
		{
			yield return new WaitForEndOfFrame();
			if (Manager.Instance.CountTime)
			{
				t += Time.deltaTime;
			}
		}
		SystemMessage.text = "";
		_runningCoroutine = null;
	}
}
