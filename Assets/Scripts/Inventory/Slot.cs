using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Slot")]
public class Slot : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] private GameObject selfPrefab;
    [SerializeField] private GameObject tooltipPrefab;
    public Sprite activeStateSprite;
    public Sprite inactiveStateSprite;

    public GameObject CreateSlotObject(Transform container)
    {
        return Instantiate(selfPrefab, container);
    }

    public GameObject CreateTooltipObject(Transform container)
    {
        return Instantiate(tooltipPrefab, container);
    }
}