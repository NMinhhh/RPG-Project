using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBowTask : TaskStep,IResetable
{
    private WeaponsController weaponController;

    public void ResetBaseState()
    {
        
    }

    public override void StartTask()
    {
        weaponController = GameObject.Find("Player").GetComponent<WeaponsController>();
    }

    private void Update()
    {
        if (weaponController.isEquippedBow)
        {
            FinishTaskStep();
        }
    }
}
