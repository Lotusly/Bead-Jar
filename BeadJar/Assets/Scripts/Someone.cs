using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Someone : Item
{
    private float _affection=0;
    private const float WalkPossibility = 0.2f;
    private bool _walking = false;
    private const float Speed = 1.5f;
    private Coroutine _walkingCoroutine;
    private bool _love = false;
    private Material _mat;

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }
    
    void Update () {
        if (carried && Manager.Instance.CountTime)
        {
            _timePoint += Time.deltaTime;
            theSlot.UpdateData(GetResourceMul(),GetContentmentMul());
            if (!_love && _timePoint > 120)
            {
                _love = true;
                name = "Lover";
                Manager.Instance.GetAchievement(2);
            }
        }
        else
        { 
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                     new Vector2(CharacterGroup.Instance.transform.position.x,
                         CharacterGroup.Instance.transform.position.z)) <
                 transform.lossyScale.x / 2 + 1)
            {
                _affection += Time.deltaTime;
            }
            else if(_affection>0)
            {
                _affection -= Time.deltaTime / 2;
            }
            if (!_walking && Random.value < Time.deltaTime * WalkPossibility)
            {
                if(_walkingCoroutine!=null) StopCoroutine(_walkingCoroutine);
                _walkingCoroutine=StartCoroutine(StartWalk());
            }
        }
    }
    
    private void OnMouseDown()
    {
        if(_affection>30) Backpack.Instance.TryGrabItem(this);
        else CanvasManager.Instance.SetSystemMessage("It refuses to go with you.");
    }

    IEnumerator StartWalk()
    {
        _walking = true;
        float timeAmount = (Random.value + 2) / Speed;
        Vector3 step = new Vector3(Random.value,0, Random.value).normalized;
        float t = 0;
        while (t < timeAmount)
        {
            if (Manager.Instance.CountTime)
            {
                transform.position += step * Time.deltaTime;
                t += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
        _walkingCoroutine = null;
        _walking = false;
    }

    private void StopWalk()
    {
        if(_walkingCoroutine!=null) StopCoroutine(_walkingCoroutine);
        _walking = false;
    }
    
    public override void BeGrabbed()
    {
        StopWalk();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        carried = true;
        
        //Destroy(this);
    }
}
