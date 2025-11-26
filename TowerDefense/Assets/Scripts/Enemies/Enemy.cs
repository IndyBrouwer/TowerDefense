using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy
{
    private NavMeshAgent agent;
    private int health;

    public void SetupEnemy(int health, float speed)
    {
        this.health = health;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        //Search for the player base and set it as it's agent destination, (target)
        Transform baseTarget = GameObject.FindGameObjectWithTag("Base").transform;
        agent.SetDestination(baseTarget.position);
    }

    public void OnEnemyReachedGoal()
    {
        Debug.Log("Enemy reached the base!");

        //Deal damage to the base here

        Destroy(gameObject);
    }
}
