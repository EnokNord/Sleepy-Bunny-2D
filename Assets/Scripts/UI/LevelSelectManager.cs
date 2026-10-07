using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] List<GameObject> levelButtons = new List<GameObject>();
    private void Start()
    {
        SavefileManager savefileManager = FindAnyObjectByType<SavefileManager>();
        int levelsProgressed = Mathf.Max(savefileManager.CurrentSave.GetProgression().levelProgressed, 1);
        for (int i = levelsProgressed; i < levelButtons.Count; i++)
        {
            levelButtons[i].SetActive(false);
        }
    }
}
