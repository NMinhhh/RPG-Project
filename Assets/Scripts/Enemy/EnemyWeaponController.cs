using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{

    [SerializeField] private EnemyWeapon rightWeapon;
    [SerializeField] private EnemyWeapon leftWeapon;

    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;

    public EnemyWeapon currentRightWeapon {  get; private set; }
    public EnemyWeapon currentLeftWeapon { get; private set; }

    void Start()
    {
        if (isLeft)
            currentLeftWeapon = leftWeapon;
        if (isRight)
            currentRightWeapon = rightWeapon;
    }

    public void SetDamageRightWeapon(float damage)
    {
        if (currentRightWeapon != null)
        {
            currentRightWeapon.SetDamage(damage);
        }
    }

    public void SetDamageLeftWeapon(float damage)
    {
        if (currentLeftWeapon != null)
        {
            currentLeftWeapon.SetDamage(damage);
        }
    }

    public void StartDealDamageToTheLeft() => currentLeftWeapon.StartDealDamage();

    public void EndDealDamageToTheLeft() => currentLeftWeapon.EndDealDamage();

    public void StartDealDamageToTheRight() => currentRightWeapon.StartDealDamage();

    public void EndDealDamageToTheRight() => currentRightWeapon.EndDealDamage();

    public void EndDealDamageAll()
    {
        if(currentLeftWeapon!= null) currentLeftWeapon.EndDealDamage();

        if(currentRightWeapon!= null) currentRightWeapon.EndDealDamage();
    }
}
