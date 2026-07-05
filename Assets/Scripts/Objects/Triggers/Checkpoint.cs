using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    public int CheckpointID{ get { return checkpointID; } set { if (checkpointID == 0) checkpointID = value; } }
    [SerializeField] int checkpointID;
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(sprite) sprite.color = Color.clear;
        SavefileManager.LevelCheckPointID = CheckpointID;
    }
}
