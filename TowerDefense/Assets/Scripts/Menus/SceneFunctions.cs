using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunctions : MonoBehaviour
{
    public Animator transitionAnimator;

    public void StartGame()
    {
        StartCoroutine(LoadScene("Game"));
    }

    public void LoadSettingsMenu()
    {
        StartCoroutine(LoadScene("Settings"));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene("Start"));
    }

    public void QuitGame()
    {
        #if UNITY_WEBGL
            //Return to main menu instead of quitting.
            StartCoroutine(LoadScene("Start"));
        #else
            Application.Quit();
        #endif
    }

    private IEnumerator LoadScene(string sceneName)
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
}
