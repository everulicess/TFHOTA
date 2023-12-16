using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    private List<string> balloons = new List<string>();
    [SerializeField] GameObject balloonPrefab;

    [Header("Minigame Settings")]
    public string[] balloonText;
    public int balloonsAmount;
    
    [HideInInspector] public int score;

    int row = 1;

    private void Awake()
    {
        PutTextOnTheList();
        ArrangeBalloons(balloonPrefab);
    }
    private void Update()
    {
        //DebugList();
    }

    public void AssignText(Balloon balloon)
    {
        //Debug.Log(balloons.Count);
        if (balloons.Count > 0)
        {
            int stringPos = Random.Range(0, balloons.Count - 1);
            balloon.balloonText.text = balloons[stringPos];
            balloons.RemoveAt(stringPos);
        }
        
    }
    public void ArrangeBalloons(GameObject _balloonPrefab)
    {
        //row = (int)Mathf.Sqrt(balloonsAmount);
        //Debug.Log(row);
        //switch (row)
        //{
        //    case 1:
        //        for (int i = 0; i < balloonsAmount; i++)
        //        {
        //            Instantiate(_balloonPrefab, this.transform.position + new Vector3(0, row, 1 * i), this.transform.rotation, this.transform);

        //        }
        //        ; break;
        //    case 2:
        //        ; break;
        //    case 3:
        //        ; break;
        //    default:
        //        break;
    //}
        for (int i = 0; i<balloonsAmount+2; i++)
        {
            Debug.Log("arranging the balloons");
            if (i<balloonsAmount / 3)
            {
                Instantiate(_balloonPrefab, this.transform.position + new Vector3(0, row, 0.5f * i), this.transform.rotation, this.transform);
            }
            else if (i < 2 * balloonsAmount / 3 && i > balloonsAmount / 3)
{
    Instantiate(_balloonPrefab, this.transform.position + new Vector3(0, row * 1.5f, 0.5f * i - 3), this.transform.rotation, this.transform);
}
else if (i > 2 * balloonsAmount / 3)
{
    Instantiate(_balloonPrefab, this.transform.position + new Vector3(0, row * 2f, 0.5f * i - 3), this.transform.rotation, this.transform);
}

        }

    }
    //private void DebugList()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        foreach (string str in balloons)
    //        {
    //            Debug.Log(str);
    //        }
    //    }
    //}

    private void PutTextOnTheList()
    {
        foreach (string str in balloonText)
        {
            balloons.Add(str);
        }
    }
}
