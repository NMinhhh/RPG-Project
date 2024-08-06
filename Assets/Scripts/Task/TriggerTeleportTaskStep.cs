using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleportTaskStep : TaskStep
{
    [SerializeField] private TeleStation teleStation;

    private void Start()
    {
        teleStation.TriggerParticleStation();
    }

    private void Update()
    {
        if (teleStation.isTriggerTele) 
        {
            FinishTaskStep();
        }
    }
}
