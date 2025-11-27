using System.Collections;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint;

    [Header("Wave references")]
    [SerializeField] private Wave[] enemyWave;
    private int currentWaveIndex = 0;

    [SerializeField] private GameResult gameResultScript;
    [SerializeField] private EnemyManager enemyManagerScript;

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
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
                    enemyScript.SetupEnemy(enemyType.enemyData, enemyManagerScript);
                }

                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }

    public IEnumerator WaveDefeated()
    {
        Debug.Log("Wave Defeated! Starting next wave in 10 seconds");

        yield return new WaitForSeconds(10f);

        //Rest period passed, start the next wave
        StartNextWave();
    }
}