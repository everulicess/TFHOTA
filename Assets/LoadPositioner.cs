using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPositioner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Start()
    {
        player.position = GameData.instance.mainSceneLoadPosition;
    }
}
