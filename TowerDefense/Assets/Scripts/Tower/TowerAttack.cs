using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour, IDamageTower,IUpgradable
{
    [HideInInspector] public TowerData currentTower;
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
    public bool canPoison;
    private bool isBoosted;

    public List<UpgradeData> availableUpgradesCopy;

    [Header("Upgrade Levels")]
    [SerializeField] private bool overrideStartingDamageLevel = false;
    [SerializeField] private UpgradeLevel overrideDamageLevel;
    private UpgradeLevel currentDamageLevel;
    private UpgradeLevel currentSpeedLevel;

    private Enemy currentTarget;

    [Header("Visuals")]
    private Renderer[] renderers;
    public Color damageColor;
    private Color[] originalColors;


    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalColors = new Color[renderers.Length];
        for (int index = 0; index < renderers.Length; index++)
        {
            originalColors[index] = renderers[index].material.color;
        }

        attackDamage = currentTower.Power;
        attackRange = currentTower.Range;
        fireCooldown = currentTower.FireCooldown;

        availableUpgradesCopy = new List<UpgradeData>(currentTower.availableUpgrades);

        currentDamageLevel = currentTower.acceptingDamageLevel;
        currentSpeedLevel = currentTower.acceptingSpeedLevel;

        if (overrideStartingDamageLevel)
        {
            currentDamageLevel = overrideDamageLevel;

            foreach (var upgrade in availableUpgradesCopy)
            {
                if (upgrade.upgradeType == UpgradeType.Damage && upgrade.upgradeLevel == currentDamageLevel)
                {
                    attackDamage *= upgrade.upgradeValue;
                }
            }
        }
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

    public void Shoot(Enemy enemy)
    {
        float finalDamage = attackDamage;

        if (isBoosted)
        {
            finalDamage *= 1.4f;
        }

        enemy.TakeDamage(finalDamage);

        Debug.Log($"Shot enemy for {finalDamage} damage.");

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

    public bool IsUpgradable()
    {
        //Check if the tower can be upgraded (based on available upgrades and their levels)
        if (availableUpgradesCopy.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void UpgradeSpeed(float value, UpgradeData upgradeData)
    {
        //Apply clicked upgrade effect
        fireCooldown -= value;

        //Change tower accepting level to the next level so that next upgrade will be different
        if (upgradeData.nextUpgrade != null)
        {
            currentSpeedLevel = upgradeData.nextUpgrade.upgradeLevel;
        }

        //Remove the just applied upgrade from the available upgrades list
        availableUpgradesCopy.Remove(upgradeData);
    }

    public void UpgradeDamage(float value, UpgradeData upgradeData)
    {
        //Apply clicked upgrade effect
        attackDamage *= value;

        //Change tower accepting level to the next level so that next upgrade will be different
        if (upgradeData.nextUpgrade != null)
        {
            currentDamageLevel = upgradeData.nextUpgrade.upgradeLevel;
        }

        Debug.Log($"Damage after upgrade: {attackDamage}");

        //Remove the just applied upgrade from the available upgrades list
        availableUpgradesCopy.Remove(upgradeData);
    }

    public UpgradeLevel GetDamageLevel()
    {
        return currentDamageLevel;
    }

    public UpgradeLevel GetSpeedLevel()
    {
        return currentSpeedLevel;
    }

    public void HighlightTower()
    {
        //Change the color of all renderers to damageColor
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = damageColor;
        }
    }

    public void ResetColor()
    {
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = originalColors[index];
        }
    }
}