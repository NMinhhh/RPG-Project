using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteracable
{
    [Header("ID")]
    [SerializeField] public int id;
    [Header("list Item in chest")]
    [SerializeField] private List<ItemData> itemList;
    [Header("Chest Top")]
    [SerializeField] private GameObject chestTopObj;
    [SerializeField] private float openTime;
    [SerializeField] private float lootItemDelayTime;

    private BoxCollider boxCollider;

    public bool isOpen { get; private set; }

    public void Interact()
    {
        boxCollider.enabled = false;
        LeanTween.rotateX(chestTopObj, -90, openTime);
        LeanTween.delayedCall(lootItemDelayTime, LootItem);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Open Chest"), transform.position);
    }

    public void ChestOpened()
    {
        if(boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();
        isOpen = true;
        boxCollider.enabled = false;
        chestTopObj.transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    void LootItem()
    {
        SaveManager.Instance.SaveChestOpenedId(id);
        isOpen = true;
        foreach (ItemData item in itemList)
        {
            AddToInventory(item);
        }
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

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }


}
