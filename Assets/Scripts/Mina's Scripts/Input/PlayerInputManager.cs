using Events;
using Input;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Player.Movement.ForceAccumulate;
using static UnityEngine.InputSystem.InputAction;
[RequireComponent(typeof(MovementController))]
public class PlayerInputManager : MonoBehaviour
{
    private PlayerInputController _inputControls;
    public PlayerInputController.PlayerActions playerMap => _inputControls.Player;
    MovementController playerMovementController;

    private void Awake()
    {
        playerMovementController = GetComponent<MovementController>();
        _inputControls = new PlayerInputController();
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
        // playerMap.Jump.canceled += OnJumpRelist;

        // TODO: Fix pushing and pulling objects
        //playerMap.PushOrPull.performed += OnPushOrPullPreset;
        //playerMap.PushOrPull.canceled += OnPushOrPullRelist;

        playerMap.Crouch.performed += ToggleCrouching;
        playerMap.Crouch.canceled += StopCrouching;
    }
    private void SetWalkDirection(CallbackContext callbackContext) => playerMovementController.SetWalkDirection(callbackContext.ReadValue<float>());
    private void StopWalk(CallbackContext callbackContext) => playerMovementController.SetWalkDirection(0);
    private void ToggleRunning(CallbackContext callbackContext) => playerMovementController.ToggleRunning(true);
    private void StopRunning(CallbackContext callbackContext) => playerMovementController.ToggleRunning(false);
    private void DoJump(CallbackContext callbackContext) => playerMovementController.Jump();
    private void ToggleCrouching(CallbackContext callbackContext) => playerMovementController.ToggleCrouch(true);
    private void StopCrouching(CallbackContext callbackContext) => playerMovementController.ToggleCrouch(false);
}
