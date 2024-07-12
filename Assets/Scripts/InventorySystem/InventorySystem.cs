using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    [Header("UI")]
    [SerializeField] private GameObject inventorySystemUI;
    [Header("Slot template")]
    [SerializeField] private GameObject slotTemplate;
    [SerializeField] private Transform slotsContainer;
    [SerializeField] private List<GameObject> slotList = new List<GameObject>();
    [SerializeField] private List<ItemData> itemList = new List<ItemData>();

    public bool isOpen { get; private set; }
    void Start()
    {
        GenerateListSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.pressEKey)
        {
            if (!isOpen)
            {
                isOpen = true;
                inventorySystemUI.SetActive(true);
            }
            else
            {
                isOpen = false;
                inventorySystemUI.SetActive(false);
            }
        }
    }

    private void GenerateListSlots()
    {
        for (int i = 0; i < 15; i++)
        {
            ItemSlot slot = Instantiate(slotTemplate, slotsContainer).GetComponent<ItemSlot>();
            slotList.Add(slot.gameObject);
        }

    }

    public void AddToInventory(ItemData itemData)
    {
        Transform slotAvailable = GetAvailableSlot().transform;
        GameObject item = Instantiate(Resources.Load<GameObject>("Icon/"+itemData.itemName), slotAvailable.position, slotAvailable.rotation);
        item.transform.SetParent(slotAvailable);
        itemList.Add(itemData);
    }

    public void RefreshInventoryItem()
    {
        
    }


    GameObject GetAvailableSlot()
    {

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot.gameObject;
            }
        }
        return new GameObject();
    }

    public bool CheckForAvailableSlot()
    {
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return true;
            }
        }
        return false;
    }
}
