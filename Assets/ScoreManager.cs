using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public Dictionary<string, bool> _points = new Dictionary<string, bool>
    {
        {"Enlever le capuchon", false},
        {"Secouer la ventoline", false},
        {"Utiliser la chambre d'inhalation", false},
        {"4 bouffés de ventoline", false},
        {"5 respirations entre les bouffées", false},

    };

    public GameObject cap;
    public Shaking shaker;
    public VentolinManager VentolinManager;
    public GameObject breath;


    void checkCap()
    {
        if (_points["Enlever le capuchon"])
            return;
        if (cap == null)
            _points["Enlever le capuchon"] = true;
    }

    void checkShake()
    {
        if ( _points["Secouer la ventoline"])
            return;
        if (shaker.WasShaked() && !VentolinManager._isIcon)
            _points["Secouer la ventoline"] = true;
    }

    void checkChamber()
    {
        if ( _points["Utiliser la chambre d'inhalation"])
            return;
        if (breath != null && breath.transform.childCount > 0)
            _points["Utiliser la chambre d'inhalation"] = true;
    }

    void checkPumps()
    {
        if (VentolinManager.pumps == 4)
            _points["4 bouffés de ventoline"] = true;
        else
            _points["4 bouffés de ventoline"] = false;

    }

    void checkWaitPumps()
    {
        if (VentolinManager.pumps == VentolinManager.waitedPumps && VentolinManager.waitedPumps != 0)
            _points["5 respirations entre les bouffées"] = true;
        else
            _points["5 respirations entre les bouffées"] = false;
    }

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update ()
	{
	    checkCap();
	    checkShake();
	    checkChamber();
	    checkPumps();
	    checkWaitPumps();

	    foreach (var point in _points)
	    {
	        Debug.Log(String.Format("[{0}, {1}]", point.Key, point.Value));
	    }
	}
}
