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

    public Sprite YesSprite;
    public Sprite NoSprite;


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
        report.transform.GetChild(report.transform.childCount - 1).localScale = new Vector2(2.44f, 2.44f);
        report.transform.GetChild(report.transform.childCount - 1).localPosition = new Vector2(61, -494);
        for (int i = 2; i < report.transform.childCount; i++)
        {
            report.transform.GetChild(i).transform.position = new Vector3(report.transform.GetChild(i).transform.position.x,
                report.transform.GetChild(i).transform.position.y, -7);
            var blop = report.transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (blop != null)
                blop.sprite = (_points.Values.ElementAt(i - 2) == false)? NoSprite :YesSprite;
            Debug.Log("I IS : " + i);
        }

    }

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
    void Update()
    {
        checkCap();
        checkShake();
        checkChamber();
        checkPumps();
        checkWaitPumps();

    }
}
