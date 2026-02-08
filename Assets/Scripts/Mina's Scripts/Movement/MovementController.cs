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

    [Header("Jump")]
    [SerializeField] private float jumpPower = 15;
    [SerializeField] private float runJumpPower = 7.5f;

    public float WalkSpeed { get { return walkSpeed; } }
    public bool LockDirection { get; set; }
    public MovementState CurrentState { get { return moveState; } }
    public float CurrentWalkSpeed { get { return currentWalkSpeed; } set { if(value > 0) currentWalkSpeed = value; } }
    public MovementAnimationController AnimationController { get { return animationController; } }
    public float MoveDirection {  get { return moveDirection; } }
   
    
    Rigidbody2D rigidBody;
    private float currentWalkSpeed;
    private MovementState moveState = MovementState.Idle;
    private MovementAnimationController animationController;
    private float moveDirection = 0f;
    private bool isCrouching = false;
    private bool isRunning = false;
    private Vector2 originalHitboxSize;
    private Vector2 originalHitboxOffset;

    CapsuleCollider2D collider;
    private void Awake()
    {
        CurrentWalkSpeed = WalkSpeed;
        rigidBody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<MovementAnimationController>();
        collider = GetComponent<CapsuleCollider2D>();
        originalHitboxSize = collider.size;
        originalHitboxOffset = collider.offset;
    }

    private void FixedUpdate()
    {
        //TODO: grab and push reduces speed 20% per 100 mass
        float speed = currentWalkSpeed;
        switch (moveState)
        {
            case MovementState.Idle: rigidBody.linearVelocityX = 0; return;
            case MovementState.Run: speed = runSpeed; break;
            case MovementState.CrouchWalk: speed = crouchWalkSpeed; break;
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
        if (!LockDirection) animationController.UpdateDirectionalFacing(moveDirection);
        animationController.UpdateAnimationState("IsWalking", moveDirection == 0 ? false : true);
        UpdateMovementState();
    }
    public void ToggleRunning(bool isNowRunning)
    {  
        isRunning = isNowRunning;
        if (isRunning && isCrouching)
        {
            ToggleCrouch(false);
            animationController.UpdateAnimationState("IsCrouching", isCrouching);
        }
        UpdateMovementState();
    }
    public void ToggleCrouch(bool isNowCrouching)
    {
        isCrouching = isNowCrouching;
        if (isCrouching)
        {
            ToggleRunning(false);
                collider.size = (originalHitboxSize * 0.75f);
                collider.offset = (originalHitboxOffset + Vector2.down * 0.5f);
        }
        else
        {
                collider.size =  originalHitboxSize;
                collider.offset = originalHitboxOffset;
        }
        animationController.UpdateAnimationState("IsCrouching", isCrouching);
        UpdateMovementState();
    }
    public void UpdateMovementState()
    {
        //if (LockState) return;
        moveState = MovementState.Walk;
        if (isRunning) moveState = MovementState.Run;
        if (isCrouching) moveState = MovementState.CrouchWalk;
        if (moveDirection == 0) moveState = MovementState.Idle;
    }

}
