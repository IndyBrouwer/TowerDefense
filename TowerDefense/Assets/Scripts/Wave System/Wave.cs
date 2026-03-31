using UnityEngine;

[System.Serializable]
public class Wave
{
    public EnemyData[] enemyTypes;
    public int[] enemyCounts;
    public float spawnInterval = 2f;
    public int rewardMoney;

    public TowerData[] shopCards;
}