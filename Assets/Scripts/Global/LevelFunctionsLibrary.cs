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
            if (SceneUtility.GetBuildIndexByScenePath(scene) < 0)
            {
                Debug.LogError("Tried to load scene by name that doesnt exist!");
                return;
            }
            Time.timeScale = 1;
            SceneManager.LoadScene(scene);
        }
        public static void ToggleGamePause(bool paused)
        {
            if (paused) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
        public static void QuitGame()
        {
            Application.Quit();
        }
        public static void LoadNewLevel(int scene)
        {
            if (SceneManager.GetSceneByBuildIndex(scene) == null)
            {
                Debug.LogError("Tried to load scene by index that doesnt exist!");
                return;
            }
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