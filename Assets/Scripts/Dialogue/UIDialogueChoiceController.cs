using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueChoiceController : MonoBehaviour
{
    public TextMeshProUGUI choiceTextBox;

    public delegate void ButtonCallback();
    public ButtonCallback OnClickCallback;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClickCallback());
    }
}