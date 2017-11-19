using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class ChildManager : MonoBehaviour
{


    public float transtime;
    private float _transtime;

    private bool animating = false;
    private bool inCorner = false;

    public Vector3 ventolineAttach;
    public Vector3 BreathChamberAttach;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ventoline" && transform.childCount == 0)
            attachLoneVentoline(other.gameObject);
        else if (other.name == "BreathChamber" && transform.childCount == 0)
            attachBreathChamber(other.gameObject);
    }

    void attachLoneVentoline(GameObject obj)
    {
        Debug.Log("Fusing with Ventoline");
        obj.transform.parent = transform;
        obj.transform.localPosition = ventolineAttach;
        Destroy(GameObject.Find("BreathChamber"));
    }

    void attachBreathChamber(GameObject obj)
    {
        Debug.Log("Fusing with Chamber");
        obj.transform.parent = transform;
        obj.transform.localPosition = BreathChamberAttach;
    }

	// Use this for initialization
	void Start ()
	{
	    Screen.orientation = ScreenOrientation.Landscape;
	    _transtime = transtime;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
