using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDispenser : MonoBehaviour
{
    [SerializeField]
    private GameObject promptCanvasObject;
    [SerializeField, Range(0, 5)]
    private float displayHeight = 2f;
    [SerializeField, Range(5, 30)]
    private float speakRange = 10f;
    [HideInInspector]
    public bool isActive;

    private Transform player;
    private GameObject promptCanvas;
    private DialogueManager dialogueManager;

    [SerializeField]
    private Dialogue dialogue;

    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if(isActive)
        {
            if((player.position - transform.position).magnitude > speakRange)
            {
                dialogueManager.StopCurrentDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && dialogueManager.currentDialoguePartner != this)
        {
            PromptDialogue();
            player = other.transform;
        }
    }

    void OnTriggerExit()
    {
        if(promptCanvas != null)
        {
            CloseDialoguePrompt();
        }
    }

    private void PromptDialogue()
    {
        if(promptCanvas == null)
        {
            promptCanvas = Instantiate(promptCanvasObject, transform);
            promptCanvas.transform.localPosition = new Vector3(0, displayHeight, 0);
            promptCanvas.GetComponentInChildren<Button>().onClick.AddListener(DisplayDialogue);
            promptCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
            return;
        }

        promptCanvas.SetActive(true);
    }

    private void CloseDialoguePrompt()
    {
        if(promptCanvas != null)
        {
            promptCanvas.SetActive(false);
        }
    }

    public void DisplayDialogue()
    {
        dialogueManager.StartDialogue(dialogue, this);
        isActive = true;
        CloseDialoguePrompt();
    }

    //Debug Utility
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, speakRange);
    }
}
