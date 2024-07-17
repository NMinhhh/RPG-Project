using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private Transform rightWeaponsHolder;
    [SerializeField] private Transform leftWeaponsHolder;

    [SerializeField] private EnemyWeapon rightWeapon;
    [SerializeField] private EnemyWeapon leftWeapon;

    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;

    public EnemyWeapon currentRightWeapon {  get; private set; }
    public EnemyWeapon currentLeftWeapon { get; private set; }

    void Start()
    {
        if (isLeft)
            EquippedLeftWeapons(rightWeapon.name);
        if (isRight)
            EquippedRightWeapons(rightWeapon.name);
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

    public void EquippedRightWeapons(string weaponsName)
    {
        if (rightWeapon != null)
        {
            EnemyWeapon enemyWeapon = Resources.Load<EnemyWeapon>("Enemy/"+ weaponsName);
            currentRightWeapon = Instantiate(enemyWeapon, enemyWeapon.transform.localPosition, enemyWeapon.transform.rotation) as EnemyWeapon;
            currentRightWeapon.transform.SetParent(rightWeaponsHolder.transform, false);

        }

    }

    public void EquippedLeftWeapons(string weaponsName)
    {
        if (rightWeapon != null)
        {
            EnemyWeapon enemyWeapon = Resources.Load<EnemyWeapon>("Enemy/" + weaponsName);
            currentLeftWeapon = Instantiate(enemyWeapon, enemyWeapon.transform.localPosition, enemyWeapon.transform.rotation) as EnemyWeapon;
            currentLeftWeapon.transform.SetParent(leftWeaponsHolder.transform, false);

        }

    }

    public void StartDealDamageToTheLeft()
    {
        currentLeftWeapon.StartDealDamage();
    }

    public void EndDealDamageToTheLeft()
    {
        currentLeftWeapon.EndDealDamage();
    }

    public void StartDealDamageToTheRight()
    {
        currentRightWeapon.StartDealDamage();
    }

    public void EndDealDamageToTheRight()
    {
        currentRightWeapon.EndDealDamage();
    }
}
