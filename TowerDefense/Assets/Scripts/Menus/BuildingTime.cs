using TMPro;
using UnityEngine;

public class BuildingTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingTimeText;
    [SerializeField] private float buildingTime = 30f;
    private float buildTimeLeft;
    private bool isDecreasing = false;

    [Header("Scripts")]
    [SerializeField] private EnemySpawning enemySpawningScript;
    [SerializeField] private GameStateManager gameStateManagerScript;
    [SerializeField] private TowerPlacement towerPlacementScript;
    [SerializeField] private Wallet walletScript;

    private void Update()
    {
        if (isDecreasing)
        {
            buildingTime -= Time.deltaTime;

            //Update the building time text
            buildingTimeText.text = Mathf.CeilToInt(buildingTime).ToString();

            if (buildingTime <= 0f)
            {
                buildingTime = 0f;

                isDecreasing = false;

                //Cancel tower placement if time runs out
                if (towerPlacementScript.selectedTower != null)
                {
                    towerPlacementScript.OnPlayerRightClicked();
                }

                gameStateManagerScript.StartWavePhase();
            }
        }
    }

    public void DecreaseCount()
    {
        isDecreasing = true;

        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }
    }

    public void StopCount()
    {
        isDecreasing = false;

        buildTimeLeft = buildingTime;

        //Based on how much time is left, give currency if above 8 seconds left? If above 25 give most

        int reward;

        if (buildTimeLeft >= 25f)
        {
            //Heel vroeg gestopt grootste beloning
            reward = 10;
        }
        else if (buildTimeLeft >= 20)
        {
            reward = 8;
        }
        else if (buildTimeLeft >= 15)
        {
            reward = 5;
        }
        else if (buildTimeLeft >= 10)
        {
            reward = 2;
        }
        else
        {
            //Build phase bijna klaar, geen beloning
            return;
        }

        walletScript.AddCurrency(reward);
    }

    public void ResetBuildingTime()
    {
        buildingTime = 30f;
    }
}