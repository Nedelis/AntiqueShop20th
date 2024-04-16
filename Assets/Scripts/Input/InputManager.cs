using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Movement Controls")]
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction sprintAction;

    [Header("Interaction Controls")]
    public InputAction inventoryAction;
    public InputAction pickupAction;
    public InputAction dropAction;
    public InputAction escapeAction;
    public InputAction interactAction;
    
    [Header("Mouse Controls")]
    public InputAction LMBAction;

    [Header("Keyboard Digit Action")]
    public InputAction keyboardDigitAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }
    public bool InventoryToggled { get; private set; }
    public bool PickupTriggered { get; private set; }
    public bool DropTriggered { get; private set; }
    public bool EscapeToggled { get; private set; }
    public bool InteractToggled { get; private set; }
    public bool LMBToggled { get; private set; }
    public int KeyboardDigit { get; private set; }

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0.0f;
    }

    private void Update()
    {
        InventoryToggled = inventoryAction.WasPressedThisFrame();
        PickupTriggered = pickupAction.WasPressedThisFrame();
        DropTriggered = dropAction.WasPressedThisFrame();
        EscapeToggled = escapeAction.WasPressedThisFrame();
        InteractToggled = interactAction.WasPressedThisFrame();
        LMBToggled = LMBAction.WasPressedThisFrame();
        KeyboardDigit = keyboardDigitAction.WasPressedThisFrame()? (int)keyboardDigitAction.ReadValue<float>() : -1;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        inventoryAction.Enable();
        pickupAction.Enable();
        dropAction.Enable();
        escapeAction.Enable();
        interactAction.Enable();
        LMBAction.Enable();
        keyboardDigitAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        inventoryAction.Disable();
        pickupAction.Disable();
        dropAction.Disable();
        escapeAction.Disable();
        interactAction.Disable();
        LMBAction.Disable();
        keyboardDigitAction.Disable();
    }
}
