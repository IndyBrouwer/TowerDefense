using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private IGameState currentState;

    [Header("States")]
    [SerializeField] private BuildPhase buildPhase;
    [SerializeField] private WavePhase wavePhase;

    [SerializeField] private PlayerController playerControllerScript;

    private void Start()
    {
        StartWavePhase();
    }

    public void SetState(IGameState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();

        playerControllerScript.OnGameStateChanged();
    }

    public IGameState GetGameState()
    {
        return currentState;
    }

    public void StartBuildPhase()
    {
        SetState(buildPhase);
    }

    public void StartWavePhase()
    {
        SetState(wavePhase);
    }
}