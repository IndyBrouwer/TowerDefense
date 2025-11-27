using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Identification")]
    public string enemyName;
    public GameObject enemyPrefab;
    public EnemyData enemyData;

    [Header("Enemy Stats")]
    public float maxHealth;
    public float speed;
    public float damage;
}