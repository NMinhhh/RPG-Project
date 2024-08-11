using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTaskStep : TaskStep, IResetable
{
    [SerializeField] private GameObject[] trapObject;
    public void ResetBaseState()
    {
        foreach (var trap in trapObject)
        {
            IResetable resetable = trap.GetComponent<IResetable>();
            if (resetable != null)
            {
                resetable.ResetBaseState();
            }
        }
    }

    public override void StartTask()
    {
        FinishTaskStep();
    }
}
