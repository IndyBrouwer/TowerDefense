using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public CinemachineImpulseSource impulse;

    public void Shake()
    {
        impulse.GenerateImpulse();
    }
}