using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestTaskStep : TaskStep
{
    [SerializeField] private Chest chest;

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
