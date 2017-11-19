using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject endgameReport;


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
        if (VentolinManager.pumps == VentolinManager.waitedPumps && VentolinManager.waitedPumps > 2)
            _points["5 respirations entre les bouffées"] = true;
        else
            _points["5 respirations entre les bouffées"] = false;
    }

    public void End()
    {
        Destroy(GameObject.Find("IconBar"));
        Destroy(GameObject.Find("Button"));
        Destroy(GameObject.Find("Ventoline"));
        Destroy(GameObject.Find("BreathChamber"));
        Destroy(GameObject.Find("Child"));
        Destroy(GameObject.Find("Shaker"));
        var report = Instantiate(endgameReport);
        report.transform.localScale = new Vector3(1, 1, 1);
        report.transform.SetParent(GameObject.Find("Canvas").transform);
        report.transform.localPosition = new Vector3(0, 0, 0);
        report.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < report.transform.childCount; i++)
        {
            report.transform.GetChild(i).GetComponent<Text>().text = String.Format("{0}: {1}",
                _points.Keys.ElementAt(i),
                (_points.Values.ElementAt(i) == false)? "Raté" :"Réussi !");
        }
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
