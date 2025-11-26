using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunctions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        #if UNITY_WEBGL
                        // For WebGL builds, just show a message or redirect to a main menu.
                        Debug.Log("Quit not supported in WebGL. Returning to main menu...");
                        // Example: return to main menu instead of quitting.
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        #else
                Application.Quit();
        #endif
    }
}
