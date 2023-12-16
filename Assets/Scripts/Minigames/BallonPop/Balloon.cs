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
        gameManager.AssignText(this);
    }

}
