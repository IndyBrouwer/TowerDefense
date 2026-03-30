using System.Collections;
using TMPro;
using UnityEngine;

public class WaveState : MonoBehaviour
{
    [SerializeField] private CanvasGroup waveStateCanvas;
    [SerializeField] private TextMeshProUGUI waveStateText;

    [SerializeField] private GameStateManager gameStateManagerScript;

    public void ShowWaveState(string message)
    {
        waveStateText.text = message;
        waveStateCanvas.alpha = 1;

        StartCoroutine(HideCooldown());
    }

    public void HideWaveState()
    {
        waveStateCanvas.alpha = 0;
    }

    private IEnumerator HideCooldown()
    {
        yield return new WaitForSeconds(3.5f);

        HideWaveState();

        gameStateManagerScript.StartBuildPhase();
    }
}