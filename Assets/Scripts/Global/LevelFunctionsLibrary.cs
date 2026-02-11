using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelFunctionsLibrary
{
    public class LevelFunctions : MonoBehaviour
    {
        public static void ResetCurrentLevel()
        {
            Time.timeScale = 1.0f;
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }

        public static void LoadNewLevel(string scene)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(scene);
        }
        public static void LoadNewLevel(int scene)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(scene);
        }
        public static void LoadNextLevel()
        {
            Time.timeScale = 1;
            int index = SceneManager.GetActiveScene().buildIndex + 1;
            if (index > SceneManager.sceneCount - 1) SceneManager.LoadScene(0);
            SceneManager.LoadScene(index);
        }
    }
}