using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip bellSound;

    [SerializeField]
    private AudioClip[] accentSounds;

    [SerializeField]
    private float accentSoundTimeoutMin = 10.0f;
    
    [SerializeField]
    private float accentSoundTimeoutMax = 40.0f;
    
    [SerializeField]
    private GameObject AccentAudioSourcePrefab;

    private float accentSoundTimeout;

    [SerializeField]
    private AudioSource MusicAudioSource;

    public AudioSource defaultAudioSource;
    private float lastVolume = 0.0f;

    public void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(this);
        }
        
        if (MusicAudioSource is null)
        {
            Debug.LogError($"{nameof(MusicAudioSource)} is null");
            DestroyImmediate(this);
        }

        defaultAudioSource = this.GetComponent<AudioSource>();

        if (defaultAudioSource is null)
        {
            Debug.LogError($"{nameof(defaultAudioSource)} is null");
            DestroyImmediate(this);
        }

        accentSoundTimeout = accentSoundTimeoutMin;
    }

    public void Start()
    {
        PlayMusic();
    }

    public void FixedUpdate()
    {
        accentSoundTimeout -= Time.fixedDeltaTime;

        if (accentSoundTimeout < 0)
        {
            accentSoundTimeout = Random.Range(accentSoundTimeoutMin, accentSoundTimeoutMax);
            PlayAccent();
        }

        if (lastVolume != defaultAudioSource.volume)
        {
            SetAllAudioSourcesVolumes();
        }

        lastVolume = defaultAudioSource.volume;
    }

    private void SetAllAudioSourcesVolumes()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        
        foreach(AudioSource source in sources)
        {
            source.volume = defaultAudioSource.volume;
        }
    }

    public void PlayMusic()
    {
        MusicAudioSource.Play();
    }

    public void StopMusic()
    {
        MusicAudioSource.Stop();
    }

    public void PlayAccent()
    {
        AudioClip source = accentSounds[Random.Range(0, accentSounds.Length)];
        PlaySound(source);
    }

    public void RingBell()
    {
        PlaySound(bellSound);
    }

    private void PlaySound(AudioClip source)
    {
        if (defaultAudioSource.mute || defaultAudioSource.volume <= 0.0f)
        {
            return;
        }

        GameObject AccentAudioSource = Instantiate(AccentAudioSourcePrefab, this.transform);
        AccentAudioSource.transform.localPosition = Vector3.zero;
        AudioSource accentSource = AccentAudioSource.GetComponent<AudioSource>();
        accentSource.clip = source;
        accentSource.volume = defaultAudioSource.volume;
        accentSource.Play();

        Destroy(AccentAudioSource, source.length + 0.5f);
    }

    
}