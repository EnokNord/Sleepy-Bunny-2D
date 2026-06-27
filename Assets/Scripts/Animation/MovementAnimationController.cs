using Animation.States;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementAnimationController : MonoBehaviour
{
    [SerializeField]Animator animator;
    Rigidbody2D rb;
    bool alive = true;
    bool landing;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
            if(alive && rb.linearVelocityY != 0)
            {
                
                if (Global.GlobalFunctionsLibrary.IsGrounded(rb))
                {
                    UpdateAnimationState("OnGround", true);
                    UpdateAnimationState("IsFalling", false);
                    UpdateAnimationState("Landing", true);
                landing = true;
            }
                else
                {
                     UpdateAnimationState("OnGround", false);
                     if(rb.linearVelocityY <= 0) UpdateAnimationState("IsFalling", true);
                }

            }
        if (landing)
        {
            AnimatorClipInfo[] animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
            if(animatorinfo[0].clip.name != "landing_right" && animatorinfo[0].clip.name != "fall_right")
            {
                UpdateAnimationState("Landing", false);
                landing = false;
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
    public void PauseAnimations(bool pause)
    {
        if (pause) animator.speed = 0;
        else animator.speed = 1;
    }
    public void UpdateAnimationState(string animBool, bool newValue)
    {
        animator.SetBool(animBool, newValue);
    }
    public void UpdateAnimationState(string animFloat, float newValue)
    {
        animator.SetFloat(animFloat, newValue);
    }
    public void UpdateAnimationState(string trigger)
    {
        animator.SetTrigger(trigger);
    }
    public void TriggerDeathAnimation()
    {
        UpdateAnimationState("Dead", true);
        alive = false;
    }
}
