using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTaskStep : TaskStep, IResetable
{
    [Header("Trap Demo")]
    [SerializeField] private GroundBrokenTrap groundBrokenDemon;
    [Header("Trap Obj")]
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
        groundBrokenDemon.ResetBaseState();
    }

    public override void StartTask()
    {
        groundBrokenDemon.Broken();
        FinishTaskStep();
    }
}
