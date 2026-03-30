using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    [Header("Tower Identification")]
    public string TowerName;
    public string TowerType;

    [Header("Tower Stats")]
    public float Power;
    public float Range;
    public float FireCooldown;

    public int Cost;
}
