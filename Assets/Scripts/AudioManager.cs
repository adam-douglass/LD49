using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip Music;
    private AudioSource audioSource;

    public AudioSource SourceAudio
    {
        get => audioSource;
    }

    public void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        SourceAudio.clip = Music;
        SourceAudio.Play();
    }

    public void StopMusic()
    {
        SourceAudio.Stop();
    }
}