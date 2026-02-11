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
    [SerializeField] private float horizontalRunJumpPower = 7.5f;

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

    CapsuleCollider2D hitBoxCollider;
    private void Awake()
    {
        CurrentWalkSpeed = WalkSpeed;
        rigidBody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<MovementAnimationController>();
        hitBoxCollider = GetComponent<CapsuleCollider2D>();
        originalHitboxSize = hitBoxCollider.size;
        originalHitboxOffset = hitBoxCollider.offset;
    }

    private void FixedUpdate()
    {
        float speed = currentWalkSpeed;
        switch (moveState)
        {
            case MovementState.Idle: return;
            case MovementState.Run: speed = runSpeed; break;
            case MovementState.CrouchWalk: speed = crouchWalkSpeed; break;
            default:
                break;
        }
        if(Mathf.Abs(rigidBody.linearVelocityX) < 10)
        {
            transform.position += Vector3.right * (moveDirection * speed * Time.fixedDeltaTime);
        }
        
    }
    public void Jump()
    {
        if (Global.GlobalFunctionsLibrary.IsGrounded(rigidBody))
        {
            if (isRunning)
            {
                rigidBody.linearVelocityY = runJumpPower;
                rigidBody.linearVelocityX += runJumpPower * moveDirection;
            }
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
                hitBoxCollider.size = (originalHitboxSize * 0.75f);
                hitBoxCollider.offset = (originalHitboxOffset + Vector2.down * 0.5f);
        }
        else
        {
                hitBoxCollider.size =  originalHitboxSize;
                hitBoxCollider.offset = originalHitboxOffset;
        }
        animationController.UpdateAnimationState("IsCrouching", isCrouching);
        UpdateMovementState();
    }
    public void UpdateMovementState()
    {
        moveState = MovementState.Walk;
        if (isRunning) moveState = MovementState.Run;
        if (isCrouching) moveState = MovementState.CrouchWalk;
        if (moveDirection == 0) moveState = MovementState.Idle;
    }

}
