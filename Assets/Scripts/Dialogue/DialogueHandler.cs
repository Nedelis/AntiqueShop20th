using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueChoice
{
    public string choiceText = null;
    public int nextNodeIndex = 0;
}

[Serializable]
public class DialogueNode
{
    public string nodeText;
    public string characterName;
    public DialogueChoice[] choices;
}

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private DialogueNode[] dialogueNodes;
    [SerializeField] private FlowChannel flowChannel;
    [SerializeField] private UIDialogueWindowController dialogueWindowController;
    [SerializeField] private UnityEvent onDialogueEnd;

    public void StartDialogue()
    {
        if (FlowManager.Instance.CurrentState == FlowState.InDialogue) return;
        dialogueWindowController.gameObject.SetActive(true);
        flowChannel.RaiseFlowStateRequest(FlowState.InDialogue);
        AcceptNode(0);
    }

    public void EndDialogue()
    {
        dialogueWindowController.gameObject.SetActive(false);
        flowChannel.RaiseFlowStateRequest(FlowState.Free);
        onDialogueEnd?.Invoke();
    }

    public void AcceptNode(int index)
    {
        dialogueWindowController.UpdateContainer(dialogueNodes[index], this);
    }
}