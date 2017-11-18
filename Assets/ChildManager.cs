using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    public Vector3 normalPos;
    public Vector3 normalSize;
    public Vector3 miniSize;
    public Vector3 miniPos;

    public float transtime;
    private float _transtime;

    private bool animating = false;
    private bool inCorner = false;



    public void GoBig()
    {
        if (transform.position == normalPos)
            return;
        animating = true;
        inCorner = false;
        transform.position = Vector3.Lerp(miniPos, normalPos, _transtime);
        transform.localScale = Vector3.Lerp(miniSize, normalSize, _transtime);
        _transtime += 0.1f * transtime;
        if (transform.position == normalPos && transform.localScale == normalSize)
        {
            animating = false;
            _transtime = transtime;
        }
    }

    public void GoSmall()
    {
        if (transform.position == miniPos)
            return;
        animating = true;
        inCorner = true;
        transform.position = Vector3.Lerp(normalPos, miniPos, _transtime);
        transform.localScale = Vector3.Lerp(normalSize, miniSize, _transtime);
        _transtime += 0.1f * transtime;
        if (transform.position == miniPos && transform.localScale == miniSize)
        {
            animating = false;
            _transtime = transtime;
        }
    }

	// Use this for initialization
	void Start ()
	{
	    transform.localScale = normalSize;
	    transform.position = normalPos;
	    _transtime = transtime;
	}
	
	// Update is called once per frame
	void Update () {
		if (animating && inCorner)
		    GoSmall();
	    else if (animating)
	        GoBig();
	}
}
