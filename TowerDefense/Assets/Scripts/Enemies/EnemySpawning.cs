using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint;

    [Header("Wave references")]
    [SerializeField] private Wave[] enemyWave;
    private int currentWaveIndex = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveCounterText;

    [Header("Other Scripts")]
    [SerializeField] private GameResult gameResultScript;
    [SerializeField] private EnemyManager enemyManagerScript;
    [SerializeField] private Wallet walletScript;

    public void StartNextWave()
    {
        waveCounterText.text = "Wave: " + (currentWaveIndex + 1) + "/" + enemyWave.Length;

        if (currentWaveIndex < enemyWave.Length)
        {
            StartCoroutine(SpawnWave(enemyWave[currentWaveIndex]));
            currentWaveIndex++;
        }
        else
        {
            //All waves completed, show victory and go to main menu
            gameResultScript.ShowVictory();
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        int totalEnemies = 0;

        //Decide total amount of enemies in this wave
        foreach (int count in wave.enemyCounts)
        {
            totalEnemies += count;
        }

        //Inform the enemy manager about the total enemies in this wave
        enemyManagerScript.SetEnemies(totalEnemies);

        for (int enemyTypeIndex = 0; enemyTypeIndex < wave.enemyTypes.Length; enemyTypeIndex++)
        {
            EnemyData enemyType = wave.enemyTypes[enemyTypeIndex];
            int count = wave.enemyCounts[enemyTypeIndex];

            for (int spawnIndex = 0; spawnIndex < count; spawnIndex++)
            {
                GameObject newEnemy = Instantiate(enemyType.enemyPrefab, spawnPoint.position, Quaternion.identity);

                Enemy enemyScript = newEnemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.SetupEnemy(enemyType.enemyData, enemyManagerScript, walletScript);
                }

                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }
}