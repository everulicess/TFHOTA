using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text scoreText;
    private CupGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<CupGameManager>();
        CupGameManager.onScoreUpdate += UpdateScore;
        scoreText = GetComponent<TMP_Text>();
    }

    private void UpdateScore()
    {
        scoreText.text = gameManager.score.ToString() + " points";
    }
}
