using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.XR.Interaction.Toolkit;

public class CupGameManager : MonoBehaviour
{
    private List<Cup> cups = new List<Cup>();
    private int selectedCup = -1;
    private bool isSwitching;
    [HideInInspector]
    public bool isPicking = false;
    [SerializeField]
    private ButtonAnimation button;
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject tutorial;

    [Header("Gameplay config")]
    [SerializeField, Range(0.1f, 5f)]
    private float travelTime = 2f;
    [SerializeField, Range(0f, 5f)]
    private float pauseTime;
    [SerializeField, Range(1, 100)]
    private int switchAmount = 1;

    [Header("Animation")]
    [SerializeField, Range(0.1f, 1f)]
    private float raiseHeight;
    [SerializeField, Range(0.1f, 1f)]
    private float raiseTime;
    [SerializeField]
    private float cupDistance = 2f;
    [SerializeField]
    private Transform cupsOrigin;
    [SerializeField]
    private string[] buttonMessages = new string[6];

    [HideInInspector]
    public int score;

    //Logo creation
    public LogoCreator creator;

    public int[] results = new int[] { -1, -1, -1, -1, -1, -1};
    [HideInInspector]
    public int round = 0;

    public static event Action onLogoComplete;

    void Start()
    {
        foreach(Cup cup in cups)
        {
            cup.onSelect += OnBallSelected;
        }
        PositionCups(cupsOrigin.position);
    }

    private void OnBallSelected()
    {
        PreviewSelection();

        
        if(round >= 6)
        {
            StartCoroutine(ShowResults());
            isPicking = false;
            button.SetButtonText("Finished");
            button.GetComponent<XRSimpleInteractable>().enabled = false;
            button.GetComponent<MeshRenderer>().material.color = Color.grey;
            backButton.SetActive(true);
            return;
        }
        button.SetButtonText(buttonMessages[round]);

        isPicking = false;
    }

    public void SetSelection(int val)
    {
        results[round - 1] = val;
        selectedCup = val;
    }

    private void UpdatePreviews()
    {
        foreach (Cup cup in cups)
        {
            cup.DisplayPreview();
        }
    }

    private void PreviewSelection()
    {
        foreach (Cup cup in cups)
        {
            cup.DisplaySelection();
        }
        creator.ClearDisplay();
        creator.CreateLogo(results);
    }

    private void RaiseCups()
    {
        foreach(Cup cup in cups)
        {
            StartCoroutine(RaiseCup(cup));
        }
    }

    private void LowerCups()
    {
        foreach(Cup cup in cups)
        {
            StartCoroutine(LowerCup(cup));
        }
    }

    private IEnumerator ShowResults()
    {
        yield return new WaitForSeconds(2f);

        creator.CreateLogo(results);
        GameData.instance.cupGameResults = results;
        onLogoComplete?.Invoke();

        yield break;
    }

    private IEnumerator RaiseCup(Cup cup)
    {
        Vector3 target = cup.transform.position + (Vector3.up * raiseHeight);
        float startTime = Time.time;
        cup.OpenPreview();

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

        cup.ClosePreview();
        yield break;
    } 

    //Cup Shuffeling Logic
    public void Shuffle()
    {
        tutorial.SetActive(false);

        if(!isSwitching && !isPicking)
        {
            foreach (Cup cup in cups)
            {
                cup.GetComponent<XRGrabInteractable>().enabled = false;
            }
            PositionCups(cupsOrigin.position);
            StartCoroutine(SwitchCupsSequence());
        }
    }

    private IEnumerator SwitchCupsSequence()
    {
        isSwitching = true;

        UpdatePreviews();
        ToggleRigidBodies(true);

        RaiseCups();

        yield return new WaitForSeconds(raiseTime + 2f);

        LowerCups();
       

        round++;

        yield return new WaitForSeconds(raiseTime + 1f);

        for(int i = 0; i < switchAmount; i++)
        {
            SwitchRandomCups();
            yield return new WaitForSeconds(pauseTime + travelTime);
        }

        foreach(Cup cup in cups)
        {
            cup.GetComponent<XRGrabInteractable>().enabled = true;
        }

        ToggleRigidBodies(false);
        isPicking = true;
        isSwitching = false;
        yield break;
    }

    private void ToggleRigidBodies(bool val)
    {
        foreach (Cup cup in cups)
        {
            cup.GetComponent<Rigidbody>().isKinematic = val;
        }
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
            cup.transform.rotation = Quaternion.Euler(180, 0, 0);
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

    public int GetSelectedCup()
    {
        return selectedCup;
    }
}
