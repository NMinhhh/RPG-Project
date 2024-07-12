using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour, IInteracable
{
    [SerializeField] private ItemData data;


    public void Interact()
    {
        InventorySystem.Instance.AddToInventory(data);
        Destroy(gameObject);
    }

}
