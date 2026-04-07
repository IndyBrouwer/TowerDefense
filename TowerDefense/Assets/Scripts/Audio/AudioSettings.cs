using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Master Volume")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private AudioMixer masterMixer;

    [Header("Music Volume")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer musicMixer;

    [Header("SFX Volume")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer sfxMixer;

    void Start()
    {
        //Load saved values, or use default if they dont exist
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);

        //Apply saved (or default) settings to sliders
        masterSlider.value = savedMasterVolume;

        //Apply created settings to the audio mixer
        ApplyMasterVolume(savedMasterVolume);

        if (musicSlider != null && sfxSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            sfxSlider.value = savedSFXVolume;

            //Apply created settings to the audio mixer
            ApplyMusicVolume(savedMusicVolume);
            ApplySFXVolume(savedSFXVolume);
        }        
    }

    public void ApplyMasterVolume(float value)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat("MasterVolume", value);
            PlayerPrefs.SetFloat("MasterVolume", value);
        }
    }

    public void ApplyMusicVolume(float value)
    {
        if (musicMixer != null)
        {
            musicMixer.SetFloat("MusicVolume", value);
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    public void ApplySFXVolume(float value)
    {
        if (sfxMixer != null)
        {
            sfxMixer.SetFloat("SFXVolume", value);
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }
}
