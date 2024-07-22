using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    private RuntimeAnimatorController currentAnimator;

    private Animator anim;
    [SerializeField] private RuntimeAnimatorController handAnimator;
    [SerializeField] private RuntimeAnimatorController weaponInRightHandAnimator;
    [SerializeField] private RuntimeAnimatorController weaponAndShieldAnimator;
    [SerializeField] private RuntimeAnimatorController heavyWeaponAnimator;
    [SerializeField] private RuntimeAnimatorController weaponAndWeaponAnimator;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SetAnimator(TypeAnimator.AttackType.Hand);
    }

    public void SetAnimator(TypeAnimator.AttackType type)
    {
        switch (type)
        {
            case TypeAnimator.AttackType.Hand:
                anim.runtimeAnimatorController = handAnimator;
                break;
            case TypeAnimator.AttackType.WeaponsInRightHand:
                anim.runtimeAnimatorController = weaponInRightHandAnimator;
                break;
            case TypeAnimator.AttackType.WeaponsAndShield:
                anim.runtimeAnimatorController = weaponAndShieldAnimator;
                break;
            case TypeAnimator.AttackType.HeavyWeapon:
                anim.runtimeAnimatorController = heavyWeaponAnimator;
                break;
            case TypeAnimator.AttackType.WeaponAndWeapon:
                anim.runtimeAnimatorController = weaponAndWeaponAnimator;
                break;
        }
    }

}

[System.Serializable]
public class TypeAnimator
{
    public enum AttackType
    {
        Hand,
        WeaponsInRightHand,
        WeaponsAndShield,
        HeavyWeapon,
        WeaponAndWeapon,
    }

    public AttackType Type;

}
