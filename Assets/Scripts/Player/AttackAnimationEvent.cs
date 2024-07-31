using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEvent : MonoBehaviour
{
    [SerializeField] private WeaponsController weaponController;

    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

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

    public void SlashEffectRight(int order)
    {
        weaponController.SlashEffectRight(order);
    } 
    
    public void SlashEffectLeft(int order)
    {
        weaponController.SlashEffectLeft(order);
    }

    public void BowStringPull()
    {
        weaponController.BowStringPull();
        weaponController.StartArrowShoot();
    }

    public void BowStringNotPull()
    {
        weaponController.BowStringNotPull();
        weaponController.EndArrowShoot();
    }

    public void NextAttack() => InputManager.Instance.UseAttackInput();

    public void FinishAnimation()
    {
        player.FinishAnimation();
    }

    public void TriggerAnimation() => player.TrtiggerAnimation();
}
