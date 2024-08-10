using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleportTaskStep : TaskStep
{
    [SerializeField] private TeleStation teleStation;

    public override void StartTask()
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
