using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TestTextScript : MonoBehaviour {

	public int speed;
	private Vector3 direction = new Vector3(1, 1, 1);
    public int bound_up;
    public int bound_down;
    public int bound_left;
    public int bound_right;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (transform.position.x <= bound_left ||
	        transform.position.x >= bound_right)
	        direction.x *= -1;
	    if (transform.position.y >= bound_up ||
	        transform.position.y <= bound_down)
	        direction.y *= -1;

	    transform.position = new Vector3(transform.position.x + direction.x * speed,
	        transform.position.y + direction.y * speed);
	}
}
