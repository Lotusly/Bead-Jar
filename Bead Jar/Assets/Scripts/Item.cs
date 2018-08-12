using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

	private Material _mat;
	private Color _oriColor;

	[SerializeField] private float _colorMulti;

	
	private const float Value_Resource = 1;
	private const float Value_Contentment = 0;
	private const float Burden_Resource = 1;
	private const float Burden_Contentment = 1;
	private const float Treasure_Resource = 0;
	private const float Treasure_Contentment = 1;
	

	private float _oriTime;



	public float Weight;
	[SerializeField] private float _lifeLength;
	[SerializeField] private AnimationCurve _value;
	[SerializeField] private AnimationCurve _burden;
	[SerializeField] private AnimationCurve _treasure;
	
	private bool carried = false;
	// Use this for initialization
	void Awake ()
	{
		_mat = GetComponent<Renderer>().material;
		_oriColor = _mat.color;
		if (Weight <= 0)
		{
			Debug.LogError("Item.Awake()-"+name+"- Weight = "+Weight.ToString());
			Destroy(gameObject);
		}
		_oriTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// TMP bad UX polish later
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			_mat.EnableKeyword("_EMISSION");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			_mat.DisableKeyword("_EMISSION");
		}
	}

	private void OnMouseEnter()
	{
		_mat.color = _oriColor * _colorMulti;
		Backpack.Instance.AddHighlightedItem(this);
	}

	private void OnMouseExit()
	{
		_mat.color = _oriColor;
		Backpack.Instance.RemoveHighlightedItem(this);
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
		float _time = Time.time - _oriTime;
		float v = _value.Evaluate(_time / _lifeLength);
		float b = _burden.Evaluate(_time / _lifeLength);
		float t = _treasure.Evaluate(_time/ _lifeLength);
		float total = v + b + t;
		if (total <= 0 || v < 0 || b < 0 || t < 0) return 0;
		v = v * Weight / total;
		b = b * Weight / total;
		t = t * Weight / total;
		return v * Value_Resource +
		       b * Burden_Resource +
		       t * Treasure_Resource;
	}

	public float GetContentmentMul()
	{
		float _time = Time.time - _oriTime;
		float v = _value.Evaluate(_time / _lifeLength);
		float b = _burden.Evaluate(_time / _lifeLength);
		float t = _treasure.Evaluate(_time/ _lifeLength);
		float total = v + b + t;
		if (total <= 0 || v < 0 || b < 0 || t < 0) return 0;
		v = v * Weight / total;
		b = b * Weight / total;
		t = t * Weight / total;
		return v * Value_Contentment +
		       b * Burden_Contentment +
		       t * Treasure_Contentment;
	}

}
