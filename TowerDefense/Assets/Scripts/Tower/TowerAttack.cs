using UnityEngine;

public class TowerAttack : MonoBehaviour, ITower
{
    [SerializeField] private TowerData currentTower;
    [SerializeField] private GameObject towerTurret;
    [SerializeField] private float turretRotationSpeed = 5f;

    [Header("Tower Stats")]
    private float attackRange;
    private float attackDamage;
    private float fireCooldown;

    public float targetLockTime = 2f;
    private float fireTimer = 0f;
    private float lockTimer = 0f;

    [Header("Tower Effect Bools")]
    private bool canPoison;
    private bool isBoosted;

    private Enemy currentTarget;

    private void Start()
    {
        attackDamage = currentTower.Power;
        attackRange = currentTower.Range;
        fireCooldown = currentTower.FireCooldown;
    }


    private void Update()
    {
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
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
        if (currentTarget != null)
        {
            RotateTurretTowards(currentTarget);

            if (fireTimer <= 0f)
            {
                Shoot(currentTarget);
                fireTimer = fireCooldown;
            }
        }
    }

    private Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        //Use Infinity so there's always something to compare against (for deciding closest enemy)
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

    private void RotateTurretTowards(Enemy target)
    {
        Vector3 direction = target.transform.position - towerTurret.transform.position;

        //Ignore vertical difference
        direction.y = 0f;

        if (direction == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //Smooth rotation (optional)
        towerTurret.transform.rotation = Quaternion.Lerp(
            towerTurret.transform.rotation,
            targetRotation,
            Time.deltaTime * turretRotationSpeed
        );
    }

    private bool IsInRange(Enemy enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position) <= attackRange;
    }

    public void SetDamageBoost(bool value)
    {
        isBoosted = value;
    }

    public void SetPoisonEnabled(bool value)
    {
        canPoison = value;
    }

    private void Shoot(Enemy enemy)
    {
        float finalDamage = attackDamage;

        if (isBoosted)
        {
            finalDamage *= 1.5f;
        }

        enemy.TakeDamage(finalDamage);

        if (canPoison)
        {
            enemy.ApplyPoison();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}