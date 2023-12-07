using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ship SFX")]
    [field: SerializeField] public EventReference Booster { get; private set; }
    [field: SerializeField] public EventReference Thruster { get; private set; }
    [field: SerializeField] public EventReference ChargeUp { get; private set; }
    [field: SerializeField] public EventReference ChargeDown { get; private set; }
    [field: SerializeField] public EventReference HyperSpeed { get; private set; }
    [field: SerializeField] public EventReference HyperSpeedPreparing { get; private set; }
    [field: SerializeField] public EventReference BurstThruster { get; private set; }

    [field: Header("Ball SFX")]
    [field: SerializeField] public EventReference BallHit { get; private set; }

    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference CameraModeToggle { get; private set; }
    [field: SerializeField] public EventReference CheckpointComplete { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference SpaceAmbience { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one FMODEvents in the scene.");
        }
        instance = this;
    }
}
