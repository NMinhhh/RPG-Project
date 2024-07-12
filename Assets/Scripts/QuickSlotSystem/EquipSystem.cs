using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : Singleton<EquipSystem>
{
    [SerializeField] private Transform slotWeapon;
    [SerializeField] private WeaponsController controller;

    private GameObject currentItem;

    void Start()
    {
        
    }

    public void EquipWeapon(GameObject item)
    {
        if (currentItem != null)
        {
            currentItem = null;
        }
        currentItem = item;
        WeaponData weaponData = currentItem.GetComponent<InventoryItem>().GetWeapon;
        if (weaponData.isRightHand)
        {
            controller.EquippedWeapons(currentItem.GetComponent<InventoryItem>().GetWeapon);

        }
    }

}
