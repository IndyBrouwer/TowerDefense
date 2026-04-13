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

        DamageBuyButton.SetActive(true);
        SpeedBuyButton.SetActive(true);

        //From towerAttack.availableUpgradesCopy, find upgrade names and cost to set them in the UI
        foreach (var upgrade in towerAttack.availableUpgradesCopy)
        {
            //Damage
            if (upgrade.upgradeType == UpgradeType.Damage)
            {
                if ((int)upgrade.upgradeLevel == (int)towerAttack.GetDamageLevel())
                {
                    currentDamageUpgrade = upgrade;

                    //Check if the upgrade is maxed
                    if (upgrade.nextUpgrade == null)
                    {
                        DamageBuyButton.SetActive(false);
                        damageUpgradeName.text = "Damage Maxed";
                        damageUpgradeCost.text = "";
                    }
                    else
                    {
                        DamageBuyButton.SetActive(true);
                        damageUpgradeName.text = upgrade.upgradeName;
                        damageUpgradeCost.text = upgrade.upgradeCost.ToString();
                    }

                    AttackUpgradeValue = upgrade.upgradeValue;
                }
            }

            //Speed
            if (upgrade.upgradeType == UpgradeType.Speed)
            {
                if ((int)upgrade.upgradeLevel == (int)towerAttack.GetSpeedLevel())
                {
                    currentSpeedUpgrade = upgrade;

                    //Check if upgrade is maxed
                    if (upgrade.nextUpgrade == null)
                    {
                        SpeedBuyButton.SetActive(false);
                        speedUpgradeName.text = "Speed Maxed";
                        speedUpgradeCost.text = "";
                    }
                    else
                    {
                        SpeedBuyButton.SetActive(true);
                        speedUpgradeName.text = upgrade.upgradeName;
                        speedUpgradeCost.text = upgrade.upgradeCost.ToString();
                    }

                    SpeedUpgradeValue = upgrade.upgradeValue;
                }
            }
        }
    }

    public void ApplyAttackUpgrade()
    {
        //Check if the player can afford the upgrade
        if (walletScript.CanAfford(currentDamageUpgrade.upgradeCost))
        {
            //Deduct currency cost from wallet
            walletScript.RemoveCurrency(currentDamageUpgrade.upgradeCost);

            towerAttackScript.UpgradeDamage(AttackUpgradeValue, currentDamageUpgrade);

            if (currentDamageUpgrade.nextUpgrade != null)
            {
                SetupShop(towerAttackScript);
            }
            else
            {
                //If there is no next upgrade, disable the upgrade section and show "Maxed" text
                DamageBuyButton.SetActive(false);
                damageUpgradeName.text = "Damage Maxed";
            }
        }
        else
        {
            AudioManager.Instance.sfxManager.PlaySFX(walletScript.noFundsSound);
        }
    }

    public void ApplySpeedUpgrade()
    {
        if (walletScript.CanAfford(currentSpeedUpgrade.upgradeCost))
        {
            //Deduct currency cost from wallet
            walletScript.RemoveCurrency(currentSpeedUpgrade.upgradeCost);

            towerAttackScript.UpgradeSpeed(SpeedUpgradeValue, currentSpeedUpgrade);

            if (currentSpeedUpgrade.nextUpgrade != null)
            {
                SetupShop(towerAttackScript);
            }
            else
            {
                //If there is no next upgrade, disable the upgrade section and show "Maxed" text
                SpeedBuyButton.SetActive(false);
                speedUpgradeName.text = "Speed Maxed";
            }
        }
        else
        {
            AudioManager.Instance.sfxManager.PlaySFX(walletScript.noFundsSound);
        }
    }
}