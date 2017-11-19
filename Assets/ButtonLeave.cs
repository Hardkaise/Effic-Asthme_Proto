using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLeave : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void leave()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(1);
    }
}
