using UnityEngine;

public class WavePhase : MonoBehaviour, IGameState
{
    [Header("UI")]
    [SerializeField] private GameObject speedUpButton;

    [Header("Scripts")]
    [SerializeField] private EnemySpawning enemySpawningScript;
    [SerializeField] private UpgradeShopController upgradeShopControllerScript;

    public void Enter()
    {
        upgradeShopControllerScript.DisableShop();

        //Enable speed up button
        speedUpButton.SetActive(true);

        //Start next wave
        enemySpawningScript.StartNextWave();
    }

    public void Exit()
    {
        //Disable speed up button
        speedUpButton.SetActive(false);
    }
}