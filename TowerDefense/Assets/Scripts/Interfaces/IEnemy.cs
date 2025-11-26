using UnityEngine;

public interface IEnemy
{
    //Setup the enemy with health and speed
    void SetupEnemy(int health, float speed);

    //Called when the enemy reaches the goal/base
    void OnEnemyReachedGoal();
}
