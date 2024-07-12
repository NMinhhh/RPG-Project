using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemData : ScriptableObject
{
    public enum ItemType
    {
        weapon,
    }

    public string itemName;
    public int number;
    public Sprite image;
    public ItemType itemType;
    public bool isStackable;

}
