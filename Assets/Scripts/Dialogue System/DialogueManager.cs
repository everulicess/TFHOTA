using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private NPCAnimationController animationController;

    public Queue<DialogueFragment> dialogueQueue = new Queue<DialogueFragment>();

    [Header("References")]
    public GameObject dialogueCanvas;

    public Transform textBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button nextButton;

    private List<GameObject> activeChoices = new List<GameObject>();

    [Header("UI Config")]
    [SerializeField]
    private GameObject choiceButtonPrefab;

    [Header("Audio Config")]
    [SerializeField, Range(0f, 1f)]
    private float audioVolume = 1f;
    [SerializeField, Range(0.1f, 3f)]
    private float audioPitch = 1f;

    private bool inDialogue = false;
    private DialogueNode currentNode;

    [HideInInspector]
    public DialogueDispenser currentDialoguePartner;

    public void StartDialogue(DialogueNode rootNode, DialogueDispenser source)
    {
        if(inDialogue)
        {
            StopCurrentDialogue();
        }

        currentNode = rootNode;
        currentDialoguePartner = source;

        if(rootNode.dialogue.dialogueFragments.Length > 0)
        {
            foreach(DialogueFragment fragment in rootNode.dialogue.dialogueFragments)
            {
                dialogueQueue.Enqueue(fragment);
            }
        }

        animationController.ChangeAnimation("Talk");

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
            }

            var curFragment = dialogueQueue.Dequeue();
            DisplaySentence(curFragment);
            PlayAudioFragment(curFragment);
        }
        else
        {
            if(currentNode.choices.Length > 0)
            {
                DisplayChoices(currentNode.choices);
            }
            else
            {
                if(dialogueCanvas.activeSelf == true)
                    {
                        StopCurrentDialogue();
                    }  
            }

        }
    }

    private void DisplaySentence(DialogueFragment fragment)
    {
        if(!dialogueText.gameObject.activeSelf)
        {
            dialogueText.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
        }

        nameText.text = fragment.name + ":";
        dialogueText.text = fragment.text;
    }

    private void DisplayChoices(DialogueNode[] choices)
    {
        if(dialogueText.gameObject.activeSelf)
        {
            nameText.gameObject.SetActive(false);
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }

        foreach(DialogueNode choice in choices)
        {
            CreateChoiceButton(choice);
        }
    }

    private void CreateChoiceButton(DialogueNode choice)
    {
        var button = Instantiate(choiceButtonPrefab, textBox);
        button.GetComponent<Button>().onClick.AddListener(delegate{Choose(choice);});
        button.GetComponentInChildren<TMP_Text>().text = choice.description;
        activeChoices.Add(button);
    }

    private void Choose(DialogueNode choice)
    {
        Debug.Log("You Chose: " + choice.description);
        currentNode = choice;

        foreach(GameObject button in activeChoices)
        {
            Destroy(button);
        }

        activeChoices.Clear();

        foreach(DialogueFragment fragment in choice.dialogue.dialogueFragments)
        {
            dialogueQueue.Enqueue(fragment);
        }
        NextSentence();
    }

    private void PlayAudioFragment(DialogueFragment fragment)
    {
        if(fragment.dialogueAudio != null)
        {
            currentDialoguePartner.PlayAudio(fragment.dialogueAudio.audioClip, audioVolume, audioPitch);
        }
    }

    public void StopCurrentDialogue()
    {
        dialogueCanvas.SetActive(false);
        inDialogue = false;
        dialogueQueue.Clear();

        if(activeChoices.Count > 0)
        {
            foreach(GameObject button in activeChoices)
                {
                    Destroy(button);
                }
        }

        currentDialoguePartner.isActive = false;
        currentDialoguePartner.StopAudio();
        currentDialoguePartner = null;

        animationController.ChangeAnimation("Idle");
    }
}
