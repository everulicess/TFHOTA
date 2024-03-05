using UnityEngine;
using System;

public class Cup : MonoBehaviour
{
    public int cupNumber;

    private CupGameManager gameManager;

    [SerializeField]
    private LogoCreator creator;
    [SerializeField]
    private LogoCreator screenCreator;


    public event Action onSelect;
    public GameObject preview;
    public GameObject screenPreview;

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
        //screenCreator.CreateLogo(tempArray);
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
        //screenCreator.CreateLogo(tempArray);
    }

    public void ClosePreview()
    {
        creator.HideDisplay();
        //screenCreator.HideDisplay();
    }

    public void OpenPreview()
    {
        preview.transform.position = new Vector3(transform.position.x, preview.transform.position.y, transform.position.z);
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
