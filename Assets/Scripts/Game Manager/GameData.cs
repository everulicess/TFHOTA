using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public int[] cupGameResults;
    public string balloonGameResults;

    public Vector3 mainSceneLoadPosition = new Vector3(0.7f, 0.2f, -4.12f);


    private void Awake()
    {
        if(instance != null & instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
