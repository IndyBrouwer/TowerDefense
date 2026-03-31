using UnityEngine;

public class BuildPhase : MonoBehaviour, IGameState
{
    [Header("UI")]
    [SerializeField] private GameObject buildTimer;
    [SerializeField] private GameObject towerShopButton;

    [Header("Scripts")]
    [SerializeField] private BuildingTime buildingTimeScript;
    [SerializeField] private TowerShopController towerShopControllerScript;


    public void Enter()
    {
        //Enable building timer
        buildTimer.SetActive(true);

        //Enable Tower Shop Button
        towerShopButton.SetActive(true);

        //Start decreasing building time
        buildingTimeScript.DecreaseCount();

        //Show tower shop menu
        towerShopControllerScript.ToggleTowerShopMenu();
    }

    public void Exit()
    {
        //Stop decreasing building time
        buildingTimeScript.StopCount();

        //Disable building timer
        buildTimer.SetActive(false);

        //Hide tower shop menu
        towerShopControllerScript.DisableShop();
    }
}