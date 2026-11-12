using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PickupsManager : MonoBehaviour
{
  //  [SerializeField] List<CollectibleController> dustbunnies = new List<CollectibleController>();
   // [SerializeField] List<CollectibleController> carrots = new List<CollectibleController>();
    CheckpointManager checkpointManager;

    private void Start()
    {
        checkpointManager = FindAnyObjectByType<CheckpointManager>();
        if (checkpointManager)
        {
            foreach(Checkpoint checkpoint in checkpointManager.CheckpointList)
            {
                checkpoint.OnCheckpointTriggered.AddListener(SaveCollectables);
            }
        }
    }
    private void SaveCollectables(int checkpoint)
    {
        //TODO: Save collectables to file
    }
}
