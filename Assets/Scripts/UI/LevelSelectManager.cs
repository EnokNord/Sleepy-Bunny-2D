using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] List<GameObject> levelButtons = new List<GameObject>();
    private void OnEnable()
    {
        SavefileManager savefileManager = FindAnyObjectByType<SavefileManager>();
        int levelsProgressed = Mathf.Max(savefileManager.CurrentSave.GetProgression().levelProgressed, 1);
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if(i < levelsProgressed)
            {
                levelButtons[i].SetActive(true);
            }
            else
            {
                levelButtons[i].SetActive(false);
            }
           
        }
    }
}
