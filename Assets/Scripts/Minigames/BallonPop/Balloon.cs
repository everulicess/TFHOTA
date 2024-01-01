using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    private BalloonManager gameManager;
    
    public TextMeshPro balloonText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<BalloonManager>();
        gameManager.GetText(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dart"))
        {
            gameManager.DisplayResultText(balloonText.text);
            Debug.Log($"has been hit: {balloonText.text}");
            this.gameObject.SetActive(false);
        }
    }
}
