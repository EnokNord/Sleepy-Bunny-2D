using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    [SerializeField] float climbSpeed = 5;

    GameObject climbableObject;
    MovementController movementController;
    Rigidbody2D rb;
    float climbDir = 0;
    float gravity;
    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }
    private void Update()
    {
        if (climbableObject == null || climbDir == 0) return;
        transform.Translate(Vector3.up * climbDir * climbSpeed * Time.deltaTime);
    }
    public void TryClimb(float _climbDir)
    {
        if (climbableObject == null) return;
        rb.linearVelocityY = 0;
        rb.linearVelocityX = 0;
        rb.gravityScale = 0;
        movementController.OnGround = true;
        climbDir = _climbDir;
    }
    public void StopClimbing()
    {
        rb.gravityScale = gravity;
        climbDir = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Climbable") 
        {
            climbableObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Climbable")
        {
            climbableObject = null;
            StopClimbing();
        }
    }
}
