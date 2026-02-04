using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public void UnpauseGame()
    {
       gameObject.SetActive(false);
       Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadStartMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
