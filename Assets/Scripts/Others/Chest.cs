using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteracable
{
    [SerializeField] private List<ItemData> itemList;

    private Animator anim;

    private BoxCollider boxCollider;

    public bool isOpen;

    public void Interact()
    {
        boxCollider.enabled = false;
        anim.SetTrigger("Open");
    }

    void TriggerAnimation()
    {
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
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }


}
