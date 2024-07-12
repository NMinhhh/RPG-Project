using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    public bool isEquip;

    public WeaponData GetWeapon
    {
        get { return weaponData; }
    }
}
