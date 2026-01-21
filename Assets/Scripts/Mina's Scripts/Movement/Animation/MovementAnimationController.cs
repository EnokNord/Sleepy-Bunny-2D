using UnityEngine;
using UnityEngine.EventSystems;

public class MovementAnimationController : MonoBehaviour
{
    MovementState animationState = MovementState.Idle;
    [SerializeField]Animator animator;
    MovementController controller;
    Rigidbody2D rb;
    private void Awake()
    {
        controller = GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
            if(rb.linearVelocityY != 0)
            {
                
                if (Global.GlobalFunctionsLibrary.IsGrounded(rb, 1.5f))
                {
                    UpdateAnimationState("OnGround", true);
                    UpdateAnimationState("IsFalling", false);
                }
                else
                {
                     UpdateAnimationState("OnGround", false);
                     if(rb.linearVelocityY <= 0) UpdateAnimationState("IsFalling", true);
                }

            }
    }
    public void UpdateDirectionalFacing(float moveDirection)
    {
        if (moveDirection > 0)
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }
        if (moveDirection < 0)
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void UpdateAnimationState(string animBool, bool newValue)
    {
        animator.SetBool(animBool, newValue);
    }
    public void UpdateAnimationState(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
