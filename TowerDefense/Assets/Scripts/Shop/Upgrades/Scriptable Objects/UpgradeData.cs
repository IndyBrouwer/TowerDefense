using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType upgradeType;
    public UpgradeLevel upgradeLevel;

    [Header("The 'leveled up' upgrade from this upgrade")]
    public UpgradeData nextUpgrade;

    public float upgradeValue;
    public int upgradeCost;
}

public enum UpgradeType
{
    Damage,
    Speed
}

public enum UpgradeLevel
{
    Level1 = 1,
    Level2 = 2
}