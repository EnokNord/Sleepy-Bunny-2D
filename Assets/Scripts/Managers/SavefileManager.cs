using UnityEngine;

public class SavefileManager : MonoBehaviour
{
    SaveData currentSave;

    public static float LevelCheckPointID;
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
        if (level > currentSave.GetProgression().levelProgressed) 
        { 
            currentSave.GetProgression().levelProgressed = level;
        }


        PlayerInputManager[] player = FindObjectsByType<PlayerInputManager>(FindObjectsSortMode.None);
        if (player == null) { return; }
        Checkpoint[] sceneCheckpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
        if(level == previousLevel && LevelCheckPointID != 0) 
        { 
            foreach(Checkpoint checkpoint in sceneCheckpoints)
            {
                if(checkpoint.CheckpointID == LevelCheckPointID)
                {
                    player[0].transform.position = checkpoint.transform.position;
                    break;
                }
            }
            //load checkpoint
            //re collect collectables
        }
        else
        {
            LevelCheckPointID = 0;
        }


        previousLevel = level;
    }
#pragma warning enable

    private void OnApplicationQuit()
    {
        SaveFileSystem.Save(currentSave);
    }
}
