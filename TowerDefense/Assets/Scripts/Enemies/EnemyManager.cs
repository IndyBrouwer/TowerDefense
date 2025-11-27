using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    [SerializeField] private TextMeshProUGUI enemiesAliveCounter;
    [SerializeField] private EnemySpawning enemySpawningScript;
    [SerializeField] private WaveState waveStateScript;

    public void SetEnemies(int enemyCount)
    {
        enemiesAlive = enemyCount;
        UpdateCounterUI();
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        UpdateCounterUI();

        if (enemiesAlive <= 0)
        {
            //Show wave state text UI
            waveStateScript.ShowWaveState("Wave Defeated!");

            //All enemies are dead, notify the spawner to start the next wave
            enemySpawningScript.StartCoroutine(enemySpawningScript.WaveDefeated());
        }
    }

    public void UpdateCounterUI()
    {
        enemiesAliveCounter.text = "Enemies left: " + enemiesAlive;
    }
}