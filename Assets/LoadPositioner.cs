using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPositioner : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private LogoCreator pcCreator;

    void Start()
    {
        player.position = GameData.instance.mainSceneLoadPosition;
        pcCreator.CreateLogo(GameData.instance.cupGameResults);
    }
}
