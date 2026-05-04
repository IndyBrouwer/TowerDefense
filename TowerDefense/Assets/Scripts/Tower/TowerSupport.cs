using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSupport : MonoBehaviour
{
    [Header("Tower Stats")]
    private float supportRange;
    [SerializeField] private float slowMultiplier = 0.5f;

    [SerializeField] private TowerData thisTower;
    [SerializeField] private SupportType supportType;

    /// <summary>
    /// HashSets are kind of like a Dictionary that has only Keys and no Values.
    /// Their main use case is if you're going to need to search your collection to see whether a given item is in it or not,
    /// but don't need any additional data beyond “yes it's here” or “no it's not”.
    /// </summary>
    private HashSet<Enemy> affectedEnemies = new HashSet<Enemy>();

    private void Start()
    {
        supportRange = thisTower.Range;
    }

    private void Update()
    {
        switch (supportType)
        {
            case SupportType.Boost:
                ApplyBoost();
                break;

            case SupportType.Slow:
                ApplySlow();
                break;

            case SupportType.Poison:
                ApplyPoison();
                break;
        }
    }
    
    private List<Enemy> GetEnemiesInRange()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        List<Enemy> result = new List<Enemy>();

        float rangeSqr = supportRange * supportRange;

        foreach (Enemy enemy in enemies)
        {
            if ((transform.position - enemy.transform.position).sqrMagnitude < rangeSqr)
            {
                result.Add(enemy);
            }
        }

        return result;
    }

    private List<TowerAttack> GetTowersInRange()
    {
        TowerAttack[] towers = FindObjectsByType<TowerAttack>(FindObjectsSortMode.None);
        List<TowerAttack> result = new List<TowerAttack>();

        float rangeSqr = supportRange * supportRange;

        foreach (TowerAttack tower in towers)
        {
            if ((transform.position - tower.transform.position).sqrMagnitude < rangeSqr)
            {
                result.Add(tower);
            }
        }

        return result;
    }

    private void ApplyBoost()
    {
        foreach (TowerAttack tower in GetTowersInRange())
        {
            tower.SetDamageBoost(true);
        }
    }

    private void ApplyPoison()
    {
        foreach (TowerAttack tower in GetTowersInRange())
        {
            tower.SetPoisonEnabled(true);
        }
    }

    private void ApplySlow()
    {
        List<Enemy> current = GetEnemiesInRange();

        foreach (Enemy enemy in current)
        {
            if (!affectedEnemies.Contains(enemy))
            {
                enemy.EnterSlow(slowMultiplier);
                affectedEnemies.Add(enemy);
            }
        }

        //Enemies that are no longer in range
        List<Enemy> toRemove = new List<Enemy>();

        foreach (Enemy enemy in affectedEnemies)
        {
            if (enemy == null)
            {
                toRemove.Add(enemy);
                continue;
            }

            //Reset speed when leaving range
            if (!current.Contains(enemy))
            {
                enemy.ExitSlow();
                toRemove.Add(enemy);
            }
        }

        foreach (Enemy enemy in toRemove)
        {
            affectedEnemies.Remove(enemy);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}

public enum SupportType
{
    Boost,
    Slow,
    Poison
}