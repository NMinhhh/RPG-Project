using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{

    [SerializeField] private Transform rightWeaponsHolder;
    [SerializeField] private Transform leftWeaponsHolder;
    private EquippedWeapons currentLeftWeapons;
    private EquippedWeapons currentRightWeapons;
    PlayerAnimatorController playerAnimatorController;
    [SerializeField] private WeaponData weaponData;

    public WeaponData currentWeaponData { get; private set; }

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        EquippedWeapons(weaponData);
    }

    public void EquippedWeapons(WeaponData weaponData)
    {
        RemoveWeapons();
        currentWeaponData = weaponData;
        if (currentWeaponData.isRightHand)
        {
            EquippedRightWeapons(currentWeaponData);
        }
        if (currentWeaponData.isLeftHand)
        {
            EquippedLeftWeapons(currentWeaponData);    
        }
        player.ResetComboAttack();
        playerAnimatorController.SetAnimator(currentWeaponData.attackType);
    }

    public void EquippedLeftWeapons(WeaponData weaponData)
    {
        EquippedWeapons weapons = weaponData.modelRightHand.GetComponent<EquippedWeapons>();
        weapons.SetDamage(weaponData.damage);
        currentRightWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentRightWeapons.transform.SetParent(leftWeaponsHolder.transform, false);

    }


    public void EquippedRightWeapons(WeaponData weaponData)
    {
        EquippedWeapons weapons = weaponData.modelRightHand.GetComponent<EquippedWeapons>();
        currentRightWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentRightWeapons.transform.SetParent(rightWeaponsHolder.transform, false);

    }

    void RemoveWeapons()
    {
        if (currentLeftWeapons != null)
        {
            DestroyImmediate(currentLeftWeapons.gameObject);
            currentLeftWeapons = null;
        }
        if (currentRightWeapons != null)
        {
            DestroyImmediate(currentRightWeapons.gameObject);
            currentRightWeapons = null;
        }
    }

    public void StartDealDamageToTheLeft()
    {
        currentLeftWeapons.StartDealDamage();
    }

    public void EndDealDamageToTheLeft()
    {
        currentLeftWeapons.EndDealDamage();
    }

    public void StartDealDamageToTheRight()
    {
        currentRightWeapons.StartDealDamage();
    }

    public void EndDealDamageToTheRight()
    {
        currentRightWeapons.EndDealDamage();
    }

}
