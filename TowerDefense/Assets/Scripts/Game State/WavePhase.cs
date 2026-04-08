using UnityEngine;

public class WavePhase : MonoBehaviour, IGameState
{
    [Header("UI")]
    [SerializeField] private GameObject speedUpButton;

    [Header("Scripts")]
    [SerializeField] private EnemySpawning enemySpawningScript;
    [SerializeField] private UpgradeShopController upgradeShopControllerScript;
    [SerializeField] private GameSpeed gameSpeedScript;

    public void Enter()
    {
        upgradeShopControllerScript.DisableShop();

        //Enable speed up button
        speedUpButton.SetActive(true);

        //Start next wave
        enemySpawningScript.StartNextWave();

        //Load in speed the player selected before
        Time.timeScale = gameSpeedScript.savedSpeedValue;
    }

    public void Exit()
    {
        //Disable speed up button
        speedUpButton.SetActive(false);
    }
}