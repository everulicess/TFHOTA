using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonManager : MonoBehaviour
{
    private List<string> balloons = new List<string>();
    private List<string> textListToCheck = new List<string>();
    private List<GameObject> Darts = new List<GameObject>();

    [SerializeField] TextMeshProUGUI postDescription;

    [Header("Minigame Settings")]
    public string[] balloonText;

    //game flow variables
    [SerializeField] private bool isExplanationDone = false;
    [SerializeField] GameObject gameExplanationObject;
    [SerializeField] private int balloonsCount;
    [SerializeField] GameObject resultObject;
    private GameState gameState;

    private void Awake()
    {
        gameState = GameState.Explanation;
        PutTextOnTheList();
        foreach (GameObject dart in GameObject.FindGameObjectsWithTag("Dart"))
        {
            Darts.Add(dart);
        }
        DisplayResultText("");
    }
    private void Update()
    {
        HandleMinigameFlow();
    }

    public void GetText(Balloon balloon)
    {
        if (balloons.Count <= 0) return;
        
        int stringPos = Random.Range(0, balloons.Count - 1);
        balloon.balloonText.text = balloons[stringPos];
        balloons.RemoveAt(stringPos);
    }
    public void DisplayResultText(string _balloonText)
    {
        for (int i = 0; i < balloonText.Length; i++)
        {
            if (!string.Equals(_balloonText, balloonText[i])) return;
            
            textListToCheck.Remove(balloonText[i]);      
            
            postDescription.text = $" ";
            foreach (string str in textListToCheck)
            {
                postDescription.text += $"{str}\n ";
            }
        } Debug.Log(textListToCheck.Count);
    }
    private void PutTextOnTheList()
    {
        foreach (string str in balloonText)
        {
            balloons.Add(str);

            textListToCheck.Add(str);
        }
    }
    public void OnStartClicked()
    {
        isExplanationDone = true;
    }
    enum GameState
    {
        Explanation,
        PlayingTheGame,
        ShowingTheResult,
        Finished,
    }
    private void HandleMinigameFlow()
    {
        Debug.Log($"game state: {gameState}");
        switch (gameState)
        {
            case GameState.Explanation:
                gameExplanationObject.SetActive(true);
                resultObject.SetActive(false);
                if (!isExplanationDone) return;
                gameExplanationObject.SetActive(false);
                gameState = GameState.PlayingTheGame;
                break;
            case GameState.PlayingTheGame:
                if (balloonsCount > 0) return;
                gameState = GameState.ShowingTheResult;
                break;
            case GameState.ShowingTheResult:
                resultObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
