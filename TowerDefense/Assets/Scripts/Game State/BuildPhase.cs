using UnityEngine;

public class BuildPhase : MonoBehaviour, IGameState
{
    [Header("UI")]
    [SerializeField] private GameObject buildTimer;
    [SerializeField] private GameObject towerShopButton;
    [SerializeField] private GameObject endBuildPhaseButton;
    [SerializeField] private GameObject tipBox;

    [Header("Scripts")]
    [SerializeField] private BuildingTime buildingTimeScript;
    [SerializeField] private TowerShopController towerShopControllerScript;
    [SerializeField] private EnemySpawning enemySpawningScript;
    [SerializeField] private UpgradeShopController upgradeShopControllerScript;


    public void Enter()
    {
        //Last wave check
        if (enemySpawningScript.AreAllWavesCompleted() == true)
        {
            return;
        }

        buildingTimeScript.ResetBuildingTime();

        //Enable building timer
        buildTimer.SetActive(true);

        //Enable Tower Shop Button
        towerShopButton.SetActive(true);

        endBuildPhaseButton.SetActive(true);

        //Start decreasing building time
        buildingTimeScript.DecreaseCount();

        //Show tower shop menu
        towerShopControllerScript.EnableShop();
    }

    public void Exit()
    {
        //Stop decreasing building time
        buildingTimeScript.StopCount();

        //Disable building timer
        buildTimer.SetActive(false);

        endBuildPhaseButton.SetActive(false);

        //Hide tower shop menu
        towerShopControllerScript.DisableShop();

        upgradeShopControllerScript.DisableShop();

        tipBox.SetActive(false);
    }
}