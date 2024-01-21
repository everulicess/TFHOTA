using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public TMP_Text text;

    public void SetButtonText(string _text)
    {
        text.text = _text;
    }
}
