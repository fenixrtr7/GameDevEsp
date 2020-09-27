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
public class AudioManager : Manager<AudioManager>
{
    public Sound[] sounds;
    public static AudioManager instance;
    public Dictionary<string, AudioSource> dic_Audio;
    GameObject[] audioListObjects;

    void Start()
    {
        InitializeManager();
    }

    public void InitializeManager()
    {
        AvoidDuplicate();
        DontDestroyOnLoad(gameObject);
        audioListObjects = GameObject.FindGameObjectsWithTag("Audio");//new List<AudioSource>();
        dic_Audio = new Dictionary<string, AudioSource>();
        for (int i = 0; i < audioListObjects.Length; i++)
        {
            string name = audioListObjects[i].name;
            if (dic_Audio.ContainsKey(name))
                name = name + "Secundary";
            if (dic_Audio.ContainsKey(name))
                continue;
            dic_Audio.Add(name, audioListObjects[i].GetComponent<AudioSource>());
        }
        //foreach (Sound snd in sounds)
        //{
        //    snd.source = gameObject.AddComponent<AudioSource>();
        //    snd.source.clip = snd.clip;*/
        //}
        StopAllAudioSource();
    }

    public void StopAllAudioSource()
    {
        foreach (KeyValuePair<string, AudioSource> audio in dic_Audio)
        {
            dic_Audio[audio.Key].Stop();
        }
    }

    public void PlayClipInSource(string audioSource, AudioClip clipToPlay = null, float volume = 1)
    {
        bool playAudio = false;

        foreach (KeyValuePair<string, AudioSource> audio in dic_Audio)
        {
            if (audioSource.CompareTo(audio.Key) == 0)
            {
                playAudio = true;
            }
        }

        if (playAudio == false)
        {
            Debug.LogError("The audio source key (string) does't exist");
            return;
        }
        StopAllAudioSource();
        
        if (clipToPlay != null)
        {
            dic_Audio[audioSource].clip = clipToPlay;
        }

        dic_Audio[audioSource].volume = volume;
        dic_Audio[audioSource].Play();
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
