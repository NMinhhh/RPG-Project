using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler
{

    public GameObject Getitem
    {
        get
        {
            if (DragDrop.itemBeingDragged != null)
            {
                return DragDrop.itemBeingDragged.gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Getitem)
        {
            DragDrop.itemBeingDragged.SetNewPos(transform.position, transform);
        }
    }

}
