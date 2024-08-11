using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleportTaskStep : TaskStep, IResetable
{
    [SerializeField] private TeleStation teleStation;

    public void ResetBaseState()
    {
        
    }

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
