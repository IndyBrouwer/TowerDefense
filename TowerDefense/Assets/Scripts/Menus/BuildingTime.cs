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
    }

    public void ResetBuildingTime()
    {
        buildingTime = 30f;
    }
}