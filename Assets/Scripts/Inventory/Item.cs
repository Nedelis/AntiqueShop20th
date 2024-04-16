using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Item Properties")]
    [SerializeField] private int id;
    public string itemName;
    public Sprite itemSprite;
    public GameObject itemModel;

    [Header("Item Object Properties")]
    public CrosshairTooltip crosshairTooltip; 
    public Material hoverOutline;

    public bool Pickup(InventoryController inventoryController, int index = -1)
    {
        crosshairTooltip.HideTooltip();
        return inventoryController.AddItem(this, index);
    }

    public GameObject Drop(Vector3 position)
    {
        return Instantiate(itemModel, position, Quaternion.Euler(0, 0, 0));
    }
}