using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class CupGameManager : MonoBehaviour
{
    private List<Cup> cups = new List<Cup>();
    private Cup selectedCup;
    private bool isSwitching;
    private int timesPlayed = 0;

    [Header("Gameplay config")]
    [SerializeField, Range(0.1f, 5f)]
    private float travelTime = 2f;
    [SerializeField, Range(0f, 5f)]
    private float pauseTime;
    [SerializeField, Range(1, 100)]
    private int switchAmount = 1;

    [Header("Animation")]
    [SerializeField, Range(1f, 5f)]
    private float raiseHeight;
    [SerializeField, Range(0.1f, 1f)]
    private float raiseTime;
    [SerializeField]
    private float ballScale = 0.2f;
    [SerializeField]
    private float cupDistance = 2f;
    [SerializeField]
    private Transform cupsOrigin;

    public static event Action onScoreUpdate;
    [HideInInspector]
    public int score;

    void Start()
    {
        foreach(Cup cup in cups)
        {
            cup.onSelect += OnBallSelected;
            cup.onCorrectGuess += IncreaseScore;
        }
        PositionCups(cupsOrigin.position);
    }

    //Ball
    private void PlaceBall()
    {
        selectedCup = cups[UnityEngine.Random.Range(0, cups.Count)];

        foreach(Cup cup in cups)
        {
            cup.ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cup.ball.transform.position = cup.transform.position - new Vector3(0, cup.transform.position.y - ballScale/2, 0);
            cup.ball.transform.localScale = new Vector3(ballScale, ballScale, ballScale);

            if(cup == selectedCup)
            {
                cup.SetBallColor(Color.green);
            }

            StartCoroutine(RaiseCup(cup));
        }
    }

    private void OnBallSelected()
    {
        foreach(Cup cup in cups)
        {
            cup.HideSelectionButton();
        }

        ShowBalls();
    }

    private void IncreaseScore()
    {
        score++;
        onScoreUpdate?.Invoke();
    }

    private void ShowBalls()
    {
        foreach(Cup cup in cups)
        {
            cup.ball.transform.position = cup.transform.position - new Vector3(0, cup.transform.position.y - ballScale/2, 0);
            StartCoroutine(RaiseCup(cup));
            isSwitching = false;
        }
    }

    private void LowerCups()
    {
        foreach(Cup cup in cups)
        {
            StartCoroutine(LowerCup(cup));
        }
    }

    private IEnumerator RaiseCup(Cup cup)
    {
        Vector3 target = cup.transform.position + (Vector3.up * raiseHeight);
        float startTime = Time.time;
        cup.ball.SetActive(true);

        while(Time.time - startTime < raiseTime)
        {
            float step = 1 / raiseTime * Time.deltaTime;
            cup.transform.position = Vector3.Lerp(cup.transform.position, target, step);
            yield return null;
        }

        yield break;
    }

    private IEnumerator LowerCup(Cup cup)
    {
        Vector3 target = cup.transform.position - (Vector3.up * raiseHeight);
        float startTime = Time.time;

        while(Time.time - startTime < raiseTime)
        {
            float step = 1 / raiseTime * Time.deltaTime;
            cup.transform.position = Vector3.Lerp(cup.transform.position, target, step);
            yield return null;
        }

        cup.ball.SetActive(false);
        yield break;
    }

    //Cup Shuffeling Logic
    public void Shuffle()
    {
        if(!isSwitching)
        {
            StartCoroutine(SwitchCupsSequence());
        }
    }

    private IEnumerator SwitchCupsSequence()
    {
        isSwitching = true;


        if(timesPlayed == 0)
        {
            PlaceBall();

            yield return new WaitForSeconds(raiseTime + 2f);
        }

        LowerCups();

        yield return new WaitForSeconds(raiseTime + 1f);

        for(int i = 0; i < switchAmount; i++)
        {
            SwitchRandomCups();
            yield return new WaitForSeconds(pauseTime + travelTime);
        }

        foreach(Cup cup in cups)
        {
            cup.OpenSelectionButton();
        }
        timesPlayed++;
        yield break;
    }

    public void SwitchRandomCups()
    {
        int randCup1 = UnityEngine.Random.Range(0, cups.Count);
        int randCup2 = RandomNumberInRangeException(randCup1, 0, cups.Count);
        StartCoroutine(MoveCups(cups[randCup1].transform, cups[randCup2].transform));
    }

    private void PositionCups(Vector3 origin)
    {
        Vector3 pos = origin - new Vector3(cupDistance, 0, 0) * (cups.Count - 1) /2;
        foreach(Cup cup in cups)
        {
            cup.transform.position = pos;
            pos += new Vector3(cupDistance, 0, 0);
        }
    }

    private IEnumerator MoveCups(Transform cup1, Transform cup2)
    {
        Vector3 center = (cup1.position + cup2.position) / 2;
        Vector3 target1 = cup2.position;
        Vector3 target2 = cup1.position;

        float startTime = Time.time;

        while(Time.time - startTime < travelTime)
        {
            float fracComplete = (Time.time - startTime) / travelTime;
            cup1.RotateAround(center, Vector3.up, 180 / travelTime * Time.deltaTime);
            cup2.RotateAround(center, Vector3.up, 180 / travelTime * Time.deltaTime);
            yield return null;
        }
        cup1.position = target1;
        cup2.position = target2;
        yield break;
    }

    //Helper functions
    public void SubscribeCup(Cup cup)
    {
        cups.Add(cup);
    }

    private int RandomNumberInRangeException(int exception, int min, int max)
    {
        var selection = UnityEngine.Random.Range(min, max);

        if(selection != exception)
        {
            return selection;
        }
        
        return RandomNumberInRangeException(exception, min, max);
    }

    public Cup GetSelectedCup()
    {
        return selectedCup;
    }
}
