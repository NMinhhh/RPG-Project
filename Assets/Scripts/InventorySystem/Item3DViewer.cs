using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    [SerializeField] private Text nameText;
    [SerializeField] private float sensivitive;

    private float currentRotationPitch;
    private float currentRotationYaw;

    private GameObject itemPref;
    private void Start()
    {
        InventorySystem.OnItemSelected += OnItemSelected;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (itemPref != null)
            Destroy(itemPref);
    }

    private void OnItemSelected()
    {
        if (itemPref != null)
        {
            Destroy(itemPref);
        }
        nameText.text = InventoryItem.itemSelected.GetItemData.itemName;
        GameObject viewerModel = InventoryItem.itemSelected.GetItemData.item3DViewerModel;
        itemPref = Instantiate(viewerModel, viewerModel.transform.position, Quaternion.identity);
        currentRotationPitch = 0;
        currentRotationYaw = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemPref == null) return;
        currentRotationPitch = GetRotaion(currentRotationPitch, -eventData.delta.y * sensivitive * Time.deltaTime);
        currentRotationYaw = GetRotaion(currentRotationYaw, eventData.delta.x * sensivitive * Time.deltaTime);
        itemPref.transform.rotation = Quaternion.Euler(currentRotationPitch, currentRotationYaw, itemPref.transform.rotation.z);
    }

    public float GetRotaion(float currentRoation, float value)
    {
        currentRoation -= value;
        return currentRoation;
    }

}
