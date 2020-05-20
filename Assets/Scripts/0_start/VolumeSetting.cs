using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetting : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 1f;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    void Update()
    {
        audioSrc.volume = musicVolume;
    }
    public void SetVolume(float vol)
    {
        musicVolume = vol;
        SaveSliderValue(vol);
    }
    void SaveSliderValue(float vol)
    {
        PlayerPrefs.SetFloat("SliderVolumeLevel", vol);
    }
}
