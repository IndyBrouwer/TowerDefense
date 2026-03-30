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
            waveStateScript.ShowWaveState("Wave Completed!");
        }
    }

    public void UpdateCounterUI()
    {
        enemiesAliveCounter.text = "Enemies left: " + enemiesAlive;
    }
}