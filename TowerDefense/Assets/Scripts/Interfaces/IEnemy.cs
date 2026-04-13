using UnityEngine;

public interface IEnemy
{
    //Setup the enemy with health and speed
    void SetupEnemy(EnemyData data, EnemyManager enemyManagerScript, Wallet wallet, float hpMultiplier);

    //Called when the enemy reaches the goal/base
    void OnEnemyReachedGoal();
}