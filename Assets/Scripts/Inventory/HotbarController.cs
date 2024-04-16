using UnityEngine;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour
{
    [Header("Hotbar Settings")]
    [SerializeField] private int hotbarSize = 5;
    [SerializeField] private InventoryController linkedInventory;
    [SerializeField] private Transform hotbarContainer;
    [SerializeField] private Slot slotType;

    [Header("Drop Settings")]
    [SerializeField] private Transform dropPosition;
    [SerializeField] private float dropDistance;
    [SerializeField] private float dropStrength;

    private int activeSlot = 0;
    
    private void Awake()
    {
        for (int i = 0; i < hotbarSize; i++)
            slotType.CreateSlotObject(hotbarContainer.transform);
        hotbarContainer.GetChild(0).GetComponent<Image>().sprite = slotType.activeStateSprite;
    }

    private void Update()
    {
        if (!hotbarContainer.gameObject.activeSelf) return;

        if (InputManager.Instance.DropTriggered && GetActiveItem() != null)
        {
            linkedInventory.RemoveItem(activeSlot).Drop(dropPosition.position + dropDistance * dropPosition.forward).TryGetComponent(out Rigidbody itemRb);
            if (itemRb != null) itemRb.AddForce(dropStrength * dropPosition.forward, ForceMode.Impulse);
            UpdateHotbar();
        }
        
        var newActiveSlot = InputManager.Instance.KeyboardDigit;
        if (newActiveSlot != -1 && newActiveSlot < hotbarSize)
        {
            hotbarContainer.GetChild(activeSlot).GetComponent<Image>().sprite = slotType.inactiveStateSprite;
            activeSlot = newActiveSlot;
            hotbarContainer.GetChild(activeSlot).GetComponent<Image>().sprite = slotType.activeStateSprite;
        }
    }

    public Item GetActiveItem()
    {
        return linkedInventory.GetItem(activeSlot);
    }

    public void PickupItem(ItemController itemToPut)
    {
        itemToPut.Pickup(linkedInventory, GetActiveItem() == null? activeSlot : -1);
        UpdateHotbar();
    }

    public void UpdateHotbar()
    {
        for (int i = 0; i < hotbarSize; i++)
        {
            var itemStackImage = hotbarContainer.GetChild(i).Find("ItemStack").GetComponent<Image>();
            var correspondingItem = linkedInventory.GetItem(i);
            if (correspondingItem != null)
            {
                itemStackImage.sprite = correspondingItem.itemSprite;
                itemStackImage.color = Color.white;
            }
            else
            {
                itemStackImage.sprite = null;
                itemStackImage.color = Color.clear;
            }
        }
    }
}