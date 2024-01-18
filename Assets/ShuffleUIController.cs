using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject shuffleButton;
    [SerializeField]
    private GameObject backButton;

    void Start()
    {
        CupGameManager.onLogoComplete += SwitchToEndUI;
    }

    private void SwitchToEndUI()
    {
        shuffleButton.SetActive(false);
        backButton.SetActive(true);
    }
}
