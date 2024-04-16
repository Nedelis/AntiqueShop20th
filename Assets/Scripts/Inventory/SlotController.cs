using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slot slotType;
    public Transform tooltipContainer;
    public UnityEvent<SlotController> onClickEvent;

    private bool mouseOver = false;
    private GameObject tooltipObject;
    private Item content;

    public Item GetItem()
    {
        return content;
    }

    public Item PutItem(Item itemToPut)
    {
        var temp = GetItem();
        content = itemToPut;
        UpdateSlot();
        return temp;
    }

    private void UpdateSlot()
    {
        var itemStackImage = transform.Find("ItemStack").GetComponent<Image>();
        if (GetItem() != null)
        {
            itemStackImage.sprite = GetItem().itemSprite;
            itemStackImage.color = Color.white;
        }
        else
        {
            if (tooltipObject != null)
            {
                Destroy(tooltipObject);
                tooltipObject = null;
            }
            itemStackImage.sprite = null;
            itemStackImage.color = Color.clear;
        }
    }

    private void Update()
    {
        if (!mouseOver)
        {
            if (tooltipObject != null)
            {
                Destroy(tooltipObject);
                tooltipObject = null;
            }
            return;
        }
        if (tooltipObject == null && GetItem() != null && slotType != null)
        {
            tooltipObject = slotType.CreateTooltipObject(tooltipContainer);
            tooltipObject.transform.Find("TooltipText").GetComponent<TextMeshProUGUI>().text = GetItem().itemName;
        }
        if (InputManager.Instance.LMBToggled) onClickEvent?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}