using Animation;
using Player.Movement;
using UnityEngine;
using static Global.GlobalEnumLibrary;
[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float runSpeed = 15;

    [Header("crouch")]
    [SerializeField] private float crouchWalkSpeed = 5;
    [SerializeField] private float crouchRunSpeed = 7.5f;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 15;
    [SerializeField] private float runJumpPower = 7.5f;

    Rigidbody2D rigidBody;
    private float moveDirection = 0f;
    private bool isCrouching = false;
    private bool isRunning = false;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (moveDirection == 0) return;
        float speed = walkSpeed;
        // Todo: Create state machine to be used for both anims and a switch case here
        if (isRunning) speed = runSpeed;
        if (isCrouching) speed = crouchWalkSpeed;
        if (isCrouching && isRunning) speed = crouchRunSpeed;
        gameObject.transform.position += Vector3.right * (moveDirection * speed * Time.fixedDeltaTime);
    }
    public void Jump()
    {
        if (Global.GlobalFunctionsLibrary.IsGrounded(rigidBody))
        {
            rigidBody.AddForce(Vector2.up * jumpPower * 1000);
        }
    }
   public void SetWalkDirection(float newMoveDirection)
    {
        moveDirection = newMoveDirection;

        //Todo: Fix Animation update

        //UpdatePlayerAnimationsDirection();

        //if (Crouch.GetIsCrouching)
        //{
        //    PlayerMoveState = MoveState.CrouchWalk;
        //    AnimationsStateMachine.SetPlayerAnimationAndAnimationsDirection(GetPlayerController.MainAnimationBone, AnimationsStates.IsCrouchingWalkLeft, AnimationsStates.IsCrouchingWalkRight);
        //    return;
        //}
        //PlayerMoveState = MoveState.Walk;
        //AnimationsStateMachine.SetPlayerAnimationAndAnimationsDirection(GetPlayerController.MainAnimationBone, AnimationsStates.IsWalkingLeft, AnimationsStates.IsWalkingRight);

    }
    public void ToggleRunning(bool isNowRunning)
    {  
        //Todo: Undo crouch and start running anim
        isRunning = isNowRunning; 
    }
    public void ToggleCrouch(bool isNowCrouching)
    {
        // Todo: Toggle model crouch both animation and hitbox 
        isCrouching = isNowCrouching;
    }

}
