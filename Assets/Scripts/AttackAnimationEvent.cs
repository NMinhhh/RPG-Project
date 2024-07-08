using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEvent : MonoBehaviour
{
    [SerializeField] private WeaponsController weaponController;

    public void StartDealDamageToTheLeft()
    {
        weaponController.StartDealDamageToTheLeft();
    }

    public void EndDealDamageToTheLeft()
    {
        weaponController.EndDealDamageToTheLeft();
    }

    public void StartDealDamageToTheRight()
    {
        weaponController.StartDealDamageToTheRight();
    }

    public void EndDealDamageToTheRight()
    {
        weaponController.EndDealDamageToTheRight();
    }

    public void NextAttack() => InputManager.Instance.UseAttackInput();
}