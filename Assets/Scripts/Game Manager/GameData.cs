using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public int[] cupGameResults = new int[] { -1, -1, -1, -1, -1, -1};
    public string balloonGameResults;

    public Vector3 mainSceneLoadPosition = new Vector3(-19.68f, 0.1956f, 3.47f);


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
