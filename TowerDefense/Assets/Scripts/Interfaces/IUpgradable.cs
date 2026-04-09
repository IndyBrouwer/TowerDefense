using UnityEngine;

public interface IUpgradable
{
    bool IsUpgradable();

    void UpgradeSpeed(float value, UpgradeData upgradeData);

    void UpgradeDamage(float value, UpgradeData upgradeData);
}
