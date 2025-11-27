using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy, IDamageable
{
    private NavMeshAgent agent;
    private float health;
    private float damage;

    [Header("Damage Flash Effect")]
    private Renderer[] renderers;
    private Color[] originalColors;
    public Color damageColor = Color.red;
    public float flashDuration = 0.1f;

    private PlayerHealth playerHealthScript;

    private EnemyManager enemyManagerScript;
    private EnemyData enemyData;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalColors = new Color[renderers.Length];
        for (int index = 0; index < renderers.Length; index++)
        {
            originalColors[index] = renderers[index].material.color;
        }
    }

    private void Update()
    {
        //Check if the enemy has reached its destination
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            OnEnemyReachedGoal();
        }
    }

    public void SetupEnemy(EnemyData data, EnemyManager enemyManager)
    {
        enemyData = data;
        enemyManagerScript = enemyManager;

        health = data.maxHealth;
        damage = data.damage;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;

        //Search for the player base and set it as it's agent destination, (target)
        Transform baseTarget = GameObject.FindGameObjectWithTag("Base").transform;

        //Get the PlayerHealth script from the base target
        playerHealthScript = baseTarget.GetComponent<PlayerHealth>();

        //Set the agent destination to the base target position
        agent.SetDestination(baseTarget.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (renderers != null && renderers.Length > 0)
        {
            StartCoroutine(FlashRed());
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        //Change the color of all renderers to damageColor
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = damageColor;
        }

        yield return new WaitForSeconds(flashDuration);

        //Revert the color of all renderers to their original colors
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = originalColors[index];
        }
    }


    public void OnEnemyReachedGoal()
    {
        //Deal damage to the base here

        if (playerHealthScript != null)
        {
            playerHealthScript.TakeDamage(damage);
        }

        Die();
    }

    private void Die()
    {
        //Notify the EnemyManager that this enemy has died
        enemyManagerScript.EnemyDied();

        //Destroy this enemy game object
        Destroy(gameObject);
    }
}