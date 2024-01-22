using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    float mainVolume;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private void Update()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = mainVolume;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void VolumeControl(float vol)
    {
            mainVolume = vol;
    }

    //public void ToggleSound(bool check)
    //{
    //    if (!check)
    //    {
    //        mainVolume = 0;
    //    }
    //}

    //public void ToggleMusic(bool check)
    //{
    //    foreach (Sound s in sounds)
    //    {
    //        if (!check)
    //        {
    //            if (!s.loop)
    //            {
    //                s.volume = 0;
    //            }
    //        }

    //        else
    //        {
    //            s.volume = mainVolume;
    //        }
    //    }
    //}
    // FindObjectOfType<AudioManager>().Play(""); use this in other scripts
}
