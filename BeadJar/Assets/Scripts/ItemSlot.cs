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

	public void UpdateData(float resourceMul, float contentmentMul)
	{
		_text.text = theItem.name + "\t\t\t" + string.Format("{0:F2}",theItem.Weight)+
		             "\n"+string.Format("{0:F2}",resourceMul)+
		             "\t\t\t\t"+string.Format("{0:F2}",contentmentMul);
	}

	public void OnPointerEnter()
	{
		_text.color = Color.black;
		_text.fontStyle = FontStyle.Bold;
		Backpack.Instance.AddHighlightedItem(theItem);
	}

	public void OnPointerExit()
	{
		_text.color = Color.white;
		_text.fontStyle = FontStyle.Normal;
		Backpack.Instance.RemoveHighlightedItem(theItem);
	}

	public void OnPointerDown()
	{
		Backpack.Instance.DropItem(theItem);
		CanvasManager.Instance.RemoveSlot(this);
		Destroy(gameObject);
	}


}
