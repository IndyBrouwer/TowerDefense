using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("Tower Settings")]
    public float attackRange = 4f;
    public float attackDamage = 20f;
    public float fireRate = 1.5f;
    public float targetLockTime = 2f;

    private float fireCooldown = 0f;
    private float lockTimer = 0f;

    private Enemy currentTarget;

    private void Update()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (lockTimer > 0f)
        {
            lockTimer -= Time.deltaTime;
        }

        //If no current target or target out of range, reset target
        if (currentTarget == null || !IsInRange(currentTarget))
        {
            //No enemy detected or out of range, find a new target
            currentTarget = null;
            lockTimer = 0f;
        }

        // Try to lock onto a new target if none is currently locked
        if (currentTarget == null && lockTimer <= 0f)
        {
            currentTarget = FindClosestEnemy();

            if (currentTarget != null)
            {
                lockTimer = targetLockTime;
            }
        }

        // Attack the current target if possible
        if (currentTarget != null && fireCooldown <= 0f)
        {
            Shoot(currentTarget);
            fireCooldown = fireRate;
        }
    }

    private Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        float closestDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < attackRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private bool IsInRange(Enemy enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position) <= attackRange;
    }

    private void Shoot(Enemy enemy)
    {
        enemy.TakeDamage(attackDamage);
    }
}
