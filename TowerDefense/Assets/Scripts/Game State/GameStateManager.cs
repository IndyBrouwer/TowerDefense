using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private IGameState currentState;

    [Header("States")]
    [SerializeField] private BuildPhase buildPhase;
    [SerializeField] private WavePhase wavePhase;

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