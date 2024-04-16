using UnityEngine;

public class PlayerOverlayController : MonoBehaviour
{
    [Header("Flow Settings")]
    [SerializeField] private FlowChannel flowChannel;

    [Header("Overlay Containers")]
    public GameObject pauseMenu;
    public GameObject inventoryMenu;
    public GameObject crosshairTooltipsContainer;

    public static PlayerOverlayController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void CloseMenu(string menuName)
    {
        if (menuName == "PauseMenu" && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            flowChannel.RaiseFlowStateRequest(FlowState.Free);
        }
        else if (menuName == "PlayerInventory" && inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(false);
            flowChannel.RaiseFlowStateRequest(FlowState.Free);
        }
        else if (menuName == "CrosshairTooltips" && crosshairTooltipsContainer.activeSelf)
        {
            crosshairTooltipsContainer.SetActive(false);
        }
    }

    private void Update()
    {
        if (FlowManager.Instance.CurrentState == FlowState.Free)
        {
            if (InputManager.Instance.EscapeToggled)
            {
                pauseMenu.SetActive(true);
                flowChannel.RaiseFlowStateRequest(FlowState.Pause);
                return;
            }
            else if (InputManager.Instance.InventoryToggled)
            {
                inventoryMenu.SetActive(true);
            }
            else
            {
                return;
            }
            flowChannel.RaiseFlowStateRequest(FlowState.InMenu);
        }
        else
        {
            if (InputManager.Instance.EscapeToggled)
            {
                if (pauseMenu.activeSelf) pauseMenu.SetActive(false);
                else if (inventoryMenu.activeSelf) inventoryMenu.SetActive(false);
                else return;
                flowChannel.RaiseFlowStateRequest(FlowState.Free);
            }
            else
            {
                if (InputManager.Instance.InventoryToggled && inventoryMenu.activeSelf) inventoryMenu.SetActive(false);
                else return;
                flowChannel.RaiseFlowStateRequest(FlowState.Free);
            }
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}