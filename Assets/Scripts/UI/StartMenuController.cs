using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartNewGame()
    {
       LevelFunctionsLibrary.LevelFunctions.LoadNewLevel(1);
    }
}
