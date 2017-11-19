using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BreathControl : MonoBehaviour
{

    public Sprite iconSprite;
    public Vector3 iconSize;
    public Vector3 iconPos;
    public Vector3 iconHBox;

    public Sprite dispSprite;
    public Vector3 dispSize;
    public Vector3 dispPos;
    public Vector3 dispHBox;

    private float _transtime;
    public float transtime;

    public Vector3 fusedTransform;

    private bool _isIcon = true;
    private SpriteRenderer _renderer;
    private bool animating;
    private bool first_trigger = true;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "IconBar")
        {
            if (!animating)
                IconToItem();
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
        }
        else if (other.name == "Ventoline" && transform.childCount == 0)
            FuseWithVentoline(other.gameObject);
    }

    void FuseWithVentoline(GameObject other)
    {
        other.transform.parent = transform;
        other.transform.localPosition = fusedTransform;
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
        _renderer.sprite = iconSprite;
        animating = true;
        _isIcon = true;

    }

    public void IconToItem()
    {
        _renderer.sprite = dispSprite;
        animating = true;
        _isIcon = false;
    }

	// Use this for initialization
	void Start () {
	    _renderer = GetComponent<SpriteRenderer>();
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
        //DevAdjust();
        if (animating && _isIcon)
            GoSmall();
        else if (animating)
            GoBig();
        else if (transform.parent == null)
            TouchMove();
    }
}
