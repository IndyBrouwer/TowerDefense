using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public float savedSpeedValue = 1f;

    public void ToggleGameSpeed()
    {
        if (Time.timeScale == 1)
        {
            //Speed up the game (twice the normal speed)
            Time.timeScale = 2;
            savedSpeedValue = Time.timeScale;
        }
        else if (Time.timeScale == 2)
        {
            //Speed up the game extra fast, (four times the normal speed)
            Time.timeScale = 3;
            savedSpeedValue = Time.timeScale;
        }
        else
        {
            //Back to normal speed
            Time.timeScale = 1;
            savedSpeedValue = Time.timeScale;
        }
    }
}