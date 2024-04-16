using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tooltips/Crosshair Tooltip")]
public class CrosshairTooltip : ScriptableObject
{
    [SerializeField] private Sprite tooltipIcon;
    [SerializeField] private string tooltipPrefix;
    [SerializeField] private GameObject tooltipTemplate;

    private GameObject tooltipObject;

    public bool IsActive()
    {
        return tooltipObject != null;
    }

    public void ShowTooltip(GameObject tooltipsContainer, string tooltipText)
    {
        HideTooltip();
        tooltipObject = Instantiate(tooltipTemplate, tooltipsContainer.transform);
        tooltipObject.transform.Find("Icon").GetComponent<Image>().sprite = tooltipIcon;
        tooltipObject.transform.Find("DescriptionContainer").Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = tooltipPrefix + " " + tooltipText;
    }

    public void HideTooltip()
    {
        if (tooltipObject != null)
        {
            Destroy(tooltipObject);
            tooltipObject = null;
        }
    }
}