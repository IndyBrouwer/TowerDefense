using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    [SerializeField] private GameObject GameResultPanel;
    [SerializeField] private TextMeshProUGUI GameResultText;

    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;

    public float DelayMenuLoad = 7f;

    public void ShowVictory()
    {
        Debug.Log("VICTORY!");

        CanvasGroup resultCanvas = GameResultPanel.GetComponent<CanvasGroup>();
        resultCanvas.alpha = 1f;

        GameResultText.text = "VICTORY!";
        AudioManager.Instance.sfxManager.PlaySFX(victorySound);

        //Start delay of loading main menu
        StartCoroutine(LoadMenuScene());
    }

    public void ShowDefeat()
    {
        Debug.Log("DEFEAT!");

        CanvasGroup resultCanvas = GameResultPanel.GetComponent<CanvasGroup>();
        resultCanvas.alpha = 1f;

        GameResultText.text = "DEFEAT!";
        AudioManager.Instance.sfxManager.PlaySFX(defeatSound);

        //Start delay of loading main menu
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        //Wait for delayed time
        yield return new WaitForSeconds(DelayMenuLoad);

        //Go back to main menu
        SceneManager.LoadScene("Start");
    }
}