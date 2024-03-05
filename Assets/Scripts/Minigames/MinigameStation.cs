using UnityEngine;

public class MinigameStation : MonoBehaviour
{
    [SerializeField] GameObject minigameButton;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            minigameButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            minigameButton.SetActive(false);
        }
    }
}
