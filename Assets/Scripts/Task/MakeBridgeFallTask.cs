using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBridgeFallTask : TaskStep,IResetable
{
    [Header("Bridge")]
    [SerializeField] private Bridge bridge;

    public void ResetBaseState()
    {
        bridge.ResetBridge();
    }

    public override void StartTask()
    {
        bridge.StartAppear();
        bridge.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (bridge.isFinishFall)
        {
            FinishTaskStep();
        }
    }

}
