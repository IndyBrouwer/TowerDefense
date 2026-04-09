using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    [Header("Tower Identification")]
    public string TowerName;
    public string TowerType;
    public Sprite TowerSprite;
    public string TowerDescription;
    public GameObject TowerPrefab;

    [Header("Tower Stats")]
    public float Power;
    public float Range;
    public float FireCooldown;

    [Header("Upgrade Levels")]
    public UpgradeLevel acceptingDamageLevel;
    public UpgradeLevel acceptingSpeedLevel;

    public List<UpgradeData> availableUpgrades;

    public int Cost;
    public Color TowerColor;
}