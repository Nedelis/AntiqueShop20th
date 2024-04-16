using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    [SerializeField] private float jumpStrength = 1.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 1.0f;
    [Range(0f, 90f)] [SerializeField] private float verticalRotationLimit = 90f;
    [SerializeField] private float walkFov = 60f;
    [SerializeField] private float sprintFov = 90f;
    [SerializeField] private float fovChangeSpeed = 20f;

    [Header("Interaction")]
    [SerializeField] private float interactionDistance;
    [SerializeField] private HotbarController hotbarController;

    private CharacterController characterController;
    private Vector3 currentMovement;
    private float verticalRotation;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (FlowManager.Instance.CurrentState == FlowState.Pause)
        {
            return;
        }
        else if (FlowManager.Instance.CurrentState == FlowState.Free)
        {
            HandleMovementInput();
            HandleRotation();
        }
        else
        {
            currentMovement.x = 0;
            currentMovement.z = 0;
        }
        HandlePhysics();
    }

    void HandleMovementInput()
    {
        Vector3 horizontalMovement = walkSpeed * (transform.forward * InputManager.Instance.MoveInput.y + transform.right * InputManager.Instance.MoveInput.x);
        
        if (InputManager.Instance.SprintValue > 0)
        {
            horizontalMovement *= sprintMultiplier;
            if (Camera.main.fieldOfView < sprintFov) Camera.main.fieldOfView += fovChangeSpeed * Time.deltaTime;
        }
        else if (Camera.main.fieldOfView > walkFov)
        {
            Camera.main.fieldOfView -= fovChangeSpeed * Time.deltaTime;
        }

        currentMovement.x = horizontalMovement.x;
        if (characterController.isGrounded) currentMovement.y = InputManager.Instance.JumpTriggered? jumpStrength : -0.5f;
        currentMovement.z = horizontalMovement.z;
    }

    void HandleRotation()
    {
        Vector3 rotation = mouseSensitivity * new Vector3(-InputManager.Instance.LookInput.y, InputManager.Instance.LookInput.x);
        verticalRotation = Mathf.Clamp(verticalRotation + rotation.x, -verticalRotationLimit, verticalRotationLimit);

        transform.Rotate(0, rotation.y, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void HandlePhysics()
    {
        if (!characterController.isGrounded) currentMovement.y -= Time.deltaTime * gravity;
        characterController.Move(Time.deltaTime * currentMovement);
    }

    public void PutInInventory(ItemController itemController)
    {
        hotbarController.PickupItem(itemController);
    }

    public bool CanInteract(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position) <= interactionDistance;
    }
}