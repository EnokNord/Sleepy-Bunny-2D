using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

enum ToolTipType
{
    Persistent, Vanishing, Destructive
}
[RequireComponent(typeof(BoxCollider2D))]
public class ToolTipController : MonoBehaviour
{
    [SerializeField] ToolTipType toolTipType;
    [SerializeField] GameObject tooltipTextObject;
    [SerializeField] bool startsDisabled = false;

    [SerializeField] string keyboardText;
    [SerializeField] string controllerText;
    TextMeshPro tooltipText;
    bool isKeyboardAndMouse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tooltipText = tooltipTextObject.GetComponent<TextMeshPro>();
        InputSystem.onActionChange += InputActionChangeCallback;
    }
    private void OnDisable()
    {
        InputSystem.onActionChange -= InputActionChangeCallback;
    }
    private void InputActionChangeCallback(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction receivedInputAction = (InputAction)obj;
            
            InputDevice lastDevice = receivedInputAction.activeControl.device;
            if (lastDevice.name.Equals("Mouse") || receivedInputAction.name == "Navigate" || receivedInputAction.name == "Look") return;
            isKeyboardAndMouse = lastDevice.name.Equals("Keyboard");
            if(isKeyboardAndMouse && tooltipText.text != keyboardText) tooltipText.text = keyboardText;
            if(!isKeyboardAndMouse && tooltipText.text != controllerText) tooltipText.text = controllerText;
        }
    }
       
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (toolTipType == ToolTipType.Vanishing || startsDisabled)
        {
            tooltipTextObject.SetActive(true);
            startsDisabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (toolTipType == ToolTipType.Vanishing || toolTipType == ToolTipType.Destructive)
        {
            tooltipTextObject.SetActive(false);
        }
    }
}
