using TMPro;
using UnityEngine;

public class BuildingTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingTimeText;
    [SerializeField] private float buildingTime = 30f;
    private bool isDecreasing = false;

    [Header("Scripts")]
    [SerializeField] private EnemySpawning enemySpawningScript;
    [SerializeField] private GameStateManager gameStateManagerScript;

    private void Start()
    {
        buildingTime = 30f;
    }

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
}