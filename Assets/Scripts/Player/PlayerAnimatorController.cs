using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    private RuntimeAnimatorController currentAnimator;

    private Animator _anim;
    [SerializeField] private RuntimeAnimatorController _handAnimator;
    [SerializeField] private RuntimeAnimatorController _weaponsInRightHandAnimator;
    //[SerializeField] private RuntimeAnimatorController weaponsAndShieldAnimator;
    //[SerializeField] private RuntimeAnimatorController heavyWeaponAnimator;
    
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        SetAnimator(TypeAnimator.AttackType.Hand);
    }

    public void SetAnimator(TypeAnimator.AttackType type)
    {
        switch (type)
        {
            case TypeAnimator.AttackType.Hand:
                _anim.runtimeAnimatorController = _handAnimator;
                break;
            case TypeAnimator.AttackType.WeaponsInRightHand:
                _anim.runtimeAnimatorController = _weaponsInRightHandAnimator;
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

    public AttackType Type;

}
