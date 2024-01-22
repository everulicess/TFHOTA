using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsDisplay : MonoBehaviour
{
    [SerializeField]
    private LogoCreator logo;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private GameObject display;

    void Start()
    {

        if (GameData.instance.balloonGameResults != string.Empty && GameData.instance.cupGameResults.Length > 0)
        {
            display.SetActive(true);
            logo.CreateLogo(GameData.instance.cupGameResults);
            text.text = GameData.instance.balloonGameResults;
        }

    }
}
