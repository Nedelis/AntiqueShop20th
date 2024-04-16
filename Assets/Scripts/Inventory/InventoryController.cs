using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Cursor")]
    [SerializeField] private SlotController cursorSlot;
    [SerializeField] private Transform tooltipContainer;

    [Header("Inventory Settings")]
    [SerializeField] private string inventoryName;
    [SerializeField] private int slotsAmount;
    [SerializeField] private Slot slotType;
    [SerializeField] private GameObject inventoryContainer;

    private readonly List<SlotController> slots = new();

    private void Awake()
    {
        if (inventoryName != null) inventoryContainer.transform.Find("InventoryName").GetComponent<TextMeshProUGUI>().text = inventoryName;
        var slotsContainer = inventoryContainer.transform.Find("SlotsContainer");
        for (int i = 0; i < slotsAmount; i++)
        {
            var slotController = slotType.CreateSlotObject(slotsContainer).AddComponent<SlotController>();
            slotController.slotType = slotType;
            slotController.tooltipContainer = tooltipContainer;
            slotController.onClickEvent = new();
            slotController.onClickEvent.AddListener(sc => {
                if (inventoryContainer.activeSelf) sc.PutItem(cursorSlot.PutItem(sc.GetItem()));
            });
            slots.Add(slotController);
        }
    }

    public List<Item> GetItems(int count, int startIndex = 0)
    {
        return slots.GetRange(startIndex, count).Select(sc => sc.GetItem()).ToList();
    }

    public Item GetItem(int index)
    {
        return slots[index].GetItem();
    }

    public bool AddItem(Item item, int index = -1)
    {
        if (index == -1)
        {
            var emptySlot = slots.Find(slot => slot.GetItem() == null);
            if (emptySlot == null) return false;
            emptySlot.PutItem(item); 
            return true;
        }
        else if (slots[index].GetItem() == null)
        {
            slots[index].PutItem(item);
            return true;
        }
        return false;
    }

    public Item RemoveItem(int index)
    {
        var slot = slots[index];
        var item = slot.GetItem();
        slot.PutItem(null);
        return item;
    }
}