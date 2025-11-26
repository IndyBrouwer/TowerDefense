using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy, IDamageable
{
    private NavMeshAgent agent;
    private int health;

    private void Update()
    {
        //Check if the enemy has reached its destination
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            OnEnemyReachedGoal();
        }
    }

    public void SetupEnemy(int health, float speed)
    {
        this.health = health;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        //Search for the player base and set it as it's agent destination, (target)
        Transform baseTarget = GameObject.FindGameObjectWithTag("Base").transform;
        agent.SetDestination(baseTarget.position);
    }

    public void TakeDamage(int amount)
    {
        //Flash red damage effect

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void OnEnemyReachedGoal()
    {
        Debug.Log("Enemy reached the base!");

        //Deal damage to the base here

        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
