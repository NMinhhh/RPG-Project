using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemData", menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Potion
    }

    public string itemName;
    public int number;
    public Sprite image;
    public ItemType itemType;

    [Header("Weapon Data")]
    public TypeAnimator.AttackType attackType;
    public float damage;
    [Range(0,100)]
    public float criticalRate;
    public bool isRightHand;
    public bool isLeftHand;
    public GameObject modelRightHand;
    public GameObject modelLeftHand;
    public bool isBow;

    [Header("Potion Data")]
    public float consumeValue;

    [Header("3D Viewer Modle")]
    public GameObject item3DViewerModel;
}
