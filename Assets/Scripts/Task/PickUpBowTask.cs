using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBowTask : TaskStep
{
    private WeaponsController weaponController;

   

    private void Start()
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
