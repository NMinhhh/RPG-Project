using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : Singleton<EquipSystem>
{
    [Header("Weapon Slot")]
    [SerializeField] private Transform slotWeapon;
    [SerializeField] private WeaponsController controller;

    private GameObject currentItem;

    [Header("Potion Slot")]
    [SerializeField] private Transform postionSlot;
    [SerializeField] private float health = 10;
    public event Action<float> usePotion;
    void Start()
    {
        
    }

    private void Update()
    {
        if (InputManager.instance.pressTKey)
        {
            UsePotion(health);
        }
    }

    public void UsePotion(float health)
    {
        if (usePotion != null) usePotion(health);
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
