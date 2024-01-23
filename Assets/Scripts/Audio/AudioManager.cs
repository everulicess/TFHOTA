using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public Slider volumeSlider;

    public Toggle mainAudioToggle;
    public Toggle musicToggle;

    float mainVolume = 0.5f;
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

    void Start()
    {
        Play("BackgroundMusic");
    }

    private void Update()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = mainVolume;
        }

        if (!mainAudioToggle.isOn)
        {
            mainVolume = 0f;
        }
        else
        {
            mainVolume = volumeSlider.value;
        }

        if (musicToggle != null)
        {
            foreach (Sound s in sounds)
            {
                if (s.loop)
                {
                    s.source.mute = !musicToggle.isOn;
                }
            }
        }

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

    public void VolumeControl()
    {
            mainVolume = volumeSlider.value;
    }
}
