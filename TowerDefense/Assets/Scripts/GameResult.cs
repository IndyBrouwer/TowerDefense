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

        //Disable being able to damage enemies, stop spawning new enemies and stop all existing enemies from moving
        DisableEnemies();

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

    private void DisableEnemies()
    {
        //Get all enemy scripts in scene
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        //Set canNotDie var to true for all enemies so they can't die
        foreach (Enemy enemy in enemies)
        {
            enemy.canNotDie = true;
            enemy.agent.speed = 0f;
        }
    }
}