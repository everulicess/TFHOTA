using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 0.8f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;

}
