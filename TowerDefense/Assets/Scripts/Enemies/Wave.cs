using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;
    public int enemyCount;
    public float spawnInterval = 2f;
    public int rewardMoney;
}