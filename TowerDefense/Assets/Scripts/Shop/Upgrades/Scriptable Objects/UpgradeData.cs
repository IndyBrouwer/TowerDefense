using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType upgradeType;
    public UpgradeLevel upgradeLevel;

    public bool isHighestLevel;

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