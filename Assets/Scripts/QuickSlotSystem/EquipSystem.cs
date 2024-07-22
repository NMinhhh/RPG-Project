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
    
    [Header("Potion Slot")]
    [SerializeField] private Transform postionSlot;
    [SerializeField] private ItemData potionItem;
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
        NoticePickUpManager.instance.Notice(potionItem.image, "+1 "+ number + " " + potionItem.name);
        numberOfItem += number;
        ChangeNumberOfItem?.Invoke();
    }

    public void UsePotion(float health)
    {
        if (usePotion != null) usePotion(health);
        numberOfItem -= 1;
        ChangeNumberOfItem?.Invoke();
    }

    public void EquipWeapon(ItemData itemData)
    {
        if (itemData.itemType == ItemData.ItemType.Weapon)
        {
            controller.EquippedWeapons(itemData);

        }
    }

}

