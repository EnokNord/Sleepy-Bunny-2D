using Events;
using Input;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Player.Movement.ForceAccumulate;
using static UnityEngine.InputSystem.InputAction;
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(ObjectMovingComponent))]
public class PlayerInputManager : MonoBehaviour, IDeathEvent
{
    public PlayerInputController.PlayerActions playerMap => inputControls.Player;
    [SerializeField] GameObject pauseMenuCanvas;
    
    MovementController playerMovementController;
    ObjectMovingComponent objectMovingComponent;
    PlayerInputController inputControls;

    private void Awake()
    {
        playerMovementController = GetComponent<MovementController>();
        objectMovingComponent = GetComponent<ObjectMovingComponent>();
        inputControls = new PlayerInputController();
        playerMap.Enable();
        SetupInputBindings();
    }
    private void SetupInputBindings()
    {
        playerMap.Walk.performed += SetWalkDirection;
        playerMap.Walk.canceled += StopWalk;

        playerMap.Run.performed += ToggleRunning;
        playerMap.Run.canceled += StopRunning;

        playerMap.Jump.performed += DoJump;
        playerMap.Jump.performed += ReleaseObject;

        playerMap.Crouch.performed += ToggleCrouching;
        playerMap.Crouch.canceled += StopCrouching;

        playerMap.PushOrPull.performed += GrabObject;
        playerMap.PushOrPull.canceled += ReleaseObject;

        playerMap.Pause.performed += TogglePauseMenu;
    }
    private void SetWalkDirection(CallbackContext callbackContext) => playerMovementController.SetWalkDirection(callbackContext.ReadValue<float>());
    private void StopWalk(CallbackContext callbackContext) => playerMovementController.SetWalkDirection(0);
    private void ToggleRunning(CallbackContext callbackContext) => playerMovementController.ToggleRunning(true);
    private void StopRunning(CallbackContext callbackContext) => playerMovementController.ToggleRunning(false);
    private void DoJump(CallbackContext callbackContext) => playerMovementController.Jump();
    private void ToggleCrouching(CallbackContext callbackContext) => playerMovementController.ToggleCrouch(true);
    private void StopCrouching(CallbackContext callbackContext) => playerMovementController.ToggleCrouch(false);
    private void GrabObject(CallbackContext callbackContext) => objectMovingComponent.GrabObject();
    private void ReleaseObject(CallbackContext callbackContext) => objectMovingComponent.ReleaseObject();

    public void TogglePauseMenu(CallbackContext callbackContext)
    {
        if (pauseMenuCanvas == null) return;
        if(pauseMenuCanvas.activeSelf)
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void TriggerDeathEvent()
    {
        //Todo: toggle reset
        playerMap.Disable();
        GetComponent<MovementAnimationController>().TriggerDeathAnimation();
    }
}
