using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public int[] cupGameResults;

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
