using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] private MenuController menuController;

    private void Start()
    {
        SetMusiCVolume(PlayerPrefs.GetFloat("SavedMusicVolume", 100));
        SetSfxVolume(PlayerPrefs.GetFloat("SavedSFXVolume", 100));
    }

    //Music
    public void SetMusiCVolume(float value)
    {
        if (value < 1) {
            value = 0.001f;
        }
        RefreshMusicSlider(value);
        PlayerPrefs.SetFloat("SavedMusicVolume", value);
        masterMixer.SetFloat("Music", Mathf.Log10(value / 100) * 20f);
    }
    
    public void RefreshMusicSlider(float value)
    {
        menuController.SetMusicSliderValue(value);
    }
    
    //SFX
    public void SetSfxVolume(float value)
    {
        if (value < 1) {
            value = 0.001f;
        }
        RefreshSfxSlider(value);
        PlayerPrefs.SetFloat("SavedSFXVolume", value);
        masterMixer.SetFloat("SFX", Mathf.Log10(value / 100) * 20f);
    }
    
    public void RefreshSfxSlider(float value)
    {
        menuController.SetSfxSliderValue(value);
    }
}
