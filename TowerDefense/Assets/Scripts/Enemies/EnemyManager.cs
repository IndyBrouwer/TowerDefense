using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI enemiesAliveCounter;

    [Header("Other Scripts")]
    [SerializeField] private EnemySpawning enemySpawningScript;
    [SerializeField] private WaveState waveStateScript;
    [SerializeField] private GameResult gameResultScript;


    public void SetEnemies(int enemyCount)
    {
        enemiesAlive = enemyCount;
        UpdateCounterUI();
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        UpdateCounterUI();

        if (enemiesAlive <= 0 && enemySpawningScript.AreAllWavesCompleted() == false)
        {
            //Show wave state text UI
            waveStateScript.ShowWaveState("Wave Completed!");
        }
        else if (enemiesAlive <= 0 && enemySpawningScript.AreAllWavesCompleted() == true)
        {
            //All waves completed, show victory and go to main menu
            gameResultScript.ShowVictory();
        }
    }

    public void UpdateCounterUI()
    {
        enemiesAliveCounter.text = "Enemies left: " + enemiesAlive;
    }
}