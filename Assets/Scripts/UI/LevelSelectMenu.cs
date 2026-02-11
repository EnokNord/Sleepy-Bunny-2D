using UnityEngine;
using UnityEngine.SceneManagement;
namespace LevelFunctionsLibrary
{
    public class LevelSelectMenu : MonoBehaviour
    {
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void LoadScene(string scene)
        {
            LevelFunctions.LoadNewLevel(scene);
        }
       
    }

}