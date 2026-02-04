using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public void UnpauseGame()
    {
       gameObject.SetActive(false);
       Time.timeScale = 1;
    }
}
