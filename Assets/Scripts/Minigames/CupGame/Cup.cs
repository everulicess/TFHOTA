using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Cup : MonoBehaviour
{
    [SerializeField]
    private int cupNumber;

    private CupGameManager gameManager;

    [SerializeField]
    private LogoCreator creator;
    [SerializeField]
    private GameObject selectButton;

    private Button button;

    public event Action onSelect;
    public GameObject preview;

    void Awake()
    {
        gameManager = FindObjectOfType<CupGameManager>();
        gameManager.SubscribeCup(this);
        button = GetComponentInChildren<Button>();
    }

    void Start()
    {
        button.onClick.AddListener(SelectCup);
        selectButton.SetActive(false);
    }

    public void OpenSelectionButton()
    {
        selectButton.SetActive(true);
        selectButton.transform.rotation = Quaternion.identity;
    }

    public void HideSelectionButton()
    {
        selectButton.SetActive(false);
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
            Debug.Log(gameManager.results[i]);
        }

        tempArray[gameManager.round] = cupNumber;
        
        creator.CreateLogo(tempArray);
    }

    public void ClosePreview()
    {
        creator.HideDisplay();
    }

    public void OpenPreview()
    {
        creator.display.position = new Vector3(transform.position.x, creator.display.position.y, transform.position.z);
        creator.ShowDisplay();
    }

    private void SelectCup()
    {
        gameManager.SetSelection(cupNumber);

        onSelect?.Invoke();
    }
}
