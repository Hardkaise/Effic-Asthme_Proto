using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentolinManager : MonoBehaviour
{

    public Sprite ventolinIcon;
    public Vector3 iconSize;
    public Vector3 iconPos;

    public Sprite ventolinDisplay;
    public Vector3 dispSize;
    public Vector3 dispPos;

    public float transtime;
    private float _transtime;

    private bool _isIcon = true;
    private bool animating = false;
    private SpriteRenderer _renderer;
    private bool first_trigger = true;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "IconBar")
        {
            if (!animating)
                IconToItem();
            Debug.Log("Exit IconBar");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "IconBar")
        {
            if (!first_trigger && !animating)
                ItemToIcon();
            else
                first_trigger = !first_trigger;
            Debug.Log("Enter IconBar");
        }
    }

    private void GoSmall()
    {
        if (transform.position == iconPos && transform.localScale == iconSize)
        {
            animating = false;
            _transtime = transtime;
            return;
        }
        transform.position = Vector3.Lerp(dispPos, iconPos, _transtime);
        transform.localScale = Vector3.Lerp(dispSize, iconSize, _transtime);
        _transtime += 0.1f * transtime;

    }

    private void GoBig()
    {
        if (transform.position == dispPos && transform.localScale == dispSize)
        {
            animating = false;
            _transtime = transtime;
            return;
        }
        transform.position = Vector3.Lerp(iconPos, dispPos, _transtime);
        transform.localScale = Vector3.Lerp(iconSize, dispSize, _transtime);
        _transtime += 0.1f * transtime;
    }

    public void ItemToIcon()
    {
        _renderer.sprite = ventolinIcon;
        _isIcon = true;
        animating = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void IconToItem()
    {
        _renderer.sprite = ventolinDisplay;
        _isIcon = false;
        animating = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
	    transform.GetChild(0).gameObject.SetActive(false);
	    _renderer.sprite = ventolinIcon;
	    _transtime = transtime;
	}

    void DevAdjust()
    {
        if (_isIcon)
            transform.localScale = iconSize;
        else
            transform.localScale = dispSize;
    }

    void TouchMove()
    {
    }

	// Update is called once per frame
	void Update ()
	{
	    //DevAdjust();
	    if (animating && _isIcon)
	        GoSmall();
	    else if (animating)
	        GoBig();
	    else
	        TouchMove();

	}
}
