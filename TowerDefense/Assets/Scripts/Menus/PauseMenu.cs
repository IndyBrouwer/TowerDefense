using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private GameObject TipBoxUI;

    [SerializeField] private GameStateManager gameStateManagerScript;

    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            //Resume the game
            Time.timeScale = 1;

            //Show the pause button and hide the pause menu
            pauseButton.SetActive(true);
            pauseMenuUI.SetActive(false);

            if (gameStateManagerScript.GetGameState() is BuildPhase)
            {
                //Turn on tips UI
                TipBoxUI.SetActive(true);
            }
        }
        else
        {
            //Pause the game
            Time.timeScale = 0;

            //Hide the pause button and show the pause menu
            pauseButton.SetActive(false);
            pauseMenuUI.SetActive(true);

            //Turn off Tips UI
            TipBoxUI.SetActive(false);
        }
    }
}