using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Item/Weapon Item Data")]
public class WeaponData : ItemData
{
    public TypeAnimator.AttackType attackType;
    public int combo;
    public bool isRightHand;
    public bool isLeftHand;
    public GameObject modelRightHand;
    public GameObject modelLeftHand;

}
