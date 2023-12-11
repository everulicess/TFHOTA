using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    [Header("Node Info")]
    public string description;
    public Dialogue dialogue;

    [Header("Dialogue Choices")]
    public DialogueNode[] choices;
}
