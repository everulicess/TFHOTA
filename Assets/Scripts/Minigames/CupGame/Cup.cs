using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Cup : MonoBehaviour
{
    public int cupNumber;

    private CupGameManager gameManager;

    [SerializeField]
    private LogoCreator creator;


    public event Action onSelect;
    public GameObject preview;

    void Awake()
    {
        gameManager = FindObjectOfType<CupGameManager>();
        gameManager.SubscribeCup(this);
    }
    
    public void DisplayPreview()
    {
        OpenPreview();
        creator.ClearDisplay();
        int[] tempArray = new int[] { -1, -1, -1, -1, -1, -1};

        for(int i = 0; i < tempArray.Length -1; i++)
        {
            int value = gameManager.results[i];
            tempArray[i] = value;
        }

        tempArray[gameManager.round] = cupNumber;
        
        creator.CreateLogo(tempArray);
    }

    public void DisplaySelection()
    {
        OpenPreview();
        creator.ClearDisplay();
        int[] tempArray = new int[] { -1, -1, -1, -1, -1, -1 };

        for (int i = 0; i < tempArray.Length - 1; i++)
        {
            int value = gameManager.results[i];
            tempArray[i] = value;
        }

        tempArray[gameManager.round - 1] = cupNumber;

        creator.CreateLogo(tempArray);
    }

    public void ClosePreview()
    {
        creator.HideDisplay();
    }

    public void OpenPreview()
    {
        creator.ShowDisplay();
    }

    public void SelectCup()
    {
        if(gameManager.isPicking)
        {
            gameManager.SetSelection(cupNumber);

            onSelect?.Invoke();
        }
    }
}
