using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{

    [SerializeField] private GameObject slotTemplate;
    [SerializeField] private Transform slotsContainer;
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();
    [SerializeField] private List<GameObject> slotList = new List<GameObject>();
    void Start()
    {
        GenerateListSlots();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GenerateListSlots()
    {
        for (int i = 0; i < 21; i++)
        {
            ItemSlot slot = Instantiate(slotTemplate, slotsContainer).GetComponent<ItemSlot>();
            slotList.Add(slot.gameObject);
        }

    }

    public void AddToInventory(string itemName)
    {
        Transform slotAvailable = GetAvailableSlot().transform;
        GameObject item = Instantiate(Resources.Load<GameObject>("Icon/"+itemName), slotAvailable.position, slotAvailable.rotation);
        item.transform.SetParent(slotAvailable);
        itemList.Add(item);
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
