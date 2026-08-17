using UnityEngine;

public class InGameUIFunctions : MonoBehaviour
{
   public void ToggleActive(GameObject objectToToggle)
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
    }
}
