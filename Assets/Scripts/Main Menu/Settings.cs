using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Settings : MonoBehaviour
{
    bool isGameStarted = false;
    [Header("Movement")]
     ActionBasedContinuousMoveProvider continuousMov;
     TeleportationProvider teleport;
    [Tooltip("drag here the object for Right Hand Teleportation Ray")]
    public GameObject RightTeleportRay;
    [Tooltip("drag here the object for Left Hand Teleportation Ray")]
    public GameObject LeftTeleportRay;
    private void Start()
    {
        continuousMov = FindObjectOfType<ActionBasedContinuousMoveProvider>();
        teleport = FindObjectOfType<TeleportationProvider>();
        continuousTurn = FindObjectOfType<ActionBasedContinuousTurnProvider>();
        snapTurn = FindObjectOfType<ActionBasedSnapTurnProvider>();
        if (!isGameStarted)
        {
            SetMoveFromIndex(0);
            SetTurnFromIndex(0);
        }
        isGameStarted = true;
        
    }
    public void SetMoveFromIndex(int index)
    {
        if (index == 0)
        {
            continuousMov.enabled = true;
            TeleportationActivation(false);
        }
        else if (index == 1)
        {
            continuousMov.enabled = false;
            TeleportationActivation(true);
        }
        else if (index == 2)
        {
            continuousMov.enabled = true;
            TeleportationActivation(true);
        }
    }

    private void TeleportationActivation(bool _activate)
    {
        teleport.enabled = _activate;
        LeftTeleportRay.GetComponent<XRRayInteractor>().enabled = (_activate);
        RightTeleportRay.GetComponent<XRRayInteractor>().enabled = (_activate);
    }
    [Header("Turn")]
    ActionBasedContinuousTurnProvider continuousTurn;
    ActionBasedSnapTurnProvider snapTurn;
    
    public void SetTurnFromIndex(int index)
    {
        if (index == 0)
        {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }
        else if (index == 1)
        {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }
    }

}
