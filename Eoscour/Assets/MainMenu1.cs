using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    void Entrainement(string entrainement)
    {
        SceneManager.LoadScene(entrainement);        
    }

    void Quitter(int layer)
    {
        /*#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else*/
            Application.Quit();
        //#endif
    }
}
