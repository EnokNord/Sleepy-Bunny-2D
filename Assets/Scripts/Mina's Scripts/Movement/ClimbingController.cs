using UnityEngine;
using UnityEngine.EventSystems;

public class ClimbingController : MonoBehaviour
{
    [SerializeField] float climbSpeed = 5;

    GameObject climbableObject;
    MovementController movementController;
    Rigidbody2D rb;
    float climbDir = 0;
    float gravity;
    private MovementAnimationController animationController;
    bool climbing;
    const float climbCooldown = .5f;
    float climCooldownTimer = 0;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        animationController = GetComponent<MovementAnimationController>();
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }
    private void Update()
    {
        if(climCooldownTimer > 0) climCooldownTimer -= Time.deltaTime;
        if (climbableObject == null || climbDir == 0) return;
        transform.Translate(Vector3.up * climbDir * climbSpeed * Time.deltaTime);
    }
    public void TryClimb(float _climbDir)
    {
        if (climbableObject == null || climCooldownTimer > 0) return;
        rb.linearVelocityY = 0;
        rb.linearVelocityX = 0;
        rb.gravityScale = 0;
        movementController.Climbing = true;
        climbDir = _climbDir;

        animationController.UpdateAnimationState("IsMovingUpOrDown", climbDir == 0 ? false : true);
        if (!climbing)
        {
            animationController.UpdateAnimationState("StartClimbing");
            animationController.UpdateAnimationState("IsClimbing", true);
            climbing = true;
        }
        switch (climbDir)
        {
            case < 0: animationController.UpdateAnimationState("AnimDir", -1f); break;
            case > 0: animationController.UpdateAnimationState("AnimDir", 1f); break;
            case 0: animationController.UpdateAnimationState("AnimDir", 0f); break;
            default:
                break;
        }

        animationController.UpdateAnimationState("IsFalling", false);

    }
    public void StopClimbing()
    {
        if (!climbing) return;
        rb.gravityScale = gravity;
        climbDir = 0;
        climCooldownTimer = climbCooldown;
        movementController.Climbing = false;
        animationController.PauseAnimations(false);
        animationController.UpdateAnimationState("IsMovingUpOrDown", false);
        animationController.UpdateAnimationState("IsClimbing", false);
        climbing = false;
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
