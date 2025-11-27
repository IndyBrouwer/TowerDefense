using System.Collections;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject defaultEnemyPrefab;
    [SerializeField] private GameObject strongEnemyPrefab;
    [SerializeField] private GameObject tankyEnemyPrefab;

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
        foreach (GameObject enemyPrefab in wave.enemies)
        {
            //Spawn the enemy at the spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            //Interface setup for the enemy
            IEnemy enemy = newEnemy.GetComponent<IEnemy>();
            if (enemy != null)
            {
                //Send health and speed values and assign the enemy manager script
                enemy.SetupEnemy(100, 2f, enemyManagerScript);
            }

            //Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(wave.spawnInterval);
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