using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestTaskStep : TaskStep, IResetable
{
    [SerializeField] private Chest chest;

    public void ResetBaseState()
    {
    }

    public override void StartTask()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (chest.isOpen)
        {
            FinishTaskStep();
        }
    }

}
