using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentolinManager : MonoBehaviour
{

    public Sprite ventolinIcon;
    public Vector3 iconSize;
    public Vector3 iconPos;
    public Vector3 iconHBox;

    public Sprite ventolinDisplay;
    public Vector3 dispSize;
    public Vector3 dispPos;
    public Vector3 dispHBox;

    public float transtime;
    private float _transtime;

    public int pumps = 0;
    public int waitedPumps = 0;

    public bool _isIcon = true;
    private bool animating = false;
    private SpriteRenderer _renderer;
    private bool first_trigger = true;


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "IconBar")
        {
            if (!animating && transform.parent == null)
                IconToItem();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "IconBar")
        {
            if (!first_trigger && !animating && transform.parent == null)
                ItemToIcon();
            else
                first_trigger = !first_trigger;
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
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().size = iconHBox;
    }

    public void IconToItem()
    {
        _renderer.sprite = ventolinDisplay;
        _isIcon = false;
        animating = true;
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().size = dispHBox;
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

    void TouchDrag ()
    {
        Debug.Log("LOLMDR");
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            Vector3 finger = Input.GetTouch(0).position;
            Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(finger);
            Vector2 touchPosWorld2D = new Vector2(touchDeltaPosition.x , touchDeltaPosition.y);
            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                touchDeltaPosition.z = transform.position.z;
                transform.position = touchDeltaPosition;
            }
        }
    }

	// Update is called once per frame
	void Update ()
	{
	    if (animating && _isIcon)
	    {
	        Debug.Log("Small");
	        GoSmall();
	    }
	    else if (animating)
	    {
	        Debug.Log("Big");
	        GoBig();
	    }
	    else if (transform.parent == null)
	    {
	        Debug.Log("Drag");
	        TouchDrag();
	    }

	}
}
