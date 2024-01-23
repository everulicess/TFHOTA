using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateButton : MonoBehaviour
{
    [SerializeField] Button button;
    private void Start()
    {
        //ActivateThisButton(true);
    }
    public void ActivateThisButton(bool isactive)
    {
        button.interactable = isactive;
    }
}
