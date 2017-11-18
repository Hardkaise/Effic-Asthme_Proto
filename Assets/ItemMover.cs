using System;
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
            Debug.Log("ANDROID TOUCH DETECTED");
            Vector3 finger = Input.GetTouch(0).position;
            Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(finger);

            Vector2 touchPosWorld2D = new Vector2(touchDeltaPosition.x , touchDeltaPosition.y);

            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hit.collider != null)
            {
                Debug.Log(String.Format("Object is in {0}\nFinger touch in {1}\nDeltaTouched in {2}",
                    transform.position, finger, touchDeltaPosition));
                if (hit.collider.gameObject == this.gameObject)
                {
                    touchDeltaPosition.z = transform.position.z;
                    Debug.Log(String.Format("Raycast from {0} to {1}", transform.position, touchDeltaPosition.normalized));
                    //transform.position = Vector3.Lerp(transform.position, touchDeltaPosition, Time.deltaTime);
                    transform.position = touchDeltaPosition;
                }
            }

        }
    }


	// Update is called once per frame
	void Update () {
	    TouchDrag();
	}
}
