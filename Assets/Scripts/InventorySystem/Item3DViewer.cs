using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    private GameObject itemPref;

    private void Start()
    {
        InventorySystem.OnItemSelected += OnItemSelected;
        gameObject.SetActive(false);
    }

    private void OnItemSelected()
    {
        if (itemPref != null)
        {
            Destroy(itemPref);
        }
        GameObject viewerModel = InventoryItem.itemSelected.GetItemData.item3DViewerModel;
        itemPref = Instantiate(viewerModel, viewerModel.transform.position, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemPref.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }
}
