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
    private float fireTimer = 0f;

    [Header("Tower Effect Bools")]
    public bool canPoison;
    private bool isBoosted;

    public List<UpgradeData> availableUpgradesCopy;

    [Header("Upgrade Levels")]
    [SerializeField] private bool overrideStartingDamageLevel = false;
    [SerializeField] private UpgradeLevel startingDamageLevel;
    private UpgradeLevel currentAcceptingDamageLevel;
    private UpgradeLevel currentAcceptingSpeedLevel;
    UpgradeData upgradeToRemove = null;

    private Enemy currentTarget;

    [Header("Visuals")]
    private Renderer[] renderers;
    public Color hoverColor;
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

        currentAcceptingDamageLevel = currentTower.acceptingDamageLevel;
        currentAcceptingSpeedLevel = currentTower.acceptingSpeedLevel;

        if (overrideStartingDamageLevel)
        {
            currentAcceptingDamageLevel = startingDamageLevel;

            foreach (var upgrade in availableUpgradesCopy)
            {
                if (upgrade.upgradeType == UpgradeType.Damage && upgrade.upgradeLevel == startingDamageLevel)
                {
                    attackDamage *= upgrade.upgradeValue;
                    upgradeToRemove = upgrade;
                    break;
                }
            }

            if (upgradeToRemove != null)
            {
                availableUpgradesCopy.Remove(upgradeToRemove);
            }

            currentAcceptingDamageLevel = FindNextDamageLevel();
        }
    }


    private void Update()
    {
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }

        //If no current target or target out of range, reset target
        if (currentTarget == null || !IsInRange(currentTarget))
        {
            //No enemy detected or out of range, find a new target
            currentTarget = null;
        }

        // Try to lock onto a new target if none is currently locked
        if (currentTarget == null)
        {
            currentTarget = FindOldestEnemy();
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

    private Enemy FindOldestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        //Use Infinity so there's always something to compare against (for deciding closest enemy)
        float oldestTime = -Mathf.Infinity;
        Enemy oldestEnemy = null;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < attackRange)
            {
                if (enemy.LifeTime > oldestTime)
                {
                    oldestTime = enemy.LifeTime;
                    oldestEnemy = enemy;
                }
            }
        }

        return oldestEnemy;
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
        if (upgradeData != null)
        {
            currentAcceptingSpeedLevel = upgradeData.upgradeLevel + 1;
        }

        //Remove the just applied upgrade from the available upgrades list
        availableUpgradesCopy.Remove(upgradeData);
    }

    public void UpgradeDamage(float value, UpgradeData upgradeData)
    {
        //Apply clicked upgrade effect
        attackDamage *= value;

        //Change tower accepting level to the next level so that next upgrade will be different
        if (upgradeData != null)
        {
            currentAcceptingDamageLevel = upgradeData.upgradeLevel + 1;
        }

        Debug.Log($"Damage after upgrade: {attackDamage}");

        //Remove the just applied upgrade from the available upgrades list
        availableUpgradesCopy.Remove(upgradeData);
    }

    private UpgradeLevel FindNextDamageLevel()
    {
        foreach (var upgrade in availableUpgradesCopy)
        {
            if (upgrade.upgradeType == UpgradeType.Damage)
            {
                return upgrade.upgradeLevel;
            }
        }

        //Fallback
        return currentAcceptingDamageLevel;
    }

    public UpgradeLevel GetAcceptingDamageLevel()
    {
        return currentAcceptingDamageLevel;
    }

    public UpgradeLevel GetAcceptingSpeedLevel()
    {
        return currentAcceptingSpeedLevel;
    }

    public void HighlightTower()
    {
        //Change the color of all renderers to damageColor
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = hoverColor;
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