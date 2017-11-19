﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}

    void TouchDrag ()
    {
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            Vector3 finger = Input.GetTouch(0).position;
            Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(finger);
            Vector2 touchPosWorld2D = new Vector2(touchDeltaPosition.x , touchDeltaPosition.y);
            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                transform.position = touchDeltaPosition;
        }
    }


	// Update is called once per frame
	void Update () {
	    TouchDrag();
	}
}
