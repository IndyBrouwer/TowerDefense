using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public void ToggleGameSpeed()
    {
        if (Time.timeScale == 1)
        {
            //Speed up the game
            Time.timeScale = 2;
        }
        else
        {
            //Back to normal speed
            Time.timeScale = 1;
        }
    }
}