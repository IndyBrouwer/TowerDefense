using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip slideSound;

    public void Click()
    {
        AudioManager.Instance.sfxManager.PlaySFX(clickSound);
    }

    public void Hover()
    {
        AudioManager.Instance.sfxManager.PlaySFX(hoverSound);
    }

    public void Slide()
    {
        AudioManager.Instance.sfxManager.PlaySFX(slideSound);
    }
}