using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Settings : MonoBehaviour
{
    [Header("Movement")]
    public ActionBasedContinuousMoveProvider continuousMov;
    public TeleportationProvider teleport;
    public void SetMoveFromIndex(int index)
    {
        if (index == 0)
        {
            continuousMov.enabled = true;
            teleport.enabled = false;
        }
        else if (index == 1)
        {
            continuousMov.enabled = false;
            teleport.enabled = true;
        }
        else if (index == 2)
        {
            continuousMov.enabled = true;
            teleport.enabled = true;
        }
    }
    [Header("Turn")]
    public ActionBasedContinuousTurnProvider continuousTurn;
    public ActionBasedSnapTurnProvider snapTurn;
    
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
