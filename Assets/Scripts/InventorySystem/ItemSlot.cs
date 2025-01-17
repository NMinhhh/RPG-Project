using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler
{
    [SerializeField] private bool isQuickSlot;
    [SerializeField] private bool isLock;
    public GameObject Getitem
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }



    public void OnDrop(PointerEventData eventData)
    {
        if (isLock || !DragDrop.itemBeingDragged) return;
        if (Getitem)
        {
            if (isQuickSlot)
                SaveManager.Instance.SaveItem(Getitem.GetComponent<InventoryItem>().GetItemData.name);
            Getitem.GetComponent<DragDrop>().SetCanDrag(true);
            Getitem.GetComponent<DragDrop>().SetNewPos(DragDrop.itemBeingDragged.startPos, DragDrop.itemBeingDragged.startParent);
        }
        DragDrop.itemBeingDragged.SetNewPos(transform.position, transform);
        DragDrop.itemBeingDragged.SetCanDrag(true);
        if (isQuickSlot)
        {
            DragDrop.itemBeingDragged.SetCanDrag(false);
            EquipSystem.Instance.EquipWeapon(DragDrop.itemBeingDragged.GetComponent<InventoryItem>().GetItemData);
        }
    }
}
