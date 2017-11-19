using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Entrainement : MonoBehaviour
    {
        public void BackMenu(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LevelOne()
        {
            SceneManager.LoadScene(2);
        }
    }
}