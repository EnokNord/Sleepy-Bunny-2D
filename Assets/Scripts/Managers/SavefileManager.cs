using System.Linq;
using UnityEngine;

public class SavefileManager : MonoBehaviour
{
    SaveData currentSave;

    int previousLevel = 0;

    public SaveData CurrentSave { get { return currentSave; } }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentSave = SaveFileSystem.FindSaveData();
    }
#pragma warning disable
    private void OnLevelWasLoaded(int level)
    {
        if (level > currentSave.LevelProgressed)
        {
            currentSave.LevelProgressed = level;
            SaveFileSystem.Save(currentSave);
        }


        PlayerInputManager[] player = FindObjectsByType<PlayerInputManager>(FindObjectsSortMode.None);
        if (player == null) { return; }
        if (level < currentSave.levelCheckpoints.Length && currentSave.levelCheckpoints[level] != 0) 
        {
            CheckpointManager checkpointManager = FindAnyObjectByType<CheckpointManager>();
            if (checkpointManager != null)
            {
                foreach (Checkpoint checkpoint in checkpointManager.CheckpointList)
                {
                    if (checkpoint.CheckpointID == currentSave.levelCheckpoints[level])
                    {
                        player[0].transform.position = checkpoint.transform.position;
                        //load checkpoint
                        //re collect collectables
                        break;
                    }
                }

            }
            
        }


        previousLevel = level;
    }
    public void SaveGame()
    {
        SaveFileSystem.Save(currentSave);
    }
#pragma warning enable

    private void OnApplicationQuit()
    {
        SaveFileSystem.Save(currentSave);
    }
}
