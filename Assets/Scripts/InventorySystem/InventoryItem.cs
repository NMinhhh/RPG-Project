using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private ItemData itemData;
    public bool isEqui0ped;

    public ItemData GetItemData
    {
        get { return itemData; }
    }

    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
    }
}
