using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public Queue<Sentence> dialogueQueue = new Queue<Sentence>();
    public GameObject dialogueCanvas;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private bool inDialogue = false;
    public DialogueDispenser currentDialoguePartner;
    public void StartDialogue(Dialogue dialogue, DialogueDispenser source)
    {
        if(inDialogue)
        {
            StopCurrentDialogue();
        }

        currentDialoguePartner = source;

        if(dialogue.dialogueText.Length > 0)
        {
            foreach(Sentence sentence in dialogue.dialogueText)
            {
                dialogueQueue.Enqueue(sentence);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(dialogueQueue.Count > 0)  
        {
            if(dialogueCanvas.activeSelf == false)
            {
                dialogueCanvas.SetActive(true);
                inDialogue = true;
                Debug.Log("Set True");
            }

            nameText.text = dialogueQueue.Peek().name + ":";
            dialogueText.text = dialogueQueue.Dequeue().text;
        }
        else
        {
            if(dialogueCanvas.activeSelf == true)
                {
                    StopCurrentDialogue();
                }  
        }
    }

    public void StopCurrentDialogue()
    {
        dialogueCanvas.SetActive(false);
        inDialogue = false;
        dialogueQueue.Clear();
        currentDialoguePartner.isActive = false;
        currentDialoguePartner = null;
        Debug.Log("Set False");
    }
}
