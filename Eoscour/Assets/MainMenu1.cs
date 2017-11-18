using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    public void Entrainement(string entrainement)
    {
        SceneManager.LoadScene(entrainement);        
    }

    public void Quitter(int layer)
    {
            Application.Quit();
    }
}
