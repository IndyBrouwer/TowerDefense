using UnityEngine;

public class WavePhase : MonoBehaviour, IGameState
{
    [Header("UI")]
    [SerializeField] private GameObject speedUpButton;

    [Header("Scripts")]
    [SerializeField] private EnemySpawning enemySpawningScript;

    public void Enter()
    {
        //Enable speed up button
        speedUpButton.SetActive(true);

        //Start next wave
        enemySpawningScript.StartNextWave();
    }

    public void Exit()
    {
        //Disable speed up button
        speedUpButton.SetActive(false);
    }
}