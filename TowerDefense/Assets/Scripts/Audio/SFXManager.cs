using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio References")]
    public AudioSource[] sources;
    public AudioSource selectedSFXSource;
    private int nextIndex = 0;

    public void PlaySFX(AudioClip soundEffect)
    {
        //Get one of the audio sources for SFX
        selectedSFXSource = sources[nextIndex];

        //Assign clip to audio source
        selectedSFXSource.clip = soundEffect;

        if (!selectedSFXSource.isPlaying)
        {
            //Play audio
            selectedSFXSource.Play();
        }

        //Move on to next audio source
        nextIndex++;

        if (nextIndex >= sources.Length)
        {
            nextIndex = 0;
        }
    }
}
