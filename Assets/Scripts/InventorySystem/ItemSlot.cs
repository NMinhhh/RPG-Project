using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler
{
    [SerializeField] private bool isQuickSlot;
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
        if (Getitem)
        {
            Getitem.GetComponent<DragDrop>().SetNewPos(DragDrop.itemBeingDragged.startPos, DragDrop.itemBeingDragged.startParent);
        }
        DragDrop.itemBeingDragged.SetNewPos(transform.position, transform);
        if (isQuickSlot)
        {
            EquipSystem.Instance.EquipWeapon(DragDrop.itemBeingDragged.gameObject);
        }
    }
}
