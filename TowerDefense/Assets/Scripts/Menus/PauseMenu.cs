using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenuUI;

    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            //Resume the game
            Time.timeScale = 1;

            //Show the pause button and hide the pause menu
            pauseButton.SetActive(true);
            pauseMenuUI.SetActive(false);
        }
        else
        {
            //Pause the game
            Time.timeScale = 0;

            //Hide the pause button and show the pause menu
            pauseButton.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
    }
}

//fast forward timeScale = 2;