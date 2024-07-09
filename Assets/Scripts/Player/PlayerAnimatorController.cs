using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    private RuntimeAnimatorController currentAnimator;

    private Animator anim;
    [SerializeField] private RuntimeAnimatorController handAnimator;
    [SerializeField] private RuntimeAnimatorController weaponsInRightHandAnimator;
    //[SerializeField] private RuntimeAnimatorController weaponsAndShieldAnimator;
    //[SerializeField] private RuntimeAnimatorController heavyWeaponAnimator;
    
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
                anim.runtimeAnimatorController = weaponsInRightHandAnimator;
                break;
            //case TypeAnimator.AttackType.WeaponsAndShield:
            //    anim.runtimeAnimatorController = weaponsAndShieldAnimator;
            //    break;
            //case TypeAnimator.AttackType.HeavyWeapon:
            //    anim.runtimeAnimatorController = heavyWeaponAnimator;
            //    break;
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
        HeavyWeapon
    }

    public AttackType attackType;

}
