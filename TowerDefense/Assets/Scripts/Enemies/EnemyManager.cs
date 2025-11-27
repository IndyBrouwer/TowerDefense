using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    [SerializeField] private EnemySpawning enemySpawningScript;

    public void EnemySpawned()
    {
        enemiesAlive++;
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            //All enemies are dead, notify the spawner to start the next wave
            enemySpawningScript.StartCoroutine(enemySpawningScript.WaveDefeated());
        }
    }
}
