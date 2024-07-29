using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Button button;

    private ItemData itemData;

    public bool isEquipped;

    public event Action OnSelectedItem;

    public static InventoryItem itemSelected;

    private void OnDisable()
    {
        itemSelected = null;
    }

    public ItemData GetItemData
    {
        get { return itemData; }
    }

    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
    }

    public void SetSelectedItem(Action action)
    {
        OnSelectedItem += action;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left && itemSelected != this)
        {
            itemSelected = this;
            OnSelectedItem?.Invoke();
        }
    }
}
