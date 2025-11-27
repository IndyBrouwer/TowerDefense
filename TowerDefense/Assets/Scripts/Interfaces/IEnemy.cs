using UnityEngine;

public interface IEnemy
{
    //Setup the enemy with health and speed
    void SetupEnemy(EnemyData data, EnemyManager enemyManagerScript, Wallet wallet);

    //Called when the enemy reaches the goal/base
    void OnEnemyReachedGoal();
}