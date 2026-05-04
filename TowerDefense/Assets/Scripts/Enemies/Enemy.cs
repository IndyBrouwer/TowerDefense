using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy, IDamageable
{
    public NavMeshAgent agent;
    public float health;
    private float damage;
    public float speed;
    public float LifeTime;

    [Header("Invincibility")]
    [SerializeField] private float invincibilityDuration = 0.5f;
    private bool isInvincible = false;

    private bool isDead = false;
    public bool canNotDie = false;

    private int slowSources = 0;

    [Header("Damage Flash Effect")]
    private Renderer[] renderers;
    private Color[] originalColors;
    public Color damageColor = Color.red;
    public float flashDuration = 0.1f;

    [Header("Poison")]
    [SerializeField] private float poisonDuration = 5f;
    [SerializeField] private float poisonTickInterval = 1f;
    [SerializeField] private float poisonDamage = 30f;
    private bool isPoisoned = false;
    private float poisonTimer = 0f;

    [Header("Other Scripts")]
    private PlayerHealth playerHealthScript;
    private EnemyManager enemyManagerScript;
    private EnemyData enemyData;
    private Wallet wallet;

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
        LifeTime += Time.deltaTime;

        //Check if the enemy has reached its destination
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            OnEnemyReachedGoal();
        }
    }

    public void SetupEnemy(EnemyData data, EnemyManager enemyManager, Wallet walletScript, float hpMultiplier)
    {
        enemyData = data;
        enemyManagerScript = enemyManager;
        wallet = walletScript;

        health = data.maxHealth * hpMultiplier;
        damage = data.damage;
        speed = data.speed;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        //Search for the player base and set it as it's agent destination, (target)
        Transform baseTarget = GameObject.FindGameObjectWithTag("Base").transform;

        //Get the PlayerHealth script from the base target
        playerHealthScript = baseTarget.GetComponent<PlayerHealth>();

        //Set the agent destination to the base target position
        agent.SetDestination(baseTarget.position);

        //Start the invincibility coroutine to make the enemy invincible for a short duration after spawning
        StartCoroutine(InvincibilityCoroutine());
    }

    public void EnterSlow(float multiplier)
    {
        slowSources++;

        if (slowSources == 1)
        {
            agent.speed = speed * multiplier;
        }
    }

    public void ExitSlow()
    {
        slowSources = Mathf.Max(0, slowSources - 1);

        if (slowSources == 0)
        {
            agent.speed = speed;
        }
    }

    public void ApplyPoison()
    {
        poisonTimer = poisonDuration;

        if (!isPoisoned)
        {
            StartCoroutine(PoisonEffect());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    private IEnumerator PoisonEffect()
    {
        isPoisoned = true;

        while (poisonTimer > 0f)
        {
            TakeDamage(poisonDamage);

            yield return new WaitForSeconds(poisonTickInterval);

            poisonTimer -= poisonTickInterval;
        }

        isPoisoned = false;
    }

    public void TakeDamage(float amount)
    {
        //Invincibility check (is there for 0.5 seconds after spawning)
        if (isInvincible)
        {
            return;
        }

        health -= amount;

        if (renderers != null && renderers.Length > 0)
        {
            StartCoroutine(FlashRed());
        }

        if (health <= 0)
        {
            //Put earn money here to avoid getting money while they explode at base
            wallet.AddCurrency(enemyData.coins);

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
        //Deal damage to the player base
        if (playerHealthScript != null)
        {
            playerHealthScript.TakeDamage(damage);
        }

        Die();
    }

    private void Die()
    {
        if (isDead || canNotDie)
        {
            return;
        }

        isDead = true;

        //Notify the EnemyManager that this enemy has died
        enemyManagerScript.EnemyDied();

        //Destroy this enemy game object
        Destroy(gameObject);
    }
}