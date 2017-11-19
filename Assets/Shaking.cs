using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using System;

public class Shaking : MonoBehaviour
{
    public int shakingMotions = 1;
    public int shakingThreshold = 3;
    private List<double> lastAccValues = new List<double>();
    private AudioSource sound;
    private float lastbeep;

	void Start ()
	{
	    sound = GetComponent<AudioSource>();
	}

    bool Shaked()
    {
        return lastAccValues.Exists(val => Math.Abs(val) > 2 &&
                                    lastAccValues.Exists(val2 => Math.Abs(val2) > val &&
                                                                 ((int)val ^ (int)val2) > 0));
    }

    public bool WasShaked()
    {
        return Time.time - lastbeep < 1;
    }

    void FixedUpdate () {

        foreach (var inp in Input.accelerationEvents)
            lastAccValues.Add(Math.Round(inp.acceleration.y, 2));
        if (Shaked())
        {
            lastAccValues.Clear();
            //Debug.Log("Shake !");
            shakingMotions += 1;
        }
        if (shakingMotions >= shakingThreshold && Time.time - lastbeep > 3)
        {
            //Debug.Log("DING !");
            lastbeep = Time.time;
            sound.Play(0);
            shakingMotions = 0;
        }
    }

}
