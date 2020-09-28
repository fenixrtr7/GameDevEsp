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
        StopAllAudioSource(true);
    }

    public void StopAllAudioSource(bool stopNow = false)
    {
        foreach (KeyValuePair<string, AudioSource> audio in dic_Audio)
        {
            if (stopNow)
                dic_Audio[audio.Key].Stop();
            else
                ModifyVolumeToCap(audio.Key, 0, true);
        }
    }

    public void PlayClipInSource(string audioSource, AudioClip clipToPlay = null, int cap = 1)
    {
        bool playAudio = CheckAudioSource(audioSource);

        if (playAudio == false)
        {
            Debug.LogError("The audio source key (string) does't exist");
            return;
        }
        if (!dic_Audio[audioSource].isPlaying)
            StopAllAudioSource();

        if (clipToPlay != null)
        {
            dic_Audio[audioSource].clip = clipToPlay;
        }

        ModifyVolumeToCap(audioSource, cap);
        
    }

    List<Coroutine> modCoroutines = new List<Coroutine>();
    public void ModifyVolumeToCap(string audioSource, int cap, bool stopAudio = false)
    {
        bool playAudio = CheckAudioSource(audioSource);
        if (!playAudio)
        {
            Debug.LogError("The audio source key (string) does't exist");
            return;
        }
        modCoroutines.Add(StartCoroutine(EModifyVolume(dic_Audio[audioSource], cap, 1, stopAudio)));
    }
    private IEnumerator EModifyVolume(AudioSource audioSource, int cap, float modif = 0.01f, bool stopAudio = false)
    {
        if (cap == 0 || stopAudio)
        {
            audioSource.volume = 1;
            modif = -modif;
        }
        if (cap == 1 && !audioSource.isPlaying)
        {
            if (audioSource.volume > 0)
                audioSource.volume = 0;
            audioSource.Play();
        }
        while ((cap == 0 && audioSource.volume > 0) || (cap == 1 && audioSource.volume < 1))
        {
            audioSource.volume = modif;
            modif += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (cap == 0)
        {
            audioSource.volume = 0;
            audioSource.Stop();
        }
        else
            audioSource.volume = 1f;
        modCoroutines.Remove(modCoroutines[0]);
    }

    public void FadeOutPitch(string sourceToPitch, string sourceToPlay)
    {
        bool startPitch = CheckAudioSource(sourceToPitch);
        if (!startPitch)
        {
            Debug.LogError("The audio source key " + sourceToPitch + " doesn't exist");
            return;
        }
        startPitch = CheckAudioSource(sourceToPlay);
        if (!startPitch)
        {
            Debug.LogError("The audio source key " + sourceToPlay + " doesn't exist");
            return;
        }
        StartCoroutine(EFadeOutPitch(dic_Audio[sourceToPitch], dic_Audio[sourceToPlay]));
    }

    private IEnumerator EFadeOutPitch(AudioSource audioSource, AudioSource continualSource = null, AudioClip clip = null)
    {
        bool stop = false;
        float modif = 1;
        while (!stop)
        {
            modif -= Time.deltaTime;
            audioSource.pitch = modif;
            yield return new WaitForEndOfFrame();
            if (audioSource.pitch <= 0)
                stop = true;
        }
        audioSource.Stop();
        audioSource.pitch = 1;
        if (continualSource != null)
        {
            if (clip != null)
                continualSource.clip = clip;
            ModifyVolumeToCap(continualSource.gameObject.name, 1);
        }
    }

    public bool CheckAudioSource(string audioSource)
    {
        bool check = false;
        foreach (KeyValuePair<string, AudioSource> audio in dic_Audio)
        {
            if (audioSource.CompareTo(audio.Key) == 0)
            {
                check = true;
                break;
            }
        }
        return check;
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
