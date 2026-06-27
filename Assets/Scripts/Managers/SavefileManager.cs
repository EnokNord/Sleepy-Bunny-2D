using UnityEngine;

public class SavefileManager : MonoBehaviour
{
    public static float LevelCheckPointID;
    int previousLevel = 0;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {
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

}
