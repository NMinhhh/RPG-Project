using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public int number;
    public Sprite image;
    public bool isStackable;
}
