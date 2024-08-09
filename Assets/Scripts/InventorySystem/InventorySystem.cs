using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : Singleton<InventorySystem>
{
    [Header("Slot template")]
    [SerializeField] private GameObject slotTemplate;
    [SerializeField] private Transform slotsContainer;

    [Header("Slot List (Read only)")]
    [SerializeField] private List<GameObject> slotList = new List<GameObject>();

    [Header("Weapon Item List (Read only)")]
    [SerializeField] private List<ItemData> weaponItemList = new List<ItemData>();

    public static event Action OnItemSelected;


    public event Action equippedBow;

    public bool isOpen { get; private set; }

    void Start()
    {
        GenerateListSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.pressEKey && !IntroManager.Instance.isPlayIntro && !MenuGameManager.Instance.isOpen)
        {
            if (!isOpen)
            {
                isOpen = true;
                CanvasManager.Instance.OpenUI(UIObject.UIName.Inventory);
                CanvasManager.Instance.CursorUnLock();
                InputManager.Instance.CanNotGetInput();
            }
            else
            {
                isOpen = false;
                CanvasManager.Instance.CloseUI(UIObject.UIName.Inventory);
                CanvasManager.Instance.CloseUI(UIObject.UIName.Item3DViewer);
                CanvasManager.Instance.CursorLock();
                InputManager.Instance.CanGetInput();
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
        if (!SaveManager.Instance.isLoading)
        {
            SaveManager.Instance.SaveItem(itemData.name);
            NoticePickUpManager.Instance.Notice(itemData.image, "+1 " + itemData.itemName);
        }
        if (itemData.isBow)
        {
            equippedBow?.Invoke();
            return;
        }
        Transform slotAvailable = GetAvailableSlot().transform;
        GameObject item = ObjectPool.Instance.SpawnFromPool("Item UI", slotAvailable.position, slotAvailable.rotation);
        InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
        inventoryItem.SetItemData(itemData);
        inventoryItem.SetSelectedItem(OnItemSelected);
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
