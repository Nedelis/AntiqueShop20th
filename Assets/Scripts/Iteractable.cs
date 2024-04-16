using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private InputAction interactAction;
    [SerializeField] private UnityEvent onMouseEnter;
    [SerializeField] private UnityEvent onMouseOver;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent onMouseExit;

    private bool CanBeInteracted()
    {
        return FlowManager.Instance.CurrentState == FlowState.Free && PlayerController.Instance.CanInteract(gameObject);
    }

    private void OnMouseEnter()
    {
        if (CanBeInteracted())
            onMouseEnter?.Invoke();
    }

    private void OnMouseOver()
    {
        if (CanBeInteracted())
        {
            onMouseOver?.Invoke();
            if (interactAction.triggered)
                onInteract?.Invoke();
        }
        else
        {
            OnMouseExit();
        }
    }

    private void OnMouseExit()
    {
        onMouseExit?.Invoke();
    }

    private void OnEnable()
    {
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }
}