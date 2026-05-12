using Events;
using Input;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Player.Movement.ForceAccumulate;
using static UnityEngine.InputSystem.InputAction;
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(ObjectMovingComponent))]
[RequireComponent(typeof(ClimbingController))]
[RequireComponent(typeof(PlayerHealthComponent))]
public class PlayerInputManager : MonoBehaviour
{
    public PlayerInputController.PlayerActions playerMap => inputControls.Player;
    [SerializeField] GameObject pauseMenuCanvas;
    
    MovementController playerMovementController;
    ClimbingController climbingController;
    ObjectMovingComponent objectMovingComponent;
    PlayerInputController inputControls;

    private void Awake()
    {
        playerMovementController = GetComponent<MovementController>();
        objectMovingComponent = GetComponent<ObjectMovingComponent>();
        climbingController = GetComponent<ClimbingController>();
        inputControls = new PlayerInputController();
        playerMap.Enable();
        SetupInputBindings();
    }
    private void OnDisable()
    {
        playerMap.Walk.performed -= SetWalkDirection;
        playerMap.Walk.canceled -= StopWalk;

        playerMap.Run.performed -= ToggleRunning;
        playerMap.Run.canceled -= StopRunning;

        playerMap.Jump.performed -= DoJump;
        playerMap.Jump.performed -= StopClimb;
        playerMap.Jump.performed -= InteractReleased;

        playerMap.Crouch.performed -= ToggleCrouching;
        playerMap.Crouch.canceled -= StopCrouching;

        playerMap.Interact.performed -= InteractPressed;
        playerMap.Interact.canceled -= InteractReleased;

        playerMap.Pause.performed -= TogglePauseMenu;

        playerMap.Climb.performed -= Climb;
        playerMap.Climb.canceled -= StopMidClimb;

        playerMap.Disable();
    }
    private void SetupInputBindings()
    {
        playerMap.Walk.performed += SetWalkDirection;
        playerMap.Walk.canceled += StopWalk;

        playerMap.Run.performed += ToggleRunning;
        playerMap.Run.canceled += StopRunning;

        playerMap.Jump.performed += DoJump;
        playerMap.Jump.performed += StopClimb;
        playerMap.Jump.performed += InteractReleased;

        playerMap.Crouch.performed += ToggleCrouching;
        playerMap.Crouch.canceled += StopCrouching;

        playerMap.Interact.performed += InteractPressed;
        playerMap.Interact.canceled += InteractReleased;

        playerMap.Pause.performed += TogglePauseMenu;

        playerMap.Climb.performed += Climb;
        playerMap.Climb.canceled += StopMidClimb;
    }
    private void SetWalkDirection(CallbackContext callbackContext) => playerMovementController.SetWalkDirection(callbackContext.ReadValue<float>());
    private void StopWalk(CallbackContext callbackContext) => playerMovementController.SetWalkDirection(0);
    private void ToggleRunning(CallbackContext callbackContext) => playerMovementController.ToggleRunning(true);
    private void StopRunning(CallbackContext callbackContext) => playerMovementController.ToggleRunning(false);
    private void DoJump(CallbackContext callbackContext) => playerMovementController.Jump();
    private void ToggleCrouching(CallbackContext callbackContext) => playerMovementController.ToggleCrouch(true);
    private void StopCrouching(CallbackContext callbackContext) => playerMovementController.ToggleCrouch(false);
    private void Climb(CallbackContext callbackContext) => climbingController.TryClimb(callbackContext.ReadValue<float>());
    private void StopMidClimb(CallbackContext callbackContext) => climbingController.TryClimb(0);
    private void StopClimb(CallbackContext callbackContext) => climbingController.StopClimbing();

    private void InteractPressed(CallbackContext callbackContext)
    {
        if(objectMovingComponent.CanGrab) objectMovingComponent.GrabObject();
    }
    private void InteractReleased(CallbackContext callbackContext)
    {
        objectMovingComponent.ReleaseObject();
    }
    public void TogglePauseMenu(CallbackContext callbackContext)
    {
        if (pauseMenuCanvas == null) return;
        if(pauseMenuCanvas.activeSelf)
        {
            pauseMenuCanvas.SetActive(false);
            LevelFunctionsLibrary.LevelFunctions.ToggleGamePause(false);
            EnableInput();
        }
        else
        {
            pauseMenuCanvas.SetActive(true);
            LevelFunctionsLibrary.LevelFunctions.ToggleGamePause(true);
            DisableInput();
        }
    }

    public void DisableInput()
    {
        playerMap.Disable();
    }
    public void EnableInput()
    {
        playerMap.Enable();
    }
   
}
