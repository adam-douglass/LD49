using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private AudioManager audioManager;
    public Slider audioSlider;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioSlider.value = audioManager.defaultAudioSource.volume;
    }

    public void UpdateAudio()
    {
        audioManager.defaultAudioSource.volume = audioSlider.value;
    }
}
