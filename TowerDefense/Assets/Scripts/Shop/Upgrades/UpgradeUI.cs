using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [Header("Upgrade Sections")]
    [SerializeField] private GameObject DamageBuyButton;
    [SerializeField] private GameObject SpeedBuyButton;

    [Header("Upgrade Names UI")]
    [SerializeField] private TextMeshProUGUI damageUpgradeName;
    [SerializeField] private TextMeshProUGUI speedUpgradeName;

    [Header("Upgrade Costs UI")]
    [SerializeField] private TextMeshProUGUI damageUpgradeCost;
    [SerializeField] private TextMeshProUGUI speedUpgradeCost;

    [Header("Upgrade 'Strength' Values")]
    private float AttackUpgradeValue;
    private float SpeedUpgradeValue;

    private UpgradeData currentDamageUpgrade;
    private UpgradeData currentSpeedUpgrade;

    [Header("Other Scripts")]
    [SerializeField] private Wallet walletScript;
    private TowerAttack towerAttackScript;

    public void SetupShop(TowerAttack towerAttack)
    {
        towerAttackScript = towerAttack;

        DamageBuyButton.SetActive(false);
        SpeedBuyButton.SetActive(false);

        currentDamageUpgrade = null;
        currentSpeedUpgrade = null;

        foreach (var upgrade in towerAttack.availableUpgradesCopy)
        {
            //DAMAGE
            if (upgrade.upgradeType == UpgradeType.Damage &&
                (int)upgrade.upgradeLevel == (int)towerAttack.GetAcceptingDamageLevel())
            {
                currentDamageUpgrade = upgrade;
                AttackUpgradeValue = upgrade.upgradeValue;

                DamageBuyButton.SetActive(true);
                damageUpgradeName.text = upgrade.upgradeName;
                damageUpgradeCost.text = upgrade.upgradeCost.ToString();
            }

            //SPEED
            if (upgrade.upgradeType == UpgradeType.Speed &&
                (int)upgrade.upgradeLevel == (int)towerAttack.GetAcceptingSpeedLevel())
            {
                currentSpeedUpgrade = upgrade;
                SpeedUpgradeValue = upgrade.upgradeValue;

                SpeedBuyButton.SetActive(true);
                speedUpgradeName.text = upgrade.upgradeName;
                speedUpgradeCost.text = upgrade.upgradeCost.ToString();
            }
        }

        //If nothing found = maxed
        if (currentDamageUpgrade == null)
        {
            DamageBuyButton.SetActive(false);
            damageUpgradeName.text = "Damage Maxed";
            damageUpgradeCost.text = "";
        }

        if (currentSpeedUpgrade == null)
        {
            SpeedBuyButton.SetActive(false);
            speedUpgradeName.text = "Speed Maxed";
            speedUpgradeCost.text = "";
        }
    }

    public void ApplyAttackUpgrade()
    {
        //Check if the player can afford the upgrade
        if (!walletScript.CanAfford(currentDamageUpgrade.upgradeCost))
        {
            AudioManager.Instance.sfxManager.PlaySFX(walletScript.noFundsSound);
            return;
        }

        walletScript.RemoveCurrency(currentDamageUpgrade.upgradeCost);

        towerAttackScript.UpgradeDamage(AttackUpgradeValue, currentDamageUpgrade);

        SetupShop(towerAttackScript);
    }

    public void ApplySpeedUpgrade()
    {
        //Check if the player can afford the upgrade
        if (!walletScript.CanAfford(currentSpeedUpgrade.upgradeCost))
        {
            AudioManager.Instance.sfxManager.PlaySFX(walletScript.noFundsSound);
            return;
        }

        walletScript.RemoveCurrency(currentSpeedUpgrade.upgradeCost);

        towerAttackScript.UpgradeSpeed(SpeedUpgradeValue, currentSpeedUpgrade);

        SetupShop(towerAttackScript);
    }
}