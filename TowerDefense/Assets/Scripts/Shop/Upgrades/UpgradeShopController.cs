using UnityEngine;

public class UpgradeShopController : MonoBehaviour
{
    public GameObject upgradeShopMenu;
    private TowerAttack towerAttackScript;

    [SerializeField] private UpgradeUI upgradeUIScript;
    [SerializeField] private GameStateManager gameStateManagerScript;

    //If in placing phase don't do anything
    public void OnPlayerLeftClicked(TowerAttack AttackTowerScript, TowerPlacement towerPlacementScript)
    {
        //Check if in build mode, if not return
        if (gameStateManagerScript.GetGameState() is WavePhase || towerPlacementScript.placingTower)
        {
            return;
        }

        towerAttackScript = AttackTowerScript;

        if (towerAttackScript.IsUpgradable())
        {
            //Update shop menu with the tower's upgrade options
            upgradeUIScript.SetupShop(towerAttackScript);

            //Enable the shop menu at mouse position, if on top half of the screen place it below the mouse, if on bottom half place it above the mouse
            EnableShop();
        }
    }

    public void OnPlayerRightClicked()
    {
        if (upgradeShopMenu.activeSelf == true)
        {
            DisableShop();
        }
    }

    public void ToggleTowerShopMenu()
    {
        upgradeShopMenu.SetActive(!upgradeShopMenu.activeSelf);
    }

    public void EnableShop()
    {
        upgradeShopMenu.SetActive(true);
    }

    public void DisableShop()
    {
        upgradeShopMenu.SetActive(false);
    }
}