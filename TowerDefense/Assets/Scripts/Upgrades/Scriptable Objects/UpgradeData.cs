using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;

    [Header("The 'leveled up' upgrade from this upgrade")]
    public UpgradeData nextUpgrade;

    public int cost;
}