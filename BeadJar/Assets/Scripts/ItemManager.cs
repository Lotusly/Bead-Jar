using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

	[SerializeField] private GameObject[] _itemList;

	public static ItemManager Instance;

	void Awake()
	{
		if (Instance == null) Instance = this;
	}

	public void PlaceItem(int index)
	{
		GameObject newItem = Instantiate(_itemList[index], transform);
		newItem.transform.position = new Vector3(CharacterGroup.Instance.transform.position.x, 0,
			CharacterGroup.Instance.transform.position.z);
	}
}
