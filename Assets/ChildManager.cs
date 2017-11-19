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
        obj.transform.parent = transform;
        obj.transform.localPosition = ventolineAttach;
        Destroy(GameObject.Find("BreathChamber"));
        obj.transform.localScale = new Vector2(0.5f, 0.5f);
    }

    void attachBreathChamber(GameObject obj)
    {
        obj.transform.parent = transform;
        obj.transform.localPosition = BreathChamberAttach;
        obj.transform.localScale = new Vector2(0.48f, 0.64f);
        GameObject.Find("Ventoline").GetComponent<BoxCollider2D>().size = new Vector2(50, 35);
    }

	// Use this for initialization
	void Start ()
	{
	    _transtime = transtime;
	    Screen.orientation = ScreenOrientation.LandscapeRight;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
