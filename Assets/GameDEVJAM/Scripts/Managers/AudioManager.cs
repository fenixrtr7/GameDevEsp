using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
Para mandar llamar un sonido es necesario que el objeto que contiene el script sea "AudioManager", se utilizara el siguiente código
FindObjectOfType<AudioManager>().Play("XXXXXXXXX");
FindObjectOfType<AudioManager>().Pause("XXXXXXXXX");
Donde XXXX equivale al nombre del audio para reproducir o eliminar
*/
public class AudioManager : MonoBehaviour
{
    //public Sound[] sounds;
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        AvoidDuplicate();
        DontDestroyOnLoad(gameObject);

        foreach (Sound snd in sounds)
        {
            snd.source = gameObject.AddComponent<AudioSource>();
            snd.source.clip = snd.clip;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        else 
        {
            s.source.Play();
        }
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        else 
        {
            s.source.Pause();
        }
    }

    public void AvoidDuplicate()
    {
        //Revisa que el objeto no se duplique
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
