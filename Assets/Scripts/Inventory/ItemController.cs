using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Item item;
    private Material[] originalMaterials;

    private void Awake()
    {
        originalMaterials = gameObject.GetComponent<Renderer>().materials;
    }

    public void WhileTargeted()
    {
        if (!item.crosshairTooltip.IsActive())
        {
            item.crosshairTooltip.ShowTooltip(PlayerOverlayController.Instance.crosshairTooltipsContainer, item.itemName);
            GetComponent<Renderer>().materials = originalMaterials.Concat(new Material[] { item.hoverOutline }).ToArray();
        }
    }

    public void OnInteracted()
    {
        PlayerController.Instance.PutInInventory(this);
    }

    public void OnLeft()
    {
        item.crosshairTooltip.HideTooltip();
        GetComponent<Renderer>().materials = originalMaterials;
    }

    public void Pickup(InventoryController inventoryController, int index = -1)
    {
        if (item.Pickup(inventoryController, index)) Destroy(gameObject);
    }
}