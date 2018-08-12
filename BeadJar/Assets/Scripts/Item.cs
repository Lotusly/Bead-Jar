using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

	protected Material _mat;
	protected Color _oriColor;

	[SerializeField] protected float _colorMulti;

	
	protected const float Value_Resource = 1;
	protected const float Value_Contentment = 0;
	protected const float Burden_Resource = 0.1f;
	protected const float Burden_Contentment = -0.3f;
	protected const float Treasure_Resource = 0;
	protected const float Treasure_Contentment = 1;
	

	protected float _timePoint=0;



	public float Weight;
	//[SerializeField] private float _lifeLength;
	[SerializeField] protected AnimationCurve _value;
	[SerializeField] protected AnimationCurve _burden;
	[SerializeField] protected AnimationCurve _treasure;
	
	protected bool carried = false;
	public ItemSlot theSlot;
	// Use this for initialization
	void Awake ()
	{
		_mat = GetComponent<Renderer>().material;
		_oriColor = _mat.color;
		if (Weight <= 0)
		{
			Debug.LogError("Item.Awake()-"+name+"- Weight = "+Weight);
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (carried && Manager.Instance.CountTime)
		{
			_timePoint += Time.deltaTime;
			theSlot.UpdateData(GetResourceMul(),GetContentmentMul());
		}
	}

	

	private void OnMouseEnter()
	{
		_mat.color = _oriColor * _colorMulti;
		//Backpack.Instance.AddHighlightedItem(this);
	}

	private void OnMouseExit()
	{
		_mat.color = _oriColor;
		//Backpack.Instance.RemoveHighlightedItem(this);
	}

	private void OnMouseDown()
	{
		Backpack.Instance.TryGrabItem(this);
	}

	public void BeGrabbed()
	{
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		carried = true;
		//Destroy(this);
	}

	public void BeDropped()
	{
		transform.position = CharacterGroup.Instance.transform.position;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
		carried = false;
	}


	public float GetResourceMul()
	{
		float v = _value.Evaluate(_timePoint);
		float b = _burden.Evaluate(_timePoint);
		float t = _treasure.Evaluate(_timePoint);
		float total = v + b + t;
		if (total <= 0 || v < -0.1f || b < -0.1f || t < -0.1f)
		{
			print(v+"\t"+b+"\t"+t);
			return 0;
		}
		return v * Value_Resource +
		       b * Burden_Resource +
		       t * Treasure_Resource;
	}

	public float GetContentmentMul()
	{
		float v = _value.Evaluate(_timePoint);
		float b = _burden.Evaluate(_timePoint);
		float t = _treasure.Evaluate(_timePoint);
		float total = v + b + t;
		if (total <= 0 || v < -0.1f || b < -0.1f || t < -0.1f)
		{
			print(v + "\t" + b + "\t" + t);
			return 0;
		}
		return v * Value_Contentment +
		       b * Burden_Contentment +
		       t * Treasure_Contentment;
	}

}
