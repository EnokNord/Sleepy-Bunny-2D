using UnityEngine;
using UnityEngine.Events;
using static Global.GlobalVariablesLibrary;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    
    public int CheckpointID{ get { return checkpointID; } set { if (checkpointID == 0) checkpointID = value; } }
    [SerializeField] int checkpointID;
    UnityEvent<int> onCheckpointTriggered;
    public UnityEvent<int> OnCheckpointTriggered {  get { return onCheckpointTriggered; } }
    private void Awake()
    {
        onCheckpointTriggered = new UnityEvent<int>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != PlayerLayer) return;
        onCheckpointTriggered.Invoke(checkpointID);
    }
    
}
