using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Cup : MonoBehaviour
{
    private CupGameManager gameManager;
    private GameObject selectButton;

    public event Action onSelect;
    public event Action onCorrectGuess;
    public GameObject ball;

    void Awake()
    {
        gameManager = FindObjectOfType<CupGameManager>();
        gameManager.SubscribeCup(this);
        GetComponentInChildren<Button>().onClick.AddListener(SelectCup);
        selectButton = GetComponentInChildren<Canvas>().gameObject;
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

    public void SetBallColor(Color color)
    {
        ball.GetComponent<Renderer>().material.color = color;
    }

    public void SelectCup()
    {
        if(gameManager.GetSelectedCup() == this)
        {
            onCorrectGuess?.Invoke();
        }

        onSelect?.Invoke();
    }
}
