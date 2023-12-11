using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource), (typeof(SphereCollider)))]
public class DialogueDispenser : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject promptCanvasObject;

    [Header("UI Config")]
    [SerializeField, Range(0f, 5f)]
    private float displayHeight = 2f;

    [Header("Range Config")]
    [SerializeField, Range(5f, 30f)]
    private float speakRange = 10f;
    [SerializeField, Range(2f, 20f)]
    private float promptRange = 5f;

    [Header("Dialogue Config")]
    [SerializeField]
    private DialogueNode rootNode;

    [HideInInspector]
    public bool isActive;

    private Transform player;
    private GameObject promptCanvas;
    private DialogueManager dialogueManager;
    private AudioSource audioSource;
    private SphereCollider promptTrigger;


    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        audioSource = GetComponent<AudioSource>();
        promptTrigger = GetComponent<SphereCollider>();
        promptTrigger.isTrigger = true;
        promptTrigger.radius = promptRange;
        audioSource.loop = false;
        audioSource.spatialBlend = 1f;
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
        dialogueManager.StartDialogue(rootNode, this);
        isActive = true;
        CloseDialoguePrompt();
    }

    public void PlayAudio(AudioClip audio, float volume, float pitch)
    {
        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    //Debug Utility
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, speakRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, promptRange);
    }
}
