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
        //numberOfItem = 2;
        ChangeNumberOfItem?.Invoke();
    }
    private void Update()
    {
        if (InputManager.Instance.pressTKey && numberOfItem > 0 && !GameManager.Instance.isLoss)
        {
            UsePotion(potionItem.consumeValue);
        }
        
    }

    public void AddPotionItem(int number)
    {
        if(!SaveManager.Instance.isLoading)
            NoticePickUpManager.Instance.Notice(potionItem.image,  "+ " + number + " " + potionItem.itemName);
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
        if (!SaveManager.Instance.isLoading)
            SaveManager.Instance.SaveItemEquipped(itemData.name);
        else
        {
            GameObject item = ObjectPool.Instance.SpawnFromPool("Item UI", slotWeapon.position, slotWeapon.rotation);
            InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
            inventoryItem.SetItemData(itemData);
            item.GetComponent<Image>().sprite = itemData.image;
            item.transform.SetParent(slotWeapon);
        }

    }

    public void ResetEquip()
    {
        numberOfItem = 0;
    }

}

