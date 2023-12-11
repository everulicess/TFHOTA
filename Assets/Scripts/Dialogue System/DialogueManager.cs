using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public Queue<DialogueFragment> dialogueQueue = new Queue<DialogueFragment>();

    [Header("References")]
    public GameObject dialogueCanvas;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private bool inDialogue = false;

    [HideInInspector]
    public DialogueDispenser currentDialoguePartner;
    public void StartDialogue(Dialogue dialogue, DialogueDispenser source)
    {
        if(inDialogue)
        {
            StopCurrentDialogue();
        }

        currentDialoguePartner = source;

        if(dialogue.dialogueFragments.Length > 0)
        {
            foreach(DialogueFragment fragment in dialogue.dialogueFragments)
            {
                dialogueQueue.Enqueue(fragment);
            }
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if(dialogueQueue.Count > 0)  
        {
            if(dialogueCanvas.activeSelf == false)
            {
                dialogueCanvas.SetActive(true);
                inDialogue = true;
                Debug.Log("Set True");
            }

            var curFragment = dialogueQueue.Dequeue();
            DisplaySentence(curFragment);
            PlayAudioFragment(curFragment);
        }
        else
        {
            if(dialogueCanvas.activeSelf == true)
                {
                    StopCurrentDialogue();
                }  
        }
    }

    private void DisplaySentence(DialogueFragment fragment)
    {
        nameText.text = fragment.name + ":";
        dialogueText.text = fragment.text;
    }

    private void PlayAudioFragment(DialogueFragment fragment)
    {
        if(fragment.dialogueAudio != null)
        {
            currentDialoguePartner.PlayAudio(fragment.dialogueAudio.audioClip);
        }
    }

    public void StopCurrentDialogue()
    {
        dialogueCanvas.SetActive(false);
        inDialogue = false;
        dialogueQueue.Clear();
        currentDialoguePartner.isActive = false;
        currentDialoguePartner.StopAudio();
        currentDialoguePartner = null;
        Debug.Log("Set False");
    }
}
