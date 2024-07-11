using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationToMachine : MonoBehaviour
{
    [SerializeField] private EnemyWeaponController weaponController;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
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

    public void FinishAnimation() => enemy.FinishAnimation();

}
