using Animation;
using Player.Movement;
using UnityEngine;
using static Global.GlobalEnumLibrary;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementAnimationController))]
public class MovementController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float runSpeed = 15;

    [Header("Crouch")]
    [SerializeField] private float crouchWalkSpeed = 5;
    [SerializeField] private float crouchRunSpeed = 7.5f;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 15;
    [SerializeField] private float runJumpPower = 7.5f;

    Rigidbody2D rigidBody;
    private MovementState moveState = MovementState.Idle;
    private MovementAnimationController animationController;
    private float moveDirection = 0f;
    private bool isCrouching = false;
    private bool isRunning = false;
    public MovementState Movestate { get { return moveState; } }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<MovementAnimationController>();
    }

    private void FixedUpdate()
    {
        //TODO: grab and push reduces speed 20% per 100 mass
        float speed = walkSpeed;
        switch (moveState)
        {
            case MovementState.Idle: rigidBody.linearVelocityX = 0; return;
            case MovementState.Run: speed = runSpeed; break;
            case MovementState.CrouchWalk: speed = crouchWalkSpeed; break;
            case MovementState.CrouchRun: speed = crouchRunSpeed; break;
            default:
                break;
        }
        rigidBody.linearVelocityX = moveDirection * speed * Time.fixedDeltaTime;
    }
    public void Jump()
    {
        if (Global.GlobalFunctionsLibrary.IsGrounded(rigidBody))
        {
            if(isRunning) rigidBody.linearVelocityY = runJumpPower;
            else rigidBody.linearVelocityY = jumpPower;
            animationController.UpdateAnimationState("Jump");
        }
    }
   public void SetWalkDirection(float newMoveDirection)
    {
        moveDirection = newMoveDirection;
        animationController.UpdateDirectionalFacing(moveDirection);
        animationController.UpdateAnimationState("IsWalking", moveDirection == 0 ? false : true);
        UpdateMovementState();
    }
    public void ToggleRunning(bool isNowRunning)
    {  
        //Todo: Undo crouch and start running anim
        isRunning = isNowRunning;
        UpdateMovementState();
    }
    public void ToggleCrouch(bool isNowCrouching)
    {
        // Todo: Toggle model crouch both animation and hitbox 
        isCrouching = isNowCrouching;
        animationController.UpdateAnimationState("IsCrouching", isCrouching);
        UpdateMovementState();
    }
    private void UpdateMovementState()
    {
        // Todo: Make this better
        if (moveState == MovementState.Idle) moveState = MovementState.Walk;
        if (isRunning) moveState = MovementState.Run;
        if (isCrouching) moveState = MovementState.CrouchWalk;
        if (isRunning && isCrouching) moveState = MovementState.CrouchWalk;
        if (moveDirection == 0) moveState = MovementState.Idle;
    }

}
