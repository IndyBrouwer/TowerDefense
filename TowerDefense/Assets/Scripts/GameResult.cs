using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    [SerializeField] private GameObject GameResultPanel;
    [SerializeField] private TextMeshProUGUI GameResultText;

    public float DelayMenuLoad = 7f;

    public void ShowVictory()
    {
        Debug.Log("VICTORY!");
        GameResultPanel.SetActive(true);
        GameResultText.text = "VICTORY!";

        //Start delay of loading main menu
        StartCoroutine(LoadMenuScene());
    }

    public void ShowDefeat()
    {
        Debug.Log("DEFEAT!");
        GameResultPanel.SetActive(true);
        GameResultText.text = "DEFEAT!";

        //Start delay of loading main menu
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        //Wait for delayed time
        yield return new WaitForSeconds(DelayMenuLoad);

        //Go back to main menu
        SceneManager.LoadScene("Main");
    }
}