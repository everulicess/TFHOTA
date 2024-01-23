using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CupTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialCanvas;
    [SerializeField]
    private GameObject[] tutorialScreens = new GameObject[4];
    [SerializeField]
    private TMP_Text buttonText;

    private int currentScreen = -1;

    public void DisplayNext()
    {
        if(currentScreen == -1)
        {
            tutorialCanvas.SetActive(true);
            buttonText.text = "Next >";
        }

        currentScreen++;
        HideScreens();

        if(currentScreen == 3)
        {
            buttonText.text = "Finish Tutorial";
        }

        if(currentScreen > tutorialScreens.Length - 1)
        {
            currentScreen = -1;
            tutorialCanvas.SetActive(false);
            buttonText.text = "Start Tutorial";
            return;
        }

        DisplayScreen(currentScreen);
    }

    private void HideScreens()
    {
        foreach(GameObject screen in tutorialScreens)
        {
            screen.SetActive(false);
        }
    }

    private void DisplayScreen(int val)
    {
        tutorialScreens[val].SetActive(true);
    }
}
