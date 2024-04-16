using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDialogueWindowController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI dialogueTextBox;
    [SerializeField] private TextMeshProUGUI characterNameTextBox;
    [SerializeField] private GameObject choiceButtonsContainer;
    [SerializeField] private UIDialogueChoiceController choiceButtonPrefab;

    private delegate void OnWindowClickCallback();
    private OnWindowClickCallback OnWindowClick;

    public void UpdateContainer(DialogueNode currentNode, DialogueHandler dialogue)
    {
        dialogueTextBox.text = currentNode.nodeText;
        characterNameTextBox.text = currentNode.characterName;
        if (choiceButtonsContainer.transform.childCount > 0)
        {
            for (int i = 0; i < choiceButtonsContainer.transform.childCount; i++)
                Destroy(choiceButtonsContainer.transform.GetChild(i).gameObject);
        }
        if (currentNode.choices.Length == 0)
        {
            choiceButtonsContainer.SetActive(false);
            OnWindowClick = dialogue.EndDialogue;
        }
        else if (currentNode.choices.Length == 1)
        {
            choiceButtonsContainer.SetActive(false);
            OnWindowClick = () => dialogue.AcceptNode(currentNode.choices[0].nextNodeIndex);
        }
        else
        {
            OnWindowClick = () => {};
            choiceButtonsContainer.SetActive(true);
            foreach (var choice in currentNode.choices)
            {
                var button = Instantiate(choiceButtonPrefab, choiceButtonsContainer.transform);
                button.choiceTextBox.text = choice.choiceText;
                button.OnClickCallback += () => dialogue.AcceptNode(choice.nextNodeIndex);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnWindowClick();
    }
}