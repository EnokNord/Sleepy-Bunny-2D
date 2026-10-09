using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    [Header("Target (Camera or Player)")]
    public Transform target;  // drag your Camera or Player here in Inspector

    [Header("Parallax Amount")]
    [Range(0f, 1f)]
    public float parallaxSpeed = 0.5f;// 0 = no move, 1 = same as target

    Vector3 startPos;
    float startTargetX;
    float startTargetY;
    

    void Start()
    {
        if (target == null)
        {
            target = Camera.main.transform; // fallback
        }

        startPos = transform.position;
        startTargetX = target.position.x;
        startTargetY = target.position.y;
    }

    void LateUpdate()
    {
        float deltaX = (target.position.x - startTargetX) * parallaxSpeed;
        float deltaY = (target.position.y - startTargetY) * parallaxSpeed;

        transform.position = new Vector3(
            startPos.x + deltaX,
            startPos.y + deltaY,
            startPos.z
        );
    }
}
