using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour, IInteracable
{
    public int id;
    [SerializeField] private ItemData data;

    public void ItemWorldPickedUp()
    {
        gameObject.SetActive(false);
    }

    public void Interact()
    {
        AddToInventory(data);
        gameObject.SetActive(false);
        SaveManager.Instance.SaveItemWorldId(id);
    }

    void AddToInventory(ItemData data)
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Weapon:
                InventorySystem.Instance.AddToInventory(data);
                break;
            case ItemData.ItemType.Potion:
                EquipSystem.Instance.AddPotionItem(data.number);
                break;
        }
    }
}
