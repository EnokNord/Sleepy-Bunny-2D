using UnityEngine;
[RequireComponent(typeof(MovementController))]
public class ObjectMovingComponent : MonoBehaviour
{
    // Todo: Rework and create interact class
    // Todo: clean up the code, there are multiple things that need to be optimized
    Rigidbody2D interactableRB;
    Rigidbody2D moveCompRB;
    MovementController movementController;
    bool pulling;
    float grabDirection;
    float massDiffMod;
    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        moveCompRB = movementController.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(pulling && interactableRB)
        {
            interactableRB.linearVelocityX = moveCompRB.linearVelocityX * massDiffMod;
            if(movementController.CurrentState != MovementState.Walk && movementController.CurrentState != MovementState.Idle)
            {
                ReleaseObject();
            }
            if(movementController.MoveDirection == grabDirection)
            {
                movementController.AnimationController.UpdateAnimationState("IsPushing", true);
                movementController.AnimationController.UpdateAnimationState("IsPulling", false);
            }
            else
            {
                movementController.AnimationController.UpdateAnimationState("IsPushing", false);
                movementController.AnimationController.UpdateAnimationState("IsPulling", true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
       
       if(collision.gameObject.layer == 6 && collision.attachedRigidbody)
       {
            interactableRB = collision.attachedRigidbody;
            massDiffMod = (moveCompRB.mass / interactableRB.mass);
            movementController.CurrentWalkSpeed = movementController.WalkSpeed * Mathf.Clamp(1 - (0.2f * Mathf.RoundToInt(interactableRB.mass * 0.01f)), 0, 1);
            movementController.AnimationController.UpdateAnimationState("IsPushing", true);
            grabDirection = movementController.MoveDirection;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        movementController.AnimationController.UpdateAnimationState("IsPushing", false);
        movementController.AnimationController.UpdateAnimationState("IsPulling", false);
        movementController.CurrentWalkSpeed = movementController.WalkSpeed;
        interactableRB = null;
        movementController.LockDirection = false;
        pulling = false;
    }
    public void GrabObject()
    {
        if (interactableRB == null) return;
        movementController.LockDirection = true;
        pulling = true;
    }
    public void ReleaseObject()
    {
        if (interactableRB == null) return;
        movementController.AnimationController.UpdateAnimationState("IsPulling", false);
        movementController.LockDirection = false;
        movementController.SetWalkDirection(movementController.MoveDirection);
        pulling = false;

    }
}
