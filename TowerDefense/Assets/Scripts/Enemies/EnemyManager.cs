using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    [SerializeField] private TextMeshProUGUI enemiesAliveCounter;
    [SerializeField] private EnemySpawning enemySpawningScript;

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
            //All enemies are dead, notify the spawner to start the next wave
            enemySpawningScript.StartCoroutine(enemySpawningScript.WaveDefeated());
        }
    }

    public void UpdateCounterUI()
    {
        enemiesAliveCounter.text = "Enemies left: " + enemiesAlive;
    }
}
