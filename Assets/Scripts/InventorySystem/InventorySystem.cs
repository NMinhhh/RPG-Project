using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : Singleton<InventorySystem>
{
    [Header("UI")]
    [SerializeField] private GameObject inventorySystemUI;
    [Header("Slot template")]
    [SerializeField] private GameObject slotTemplate;
    [SerializeField] private Transform slotsContainer;

    [Header("Slot List (Read only)")]
    [SerializeField] private List<GameObject> slotList = new List<GameObject>();

    [Header("Weapon Item List (Read only)")]
    [SerializeField] private List<ItemData> weaponItemList = new List<ItemData>();

    public static event Action cameraLock;
    public static event Action cameraUnlock;

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
                cameraLock?.Invoke();
            }
            else
            {
                isOpen = false;
                inventorySystemUI.SetActive(false);
                cameraUnlock?.Invoke();
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
        GameObject item = Instantiate(Resources.Load<GameObject>("Item/item"), slotAvailable.position, slotAvailable.rotation);
        item.GetComponent<Image>().sprite = itemData.image;
        item.transform.SetParent(slotAvailable);
        weaponItemList.Add(itemData);
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
