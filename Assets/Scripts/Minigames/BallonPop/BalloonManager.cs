using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonManager : MonoBehaviour
{
    private List<string> balloons = new List<string>();
    private List<string> textListToCheck = new List<string>();

    [SerializeField] GameObject balloonPrefab;
    [SerializeField] TextMeshProUGUI postDescription;

    [Header("Minigame Settings")]
    public string[] balloonText;
    public int balloonsAmount;
    
    [HideInInspector] public int score;


    private void Awake()
    {
        PutTextOnTheList();
        DisplayResultText("");
    }

    public void GetText(Balloon balloon)
    {
        //Debug.Log(balloons.Count);
        if (balloons.Count > 0)
        {
            int stringPos = Random.Range(0, balloons.Count - 1);
            balloon.balloonText.text = balloons[stringPos];
            balloons.RemoveAt(stringPos);
        }   
    }
    public void DisplayResultText(string _balloonText)
    {
        for (int i = 0; i < balloonText.Length; i++)
        {
            if (string.Equals(_balloonText, balloonText[i]))
            {
                textListToCheck.Remove(balloonText[i]);      
            }
            postDescription.text = $" ";
            foreach (string str in textListToCheck)
            {
                postDescription.text += $"{str} ";
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
}
