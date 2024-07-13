using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : Singleton<EquipSystem>
{
    [Header("Weapon Slot")]
    [SerializeField] private Transform slotWeapon;
    [SerializeField] private WeaponsController controller;

    private GameObject currentItem;

    [Header("Potion Slot")]
    [SerializeField] private Transform postionSlot;
    [SerializeField] private PotionItemData potionItem;
    public int numberOfItem {  get; private set; }

    public event Action ChangeNumberOfItem;
    public event Action<float> usePotion;
    
    void Start()
    {
        numberOfItem = 2;
        ChangeNumberOfItem?.Invoke();
    }
    private void Update()
    {
        if (InputManager.instance.pressTKey && numberOfItem > 0)
        {
            UsePotion(potionItem.consumeValue);
        }
    }

    public void AddPotionItem(int number)
    {
        numberOfItem += number;
        ChangeNumberOfItem?.Invoke();
    }

    public void UsePotion(float health)
    {
        if (usePotion != null) usePotion(health);
        numberOfItem -= 1;
        ChangeNumberOfItem?.Invoke();
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

