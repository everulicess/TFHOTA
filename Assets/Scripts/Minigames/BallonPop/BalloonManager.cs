using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BalloonManager : MonoBehaviour
{
    private List<string> balloons = new();
    private List<string> textListToCheck = new();
    private List<GameObject> Darts = new();
    [Header("Post")]
    [SerializeField] [TextArea(3, 10)] string[] balloonText;
    [Tooltip("The text wil be displayed on this text object")]
    [SerializeField] TextMeshProUGUI postDescription;


    [Header("GameFlow")]
    [Header("Explanation")]
    [Tooltip("Attach the object that has the explanation for the game")]
    [SerializeField] GameObject gameExplanationObject;
    [Tooltip("Description for the minigame")]
    [TextArea(5,10)][SerializeField] string gameExplanationText = "";
    [Tooltip("Text object that will display the game description")]
    [SerializeField] TextMeshProUGUI gameExplanationTextHolder;

    [Header("Finished Post")]
    [Tooltip("Attach the object that has the explanation for the game")]
    [SerializeField] GameObject resultObject;
    [Tooltip("Text object that will display the post description")]
    [SerializeField] TextMeshProUGUI postResultText;

    


    //game flow variables
    [SerializeField] private bool isExplanationDone = false;
    [SerializeField] private bool isGameDone = false;
    private int balloonsCount = 1;
    private GameState gameState;

    private void Awake()
    {
        gameExplanationTextHolder.text = gameExplanationText;
        gameState = GameState.Explanation;
        PutTextOnTheList();
        foreach (GameObject dart in GameObject.FindGameObjectsWithTag("Dart"))
        {
            Darts.Add(dart);
        }
        DisplayResultText("Init");

    }
    private void Update()
    {
        HandleMinigameFlow();
        

    }

    public void GetText(Balloon balloon)
    {
        if (balloons.Count <= 0) return;
        balloonsCount++;
        //Debug.Log($"balloons count is: {balloonsCount}");
        int stringPos = Random.Range(0, balloons.Count - 2);
        balloon.balloonText.text = balloons[stringPos];
        balloons.RemoveAt(stringPos);
    }
    public void DisplayResultText(string _balloonText)
    {
        
        for (int i = 0; i < balloonText.Length; i++)
        {
            if (string.Equals(_balloonText, balloonText[i]))
            {
                textListToCheck.Remove(balloonText[i]);
                balloonsCount--;
                postDescription.text = $"";
                foreach (string str in textListToCheck)
                {

                    postDescription.text += $"{str}\n ";
                }
            }
        } 
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
        //Debug.Log($"game state: {gameState}");
        //Debug.Log($"POST DESCRIPTION:  {postDescription.text}");
        switch (gameState)
        {
            case GameState.Explanation:
                ExplanationHandle();
                
                break;
            case GameState.PlayingTheGame:
                //DisplayResultText("");
                Debug.Log($"balloons count : {balloonsCount}");

                if (balloonsCount > 5) return;
                gameState = GameState.ShowingTheResult;
                break;
            case GameState.ShowingTheResult:
                ShowResult();
                break;
            default:
                break;
        }
    }

    private void ShowResult()
    {
        Debug.Log(postDescription.text);
        resultObject.SetActive(true);
        postResultText.text = postDescription.text;
        GameData.instance.balloonGameResults = postDescription.text;
    }

    private void ExplanationHandle()
    {
        gameExplanationObject.SetActive(true);
        resultObject.SetActive(false);
        if (!isExplanationDone) return;
        gameExplanationObject.SetActive(false);
        gameState = GameState.PlayingTheGame;
    }

    public void OnAgainClicked()
    {
        gameState = GameState.Explanation;
        Debug.Log("AGAIN WAS PRESSED");
    }
    public void OnFinishedClicked()
    {
        //change scene and stave the result
        Debug.Log("FIINISHED WAS PRESSED");
    }
}
