using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private AudioMixer masterMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Load saved values, or use default if they dont exist
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);

        //Apply saved (or default) settings to sliders
        masterSlider.value = savedMasterVolume;

        //Apply created settings to the audio mixer
        ApplyMasterVolume(savedMasterVolume);
    }

    public void ApplyMasterVolume(float value)
    {
        if (masterMixer != null)
        {
            //Convert slider value to volume the audiomixer will recognize
            masterMixer.SetFloat("MasterVolume", value);
            PlayerPrefs.SetFloat("MasterVolume", value);
        }
    }
}
