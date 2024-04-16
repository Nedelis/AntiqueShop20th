using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class NPCSpecialDialogue
{
    public DialogueHandler specialDialogue;
    public Item triggerItem = null;
}

public class NPCController : MonoBehaviour
{
    [Header("Main NPC Settings")]
    [SerializeField] private string npcName;
    [SerializeField] private CrosshairTooltip crosshairTooltip;
    [SerializeField] private DialogueHandler defaultDialogue;

    [Header("Advanced NPC Settings")]
    [SerializeField] private bool hasSpecialDialogues = false;
    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private NPCSpecialDialogue[] dialogues;

    public void WhileTargeted()
    {
        if (!crosshairTooltip.IsActive())
            crosshairTooltip.ShowTooltip(PlayerOverlayController.Instance.crosshairTooltipsContainer, npcName);
    }

    public void OnInteracted()
    {
        if (!hasSpecialDialogues)
        {
            defaultDialogue.StartDialogue();
        }
        else
        {
            var dialogue = dialogues.FirstOrDefault(npcDialogue => npcDialogue.triggerItem == hotbarController.GetActiveItem());
            if (dialogue != null) dialogue.specialDialogue.StartDialogue();
            else defaultDialogue.StartDialogue();
        }
        OnLeft();
    }

    public void OnLeft()
    {
        crosshairTooltip.HideTooltip();
    }
}