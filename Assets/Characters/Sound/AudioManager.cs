using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    public float effectsVolume = 1.0f;
    public float musicVolume = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name && sound.isMusic);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        s.source.Play();
    }

    public void PlaySoundEffect(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name && !sound.isMusic);
        if (s == null)
        {
            Debug.LogWarning("Sound effect: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume * effectsVolume;
        s.source.Play();
    }
}
