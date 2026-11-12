using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> CheckpointList;
    SavefileManager savefileManager;

    private void Start()
    {
        foreach (Checkpoint checkpoint in CheckpointList)
        {
            if(checkpoint != null) checkpoint.OnCheckpointTriggered.AddListener(CheckPointTriggered);
        }
        savefileManager = FindAnyObjectByType<SavefileManager>();
        if (!savefileManager)
        {
            Debug.LogError("No savefilemanager found, checkpoints will not be saved!");
        }
    }

    private void CheckPointTriggered(int checkpointID)
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if (savefileManager.CurrentSave.levelCheckpoints.Length > sceneID)
        {
            savefileManager.CurrentSave.levelCheckpoints[sceneID] = checkpointID;
        }
        
        savefileManager.SaveGame();
    }

}
